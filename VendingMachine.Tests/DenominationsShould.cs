namespace VendingMachine.Tests
{
    public class DenominationsShould
    {
        [Fact]
        public void ContainValues()
        {
            VendingMachine sut = new();

            int[] denominations = sut.Denominations;

            Assert.NotEmpty(denominations);
        }
    }
}
