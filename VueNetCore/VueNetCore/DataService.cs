using System;
using System.Data;
using System.Linq;

namespace VueNetCore
{
    public class DataService : IDataService
    {
        private DataTable table = new DataTable();

        public DataService()
        {
            DataColumn column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1
            };
            table.Columns.Add(column);
            table.Columns.Add("Payload", typeof(string));
            table.DefaultView.Sort = "Payload ASC";
        }

        public void Add(string data)
        {
            table.Rows.Add(null, data);
        }

        public Entry[] Search(string query)
        {
            var dr = table.Select("Payload LIKE '%" + query + "%'");
            Entry[] results = new Entry[0];
            if (dr.Length == 0)
            {
                return results;
            }

            DataTable dt = dr.CopyToDataTable();
            results = dt.Rows.OfType<DataRow>().Select(k => new Entry()
            {
                Id = (int)k[0], Payload = k[1].ToString()
            }).ToArray();

            return results;
        }
    }
}