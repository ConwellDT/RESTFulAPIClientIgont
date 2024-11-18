using System;
using System.IO;
using System.Net;
using System.Text;

namespace RESTfulAPIClient_Igont
{
    public class APIClient_Igont
    {
        public int Timeout { get; set; } = -1;

        public APIClient_Igont(int timeout = -1)
        {
            Timeout = timeout;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
        }

        public HttpStatusCode GETPOST(string uri, string method, out string response)
        {
            response = string.Empty;
            HttpStatusCode status = HttpStatusCode.BadRequest;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = method;// "GET" / "POST";
                request.Timeout = Timeout;// - 1;// 30 * 1000; // 30초


                using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
                {
                    status = resp.StatusCode;
                    if (status.Equals(HttpStatusCode.OK))
                    {
                        Stream respStream = resp.GetResponseStream();
                        using (StreamReader sr = new StreamReader(respStream))
                        {
                            response = sr.ReadToEnd();
                        }
                    }

                }
            }
            catch (Exception ex)
            { }
            return status;
        }
    }
}
