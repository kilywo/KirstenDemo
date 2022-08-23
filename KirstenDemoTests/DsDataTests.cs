using Microsoft.VisualStudio.TestTools.UnitTesting;
using KirstenDemo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace KirstenDemo.Tests
{
    [TestClass()]
    public class DsDataTests
    {
        /// <summary>
        /// Example of unit test, creates dsData object and aggregate data
        /// </summary>
        [TestMethod()]
        public void AggreateDataTableTest()
        {
            DataTable data = new DataTable();
            data.Columns.Add("By");
            data.Columns.Add("Alder");
            data.Columns.Add("Indhold");
            data.Rows.Add(new object[] { "Kolding", "12", 500 });
            data.Rows.Add(new object[] { "Kolding", "12", 600 });
            data.Rows.Add(new object[] { "Vejle", "12", 500 });
            data.Rows.Add(new object[] { "Vejle", "13", 700 });
            data.Rows.Add(new object[] { "Fredericia", "12", 300 });
            data.Rows.Add(new object[] { "Fredericia", "12", 300 });
            data.Rows.Add(new object[] { "Fredericia", "13", 400 });


            List<string> removeList = new List<string>() {"Alder" };
            DsData dsData = new DsData(data, removeList);

            Dictionary<string, double> aggData = dsData.AggreateDataTable();


            if (aggData.Count != 3)
                Assert.Fail();

            if (aggData["Kolding"] != 1100)
                Assert.Fail();
        }


        /// <summary>
        /// Example of unit test, creates dsData object and get max value from data
        /// </summary>
        [TestMethod()]
        public void AggreateDataTableTest1()
        {

            DataTable data = new DataTable();
            data.Columns.Add("By");
            data.Columns.Add("Alder");
            data.Columns.Add("Køn");
            data.Columns.Add("Indhold");
            data.Rows.Add(new object[] { "Kolding", "12", "F", 500 });
            data.Rows.Add(new object[] { "Kolding", "12", "M", 600 });
            data.Rows.Add(new object[] { "Vejle", "12", "F", 500 });
            data.Rows.Add(new object[] { "Vejle", "13", "M", 700 });
            data.Rows.Add(new object[] { "Fredericia", "12", "M", 300 });
            data.Rows.Add(new object[] { "Fredericia", "12", "F", 300 });
            data.Rows.Add(new object[] { "Fredericia", "13", "F", 400 });


            List<string> removeList = new List<string>() {"Køn"};
            DsData dsData = new DsData(data, removeList);

            Dictionary<string, double> aggData = dsData.AggreateDataTable();

            if (aggData.Count != 5)
                Assert.Fail();

            if (aggData["Kolding;12"] != 1100)
                Assert.Fail();
        }
        /// <summary>
        /// Example of unit test, creates dsData object and get max value from data
        /// </summary>
        [TestMethod()]
        public void GetMaxValueTest()
        {

            DataTable data = new DataTable();
            data.Columns.Add("By");
            data.Columns.Add("Alder");
            data.Columns.Add("Indhold");
            data.Rows.Add(new object[] { "Kolding", "12", 500 });
            data.Rows.Add(new object[] { "Kolding", "12", 600 });
            data.Rows.Add(new object[] { "Vejle", "12", 500 });
            data.Rows.Add(new object[] { "Vejle", "13", 700 });
            data.Rows.Add(new object[] { "Fredericia", "12", 300 });
            data.Rows.Add(new object[] { "Fredericia", "12", 300 });
            data.Rows.Add(new object[] { "Fredericia", "13", 400 });


            List<string> removeList = new List<string>() { };
            DsData dsData = new DsData(data, removeList);

            double maxValue = dsData.GetMaxValue();

            Assert.IsTrue(maxValue == 700);
        }

       

        /// <summary>
        /// Example of unit test, creates dsData object and get column names as a List from data
        /// </summary>
        [TestMethod()]
        public void GetColumnNamesTest()
        {
            DataTable data = new DataTable();
            data.Columns.Add("By123");
            data.Columns.Add("Alder.");
            data.Columns.Add("Indhold ");
            data.Rows.Add(new object[] { "Kolding", "12", 500 });
            data.Rows.Add(new object[] { "Kolding", "12", 600 });
            data.Rows.Add(new object[] { "Vejle", "12", 500 });
            data.Rows.Add(new object[] { "Vejle", "13", 700 });
            data.Rows.Add(new object[] { "Fredericia", "12", 300 });
            data.Rows.Add(new object[] { "Fredericia", "12", 300 });
            data.Rows.Add(new object[] { "Fredericia", "13", 400 });


            List<string> removeList = new List<string>() { };
            DsData dsData = new DsData(data, removeList);

            List<string> columnNames = dsData.GetColumnNames();
            List<string> columnNamesExpected = new List<string>() { "By123", "Alder", "Indhold" };

            for (int i = 0; i < columnNames.Count; i++)
            {
                if (!columnNames[i].Equals(columnNamesExpected[i]))
                    Assert.Fail();
            }
        }
    }
}