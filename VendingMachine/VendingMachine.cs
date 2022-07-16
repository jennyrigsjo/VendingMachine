using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("VendingMachine.Tests")]

namespace VendingMachine
{
    internal class VendingMachine : IVending
    {
        public VendingMachine()
        {

        }


        public readonly int[] Denominations = new int[8] { 1, 5, 10, 20, 50, 100, 500, 1000 };


        private readonly Dictionary<int, Product> Products = new()
        {
            {1, new Drink("Pepsi", 25, "Carbonated cola-flavored drink")},
            {2, new Drink("Fanta", 20, "Carbonated drink with orange flavor")},

            {3, new Sweets("Snickers", 15, "Chocolate-coated candybar with toffee, caramel and peanuts")},
            {4, new Sweets("Kexchoklad", 10, "Thin wafers layered with milk chocolate")},
            {5, new Sweets("HubbaBubba", 5, "Disgusting but iconic chewing gum from the 80's")},

            {6, new Snacks("Chips", 35, "Sourcream and onion-flavored potato chips")},
            {7, new Snacks("Popcorn", 30, "Popcorn with butter and salt")},
        };


        private Dictionary<int, int> Basket {get; set;} = new();


        public int Credit {get; private set;} = 0;


        private bool IsValidDenomination(int amount)
        {
            return Denominations.Contains(amount);
        }


        private Product GetProduct(int id)
        {
            if (!Products.ContainsKey(id))
            {
                throw new VendingMachineException($"Product with id '{id}' does not exist.");
            }

            return Products[id];
        }


        public string ShowAllProducts()
        {
            string menu = string.Empty;

            foreach (KeyValuePair<int, Product> p in Products)
            {
                int id = p.Key;
                Product product = p.Value;

                string item = $"{id}. {product.Name} - {product.Price} SEK ({product.Description})\n\n";

                menu += item;
            }

            return menu;
        }


        public void InsertMoney(int amount)
        {
            if (IsValidDenomination(amount))
            {
                Credit += amount;
            }
            else
            {
                throw new ArgumentException($"Please insert a valid denomination: {string.Join(", ", Denominations)}");
            }
        }


        public void AddToBasket(int productID)
        {
            if (!Products.ContainsKey(productID))
            {
                throw new VendingMachineException($"Product with id '{productID}' does not exist.");
            }
            
            else if (BasketTotal() + GetProduct(productID).Price > Credit)
            {
                throw new VendingMachineException("Please insert more money to select items.");
            }

            else if (Basket.ContainsKey(productID))
            {
                Basket[productID] += 1;
            }

            else
            {
                Basket[productID] = 1;
            }
        }


        public void RemoveFromBasket(int productID)
        {
            if (!Basket.ContainsKey(productID))
            {
                throw new VendingMachineException($"There is no item with id '{productID}' to remove.");
            }

            else if (Basket[productID] > 1)
            {
                Basket[productID]--;
            }

            else
            {
                Basket.Remove(productID);
            }
        }


        public string ViewBasket()
        {
            string list = string.Empty;

            foreach (KeyValuePair<int, int> product in Basket)
            {
                int id = product.Key;
                int amount = product.Value;

                list += $"{amount} x {GetProduct(id).Name} ({GetProduct(id).Price} SEK)\n";
            }

            return list;
        }


        public int BasketTotal()
        {
            int total = Basket.Count > 0 ? Basket.Sum(p => GetProduct(p.Key).Price * p.Value) : 0; //p.Key = ID of the product, p.Value = amount of each product
            return total;
        }


        public Dictionary<Product, int> Purchase()
        {
            if (BasketTotal() > Credit)
            {
                throw new VendingMachineException("You need more money to buy these items.");
            }

            if (Basket.Count == 0)
            {
                throw new VendingMachineException("You haven't selected any items yet!");
            }

            Dictionary<Product, int> products = new();

            foreach (KeyValuePair<int, int> product in Basket)
            {
                int id = product.Key;
                int amount = product.Value;

                products[GetProduct(id)] = amount;
            }

            Credit -= BasketTotal();
            Basket.Clear();

            return products;
        }


        public Dictionary<int, int> EndTransaction()
        {
            int remainder = Credit;
            Credit = 0;
            Dictionary<int, int> change = new();

            if (remainder == 0)
            {
                return change;
            }

            for (int i = Denominations.Length - 1; i >= 0; i--)
            {
                if (remainder > 0)
                {
                    int count = remainder / Denominations[i];

                    if (count > 0)
                    {
                        change[Denominations[i]] = count;
                        remainder %= Denominations[i];
                    }
                }
            }

            return change;
        }
    }
}
