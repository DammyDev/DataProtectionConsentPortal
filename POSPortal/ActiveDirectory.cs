using System;
using Newtonsoft.Json;
using System.Net.Http;
using POSPortal.Resources;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace POSPortal.Pages
{
    public class ActiveDirectory
    {
        readonly HttpClient Http = new HttpClient();
        HttpResponseMessage responseMsg = new HttpResponseMessage();
        LoginResponseResource response = new LoginResponseResource();
        //private readonly IConfiguration _config;

        //public ActiveDirectory(IConfiguration config)
        //{
        //    _config = config;
        //}

        public async Task<LoginResponseResource> Authenticate(string _userName, string _password)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(new {username = _userName, password= _password }));

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            
            try
            {
                responseMsg = await Http.PostAsync("http://iwema/api/v1/Users/Login", httpContent);

                if (responseMsg.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (responseMsg.Content != null)
                    {
                        var responseContent = await responseMsg.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<LoginResponseResource>(responseContent);
                        response.Success = true;
                        return response;
                    }
                }

                return new LoginResponseResource { Success = false };
            }
            catch (Exception e)
            {
                return new LoginResponseResource { Success = false };
                //return new LoginResponseResource
                //{
                //    Success = true,
                //    scopeLevel = new ScopeLevel { branchcode = "092" },
                //    user = new User { sAMAccountName = "Ubong.Nkana" }
                //};
            }
            finally
            {
                
            }
        }
    }
}