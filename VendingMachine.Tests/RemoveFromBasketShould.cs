namespace VendingMachine.Tests
{
    public class RemoveFromBasketShould
    {
        [Fact]
        public void ThrowVendingMachineExceptionWhenProductIsNotInBasket()
        {
            VendingMachine sut = new(); // Basket should be empty on init

            int productID = 1;

            VendingMachineException e = Assert.Throws<VendingMachineException>(() => sut.RemoveFromBasket(productID));

            Assert.Equal($"There is no item with id '{productID}' to remove.", e.Message);
        }

        [Fact]
        public void RemoveProductFromBasket()
        {
            VendingMachine sut = new(); // Basket should be empty on init

            sut.InsertMoney(50); // Add sufficient credit
            sut.AddToBasket(1); // Add item to basket

            string basket = sut.ViewBasket(); // get basket
            Assert.NotEmpty(basket); // Should not be empty

            sut.RemoveFromBasket(1); // Remove item
            basket = sut.ViewBasket(); // Update basket
            Assert.Empty(basket); // Should be empty
        }


        [Fact]
        public void DecreaseQuantityIfAmountOfProductIsGreaterThanOne()
        {
            VendingMachine sut = new();

            sut.InsertMoney(100); // Add sufficient credit

            sut.AddToBasket(1); //Add a Pepsi
            sut.AddToBasket(1); //Add another Pepsi

            string basket = sut.ViewBasket(); // Get basket
            Assert.Contains("2 x Pepsi", basket); // There should be two

            sut.RemoveFromBasket(1); // Remove one Pepsi
            basket = sut.ViewBasket(); // Update basket
            Assert.Contains("1 x Pepsi", basket); // There should be one
        }
    }
}
