using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zenith.Core.Models.Authentication;
using Zenith.Core.Shared;

namespace Zenith.Core.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void Login()
        {
            string username = "civa";
            string password = "test";

            string jsonObjUser = JsonConvert.SerializeObject(new
            {
                username = username,
                password = password
            }, 
            Newtonsoft.Json.Formatting.None,
            new Newtonsoft.Json.JsonSerializerSettings
            { StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml });

   //         Newtonsoft.Json.JsonConvert.SerializeObject(your_object,
   //Newtonsoft.Json.Formatting.None,
   //new Newtonsoft.Json.JsonSerializerSettings
   //{ StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml })

            //string test = HttpUtility.HtmlEncode(jsonObjUser);
            //ServicePointManager
            //X509Certificate2 certificates = new X509Certificate2();
            //certificates.Import();

            var client = new RestClient("http://localhost:9192/api");
            //client.ClientCertificates = new X509CertificateCollection(new X509Certificate2[] { certificates });

            var request = new RestRequest(Method.POST);
            
            request.Resource = "/users/login";
            request.AddJsonBody(new
            {
                username = username,
                password = password
            });

            IRestResponse response = null;

            try
            {
                response = client.Execute(request);
                User user = JsonConvert.DeserializeObject<User>(response.Content);
            }
            catch (Exception)
            {
                
            }
        }

        [TestMethod]
        public void PasswordHashTest()
        {
            /* Fetch the stored value */
            string savedPasswordHash = "+IOsYZLzXA9n5gbqURCGh7+2wObuZ9GuQgIyv35HtPPGLx7O";
            //string salt = "+IOsYZLzXA9n5gbqURCGhw==";
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            PasswordHash hash = new PasswordHash(hashBytes);
            if(hash.Verify("disaster"))
            {

            }
            else
            {

            }
        }

        [TestMethod]
        public void CreateHash()
        {
            PasswordHash hash = new PasswordHash("disaster");
            string hashStr = hash.HashString;
            string saltStr = hash.SaltString;
        }
    }
}
