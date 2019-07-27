using RestSharp.Deserializers;

namespace WpfAdminApp.Entities
{
    class LoginResponse
    {
        [DeserializeAs(Name = "expire")]
        public string ExpireDate { get; set; }

        [DeserializeAs(Name = "token")]
        public string Token { get; set; }
    }
}
