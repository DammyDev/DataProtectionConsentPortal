using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsentPortal.Data
{
    public class ConsentService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public ConsentService(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _environment = env;
        }

        public async Task<Consent> GetCif(string cif)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            string sQuery = $"SELECT Id, CustomerId, IsNDPRAccepted, DateCreated, CIF, CustomerName, FilePath FROM dbConsent.dbo.NDPR WHERE CIF = '{cif}'";
            var result = await connection.QueryAsync<Consent>(sQuery);
            return result.FirstOrDefault();
        }

        public async Task<int> UploadAsync(MemoryStream ms, FinacleResponse response)
        {
            var path = Path.Combine(_environment.WebRootPath, "Documents", $"{response.cif_id}.pdf");
            using FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();  
            string sQuery = $"INSERT INTO dbConsent.dbo.NDPR (CustomerId, IsNDPRAccepted, DateCreated,CIF,CustomerName, FilePath) VALUES (null, 1, GETDATE(), '{response.cif_id}','{response.acctName}','{path}')";
            var result = await connection.ExecuteAsync(sQuery);
            return result;
        }
    }
}
