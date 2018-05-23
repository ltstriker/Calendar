using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace Calendar.network
{
    //还是单例模式，getConnection拿实例，然后getConnectToGetWeatherAsync根据城市名称获取天气
    class networkConnection
    {
        private static networkConnection ins;
        private networkConnection()
        {
            ;
        }

        public static networkConnection getConnection()
        {
            if (ins == null)
            {
                ins = new networkConnection();
            }

            return ins;
        }

        public async Task<string> getConnectToGetWeatherAsync(string queryString)
        {
            //Create an HTTP client 
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;
            //The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
            //especially if the header value is coming from user input.
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }
            //Uri requestUri = new Uri("http://v.juhe.cn/weather/index?format=2&cityname="+queryString+"&key=c8f3ebd5bd91ab7aa0a9be9cc717e5dd");
            Uri requestUri = new Uri("http://v.juhe.cn/weather/index?cityname=" + queryString + "&dtype=xml&format=2&key=c8f3ebd5bd91ab7aa0a9be9cc717e5dd");
            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            try
            {
                //Send the GET request    
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                return queryString + ":" + getStringFromWeatherXMLString(httpResponseBody);
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return "Error,please retry";
            }
        }

        private string getStringFromWeatherXMLString(string xmlStr)
        {//200
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            IXmlNode root = doc.SelectSingleNode("root");
            XmlNodeList nodeList = root.ChildNodes;
            if (int.Parse(nodeList[0].InnerText) != 200)
            {
                return "city is not exist";
            }
            nodeList = nodeList[2].ChildNodes;
            nodeList = nodeList[1].ChildNodes;
            //遍历所有子节点
            string temperature = "";
            string weather = "";
            string advice = "";
            foreach (IXmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.TagName == "temperature")
                {
                    temperature = "\ntemperature: " + xe.InnerText;
                }
                else if (xe.TagName == "weather")
                {
                    weather = "\nweather: " + xe.InnerText;
                }
            }
            return temperature + weather + advice;
        }
    }
}
