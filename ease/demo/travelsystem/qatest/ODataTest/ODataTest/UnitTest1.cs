using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;

namespace ODataTest
{
    [TestClass]
    public class SubmitterSetTest
    {
        String basic_url = System.Environment.GetEnvironmentVariable("ODATA_URL");
        String ousername = System.Environment.GetEnvironmentVariable("ODATA_USER");
        String opassword = System.Environment.GetEnvironmentVariable("ODATA_PASSWORD");
        HttpBasicAuthenticator AUTH;
        string STATUS_POST = "Created";
        string STATUS_GET = "OK";
        string STATUS_PUT = "NoContent";
        string STATUS_DELETE = "NoContent";

        [TestMethod]
        [TestCategory("ApiTests_Get")]
        public void SubmitterSet_Get()
        {
            AUTH = new HttpBasicAuthenticator(ousername,opassword);
            var client = new RestClient(basic_url);
            client.Authenticator = AUTH;
            var request = new RestRequest("", Method.GET);
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            var status = response.StatusCode;
            Assert.AreEqual(STATUS_GET, status.ToString());
        }

        [TestMethod]
        [TestCategory("ApiTests_Post")]
        public void SubmitterSet_Post()
        {
            AUTH = new HttpBasicAuthenticator(ousername,opassword);
            var client = new RestClient(basic_url);
            client.Authenticator = AUTH;
            var request = new RestRequest("", Method.POST);
            request.RequestFormat = DataFormat.Json;
            Int32 id = 1000;
            SubmitterSet submitterSet = new SubmitterSet();
            submitterSet.ID = id;
            submitterSet.Name = "SubmitterSetTest" + "({id})";
            submitterSet.Org = "submitterSet for test";
            submitterSet.Company = "submitterSet for test";
            submitterSet.Address = "submitterSet for test";
            submitterSet.Phone = "submitterSet for test";
            submitterSet.Mobile = "submitterSet for test";
            submitterSet.Email = "submitterSet for test";
            request.AddJsonBody(submitterSet);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            var status = response.StatusCode;
            Console.WriteLine(content);
            Assert.AreEqual(STATUS_POST, status.ToString());
            cleanData(id);
        }

        [TestMethod]
        [TestCategory("ApiTests_Put")]
        public void SubmitterSet_Put()
        {
            AUTH = new HttpBasicAuthenticator(ousername,opassword);
            prepareData();
            var client = new RestClient(basic_url + "({id})");
            client.Authenticator = AUTH;
            Int32 id = 1000;
            var request = new RestRequest("", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            SubmitterSet submitterSet = new SubmitterSet();
            submitterSet.ID = id;
            submitterSet.Name = "SubmitterSetTest" + "({id})";
            submitterSet.Org = "submitterSet for test";
            submitterSet.Company = "submitterSet for test";
            submitterSet.Address = "submitterSet for test";
            submitterSet.Phone = "submitterSet for test";
            submitterSet.Mobile = "submitterSet for test";
            submitterSet.Email = "submitterSet for test";
            request.AddJsonBody(submitterSet);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            var status = response.StatusCode;
            Console.WriteLine(content);
            Assert.AreEqual(STATUS_PUT, status.ToString());
            cleanData(id);
        }

        [TestMethod]
        [TestCategory("ApiTests_Delete")]
        public void SubmitterSet_Delete()
        {
            AUTH = new HttpBasicAuthenticator(ousername,opassword);
            prepareData();
            var client = new RestClient(basic_url + "({id})");
            client.Authenticator = AUTH;
            var request = new RestRequest("", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", "1000");
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            var status = response.StatusCode;
            Console.WriteLine(content);
            Assert.AreEqual(STATUS_DELETE, status.ToString());
        }

        public void cleanData(Int32 delete_id) {
            AUTH = new HttpBasicAuthenticator(ousername,opassword);
            var client = new RestClient(basic_url + "({id})");
            client.Authenticator = AUTH;
            var request = new RestRequest("", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", delete_id.ToString());
            // execute the request
            IRestResponse response = client.Execute(request);
            var status = response.StatusCode;
            if (status.ToString() != STATUS_DELETE) {
                Console.WriteLine("Clean data failed, id: "+ delete_id);
            }
        }

        public void prepareData()
        {
            AUTH = new HttpBasicAuthenticator(ousername,opassword);
            var client = new RestClient(basic_url);
            client.Authenticator = AUTH;
            var request = new RestRequest("", Method.POST);
            request.RequestFormat = DataFormat.Json;
            Int32 id = 1000;
            SubmitterSet submitterSet = new SubmitterSet();
            submitterSet.ID = id;
            submitterSet.Name = "SubmitterSetTest" + "({id})";
            submitterSet.Org = "submitterSet for test";
            submitterSet.Company = "submitterSet for test";
            submitterSet.Address = "submitterSet for test";
            submitterSet.Phone = "submitterSet for test";
            submitterSet.Mobile = "submitterSet for test";
            submitterSet.Email = "submitterSet for test";
            request.AddJsonBody(submitterSet);
            IRestResponse response = client.Execute(request);
            var status = response.StatusCode;
            if (status.ToString() != STATUS_POST)
            {
                Console.WriteLine("Prepare data failed");
            }
        }

        public class SubmitterSet
        {
            public void set(Int32 id, string name, string org, string company, string address, string phone, string mobile, string email)
            {
                this.ID = id;
                this.Name = name;
                this.Org = org;
                this.Company = company;
                this.Address = address;
                this.Phone = phone;
                this.Mobile = mobile;
                this.Email = email;
            }
            public Int32 ID { get; set; }
            public string Name { get; set; }
            public string Org { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
        }
        
    }
}
