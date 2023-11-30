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
            // Line by Line
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length > 2)
                {
                    var resultList = parts.ToList();
                    resultList.Remove(parts[0]);

                    var result = string.Join("=", resultList);
                    Environment.SetEnvironmentVariable(
                        parts[0], result);
                    continue;
                }

                // TODO: check the cases
                if (parts.Length != 2)
                {
                    continue;
                }

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
            //Multi-lines variables
            var pattern_multi_line = @"([a-zA-Z_]\w*)\s*=""""""(\n|.)*?("""""")";
            string env_file = File.ReadAllText(filePath);
            MatchCollection matches = Regex.Matches(env_file, pattern_multi_line);

            foreach (Match match in matches)
            {
                var data = match.Value.Split('=');
                var key = data[0];
                var value_ = data[1];
                value_ = Regex.Replace(value_, $"\"\"\"{System.Environment.NewLine}", "");
                value_ = Regex.Replace(value_, $"{System.Environment.NewLine}\"\"\"", "");

                Environment.SetEnvironmentVariable(key,value_);
            }

        }

    }
}
