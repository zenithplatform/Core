using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zenith.Core.Models;
using Zenith.Core.Models.Serialization;
using Zenith.Core.Models.VirtualObservatory.Catalogs;
using Zenith.Core.Models.VirtualObservatory.Objects;

namespace Zenith.Core.Service
{
    public class SearchService
    {
        string _searchServiceAddress = "";
        public event EventHandler SearchStarted;
        public event EventHandler SearchFinished;

        public SearchService()
        {
            try
            {
                NameValueCollection zenithServices = (NameValueCollection)ConfigurationManager.GetSection("zenithServices");
                _searchServiceAddress = zenithServices["searchServiceAddress"];
            }
            catch(Exception exc)
            {

            }
        }

        public ObjectSearchResult SearchObjectExact(string searchTerm)
        {
            ObjectSearchResult result = null;

            if (SearchStarted != null)
                SearchStarted(this, EventArgs.Empty);

            var client = new RestClient(_searchServiceAddress);
            var request = new RestRequest(Method.GET);
            request.Resource = "object_search/exact/{searchTerm}";
            request.AddParameter("searchTerm", searchTerm, ParameterType.UrlSegment);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Binder = new InheritanceSerializationBinder()
                };

                result = JsonConvert.DeserializeObject<ObjectSearchResult>(content, settings);
            }
            catch (Exception exc)
            {

            }

            if (SearchFinished != null)
                SearchFinished(this, EventArgs.Empty);

            return result;
        }

        public ObjectSearchResult SearchObjects(string searchTerm)
        {
            ObjectSearchResult result = null;

            if (SearchStarted != null)
                SearchStarted(this, EventArgs.Empty);

            var client = new RestClient(_searchServiceAddress);
            var request = new RestRequest(Method.GET);
            request.Resource = "object_search/wildcard/{searchTerm}";
            request.AddParameter("searchTerm", searchTerm, ParameterType.UrlSegment);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Binder = new InheritanceSerializationBinder()
                };

                result = JsonConvert.DeserializeObject<ObjectSearchResult>(content, settings);
            }
            catch (Exception exc)
            {

            }

            if (SearchFinished != null)
                SearchFinished(this, EventArgs.Empty);

            return result;
        }

        public CatalogSearchResult SearchCatalog(string searchTerm)
        {
            CatalogSearchResult result = null;

            if (SearchStarted != null)
                SearchStarted(this, EventArgs.Empty);

            var client = new RestClient(_searchServiceAddress);
            var request = new RestRequest(Method.GET);
            request.Resource = "catalog_search/{searchTerm}";
            request.AddParameter("searchTerm", searchTerm, ParameterType.UrlSegment);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            try
            {
                result = JsonConvert.DeserializeObject<CatalogSearchResult>(content);
            }
            catch (Exception exc)
            {

            }

            if (SearchFinished != null)
                SearchFinished(this, EventArgs.Empty);

            return result;
        }

        public KnownCatalogs GetKnownCatalogs()
        {
            KnownCatalogs result = null;

            if (SearchStarted != null)
                SearchStarted(this, EventArgs.Empty);

            var client = new RestClient(_searchServiceAddress);
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

            if (SearchFinished != null)
                SearchFinished(this, EventArgs.Empty);

            return result;
        }
    }
}
