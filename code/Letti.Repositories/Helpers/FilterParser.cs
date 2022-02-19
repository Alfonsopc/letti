using Letti.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Letti.Repositories.Helpers
{
    public class FilterParser
    {
        private List<string> _composedLastNames;

        public FilterParser()
        {
            _composedLastNames = new List<string>();
            FillComposed();
        }

        public SearchParameter Parse(string filter)
        {
            SearchParameter searchParameter = new SearchParameter();
            string[] parameters = filter.Split(' ');
            if (parameters.Length == 1)
            {
                searchParameter.LastName = filter;
            }
            else
            {
                if (IsComposed(parameters[0]))
                {
                    var names = ComposedParse(parameters);
                    searchParameter.LastName = names.lastName;
                    searchParameter.MotherLastName = names.motherLastName;
                }
                else
                {
                    searchParameter.LastName = parameters[0];
                    searchParameter.MotherLastName = parameters[1];
                }
            }
            return searchParameter;
        }

        private (string lastName, string motherLastName) ComposedParse(string[] parameters)
        {
            string lastName = string.Empty;
            string motherLastName = string.Empty;
            int length = parameters.Length;
            string starts = parameters[0];
            if (starts == "DEL" || starts == "SAN")
            {
                lastName = $"{parameters[0]} {parameters[1]}";
                if (length > 2)
                {
                    for (int k = 2; k < length; k++)
                    {
                        motherLastName += parameters[k] + " ";
                    }
                    motherLastName = motherLastName.TrimEnd();
                }
            }
            if (starts == "DE")
            {
                if (length == 2)
                {
                    lastName = $"{parameters[0]} {parameters[1]}";
                }
                else
                {
                    if (parameters[1] != "LA")
                    {
                        lastName = $"{parameters[0]} {parameters[1]}";
                        for (int k = 2; k < length; k++)
                        {
                            motherLastName += parameters[k] + " ";
                        }
                        motherLastName = motherLastName.TrimEnd();
                    }
                    else
                    {
                        lastName = $"{parameters[0]} {parameters[1]} {parameters[2]}";
                        for (int k = 3; k < length; k++)
                        {
                            motherLastName += parameters[k] + " ";
                        }
                        motherLastName = motherLastName.TrimEnd();
                    }

                }
            }
            return (lastName, motherLastName);
        }

        private void FillComposed()
        {
            _composedLastNames.Add("DE");
            _composedLastNames.Add("DEL");
            _composedLastNames.Add("SAN");
        }

        private bool IsComposed(string lastName)
        {
            return _composedLastNames.Contains(lastName);
        }
    }
}
