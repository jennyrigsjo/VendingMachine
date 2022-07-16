using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Tests
{
    public class ViewBasketShould
    {
        [Fact]
        public void ReturnEmptyOnInit()
        {
            VendingMachine sut = new(); // Basket should be empty on init

            string basket = sut.ViewBasket(); // get basket

            Assert.Empty(basket); // Should be empty
        }
    }
}
