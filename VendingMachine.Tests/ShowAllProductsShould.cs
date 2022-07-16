namespace VendingMachine.Tests
{
    public class ShowAllProductsShould
    {
        [Fact]
        public void ReturnANotEmptyString()
        {
            VendingMachine sut = new();

            string menu = sut.ShowAllProducts();

            Assert.NotEmpty(menu);
        }
    }
}
