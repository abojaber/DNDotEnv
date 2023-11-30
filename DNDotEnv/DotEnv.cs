// <copyright file="ConfigDotEnv.cs" company="thiqah.sa">
// Copyright (c) thiqah.sa. All rights reserved.
// </copyright>
using System.Text.RegularExpressions;

namespace DNDotEnv
{
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception(string.Format(
                     filePath + " Cannot be read"));
            }
            // Single Line Variables
            string env_file = File.ReadAllText(filePath);

            var pattern_single_line = @"([a-zA-Z_]\w*)\s*=(?!""{2,})(.*?)(\n|$)";


            MatchCollection matches = Regex.Matches(env_file, pattern_single_line);

             foreach (Match match in matches)
                {
                    var key = match.Groups[1].Value.Trim();
                    var value = match.Groups[2].Value.Trim();
                    Environment.SetEnvironmentVariable(key, value);
                }

            ////////////////////////
            //Multi-lines variables
            var pattern_multi_line = @"([a-zA-Z_]\w*)\s*=""""""(\n|.)*?("""""")";

            matches = Regex.Matches(env_file, pattern_multi_line);

            foreach (Match match in matches)
            {
                var data = match.Value.Split('=');
                var key = data[0];
                var value_ = data[1];
                value_ = Regex.Replace(value_, $"\"\"\"{System.Environment.NewLine}", "");
                value_ = Regex.Replace(value_, $"{System.Environment.NewLine}\"\"\"", "");

                Environment.SetEnvironmentVariable(key, value_);
            }
        }

    }
}
