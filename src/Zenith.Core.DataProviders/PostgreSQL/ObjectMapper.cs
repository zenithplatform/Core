using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.PostgreSQL
{
    //https://codeabout.wordpress.com/2012/03/08/working-with-postgres-database-and-c-by-using-npgsql-and-reflection/
    internal static class ObjectMapper
    {
        internal static string InsertScriptFromObject<T>(T objInstance)
        {
            string sqlQuery = "";

            try
            {
                // Get type and properties (vector)
                Type typeObj = objInstance.GetType();
                PropertyInfo[] properties = typeObj.GetProperties();

                // Get table
                string[] type = typeObj.ToString().Split('.');
                string table = type[2].ToLower();

                // Start mounting string to insert
                sqlQuery = "INSERT INTO " + table + " VALUES (";

                // It goes from second until second to last
                for (int i = 1; i < properties.Length - 1; i++)
                {
                    object propValue = properties[i].GetValue(objInstance, null);
                    string[] typeValue = propValue.GetType().ToString().Split('.');
                    if (typeValue[1].Equals("String"))
                    {
                        sqlQuery += "'" + propValue.ToString() + "',";
                    }
                    else if (typeValue[1].Equals("DateTime"))
                    {
                        sqlQuery += "'" + Convert.ToDateTime(propValue.ToString()).ToShortDateString() + "',";
                    }
                    else
                    {
                        sqlQuery += propValue.ToString() + ",";
                    }
                }

                // get last attribute here
                object lastValue = properties[properties.Length - 1].GetValue(objInstance, null);
                string[] lastType = lastValue.GetType().ToString().Split('.');
                if (lastType[1].Equals("String"))
                {
                    sqlQuery += "'" + lastValue.ToString() + "'";
                }
                else if (lastType[1].Equals("DateTime"))
                {
                    sqlQuery += "'" + Convert.ToDateTime(lastValue.ToString()).ToShortDateString() + "'";
                }
                else
                {
                    sqlQuery += lastValue.ToString();
                }

                // Ends string builder
                sqlQuery += ");";
            }
            catch (Exception)
            {
                sqlQuery = "";
            }

            return sqlQuery;
        }

        internal static string UpdateScriptFromObject<T>(T objInstance, int idValue)
        {
            string sqlQuery = "";

            try
            {
                // Get table
                string[] type = objInstance.GetType().ToString().Split('.');
                string table = type[2].ToLower();

                // Start building 
                sqlQuery = "UPDATE " + table + " SET ";

                // Get types and properties
                Type type2 = objInstance.GetType();
                PropertyInfo[] properties = type2.GetProperties();

                // Goes until second to last 
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    object propValue = properties[i].GetValue(objInstance, null);
                    string[] nameAttribute = properties[i].ToString().Split(' ');
                    string[] typeAttribute = propValue.GetType().ToString().Split('.');

                    if (typeAttribute[1].Equals("String"))
                    {
                        sqlQuery += nameAttribute[1] + " = '" + propValue.ToString() + "',";
                    }
                    else if (typeAttribute[1].Equals("DateTime"))
                    {
                        sqlQuery += nameAttribute[1] + "= '" + Convert.ToDateTime(propValue.ToString()).ToShortDateString() + "',";
                    }
                    else
                    {
                        sqlQuery += nameAttribute[1] + " = " + propValue.ToString() + ",";
                    }
                }

                // Process last attribute
                object lastValue = properties[properties.Length - 1].GetValue(objInstance, null);
                string[] lastType = lastValue.GetType().ToString().Split('.');
                string[] ultimoCampo = properties[properties.Length - 1].ToString().Split(' ');
                if (lastType[1].Equals("String"))
                {
                    sqlQuery += ultimoCampo[1] + " = '" + lastValue.ToString() + "'";
                }
                else if (lastType[1].Equals("DateTime"))
                {
                    sqlQuery += ultimoCampo[1] + "= '" + Convert.ToDateTime(lastValue.ToString()).ToShortDateString() + "'";
                }
                else
                {
                    sqlQuery += ultimoCampo[1] + " = " + lastValue.ToString();
                }

                // Ends string builder
                sqlQuery += " WHERE id = " + idValue + ";";
            }
            catch (Exception)
            {
                sqlQuery = "";
            }

            return sqlQuery;
        }

        internal static string DeleteScriptFromObject<T>(T objInstance, int idValue)
        {
            string queryString = "";

            try
            {
                string[] type = objInstance.GetType().ToString().Split('.');
                string table = type[2];

                queryString = "DELETE FROM " + table + " WHERE id = " + idValue + ";";
            }
            catch (Exception)
            {
                queryString = "";
            }

            return queryString;
        }
    }
}
