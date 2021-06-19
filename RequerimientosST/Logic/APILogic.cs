using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RequerimientosST.Logic
{
    public class APILogic
    {

        public static string ObtenerRequerimientoAPI(string url)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.Method = "GET";
            request2.ContentType = "application/json";
            request2.Accept = "application/json";

            try
            {
                using (WebResponse response = request2.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) ;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();                        
                            return responseBody;
                        }
                    }
                }



            }

            catch (Exception ex)
            {
                return null;
            }
            //return null
        }

    }
}