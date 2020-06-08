using Dapper;
using Microsoft.Extensions.Configuration;
using POSPortal.Data;
using POSPortal.Extensions;
using POSPortal.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace POSPortal.Services
{
    public class POSService
    {
        private readonly IConfiguration _configuration;

        public POSService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<POSRequest> GetById(string id)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            string sQuery = $"SELECT * FROM dbPOSPortal.dbo.Requests WHERE RequestId = '{id}'";
            var result = await connection.QueryAsync<POSRequest>(sQuery);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<POSRequest>> GetBySolId(string solId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            string sQuery = $"SELECT * FROM dbPOSPortal.dbo.Requests WHERE SolId = '{solId}'";
            var result = await connection.QueryAsync<POSRequest>(sQuery);
            return result;
        }

        public async Task<IEnumerable<POSRequestResource>> GetByAccount(string acc)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            string sQuery = $"SELECT * FROM dbPOSPortal.dbo.Requests WHERE [AccountNumber] = '{acc}'";
            var result = await connection.QueryAsync<POSRequestResource>(sQuery);
            return result;
        }

        public async Task<int> SubmitAsync(SavePOSRequestResource request)
        {
            int Id = GetNextId();
            var RequestId = string.IsNullOrEmpty(request.MerchantName) ? null : $"R{Id.ToString().PadLeft(5, '0')}{request.MerchantName.Replace(" ", string.Empty).Substring(0, request.MerchantName.Length < 6 ? request.MerchantName.Length : 6).PadRight(6, 'X').ToUpper()}";

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string sQuery = $"INSERT INTO dbPOSPortal.dbo.Requests (RequestId,MerchantName,RCNumber,TerminalLocation,NumberOfTerminals,AccountNumber,City,LGA,State,NearestBusStop,Landmark,BusinessStartDate,TradeName,CorporateAddress,ReceiptHeader,ReceiptFooter,PhoneNo,Email,BusinessSegment,PrincipalContactName,PrincipalContactAddress,PrincipalContactEmail,PrincipalContactStateOfOrigin,PrincipalContactPosition,PrincipalContactPhoneNo,ContactNameAtTerminalLocation,ContactAddressAtTerminalLocation,ContactPositionAtTerminalLocation,ContactPhoneNoAtTerminalLocation,Declaration,Agreement,Status,Comments, DateSubmitted, DateUpdated, SolId) " +
                 $"VALUES ('{RequestId}','{request.MerchantName}','{request.RCNumber}','{request.TerminalLocation}','{request.NumberOfTerminals}','{request.AccountNumber}','{request.City}','{request.LGA}','{request.State}','{request.NearestBusStop}','{request.Landmark}','{request.BusinessStartDate}','{request.TradeName}','{request.CorporateAddress}','{request.ReceiptHeader}','{request.ReceiptFooter}','{request.PhoneNo}','{request.Email}','{request.BusinessSegment}','{request.PrincipalContactName}','{request.PrincipalContactAddress}','{request.PrincipalContactEmail}','{request.PrincipalContactStateOfOrigin}','{request.PrincipalContactPosition}'," +
                 $"'{request.PrincipalContactPhoneNo}','{request.ContactNameAtTerminalLocation}','{request.ContactAddressAtTerminalLocation}','{request.ContactPositionAtTerminalLocation}','{request.ContactPhoneNoAtTerminalLocation}','{request.Declaration}','{request.Agreement}','{request.Status}','{request.Status.ToDescriptionString()}','{request.DateSubmitted.ToString()}', '{request.DateUpdated.ToString()}','{request.BranchCode}')";
            
            var result = await connection.ExecuteAsync(sQuery);
            return result;
        }

        public async Task<int> SubmitAsync(string requestId, EStatus status, string comments)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            string sQuery = $"UPDATE dbPOSPortal.dbo.Requests SET Status = '{status}', Comments = '{comments}', DateUpdated = '{DateTime.Now}' WHERE RequestId = '{requestId}'";
            var result = await connection.ExecuteAsync(sQuery);
            return result;
        }

        public async Task<int> UpdateAsync(POSRequest request)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string sQuery = $"UPDATE dbPOSPortal.dbo.Requests SET MerchantName = '{request.MerchantName}',RCNumber='{request.RCNumber}',TerminalLocation='{request.TerminalLocation}',NumberOfTerminals='{request.NumberOfTerminals}',AccountNumber='{request.AccountNumber}',City='{request.City}',LGA='{request.LGA}',State='{request.State}',NearestBusStop='{request.NearestBusStop}',Landmark='{request.Landmark}',BusinessStartDate='{request.BusinessStartDate}',TradeName='{request.TradeName}',CorporateAddress='{request.CorporateAddress}',ReceiptHeader='{request.ReceiptHeader}',ReceiptFooter='{request.ReceiptFooter}',BusinessSegment='{request.BusinessSegment}',PrincipalContactName='{request.PrincipalContactName}',PrincipalContactAddress='{request.PrincipalContactAddress}',PrincipalContactEmail='{request.PrincipalContactEmail}',PrincipalContactStateOfOrigin='{request.PrincipalContactStateOfOrigin}',PrincipalContactPosition='{request.PrincipalContactPosition}'," +
                $"PrincipalContactPhoneNo='{request.PrincipalContactPhoneNo}',ContactNameAtTerminalLocation='{request.ContactNameAtTerminalLocation}',ContactAddressAtTerminalLocation='{request.ContactAddressAtTerminalLocation}',ContactPositionAtTerminalLocation='{request.ContactPositionAtTerminalLocation}',ContactPhoneNoAtTerminalLocation='{request.ContactPhoneNoAtTerminalLocation}',Declaration='{request.Declaration}',Agreement='{request.Agreement}',Status='{request.Status}',Comments='{request.Comments}', DateUpdated='{DateTime.Now}' WHERE RequestId = '{request.RequestId}'";

            var result = await connection.ExecuteAsync(sQuery);
            return result;
        }

        private int GetNextId()
        {
            int nextId = 0;
            var query = "SELECT MAX(Id) FROM [dbPOSPortal].[dbo].[Requests]";
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    nextId = (int)reader[0];
                    Console.WriteLine(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                //Log.Error($"Unable to retrieve next ID from Request Table. See error below: \n {ex.Message}");
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }

            nextId += 1;
            return nextId;
        }

    }
}
