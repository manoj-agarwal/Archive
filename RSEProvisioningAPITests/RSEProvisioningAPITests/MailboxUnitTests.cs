using System;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using Rackspace.HttpClient.Extensions;
using RseProvisioningApiTests.Models;

namespace RseProvisioningApiTests
{
    [TestFixture]
    public class MailboxUnitTests
    {
        private HttpClient HttpClient { get; set; }
        private const string ProvisioningApiBaseUri = "http://64.49.226.122";
        private const string ContentType = "application/json";

        private Mailbox createdMailbox;

        [SetUp]
        public void Setup()
        {
            var responseHandler = new HttpClientHandler();
            HttpClient = new HttpClient(responseHandler) { BaseAddress = new Uri(ProvisioningApiBaseUri) };
            HttpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType));

            createdMailbox = new Mailbox
            {
                Email = "user@example.com", 
                Mailhost = "store10a.example.com"
            };

            HttpClient.SendAsObjectAsync(HttpMethod.Post, "/v1/mailboxes", createdMailbox).Wait();
        }

        [Test]
        public void CanReadMailbox()
        {
            var mailbox = HttpClient.GetObjectAsync<Mailbox>("/v1/mailboxes/user@example.com").Result;
            mailbox.Email = "user@example.com";

            Assert.AreEqual(createdMailbox, mailbox);
        }

        [Test]
        public void CanUpdateMailbox()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CanDeleteMailbox()
        {
            throw new NotImplementedException();
        }
    }
}
