using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataService
{
    public static class PersonFormatHelper
    {
        public static IList<Person> GetPersons(string response)
        {
            IList<Person> persons = null;
            try
            {
                persons = JsonConvert.DeserializeObject<List<Person>>(response);
            }
            catch (JsonSerializationException jsEx)
            {
                persons = new List<Person>();
                var entries = Regex.Split(response, Environment.NewLine);
                var formattedJson = string.Empty;
                foreach (var entry in entries)
                {
                    var row = entry.Remove(0, 1).Remove(entry.IndexOf('}'), 1);
                    var regex = new Regex("(?<key>[^:]+):(?<value>[^,]+),?");

                    var matches = regex.Matches(row);
                    formattedJson = "{";
                    foreach (Match match in matches)
                    {
                        var property = GetQuotedString(match.Groups["key"].Value);
                        var value = GetQuotedString(match.Groups["value"].Value);
                        formattedJson += string.Format("{0}:{1},", property, value);
                    }
                    formattedJson = formattedJson.Remove(formattedJson.Length - 1, 1);
                    formattedJson += "}";
                    persons.Add(JsonConvert.DeserializeObject<Person>(formattedJson));
                }
            }
            catch (Exception ex)
            {
            }
            return persons;

        }

        private static string GetQuotedString(string value)
        {
            value = value.Trim();
            if (!value.StartsWith("\""))
            {
                value = "\"" + value;
            }
            if (!value.EndsWith("\""))
            {
                value += "\"";
            }

            return value;
        }
    }
}
