namespace VendingMachine.Tests
{
    public class CreditShould
    {
        [Fact]
        public void BeZeroOnInit()
        {
            VendingMachine sut = new();

            int credit = sut.Credit;

            Assert.Equal(0, credit);
        }


        [Fact]
        public void BeZeroAfterEndTransaction()
        {
            VendingMachine sut = new();
            sut.InsertMoney(50); // Insert money
            sut.AddToBasket(1); // Select a Pepsi

            _ = sut.Purchase(); // Buy it (and return product)
            _ = sut.EndTransaction(); // End transaction (and return change)

            int credit = sut.Credit;

            Assert.Equal(0, credit);
        }
    }
}