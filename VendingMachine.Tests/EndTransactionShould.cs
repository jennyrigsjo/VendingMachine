namespace VendingMachine.Tests
{
    public class EndTransactionShould
    {
        [Fact]
        public void ReturnEmptyIfThereIsNoChange()
        {
            VendingMachine sut = new();
            sut.InsertMoney(50); // Insert money
            sut.AddToBasket(1); // Select a Pepsi, 25 SEK
            sut.AddToBasket(1); // Select another Pepsi, 25 SEK

            _ = sut.Purchase(); // Buy items
            Dictionary<int, int> change = sut.EndTransaction();

            Assert.Empty(change);
        }


        [Fact]
        public void ReturnChange()
        {
            VendingMachine sut = new();
            sut.InsertMoney(100); // Insert money
            sut.AddToBasket(1); // Select a Pepsi, 25 SEK

            _ = sut.Purchase(); // Buy item
            Dictionary<int, int> change = sut.EndTransaction();

            List<int> values = change.Keys.ToList(); // Should be 50, 20, 5
            Assert.Contains(50, values);
            Assert.Contains(20, values);
            Assert.Contains(5, values);

            List<int> amounts = change.Values.ToList(); // Should be one of each
            List<int> expected = new() {1, 1, 1};
            Assert.Equal(expected, amounts);
        }


        [Fact]
        public void ResetCreditToZero()
        {
            VendingMachine sut = new();
            sut.InsertMoney(100); // Insert money

            Assert.True(sut.Credit > 0); // Should be more than zero

            _ = sut.EndTransaction();

            Assert.Equal(0, sut.Credit); // Should now be zero
        }
    }
}
