using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelpClassLibrary.Tool
{
    public class RequestTool
    {
        public static string HttpPostJson(string url, object postData, int cou = 0)
        {

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                //设置协议类型前设置协议版本
                request.ProtocolVersion = HttpVersion.Version11;
                //这里设置了协议类型。
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.Expect100Continue = false;

            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }



            request.Method = "POST";
            request.ContentType = "application/json";

            string postString = JsonConvert.SerializeObject(postData);
            //request.ContentLength = postData.Length;

            request.Timeout = cou * 3000 + 1;

            HttpWebResponse response = null;

            try
            {
                var swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postString);
                if (swRequestWriter != null)
                    swRequestWriter.Close();

                response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string objectString = reader.ReadToEnd();
                    return objectString;
                }
            }
            catch (Exception ex)
            {

                cou++;
                if (cou == 10)
                {
                    throw new Exception($"400|Connection error,Please refresh and try again." + ex.Message);
                }
                return HttpPostJson(url, postData, cou);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }


        public static T HttpPostJson<T>(string url, object postData, Dictionary<string, string> headers)
        {

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                //设置协议类型前设置协议版本
                request.ProtocolVersion = HttpVersion.Version11;
                //这里设置了协议类型。
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.Expect100Continue = false;

            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/json";
            if (headers != null)
            {

                foreach (var item in headers)
                {

                    request.Headers.Add(item.Key, item.Value);
                }
            }

            string postString = JsonConvert.SerializeObject(postData);

            //request.ContentLength = postData.Length;
            request.Timeout = 720000;

            HttpWebResponse response = null;

            try
            {
                var swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postString);
                if (swRequestWriter != null)
                    swRequestWriter.Close();

                try
                {
                    //request.UseDefaultCredentials = true;
                    response = (HttpWebResponse)request.GetResponse();
                    using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {

                        string objectString = reader.ReadToEnd();
                        T returnResultList = JsonConvert.DeserializeObject<T>(objectString);
                        try
                        {
                            return returnResultList;
                        }
                        catch
                        {
                            throw new Exception("数据返回有误");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }


        public static T HttpGetJson<T>(string url, Dictionary<string, string> headers)
        {

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                //设置协议类型前设置协议版本
                request.ProtocolVersion = HttpVersion.Version11;
                //这里设置了协议类型。
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.Expect100Continue = false;

            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            try
            {

                request.Method = "Get";
                request.ContentType = "application/json";
                if (headers != null)
                {

                    foreach (var item in headers)
                    {

                        request.Headers.Add(item.Key, item.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception($"400|Connection error,Please refresh and try again.");
            }





            //request.ContentLength = postData.Length;
            request.Timeout = 720000;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            try
            {
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                // return retString;


                try
                {
                    T returnResultList = JsonConvert.DeserializeObject<T>(retString);
                    return returnResultList;
                }
                catch
                {
                    throw new Exception("数据返回有误");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"400|Connection error,Please refresh and try again.");
            }
            finally
            {
                response.Close();
            }
        }
       
        

        public static T HttpGetJsond<T>(string url, Dictionary<string, string> headers, int sum = 0)
        {
            // url = "https://apitest.nissanchina.cn:8052/MstPermissionService/api/permission/datapermission?sysId=SYS078&menuId=15375286966273024";
            // var request = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //Console.WriteLine(sum);
            //Console.WriteLine($"运行：{DateTime.Now} 定时任务44", "定时任务44");
            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith(" ", StringComparison.OrdinalIgnoreCase))
            {


                request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version11;
                request.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidationCallback);
                // 这里设置了协议类型。
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2; 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                request.KeepAlive = true;
                ServicePointManager.DefaultConnectionLimit = 100;
                ServicePointManager.UseNagleAlgorithm = true;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.EnableDnsRoundRobin = true;
                ServicePointManager.CheckCertificateRevocationList = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = ServicePointManager.DefaultPersistentConnectionLimit;
                //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
                //{
                //    return true;
                //};
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Proxy = null;
            //System.GC.Collect();
            request.Method = "Get";
            request.ContentType = "application/json";
            if (headers != null)
            {

                foreach (var item in headers)
                {

                    request.Headers.Add(item.Key, item.Value);
                }
            }


            System.Net.ServicePointManager.DefaultConnectionLimit = 500;

            //Console.WriteLine($"运行：{DateTime.Now} 定时任务55", "定时任务55");

            //request.ContentLength = postData.Length;
            request.Timeout = sum * 3000 + 1;


            //request.KeepAlive = false;
            HttpWebResponse response = new HttpWebResponse();
            try
            {
                //Console.WriteLine($"运行：{DateTime.Now} 定时任务66", "定时任务66");
                response = (HttpWebResponse)request.GetResponse();
                Console.WriteLine($"运行：{DateTime.Now} 定时任务77", "定时任务66");
            }
            catch (Exception ex)
            {

                sum++;
                if (sum == 10)
                {
                    throw new Exception($"400|Connection error,Please refresh and try again." + ex.Message);
                }
                return HttpGetJsond<T>(url, headers, sum);
            }

            try
            {
                //Console.WriteLine(DateTime.Now);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                request.Abort();
                myStreamReader.Dispose();
                myResponseStream.Dispose();

                T returnResultList = JsonConvert.DeserializeObject<T>(retString);
                return returnResultList;
            }
            catch (Exception ex)
            {
                sum++;
                if (sum == 10)
                {
                    throw new Exception($"400|Connection error,Please refresh and try again." + ex.Message);
                }
                return HttpGetJsond<T>(url, headers, sum);
            }

        }


        public static T HttpDeleteJson<T>(string url, Dictionary<string, string> headers)
        {

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                //设置协议类型前设置协议版本
                request.ProtocolVersion = HttpVersion.Version11;
                //这里设置了协议类型。
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.Expect100Continue = false;

            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            try
            {

                request.Method = "Delete";
                request.ContentType = "application/json";
                if (headers != null)
                {

                    foreach (var item in headers)
                    {

                        request.Headers.Add(item.Key, item.Value);
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception($"400|Connection error,Please refresh and try again.");
            }





            //request.ContentLength = postData.Length;
            request.Timeout = 720000;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            try
            {
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                // return retString;


                try
                {
                    T returnResultList = JsonConvert.DeserializeObject<T>(retString);
                    return returnResultList;
                }
                catch
                {
                    throw new Exception("数据返回有误");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"400|Connection error,Please refresh and try again.");
            }
            finally
            {
                response.Close();
            }
        }

        public static async Task<T> HttpGetJsondAsync<T>(string url, Dictionary<string, string> headers, int sum = 0)
        {
            return await new TaskFactory().StartNew(() =>
            {

                HttpWebRequest request = null;
                //如果是发送HTTPS请求 
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    request = (HttpWebRequest)HttpWebRequest.Create(url);

                    ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                    //设置协议类型前设置协议版本
                    request.ProtocolVersion = HttpVersion.Version11;
                    //这里设置了协议类型。
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    ServicePointManager.CheckCertificateRevocationList = true;
                    ServicePointManager.DefaultConnectionLimit = 1000;
                    ServicePointManager.Expect100Continue = false;

                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }

                System.GC.Collect();
                request.Method = "Get";
                request.ContentType = "application/json";
                if (headers != null)
                {

                    foreach (var item in headers)
                    {

                        request.Headers.Add(item.Key, item.Value);
                    }
                }


                System.Net.ServicePointManager.DefaultConnectionLimit = 500;



                //request.ContentLength = postData.Length;
                request.Timeout = 720000;

                try
                {
                    request.KeepAlive = false;
                    HttpWebResponse response = new HttpWebResponse();
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (Exception ex)
                    {
                        sum++;
                        if (sum == 10)
                        {
                            throw new Exception($"400|Connection error,Please refresh and try again." + ex.Message);
                        }
                        return HttpGetJsond<T>(url, headers, sum);
                    }



                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    string retString = myStreamReader.ReadToEnd();
                    request.Abort();
                    myStreamReader.Dispose();
                    myResponseStream.Dispose();
                    // return retString;

                    try
                    {
                        T returnResultList = JsonConvert.DeserializeObject<T>(retString);
                        return returnResultList;
                    }
                    catch
                    {
                        throw new Exception("数据返回有误");
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception($"400|Connection error,Please refresh and try again.");


                }
                finally
                {

                }


            });






        }
        public static void Get(string url, Action<HttpWebResponse> calc)
        {
            //StrUtil.Assert(string.IsNullOrEmpty(url), "请求地址不能为空");

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                //设置协议类型前设置协议版本
                request.ProtocolVersion = HttpVersion.Version11;
                //这里设置了协议类型。
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 1000;
                ServicePointManager.Expect100Continue = false;

            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "Get";
            HttpWebResponse myrp = (HttpWebResponse)request.GetResponse();
            calc?.Invoke(myrp);
            myrp.Close();
            request.Abort();
        }




        public static string HttpPost(string url, object value, Dictionary<string, string> headers)
        {
            try
            {


                HttpWebRequest request = null;
                //如果是发送HTTPS请求 
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    request = (HttpWebRequest)HttpWebRequest.Create(url);

                    ServicePointManager.ServerCertificateValidationCallback = (message, certificate2, arg3, arg4) => true;
                    //设置协议类型前设置协议版本
                    request.ProtocolVersion = HttpVersion.Version11;
                    //这里设置了协议类型。
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    ServicePointManager.CheckCertificateRevocationList = true;
                    ServicePointManager.DefaultConnectionLimit = 1000;
                    ServicePointManager.Expect100Continue = false;

                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = "application/json";
                if (headers != null)
                {
                    foreach (var item in headers)
                    {

                        request.Headers.Add(item.Key, item.Value);
                    }
                }

                string jsonString = JsonConvert.SerializeObject(value);
                byte[] bytes = Encoding.UTF8.GetBytes(jsonString);

                using (Stream stream = request.GetRequestStream())
                {
                    stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new System.IO.StreamReader(myResponseStream, Encoding.UTF8);
                var retString = myStreamReader.ReadToEnd();

                try
                {
                    return retString;
                }
                catch
                {
                    throw new Exception("数据返回有误");
                }
                finally
                {

                    myStreamReader.Close();
                    myResponseStream.Close();
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"400|Connection error,Please refresh and try again.");
            }
        }
    }
}
