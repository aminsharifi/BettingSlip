namespace BettingSlip.FunctionalTests;

public abstract class BaseApiTest
{
    protected readonly HttpClient Client;

    protected BaseApiTest()
    {
        Client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5001")
        };
    }
}

