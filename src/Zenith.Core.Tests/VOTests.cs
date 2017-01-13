using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Serialization;
using Zenith.Core.Models.VirtualObservatory.Catalogs;
using Zenith.Core.Models.VirtualObservatory.Objects;

namespace Zenith.Core.Tests
{
    [TestClass]
    public class VOTests
    {
        [TestMethod]
        public void SearchExact()
        {
            var client = new RestClient("http://localhost:9191/api");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest(Method.GET);
            request.Resource = "object_search/exact/{searchTerm}";
            request.AddParameter("searchTerm", "Betelgeuse", ParameterType.UrlSegment);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Binder = new InheritanceSerializationBinder()
                };

                ObjectSearchResult items = JsonConvert.DeserializeObject<ObjectSearchResult>(content, settings);
            }
            catch (Exception exc)
            {

            }
        }

        [TestMethod]
        public void SearchCatalogs()
        {
            CatalogSearchResult result = null;

            var client = new RestClient("http://localhost:9191/api");
            var request = new RestRequest(Method.GET);
            request.Resource = "catalog_search/{searchTerm}";
            request.AddParameter("searchTerm", "Hipparcos", ParameterType.UrlSegment);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                result = JsonConvert.DeserializeObject<CatalogSearchResult>(content);
            }
            catch (Exception exc)
            {

            }
        }

        [TestMethod]
        public void GetKnownCatalogs()
        {
            KnownCatalogs result = null;

            var client = new RestClient("http://localhost:9191/api");
            var request = new RestRequest(Method.GET);
            request.Resource = "known_catalogs";

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                result = JsonConvert.DeserializeObject<KnownCatalogs>(content);
            }
            catch (Exception exc)
            {

            }
        }
    }
}
