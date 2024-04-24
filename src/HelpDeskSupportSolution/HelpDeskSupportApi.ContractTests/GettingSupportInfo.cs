
using Alba;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HelpDeskSupportApi.ContractTests;
public class GettingSupportInfo
{
    [Fact]
    public async Task GetsAnOk()
    {
        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/");
            api.StatusCodeShouldBeOk();
        });
    }
}

