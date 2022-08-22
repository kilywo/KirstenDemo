using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace KirstenDemo
{
    public class DsData
    {
        public List<string> columnsToRemove {get; set;}
        public DataTable dsData { get; set; }

        public DsData(DataTable dsData , List<string> columnsToRemove)
        {
            this.dsData = dsData;
            this.columnsToRemove = columnsToRemove;
        }

        /// <summary>
        /// Moves value column Indhold to last position
        /// </summary>
        private void MoveIndholdToLastColumn()
        {
            int columnsCount = this.dsData.Columns.Count;
            this.dsData.Columns["Indhold"].SetOrdinal(columnsCount - 1);
        }

        /// <summary>
        /// Removes columns with names in columnsToRemove
        /// </summary>
        private void RemoveColumns()
        {
            foreach (var col in columnsToRemove)
            {
                this.dsData.Columns.Remove(col);
            }
        }

        /// <summary>
        /// Sums Indhold with equal rows 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, double> AggreateDataTable()
        {
            MoveIndholdToLastColumn();
            RemoveColumns();

            int columns = this.dsData.Columns.Count;

            Dictionary<string, double> aggData = new Dictionary<string, double>();


            foreach (DataRow row in dsData.AsEnumerable())
            {
                StringBuilder thisKey = new StringBuilder();
                double value = -1;

                for (int i = 0; i < columns-2; i++)
                {
                    if (i == 0)
                    {
                        thisKey = thisKey.Append(row[i].ToString());
                    }
                    else
                    {
                        thisKey = thisKey.Append(";");
                        thisKey = thisKey.Append(row[i].ToString());
                    }
                }

                try
                {
                    value = double.Parse(row["Indhold"].ToString());
                }
                catch (Exception)
                {
                    throw  new Exception(row["Indhold"] + " is not a double value");
                }

                if(aggData.ContainsKey(thisKey.ToString()))
                {
                    aggData[thisKey.ToString()] = aggData[thisKey.ToString()] + value;
                }
                else
                {
                    aggData.Add(thisKey.ToString(), value);
                }
            }
            return aggData;
        }

        /// <summary>
        /// Returns max value for Indhold from datatable
        /// </summary>
        /// <returns></returns>
        public double GetMaxValue()
        {
            double maxValue = dsData.AsEnumerable().Max(x => double.Parse(x["Indhold"].ToString()));
            
            return maxValue;
        }

        /// <summary>
        /// Returns a list whit column names, removed unwanted characters (characters not in validPattern)
        /// </summary>
        /// <returns></returns>
        public List<string> GetColumnNames()
        {
            List<string> headlines = new List<string>();

            int columns = this.dsData.Columns.Count;
            string validPattern = "[^0-9a-zA-Z]";


            foreach (DataColumn column in dsData.Columns)
            {
                string columnName = column.ColumnName;
                columnName = System.Text.RegularExpressions.Regex.Replace(columnName, validPattern, "");
                headlines.Add(columnName);

            }

            return headlines;
        }

    }

}
