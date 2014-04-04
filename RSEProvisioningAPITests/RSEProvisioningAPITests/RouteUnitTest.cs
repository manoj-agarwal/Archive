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
    public class RouteUnitTest
    {
        private HttpClient HttpClient { get; set; }
        private const string ProvisioningApiBaseUri = "http://64.49.226.122";
        private const string ContentType = "application/json";

        [SetUp]
        public void SetUp()
        {
            var responseHandler = new HttpClientHandler();
            HttpClient = new HttpClient(responseHandler) { BaseAddress = new Uri(ProvisioningApiBaseUri) };
            HttpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType));
        }

        [Test]
        public void TestDomainRoutes()
        {
            var domain = new Domain
            {
                DomainName = "xyz.com",
                Disclaimer = new Disclaimer
                    {
                        Plain = "plain displaimer for xyz.com",
                        Html = "html displaimer for xyz.com",
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

            var returnedDomain = HttpClient.GetObjectAsync<Domain>("/v1/domains/xyz.com").Result;
            Assert.AreEqual(domain, returnedDomain);

            var updatedDomain = new Domain
            {
                Disclaimer = new Disclaimer
                    {
                        Plain = "updated plain displaimer for xyz.com",
                        Html = "updated html displaimer for xyz.com",
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

            HttpClient.PatchAsync("/v1/domains/xyz.com", updatedDomain).Wait();

            updatedDomain.DomainName = "xyz.com";
            var returnedUpdatedDomain = HttpClient.GetObjectAsync<Domain>("/v1/domains/xyz.com").Result;
            Assert.AreEqual(updatedDomain, returnedUpdatedDomain);

            HttpClient.DeleteObjectAsync("/v1/domains/xyz.com").Wait();

            Assert.Throws<Exception>(
                () =>
                {
                    var result = HttpClient.GetObjectAsync<Domain>("/v1/domains/xyz.com").Result;
                });
        }

        [Test]
        public void TestDomainAliasesRoutes()
        {
            var domain = new Domain
            {
                DomainName = "xyz.com",
                Disclaimer = new Disclaimer
                {
                    Plain = "plain displaimer for xyz.com",
                    Html = "html displaimer for xyz.com",
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

            var returnedDomainAliases = HttpClient.GetObjectAsync<DomainAliases>("/v1/domains/xyz.com/aliases").Result;
            Assert.IsEmpty(returnedDomainAliases.Aliases);



            var aliases = new DomainAliases();

            var aliasValues = new List<string> {"abc.com", "def.com", "pqr.com", "www.com"};
            aliases.Aliases = aliasValues.ToArray();

            HttpClient.SendAsObjectAsync(HttpMethod.Put, "/v1/domains/xyz.com/aliases", aliases).Wait();

            returnedDomainAliases = HttpClient.GetObjectAsync<DomainAliases>("/v1/domains/xyz.com/aliases").Result;
            Assert.IsNotEmpty(returnedDomainAliases.Aliases);

            foreach (var alias in returnedDomainAliases.Aliases)
            {
                aliasValues.Remove(alias);
            }

            Assert.IsEmpty(aliasValues);

            HttpClient.DeleteObjectAsync("/v1/domains/xyz.com").Wait();

            Assert.Throws<Exception>(
                () =>
                {
                    var result = HttpClient.GetObjectAsync<Domain>("/v1/domains/xyz.com").Result;
                });
        }

        [Test]
        public void TestAddressesRoutes()
        {
            
        }

        [Test]
        public void TestMailboxRoutes()
        {
            var mailbox = new Mailbox
            {
                Email = "user@example.com", 
                Mailhost = "store10a.example.com"
            };

            HttpClient.SendAsObjectAsync(HttpMethod.Post, "/v1/mailboxes", mailbox).Wait();
        }
    }
}
