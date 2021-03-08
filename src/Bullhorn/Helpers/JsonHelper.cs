using CodeCapital.Bullhorn.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Helpers
{
    //https://stackoverflow.com/questions/58512542/read-a-json-file-and-generate-string-keys-with-values-in-a-dictionary-like-objec
    //https://stackoverflow.com/questions/7394551/c-sharp-flattening-json-structure
    public static class JsonHelper
    {
        private static readonly string[] FlatEntities = { "candidate", "candidateReference", "personReference", "clientContact", "clientContactReference", "clientCorporation", "jobOrder", "placement", "jobSubmission", "owner", "user", "source", "editHistory" };
        private static readonly string[] DateTimeFields = { "dateAdded", "dateAvailable", "dateBegin", "dateClosed", "dateEnd", "dateLastModified", "dateLastComment", "dateLastVisit", "customDate1", "customDate2", "customDate3", "userDateAdded", "dateLastPublished" };

        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public static List<dynamic> DeserializeAndFlatten(string json)
        {
            //https://stackoverflow.com/questions/62429809/is-there-a-more-elegant-way-to-get-specific-value-from-json-object-using-system
            var token = JToken.Parse(json);

            dynamic dynamicObject = ConvertJTokenToObject(token.SelectToken("data"));

            return dynamicObject is IList ? dynamicObject : new List<dynamic> { dynamicObject };
        }

        public static object ConvertJTokenToObject(JToken token)
        {
            if (token is JValue jsonValue) return jsonValue.Value;

            if (token is JObject)
            {
                var expandoObject = new ExpandoObject();

                (from childToken in token
                 where childToken is JProperty
                 select childToken as JProperty).ToList().ForEach(
                    property =>
                    {
                        if (property.Value is JObject && FlatEntities.Contains(property.Name))
                        {
                            foreach (var childProperty in (from c in property.Value
                                                           where c is JProperty
                                                           select c as JProperty).ToList())
                            {
                                AddItem(childProperty, expandoObject, property);
                            }
                        }
                        else AddItem(property, expandoObject);
                    });

                return expandoObject;
            }

            var jsonArray = token as JArray;

            if (jsonArray == null) throw new ArgumentException($"Unknown token type '{token.GetType()}'", nameof(token));

            return jsonArray.Select(ConvertJTokenToObject).ToList();
        }

        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response, ILogger? logger = null)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), JsonSerializerOptions);
            }
            catch (Exception e)
            {
                logger?.LogError(e, "Deserialize Error at {uri}", response.RequestMessage.RequestUri);
                throw;
            }
        }

        private static object ConvertToDate(object value) => long.TryParse(value.ToString(), out var result) ? result.ToDateTime() : value;

        private static void AddItem(JProperty property, ExpandoObject expando, JProperty parentProperty = null)
        {
            var value = ConvertJTokenToObject(property.Value);

            if (value != null && DateTimeFields.Contains(property.Name)) value = ConvertToDate(value);

            var propertyName = parentProperty == null ? property.Name : $"{parentProperty.Name} {property.Name}";

            ((IDictionary<string, object>)expando).Add(propertyName, value);
        }
    }
}