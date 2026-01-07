namespace BettingSlip.FunctionalTests;

public record CreateSlipResponse(Guid Id);

public class AddSelectionE2ETests : BaseApiTest
{
    [Fact]
    public async Task AddSelection_EndToEnd_Should_Work()
    {
        var createSlipResponse = await Client.PostAsJsonAsync(
            "/api/betting-slips",
            new { StakeAmount = 100 }
        );

        createSlipResponse.StatusCode.Should().Be(HttpStatusCode.Created);


        var createdresponse = await createSlipResponse.Content
            .ReadFromJsonAsync<CreateSlipResponse>();

        var slipId = createdresponse!.Id;

        slipId.Should().NotBeEmpty();

        //Add selection
        var addSelectionRequest = new
        {
            slipId,
            eventName = "Real Madrid vs Barcelona",
            market = "Match Winner",
            odd = 1.85
        };

        var response = await Client.PostAsJsonAsync(
            $"/api/betting-slips/{slipId}/selections",
            addSelectionRequest
        );

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
