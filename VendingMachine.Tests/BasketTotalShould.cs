namespace VendingMachine.Tests
{
    public class BasketTotalShould
    {
        [Fact]
        public void ReturnZeroOnInit()
        {
            VendingMachine sut = new();

            int total = sut.BasketTotal();

            Assert.Equal(0, total);
        }


        [Fact]
        public void ReturnTotalCostOfItems()
        {
            VendingMachine sut = new();

            sut.InsertMoney(100); // Add sufficient credit

            sut.AddToBasket(1); // Pepsi, 25 SEK
            sut.AddToBasket(1); // Pepsi, 25 SEK
            sut.AddToBasket(2); // Fanta, 20 SEK

            int total = sut.BasketTotal();

            Assert.Equal(70, total);
        }


        [Fact]
        public void ReturnZeroAfterPurchase()
        {
            VendingMachine sut = new();

            sut.InsertMoney(100); // Add sufficient credit

            sut.AddToBasket(1); // Pepsi, 25 SEK
            sut.AddToBasket(1); // Pepsi, 25 SEK
            sut.AddToBasket(2); // Fanta, 20 SEK

            sut.Purchase();

            int total = sut.BasketTotal();

            Assert.Equal(0, total);
        }
    }
}
