using ClimaTime.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ClimaTime.Utilities
{
    public class ServicesAPI
    {
        private readonly IConfiguration _configuration;

        public ServicesAPI(IConfiguration configuration)
        {
     

            _configuration = configuration;

           
        }
        /// <summary>
        /// Usado para capturar uma lista de estados disponiveis
        /// </summary>
        /// <returns>Retorna Objeto de lista</returns>
        public async Task<List<UFResponseModel>> GetUf()
        {
            List<UFResponseModel> result = new List<UFResponseModel>();
            AuthenticationResponseModel _Token = await GenerateToken();

            string url = _configuration.GetSection("UrlApi").Value + "Search/GetUf/";

            if (url != null)
            {
                var client = new RestClient(url);
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + _Token.Token + "");
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.Content != null)
                {
                    result = JsonConvert.DeserializeObject<List<UFResponseModel>>(response.Content);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<CityResponseModel>> GetCity(int ufId)
        {
            List<CityResponseModel> result = new List<CityResponseModel>();
            AuthenticationResponseModel _Token = await GenerateToken();

            string url = _configuration.GetSection("UrlApi").Value + "Search/GetCity/";

            if (url != null)
            {
                var client = new RestClient(url);
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + _Token.Token + "");
                request.AddParameter("ufID", ufId);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.Content != null)
                {
                    result = JsonConvert.DeserializeObject<List<CityResponseModel>>(response.Content);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


       
        public async Task<List<ResponseModel>> HotCitys()
        {
            List<ResponseModel> result = new List<ResponseModel>();
            AuthenticationResponseModel _Token = await GenerateToken();

            string url = _configuration.GetSection("UrlApi").Value + "Search/GetHotCity/";

            if (_Token != null)
            {
                var client = new RestClient(url);
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + _Token.Token + "");
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.Content != null)
                {
                    result = JsonConvert.DeserializeObject<List<ResponseModel>>(response.Content);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ResponseModel>> ColdCitys()
        {
            List<ResponseModel> result = new List<ResponseModel>();
            AuthenticationResponseModel _Token = await GenerateToken();

            string url = _configuration.GetSection("UrlApi").Value + "Search/GetColdCity/";

            if (_Token != null)
            {
                var client = new RestClient(url);
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + _Token.Token + "");
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.Content != null)
                {
                    result = JsonConvert.DeserializeObject<List<ResponseModel>>(response.Content);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
      
        public async Task<List<ResponseModel>> NextTemperatures(int cityId)
        {
            List<ResponseModel> result = new List<ResponseModel>();
            AuthenticationResponseModel _Token = await GenerateToken();

            string url = _configuration.GetSection("UrlApi").Value + "Search/GetNextConditions/";

            if (_Token != null)
            {
                var client = new RestClient(url);
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + _Token.Token + "");
                request.AddParameter("cityId", cityId);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response.Content != null)
                {
                    result = JsonConvert.DeserializeObject<List<ResponseModel>>(response.Content);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        private async Task<AuthenticationResponseModel> GenerateToken()
        {
            var url = _configuration.GetSection("UrlApi").Value + "Login/Login";

            AuthenticationRequestModel _requestAuth = new AuthenticationRequestModel();

            _requestAuth.UserName = _configuration.GetSection("UserAplication").Value;
            _requestAuth.Password = _configuration.GetSection("PassAplication").Value;

            var JsonRequest = JsonConvert.SerializeObject(_requestAuth);
            var client = new RestClient(url);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonRequest;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.Content != null)
            {
                AuthenticationResponseModel _returnResponse = JsonConvert.DeserializeObject<AuthenticationResponseModel>(response.Content);
                return _returnResponse;
            }
            else
            {
                return null;
            }
        }
    }
}
