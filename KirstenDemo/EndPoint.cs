using System;
using System.Collections.Generic;
using System.Text;

namespace KirstenDemo
{

    class EndPoint
    {
        private const string _endPointYear = "Tid=*";
        private string _endPoint;
        private int _offset;
        private int _years;


        public EndPoint(string endPoint, int offset, int years)
        {
            _endPoint = endPoint;
            _offset = offset;
            _years = years;
        }

        /// <summary>
        /// Prepare the endpoints for http requests, it is only possible to get data for 1 year
        /// </summary>
        public List<string> PrepareEndpoints()
        {
            List<string> endPoints = new List<string>();

            int year = DateTime.Now.Year;

            for (int i = 0; i < _years; i++)
            {
                int thisYear = year -(i + _offset);
                string thisEndPoint = _endPoint.Replace(_endPointYear, "Tid=" + thisYear.ToString());
                endPoints.Add(thisEndPoint);
            }

            return endPoints;
        }
    }
}
