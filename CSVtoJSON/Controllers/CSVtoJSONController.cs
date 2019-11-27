using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TRex.Metadata;
using CSVtoJSON.Models;
using CsvHelper;
using System.IO;

namespace CSVtoJSON.Controllers
{
    public class CSVtoJSONController : ApiController
    {
        /// <summary>
        /// Convert CSV to JSON where the first row is headers
        /// </summary>
        /// <param name="body">CSV file to convert to JSON</param>
        /// <param name="delimiter">Delimeter used by CSV file</param>
        /// <returns>JSON Result - the JArray of Objects generated from each row</returns>
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, Type = typeof(JsonResult))]
        [Metadata("CSV to JSON with header row", "Convert CSV to JSON")]
        public HttpResponseMessage Post([FromBody] string body, [FromUri] char delimiter = ',')
        {
            JsonResult resultSet = new JsonResult();
            String value;
            var lineObject = new JObject();
            string[] headers = new string[1024]; //max of 1024 columns for now

            using (TextReader sr = new StringReader(body))
            {
                var csv = new CsvReader(sr);
                //set the delimiter type
                csv.Configuration.Delimiter = delimiter.ToString();
                //read header - not necessary to leverage header record functionality currently
                csv.Configuration.HasHeaderRecord = false;
                if (csv.Read())
                {
                    for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                    {
                        headers[i] = value;
                    }
                }

                //read the rest of the file
                while (csv.Read())
                {
                    for (int i = 0; csv.TryGetField<string>(i, out value); i++)
                    {
                        lineObject[headers[i]] = value;
                    }
                    resultSet.rows.Add(lineObject);
                }
            }
            return Request.CreateResponse<JsonResult>(resultSet);
        }

    }
}
