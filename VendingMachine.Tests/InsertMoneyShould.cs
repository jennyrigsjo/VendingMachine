namespace VendingMachine.Tests
{
    public class InsertMoneyShould
    {
        [Fact]
        public void AddValidDenominationToCredit()
        {
            VendingMachine sut = new();

            Random rnd = new();
            int index = rnd.Next(sut.Denominations.Length);
            int denomination = sut.Denominations[index];

            int expectation = sut.Credit + denomination;

            sut.InsertMoney(denomination);

            Assert.Equal(expectation, sut.Credit);
        }


        [Fact]
        public void ThrowArgumentExceptionOnInvalidDenomination()
        {
            VendingMachine sut = new();

            ArgumentException e = Assert.Throws<ArgumentException>(() => sut.InsertMoney(25));

            Assert.Equal($"Please insert a valid denomination: {string.Join(", ", sut.Denominations)}", e.Message);
        }
    }
}
