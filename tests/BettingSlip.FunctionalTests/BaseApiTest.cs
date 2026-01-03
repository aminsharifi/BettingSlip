namespace BettingSlip.FunctionalTests;

public abstract class BaseApiTest
{
    protected readonly HttpClient Client;

    protected BaseApiTest()
    {
        Client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5002")
        };
    }
}

