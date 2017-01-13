using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Authentication;

namespace Zenith.Core.Service
{
    public class AuthService
    {
        string _authServiceAddress = "";

        public AuthService()
        {
            try
            {
                NameValueCollection zenithServices = (NameValueCollection)ConfigurationManager.GetSection("zenithServices");
                _authServiceAddress = zenithServices["authServiceAddress"];
            }
            catch (Exception exc)
            {

            }
        }

        public User AuthenticateUser(string username, string password)
        {
            var client = new RestClient(_authServiceAddress);
            var request = new RestRequest(Method.POST);
            request.Resource = "/users/login";
            request.AddJsonBody(new
            {
                username = username,
                password = password
            });

            try
            {
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<User>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetUser(int userId, string accessToken)
        {
            var client = new RestClient(_authServiceAddress);
            var request = new RestRequest(Method.GET);

            request.Resource = "/users/{userId}";
            request.AddHeader("Authorization", accessToken);
            request.AddParameter("userId", userId, ParameterType.UrlSegment);

            try
            {
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                return JsonConvert.DeserializeObject<User>(content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User RegisterUser(string emailAddress, string username, string password)
        {
            var client = new RestClient(_authServiceAddress);
            var request = new RestRequest(Method.POST);
            request.Resource = "/users";
            request.AddJsonBody(new
            {
                emailAddress = emailAddress,
                username = username,
                password = password
            });

            try
            {
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<User>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SignOutUser(string username)
        {
            var client = new RestClient(_authServiceAddress);
            var request = new RestRequest(Method.POST);
            request.Resource = "/users/signout";
            request.AddJsonBody(new
            {
                username = username
            });

            try
            {
                IRestResponse response = client.Execute(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
