using System;
using System.Net.Http;
using System.Net.Http.Headers;
using NUnit.Framework;
using Rackspace.HttpClient.Extensions;
using RseProvisioningApiTests.Models;

namespace RseProvisioningApiTests
{
    [TestFixture]
    public class DomainAliasUnitTests
    {
        private HttpClient HttpClient { get; set; }
        private const string ProvisioningApiBaseUri = "http://64.49.226.122";
        private const string ContentType = "application/json";

        private const string DomainName = "example2.com";
        private readonly string[] aliasValues = { "alias1-example2.com", "alias2-example2.com" };

        [SetUp]
        public void Setup()
        {
            var responseHandler = new HttpClientHandler();
            HttpClient = new HttpClient(responseHandler) { BaseAddress = new Uri(ProvisioningApiBaseUri) };
            HttpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType));

            var domain = new Domain
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
        public void VerifyNoAliasesToBeginWith()
        {
            var returnedDomainAliases = HttpClient.GetObjectAsync<DomainAliases>("/v1/domains/" + DomainName + "/aliases").Result;
            Assert.IsEmpty(returnedDomainAliases.Aliases);
        }

        [Test]
        public void CanAddAlias()
        {
            CreateAlias();
            var returnedDomainAliases = HttpClient.GetObjectAsync<DomainAliases>("/v1/domains/" + DomainName + "/aliases").Result;
            Assert.IsTrue(Helper.ArraysEqual(aliasValues, returnedDomainAliases.Aliases));
        }

        private void CreateAlias()
        {
            var aliases = new DomainAliases {Aliases = aliasValues};

            HttpClient.SendAsObjectAsync(HttpMethod.Put, "/v1/domains/" + DomainName + "/aliases", aliases).Wait();            
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
