using RestSharp;
using System.Collections.Generic;
using WpfAdminApp.Entities;
using WpfAdminApp.ViewModels;

namespace WpfAdminApp
{
    public static class MarketAPI
    {
        public static string Token { get; set; }
        public const string SERVER = "http://127.0.0.1/";
        private static RestClient _client = new RestClient(SERVER + "api/v1/");


        public const string SuccessMessage = "Операция завершена удачно";
        public const string FailMessage = "Что-то пошло не так...";
        public const string PictureAbsence = "Картинка не выбрана!";


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

        #region Catalog

        public static void GetCatalog(out List<Catalog> data, out bool result)
        {
            RestRequest request = new RestRequest("catalog/", Method.GET);

            IRestResponse<List<Catalog>> response = _client.Execute<List<Catalog>>(request);

            result = response.StatusCode == System.Net.HttpStatusCode.OK;

            data = response.Data;
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

        public static bool DeleteCatalog(Catalog catalog)
        {
            RestRequest request = new RestRequest("admin/catalog/{id}", Method.DELETE);
            request.AddUrlSegment("id", catalog.ID);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        #endregion

        #region SubCatalog

        public static List<SubCatalog> GetSubCatalog()
        {
            RestRequest request = new RestRequest("sub_catalog/", Method.GET);

            return _client.Execute<List<SubCatalog>>(request).Data;
        }

        public static bool AddSubCatalog(SubCatalog subCatalog)
        {
            RestRequest request = new RestRequest("admin/sub_catalog/", Method.POST);

            request.AddParameter("name", subCatalog.Name);
            request.AddParameter("catalog_id", subCatalog.CatalogID);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }

        public static bool UpdateSubCatalog(SubCatalog subCatalog)
        {
            RestRequest request = new RestRequest("admin/sub_catalog/{id}", Method.PUT);
            request.AddUrlSegment("id", subCatalog.ID);

            request.AddParameter("name", subCatalog.Name);
            request.AddParameter("catalog_id", subCatalog.CatalogID);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public static bool DeleteSubCatalog(SubCatalog subCatalog)
        {
            RestRequest request = new RestRequest("admin/sub_catalog/{id}", Method.DELETE);
            request.AddUrlSegment("id", subCatalog.ID);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        #endregion

        #region Product

        public static void GetProducts(out List<Product> data, out bool result)
        {
            RestRequest request = new RestRequest("products/", Method.GET);

            IRestResponse<List<Product>> response = _client.Execute<List<Product>>(request);

            result = response.StatusCode == System.Net.HttpStatusCode.OK;

            data = response.Data;
        }

        public static bool AddProduct(Product product)
        {
            RestRequest request = new RestRequest("admin/products/", Method.POST);

            request.AddParameter("name", product.Name);
            request.AddParameter("price", product.Price);
            request.AddParameter("image_extension", product.ImageExtension);
            request.AddParameter("sub_catalog_id", product.SubCatalogID);

            request.AddFile("image", ProductsViewModel.LocalImagePath);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }

        public static bool UpdateProduct(Product product)
        {
            RestRequest request = new RestRequest("admin/products/{id}", Method.PUT);
            request.AddUrlSegment("id", product.ID);

            request.AddParameter("name", product.Name);
            request.AddParameter("price", product.Price);
            request.AddParameter("image_extension", product.ImageExtension);
            request.AddParameter("sub_catalog_id", product.SubCatalogID);

            if (ProductsViewModel.LocalImagePath != null)
                request.AddFile("image", ProductsViewModel.LocalImagePath, "multipart/form-data");

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public static bool DeleteProduct(Product product)
        {
            RestRequest request = new RestRequest("admin/products/{id}", Method.DELETE);
            request.AddUrlSegment("id", product.ID);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        #endregion

        #region User

        public static void GetUsers(out List<User> data, out bool result)
        {
            RestRequest request = new RestRequest("admin/users/", Method.GET);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse<List<User>> response = _client.Execute<List<User>>(request);

            result = response.StatusCode == System.Net.HttpStatusCode.OK;

            data = response.Data;
        }

        public static bool UpdateUser(User user)
        {
            RestRequest request = new RestRequest("admin/users/{id}", Method.PUT);
            request.AddUrlSegment("id", user.ID);

            request.AddParameter("email", user.Email);
            request.AddParameter("first_name", user.FirstName);
            request.AddParameter("last_name", user.LastName);
            request.AddParameter("role", user.Role);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public static bool DeleteUser(User user)
        {
            RestRequest request = new RestRequest("admin/users/{id}", Method.DELETE);
            request.AddUrlSegment("id", user.ID);

            request.AddHeader("Authorization", "Bearer " + Token);

            IRestResponse response = _client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        #endregion
    }
}
