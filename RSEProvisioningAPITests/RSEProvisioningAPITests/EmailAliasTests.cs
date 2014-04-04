using System;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using Rackspace.HttpClient.Extensions;
using RseProvisioningApiTests.Models;

namespace RseProvisioningApiTests
{
    [TestFixture]
    public class EmailAliasTests
    {
        private HttpClient HttpClient { get; set; }
        private const string ProvisioningApiBaseUri = "http://64.49.226.122";
        private const string ContentType = "application/json";

        private EmailAlias addedAlias;

        [SetUp]
        public void Setup()
        {
            var responseHandler = new HttpClientHandler();
            HttpClient = new HttpClient(responseHandler) { BaseAddress = new Uri(ProvisioningApiBaseUri) };
            HttpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType));

            addedAlias = new EmailAlias
            {
                Alias = "everybody@example1.com",
                Addresses = new[] { "user1@example1.com", "user2@example1.com", "user1@example2.com", "user2@example2.com" }
            };

            HttpClient.SendAsObjectAsync(HttpMethod.Post, "/v1/aliases", addedAlias).Wait();
        }

        [Test]
        public void CanReadAlias()
        {
            var returnedAlias = HttpClient.GetObjectAsync<EmailAlias>("/v1/aliases/everybody@example1.com").Result;
            returnedAlias.Alias = "everybody@example1.com";
            Assert.AreEqual(addedAlias, returnedAlias);
        }

        [Test]
        public void CanPatchAlias()
        {
            var updatedAddresses = new[] {"user1@example1.com", "user2@example2.com"};
            var updatedAlias = new EmailAlias {Addresses = updatedAddresses};

            HttpClient.PatchAsync("/v1/aliases/everybody@example1.com", updatedAlias).Wait();

            var returnedAlias = HttpClient.GetObjectAsync<EmailAlias>("/v1/aliases/everybody@example1.com").Result;
            Assert.AreEqual(updatedAlias, returnedAlias);
        }

        [TearDown]
        public void Dispose()
        {
            HttpClient.DeleteObjectAsync("v1/aliases/everybody@example1.com").Wait();
        }
    }
}
