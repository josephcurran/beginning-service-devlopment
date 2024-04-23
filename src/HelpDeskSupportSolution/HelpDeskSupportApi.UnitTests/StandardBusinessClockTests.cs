
using HelpDeskSupportApi.Services;

namespace HelpDeskSupportApi.UnitTests;
public class StandardBusinessClockTests
{
    // we are open monday-friday from 9:00 AM ET until 5:00 PM ET

    [Fact]
    public async Task Open()
    {
        var sut = new StandardBusinessClock();

        Assert.True(await sut.AreWeCurrentOpenAsync());
    }

    [Fact]
    public async Task Closed()
    {
        var sut = new StandardBusinessClock();

        Assert.False(await sut.AreWeCurrentOpenAsync());
    }

    // We are closed on monday and friday outside of those hours

    // we are closed on the weekends.

    // on the backlog, we are closed on certain holidays...

}
