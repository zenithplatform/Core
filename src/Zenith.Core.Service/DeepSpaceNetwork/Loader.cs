using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Zenith.Core.Models.DeepSpaceNetwork;
using Zenith.Core.Models.DeepSpaceNetwork.XML;

namespace Zenith.Core.Service.DeepSpaceNetwork
{
    internal class Loader
    {
        private readonly string dsnConfigXMLURL = @"http://eyes.nasa.gov/dsn/config.xml";
        private readonly string dsnStatusXMLURLFormat = @"http://eyes.nasa.gov/dsn/data/dsn.xml?r={0}";

        private DSNStatus _status = null;

        internal Loader()
        {
            _status = new DSNStatus();
        }

        private bool LoadConfig()
        {
            bool success = true;

            var dsnConfigDeserializer = new XmlSerializer(typeof(DSNConfigResult));

            try
            {
                WebRequest request = WebRequest.Create(dsnConfigXMLURL);
                WebResponse response = null;

                if (request != null)
                {
                    response = request.GetResponse();

                    if(response != null)
                    {
                        System.IO.Stream responseStream = response.GetResponseStream();

                        if (responseStream != null)
                        {
                            var dsnConfigXML = (DSNConfigResult)dsnConfigDeserializer.Deserialize(responseStream);

                            Normalizer.Normalize(ref _status, dsnConfigXML);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                success = false;
            }

            return success;
        }

        internal bool Load()
        {
            bool success = LoadConfig();

            if (!success)
                return false;

            var requestPeriod = (Int32)(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            string dsnStatusXMLURL = string.Format(dsnStatusXMLURLFormat, (requestPeriod / 5));

            var dsnQueryDeserializer = new XmlSerializer(typeof(DSNQueryResult));

            try
            {
                WebRequest request = WebRequest.Create(dsnStatusXMLURL);
                WebResponse response = null;

                if (request != null)
                {
                    response = request.GetResponse();

                    if (response != null)
                    {
                        System.IO.Stream responseStream = response.GetResponseStream();

                        if (responseStream != null)
                        {
                            var dsnQueryXML = (DSNQueryResult)dsnQueryDeserializer.Deserialize(responseStream);

                            Normalizer.Normalize(ref _status, dsnQueryXML);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                success = false;
            }

            return success;
        }

        internal DSNStatus Status
        {
            get { return _status; }
        }
    }
}
