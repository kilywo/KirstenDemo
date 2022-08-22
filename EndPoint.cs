using System;
using System.Collections.Generic;
using System.Text;

namespace KirstenDemo
{

    class EndPoint
    {
        private const string _endPointYear = "Tid=*"; 
        public string endPoint { get; set; }
        public int offset { get; set; }
        public int years { get; set; }


        public EndPoint(string endPoint, int offset, int years)
        {
            this.endPoint = endPoint;
            this.offset = offset;
            this.years = years;
        }

        /// <summary>
        /// Prepare the endpoints for http requests, it is only possible to get data for 1 year
        /// </summary>
        /// <returns></returns>
        public List<string> PrepareEndpoints()
        {
            List<string> endPoints = new List<string>();

            int year = DateTime.Now.Year;

            for (int i = 0; i < years; i++)
            {
                int thisYear = year -(i + offset);
                string thisEndPoint = endPoint.Replace(_endPointYear, "Tid=" + thisYear.ToString());
                endPoints.Add(thisEndPoint);
            }

            return endPoints;
        }
    }
}
