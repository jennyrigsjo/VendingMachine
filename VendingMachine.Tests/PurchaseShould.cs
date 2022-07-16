namespace VendingMachine.Tests
{
    public class PurchaseShould
    {
        [Fact]
        public void ThrowVendingMachineExceptionIfTotalExceedsCredit()
        {
            VendingMachine sut = new();

            sut.InsertMoney(100); // Add credit
            sut.AddToBasket(1); // Add item
            sut.EndTransaction(); // Reset credit to zero

            VendingMachineException e = Assert.Throws<VendingMachineException>(() => sut.Purchase()); // Try to purchase item

            Assert.Equal("You need more money to buy these items.", e.Message);
        }


        [Fact]
        public void ThrowVendingMachineExceptionIfBasketIsEmpty()
        {
            VendingMachine sut = new(); // Basket should be empty on init

            VendingMachineException e = Assert.Throws<VendingMachineException>(() => sut.Purchase()); // Try to purchase empty basket

            Assert.Equal("You haven't selected any items yet!", e.Message);
        }


        [Fact]
        public void ReturnPurchasedProducts()
        {
            VendingMachine sut = new();
            sut.InsertMoney(100); // Insert money
            sut.AddToBasket(1); // Select a Pepsi

            Dictionary<Product, int> products = sut.Purchase(); // Buy item
           
            int count = products.Count();
            Assert.Equal(1, count);

            Product product = products.Keys.First();
            Assert.Equal("Pepsi", product.Name);
        }


        [Fact]
        public void SubtractTotalFromCredit()
        {
            VendingMachine sut = new();
            sut.InsertMoney(100); // Insert money
            sut.AddToBasket(1); // Add item

            int expectedCredit = sut.Credit - sut.BasketTotal();

            _ = sut.Purchase(); // Buy item

            Assert.Equal(expectedCredit, sut.Credit);
        }


        [Fact]
        public void EmptyBasketAfterPurchase()
        {
            VendingMachine sut = new();
            sut.InsertMoney(100); // Insert money
            sut.AddToBasket(1); // Add item

            _ = sut.Purchase(); // Buy item

            Assert.Empty(sut.ViewBasket());
        }
    }
}
