using HalcyonApparelsMVC.DTO;
using HalcyonApparelsMVC.Interfaces;
using HalcyonApparelsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack.Web;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
namespace HalcyonApparelsMVC.Services
{
    public class SalesforceData : ISalesforceData
    {
        private readonly IConfiguration _config;

        public SalesforceData(IConfiguration config)
        {
            
            _config = config;
        }
        public List<CustomerDetailsMVC> SalesforceCustomerDetails(string access_token)
        {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var url = (_config["SalesforceURL:GetDetails"]);
            List<CustomerDetailsMVC> customerdata = new List<CustomerDetailsMVC>();
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + access_token;
            httpRequest.ContentType = "application/json";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var response = streamReader.ReadToEnd();
                customerdata = JsonConvert.DeserializeObject<List<CustomerDetailsMVC>>(response);
                //foreach (CustomerDetailsMVC cdata in customerdata)
                //{
                //    cdata.OrderDetails = SalesforceOrderDetails(access_token, cdata.ContactId);
                //}
            }
            return customerdata;
        }


     

        public bool Post(List<CustomerDetailsMVC> custdet)
        {

            HttpClientHandler clienthandler = new HttpClientHandler();
            clienthandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslpolicyerrors) => { return true; };

            HttpClient client = new HttpClient(clienthandler);
            client.BaseAddress = new Uri(_config["WebConfig:UserApi"]);

            var postTask = client.PostAsJsonAsync<List<CustomerDetailsMVC>>("api/SalesforceData/Post/", custdet);

            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}