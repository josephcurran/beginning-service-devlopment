
using Alba;

namespace HelpDeskSupportApi.ContractTests;
public class GettingSupportInfo
{
    [Fact]
    public async Task WhenWeAreOpen()
    {

        var expectedResults = new SupportResponseModel("Bob Smith", "555-1212", "bob@company.com");
        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/"); // Do a get request to the api to this ("/") resource.
            api.StatusCodeShouldBeOk();
        });


        var actualResponse = await response.ReadAsJsonAsync<SupportResponseModel>();

        Assert.NotNull(actualResponse);

        Assert.Equal(expectedResults, actualResponse);
    }

    [Fact]
    public async Task WhenWeAreClosed()
    {

        var expectedResults = new SupportResponseModel("Support Pros", "(800) BAD-CODE", "help@support-pros.com");
        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/"); // Do a get request to the api to this ("/") resource.
            api.StatusCodeShouldBeOk();
        });


        var actualResponse = await response.ReadAsJsonAsync<SupportResponseModel>();

        Assert.NotNull(actualResponse);

        Assert.Equal(expectedResults, actualResponse);
    }
}
