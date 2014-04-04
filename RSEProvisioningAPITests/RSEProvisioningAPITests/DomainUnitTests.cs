using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using Rackspace.HttpClient.Extensions;
using RseProvisioningApiTests.Models;

namespace RseProvisioningApiTests
{
    [TestFixture]
    public class DomainUnitTests
    {
        private HttpClient HttpClient { get; set; }
        private const string ProvisioningApiBaseUri = "http://64.49.226.122";
        private const string ContentType = "application/json";

        private const string DomainName = "example2.com";

        Domain domain;
        [SetUp]
        public void SetUp()
        {
            var responseHandler = new HttpClientHandler();
            HttpClient = new HttpClient(responseHandler) { BaseAddress = new Uri(ProvisioningApiBaseUri) };
            HttpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType));

            domain = new Domain
            {
                DomainName = DomainName,
                Disclaimer = new Disclaimer
                {
                    Plain = "plain displaimer for " + DomainName,
                    Html = "html displaimer for " + DomainName,
                    AlterSigned = true
                },
                ExternalRouting = new ExternalRouting
                {
                    Host = "google.com",
                    Port = "9001",
                    Verified = false
                },
                CatchAll = "catchall@mailtrust.com"
            };

            HttpClient.SendAsObjectAsync(HttpMethod.Post, "/v1/domains", domain).Wait();
        }

        [Test]
        public void CanReadDomain()
        {
            var returnedDomain = HttpClient.GetObjectAsync<Domain>("/v1/domains/" + DomainName).Result;
            Assert.AreEqual(domain, returnedDomain);

        }

        [Test]
        public void CanUpdateDomain()
        {
            var updatedDomain = new Domain
            {
                Disclaimer = new Disclaimer
                {
                    Plain = "updated plain displaimer for " + DomainName,
                    Html = "updated html displaimer for " + DomainName,
                    AlterSigned = false
                },
                ExternalRouting = new ExternalRouting
                {
                    Host = "updatedgoogle.com",
                    Port = "9005",
                    Verified = true
                },
                CatchAll = "updatedcatchall@mailtrust.com"
            };

            HttpClient.PatchAsync("/v1/domains/" + DomainName, updatedDomain).Wait();

            updatedDomain.DomainName = DomainName;
            var returnedUpdatedDomain = HttpClient.GetObjectAsync<Domain>("/v1/domains/" + DomainName).Result;
            Assert.AreEqual(updatedDomain, returnedUpdatedDomain);
        }



        [TearDown]
        public void Dispose()
        {
            HttpClient.DeleteObjectAsync("/v1/domains/" + DomainName).Wait();

            Assert.Throws<Exception>(
                () =>
                {
                    var result = HttpClient.GetObjectAsync<Domain>("/v1/domains/" + DomainName).Result;
                });
        }
    }
}
