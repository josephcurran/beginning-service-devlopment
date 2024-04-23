
namespace HelpDeskSupportApi.Services;

public class StandardBusinessClock : IProvideTheBusinessClock
{
    public Task<bool> AreWeCurrentOpenAsync()
    {
        return Task.FromResult(true);
    }
}
