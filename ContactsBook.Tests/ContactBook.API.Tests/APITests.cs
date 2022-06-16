using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContactBook.API.Tests
{
    public class APITests
    {
        private const string url = "https://contactbook.nakov.repl.co/api/contacts";
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            client = new RestClient();
        }

        [Test]
        public void Test1_ContactList_CheckFirstContact()
        {
            this.request = new RestRequest(url);

            var response = this.client.Execute(request, Method.Get);
            var contact = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contact.Count, Is.GreaterThan(0));
            Assert.That(contact[0].firstName, Is.EqualTo("Steve"));
            Assert.That(contact[0].lastName, Is.EqualTo("Jobs"));
        }

        [Test]
        public void Test2_Search_ExistContact()
        {
            this.request = new RestRequest(url + "/search/albert");

            var response = this.client.Execute(request, Method.Get);
            var contact = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contact.Count, Is.GreaterThan(0));
            Assert.That(contact[0].firstName, Is.EqualTo("Albert"));
            Assert.That(contact[0].lastName, Is.EqualTo("Einstein"));
        }

        [Test]
        public void Test3_Search_NonExistContact()
        {
            this.request = new RestRequest(url + "/search/noname12");

            var response = this.client.Execute(request, Method.Get);
            var contact = JsonSerializer.Deserialize<List<Contacts>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contact.Count, Is.LessThanOrEqualTo(0));
            Assert.That(response.Content, Is.EqualTo("No contacts found."));
        }

        [Test]
        public void Test4_Create_ContactWithInvalidDate()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Alina",
                email = "ali1@abc.bg",
                phone = "09999999999"
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(request, Method.Post);
            var errM = "{\"errMsg\":\"Last name cannot be empty!\"}";
           
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo(errM));
        }


        [Test]
        public void Test5_Create_ContactWithValidDate()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Alina",
                lastName = "Belina",
                email = DateTime.Now.Ticks + "aliB@gigi.com",
                phone = "0" + DateTime.Now.Ticks
            };
            request.AddJsonBody(body);
            var response = this.client.Execute(request, Method.Post);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var allContact = this.client.Execute(request, Method.Get);
            var contact = JsonSerializer.Deserialize<List<Contacts>>(allContact.Content);

            Assert.That(contact.Count, Is.GreaterThan(0));

            var lastContact = contact.Last();

            Assert.That(lastContact.firstName, Is.EqualTo("Alina"));
            Assert.That(lastContact.lastName, Is.EqualTo("Belina"));
        }

        [Test]
        public void Test6_Delete_ContactWithValidDate()
        {
            this.request = new RestRequest(url + "/{id}");
            request.AddUrlSegment("id", "<<PUT HERE ID>>");


            var response = this.client.Execute(request, Method.Delete);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.EqualTo("{\"msg\":\"Contact deleted: <<PUT HERE ID>\"}"));
        }

        [Test]
        public void Test7_ViewContactWithInvalidId()
        {
            this.request = new RestRequest(url + "/{id}");
            request.AddUrlSegment("id", "-10");

            var response = this.client.Execute(request, Method.Get);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"Cannot find contact by id: -10\"}"));
                                                   
        }
    }
  

    public class Contacts
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("firstName")]
        public string firstName { get; set; }
        [JsonPropertyName("lastName")]
        public string lastName { get; set; }
        [JsonPropertyName("email")]
        public string email { get; set; }
        [JsonPropertyName("phone")]
        public string phoneNumber { get; set; }
        [JsonPropertyName("dateCreated")]
        public string dateCreated { get; set; }
        [JsonPropertyName("comments")] 
        public string comments { get; set; }
    }
}