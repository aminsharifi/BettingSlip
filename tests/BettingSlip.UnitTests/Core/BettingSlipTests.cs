namespace BettingSlip.UnitTests.Core;
using BettingSlip.Core.SlipAggregate;

public class BettingSlipTests
{
    [Fact]
    public void CreateSlip_Should_Start_In_Draft_Status()
    {
        // Arrange
        var slip = new Slip(100);

        // Assert
        slip.Status.ShouldBe(SlipStatus.Draft);
        slip.Selections.ShouldBeEmpty();
    }

    [Fact]
    public void AddSelection_Should_Add_Selection_To_Slip()
    {
        // Arrange
        var slip = new Slip(50);

        // Act
        slip.AddSelection(slip.Id, "Real vs Barca", "Match Winner", 1.85m);

        // Assert
        slip.Selections.Count().ShouldBe(1);
        slip.Selections.First().Odd.ShouldBe(1.85m);
    }

    [Fact]
    public void TotalOdds_Should_Multiply_All_Selections()
    {
        var slip = new Slip(10);

        slip.AddSelection(slip.Id, "Man Utd vs Liverpool", "Match Winner - Man Utd", 2.10m);
        slip.AddSelection(slip.Id, "Real Madrid vs Barcelona", "Over 2.5 Goals", 1.85m);

        slip.TotalOdds.ShouldBe(3.885m);
    }

}
