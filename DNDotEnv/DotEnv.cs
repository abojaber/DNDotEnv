// <copyright file="ConfigDotEnv.cs" company="thiqah.sa">
// Copyright (c) thiqah.sa. All rights reserved.
// </copyright>

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
        }
    }
}
