using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Zenith.Core.Models;
using Zenith.Core.Models.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Serialization;
using System.Xml;

namespace Zenith.Core.Tests
{
    [TestClass]
    public class SearchServiceTests
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
            KnownCatalogsResult result = null;

            var client = new RestClient("http://localhost:9191/api");
            var request = new RestRequest(Method.GET);
            request.Resource = "known_catalogs";

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                result = JsonConvert.DeserializeObject<KnownCatalogsResult>(content);
            }
            catch (Exception exc)
            {

            }
        }
    }
}
