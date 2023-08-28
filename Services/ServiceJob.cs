namespace webapi.Services;

public class ServiceJob : IServiceJob
{
    private readonly IHelperService _helperService;
    public ServiceJob(IHelperService helperService)
    {
        this._helperService = helperService;
    }
    public string GenString()
    {
        string randomString = _helperService.GetRandomString(10);
        Console.WriteLine($"Job run ${randomString}");
        return randomString;
    }
}