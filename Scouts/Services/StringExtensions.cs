using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scouts.Services
{
    public static class StringExtensions
    {
        private static Regex Breakup = new Regex("{{(?<name>\\w+)}}",RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public static string Moustache( this string input, Dictionary<string, object> variables)
        {
            var parts = Breakup.Split(input);

            var result = new StringBuilder();

            var isKey = false;
            foreach( var val in parts)
            {
                if (isKey)
                {
                    var key = val.Trim();
                    if (variables.ContainsKey(key))
                    {
                        result.Append(variables[key]);
                    }
                }
                else
                {
                    result.Append(val);
                }

                isKey = !isKey;
            }

            return result.ToString();
        }

    }
}
