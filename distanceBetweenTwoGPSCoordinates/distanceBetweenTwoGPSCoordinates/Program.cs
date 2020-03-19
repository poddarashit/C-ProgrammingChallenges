using System;
using System.Net;
using System.IO;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace distanceBetweenTwoGPSCoordinates
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Google maps api key
                string api_key = <your key here>;

                //take user inputs for latitude and longitude for origin and destination
                Console.WriteLine("Enter latitude for origin");
                double lat_origin = ValidateLat(Console.ReadLine());
                Console.WriteLine("Enter longitude for origin");
                double lon_origin = ValidateLon(Console.ReadLine());
                Console.WriteLine("Enter latitude for destination");
                double lat_destination = ValidateLat(Console.ReadLine());
                Console.WriteLine("Enter longitude for destination");
                double lon_destination = ValidateLon(Console.ReadLine());

                //call to api
                string url = "https://maps.googleapis.com/maps/api/distancematrix/json?&origins=" + lat_origin + "," + lon_origin + "&destinations=" + lat_destination + "," + lon_destination + "&key=" + api_key;
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string result = reader.ReadToEnd();

                        //parsing json data to get distance in km
                        JObject joResponse = JObject.Parse(result);
                        JArray jArr = (JArray)joResponse["rows"];
                        JObject elemObj = (JObject)jArr[0];
                        JArray elmArr = (JArray)elemObj["elements"];
                        JObject distObj = (JObject)elmArr[0];
                        JObject dist = (JObject)distObj["distance"];

                        Console.WriteLine("Distance between the two locations is: " + dist["text"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //validate latitude values
        public static double ValidateLat(string latitude)
        {
            double lat1;
            if (Double.TryParse(latitude, out lat1))
            {
                if (lat1 < -90 || lat1 > 90)
                {
                    throw new ArgumentOutOfRangeException("Latitude must be between -90 and 90 degrees inclusive.");
                }
            }
            return lat1;
        }

        //validate longitude values
        public static double ValidateLon(string longitude)
        {
            double lon1;
            if (Double.TryParse(longitude, out lon1))
            {
                if (lon1 < -180 || lon1 > 180)
                {
                    throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180 degrees inclusive.");
                }
            }
            return lon1;
        }

    }
}
