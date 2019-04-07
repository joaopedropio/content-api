using System;
using System.Collections.Generic;

namespace ContentApi.Helpers
{
    public static class QueryHelper
    {
        public static string CreateSearchBy(string table, string column, string value)
        {

            //var query = $"SELECT ID FROM PERSONS WHERE REPLACE(NAME, ' ', '') LIKE REPLACE('%NAME%', ' ', '');;";

            if (string.IsNullOrEmpty(table))
                throw new ArgumentNullException("table");

            if (string.IsNullOrEmpty(column))
                throw new ArgumentNullException("column");

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            var words = value.Split(' ');

            var valueWithoutSpaces = string.Format("'%{0}%'", string.Join('%', words));

            return $"SELECT ID FROM {table} WHERE {column} LIKE {valueWithoutSpaces};";
        }

        public static string CreateInsertQuery(string table, List<KeyValuePair<string, string>> columns)
        {
            if(string.IsNullOrEmpty(table))
                throw new ArgumentNullException("table");

            if (columns?.Count == 0)
                throw new ArgumentNullException("columns");

            var columnsNames = new List<string>();
            var columnsValues = new List<string>();
            for (int i = 0; i < columns.Count; i++)
            {
                var name = columns[i].Key;
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException("columnName");

                columnsNames.Add($"{name}");

                var value = columns[i].Value;
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("columnValue");

                columnsValues.Add($"'{value}'");
            }
            var names = string.Join(',', columnsNames);
            var values = string.Join(',', columnsValues);

            return string.Format("INSERT INTO {0} ({1}) VALUES ({2});", table, names, values);
        }
    }
}
