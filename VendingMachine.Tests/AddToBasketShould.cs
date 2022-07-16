namespace VendingMachine.Tests
{
    public class AddToBasketShould
    {
        [Fact]
        public void ThrowVendingMachineExceptionIfProductDoesNotExist()
        {
            VendingMachine sut = new();

            int productID = 100;

            VendingMachineException e = Assert.Throws<VendingMachineException>(() => sut.AddToBasket(productID));

            Assert.Equal($"Product with id '{productID}' does not exist.", e.Message);
        }


        [Fact]
        public void ThrowVendingMachineExceptionIfTotalExceedsCredit()
        {
            VendingMachine sut = new(); // Credit chould be zero on init

            VendingMachineException e = Assert.Throws<VendingMachineException>(() => sut.AddToBasket(1)); //Add Pepsi, 25 SEK

            Assert.Equal("Please insert more money to select items.", e.Message);
        }


        [Fact]
        public void AddProductToBasketWhenThereIsEnoughCredit()
        {
            VendingMachine sut = new();

            sut.InsertMoney(50); // Add sufficient credit

            sut.AddToBasket(1); //Add Pepsi, 25 SEK

            string basket = sut.ViewBasket();

            Assert.Contains("Pepsi", basket);
        }

        [Fact]
        public void IcreaseQuantityIfProductIsAlreadyInBasket()
        {
            VendingMachine sut = new();

            sut.InsertMoney(100); // Add sufficient credit

            sut.AddToBasket(1); //Add a Pepsi
            sut.AddToBasket(1); //Add another Pepsi

            string basket = sut.ViewBasket();

            Assert.Contains("2 x Pepsi", basket);
        }
    }
}
