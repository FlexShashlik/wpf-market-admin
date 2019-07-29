using RestSharp;
using System.Collections.Generic;
using WpfAdminApp.Entities;

namespace WpfAdminApp
{
    public static class MarketAPI
    {
        public static string Token { get; set; }
        private static RestClient _client = new RestClient("http://192.168.1.162/api/v1/");


        public const string SuccessMessage = "Операция завершена удачно";
        public const string FailMessage = "Что-то пошло не так...";


        public static bool Login(string email, string password)
        {
            RestRequest request = new RestRequest("auth/login/", Method.POST);
            request.AddJsonBody(new { email, password });
            
            IRestResponse<LoginResponse> response = _client.Execute<LoginResponse>(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Token = response.Data.Token;
                return true;
            }

            return false;
        }

        public static List<Catalog> GetCatalog()
        {
            RestRequest request = new RestRequest("catalog/", Method.GET);

            return _client.Execute<List<Catalog>>(request).Data;
        }

        public static bool AddCatalog(Catalog catalog)
        {
            RestRequest request = new RestRequest("admin/catalog/", Method.POST);
            request.AddJsonBody(new { catalog.Name });

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }

        public static bool UpdateCatalog(Catalog catalog)
        {
            RestRequest request = new RestRequest("admin/catalog/{id}", Method.PUT);
            request.AddUrlSegment("id", catalog.ID);
            request.AddJsonBody(new { catalog.Name });

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);
            
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
