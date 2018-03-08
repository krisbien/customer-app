using System;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CustomerWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace CustomerWebApi.Integration.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void InitializeCustomerContext(IServiceCollection services)
        {
            services.AddDbContext<CustomerContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));
        }
    }

    public class CustomerWebApiIntegrationTests
    {
        [Fact]
        public void BasicAddListTest()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup(typeof(TestStartup));

            var server = new TestServer(builder);
            var client = server.CreateClient();
            client.BaseAddress = new Uri("http://localhost");

            var input = new
            {
                name = "john",
                surname = "wright",
                telephone = "111-11-22-33",
                address = "baker street"
            };

            var json = JsonConvert.SerializeObject(input);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var r = client.PostAsync("/api/customer/", httpContent).Result;
            var loc = r.Headers.GetValues("Location");

            Assert.True(loc.ToList().Count == 1);

            var expectedLocation = "http://localhost/api/Customer/1";
            Assert.Equal(expectedLocation, loc.First());
            var createdId = int.Parse(expectedLocation.Split("/").Last());

            var expectedObject = new {
                id = createdId,
                input.name,
                input.surname,
                input.telephone,
                input.address
            };

            var response = client.GetAsync("/api/customer/all").Result;
            var actual = response.Content.ReadAsStringAsync().Result;
            var actualResponse = JsonConvert.DeserializeObject(actual) as JArray;

            var obj = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(expectedObject));
            JArray expectedArray = new JArray(obj);
            Assert.True(JToken.DeepEquals(expectedArray, actualResponse));
        }
    }
}
