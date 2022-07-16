namespace VendingMachine
{
    internal class VendingMachineController
    {
        public VendingMachineController()
        {
            
            string action = string.Empty;

            while (true)
            {
                Heading();
                ShowErrorMessage();

                if (action != string.Empty)
                {
                    DoAction(action);
                    action = string.Empty;
                }

                if (EndProgram)
                {
                    break;
                }

                Status();

                while (action == string.Empty && ErrorMessage == string.Empty)
                {
                    action = AskForUserInput();
                }

                Console.Clear();
            }
        }


        private readonly VendingMachine Vend = new();


        private bool ShowIntroOnce { get; set; } = true;


        private int InsertSum { get; set; } = 0;


        private int ItemID { get; set; } = 0;


        private Dictionary<Product, int> PurchasedItems { get; set; } = new();


        private string PurchasedItemName { get; set; } = string.Empty;


        private Dictionary<int, int> Change { get; set; } = new();


        private string ErrorMessage { get; set; } = string.Empty;


        private bool EndProgram { get; set; } = false;


        private void Heading()
        {
            if (ShowIntroOnce)
            {
                Console.WriteLine("*-*-*-*-*-*-*-*-Vending Machine-*-*-*-*-*-*-*-*\n");
                ListOptions();
                ShowIntroOnce = false;
            }
            else
            {
                Console.WriteLine("*-*-*-*-*-*-*-*-Vending Machine-*-*-*-*-*-*-*-*\n");
            }
        }


        private static void ListOptions()
        {
            Console.WriteLine("------Options------");
            Console.WriteLine("'m', 'menu'                  : view menu");
            Console.WriteLine("'in [amount]'                : insert money");
            Console.WriteLine("'d'                          : accepted currency denominations");
            Console.WriteLine("'sel [item nr]'              : select item from menu");
            Console.WriteLine("'rem [item nr]'              : remove selected item");
            Console.WriteLine("'p', 'purchase'              : purchase selected items");
            Console.WriteLine("'r', 'return'                : return unused credit");
            Console.WriteLine("'exam [item name]'           : examine purchased item");
            Console.WriteLine("'use [item name]'            : use purchased item");
            Console.WriteLine("'o', 'options', 'h', 'help'  : view options (this list)");
            Console.WriteLine("'q', 'quit'                  : quit/end program\n");
        }


        private void ShowErrorMessage()
        {
            if (ErrorMessage != string.Empty)
            {
                Console.WriteLine($"{ErrorMessage}\n");
                ErrorMessage = string.Empty;
            }
        }


        private string AskForUserInput()
        {
            Console.Write("Your choice: ");
            string input = Console.ReadLine() ?? string.Empty;
            string action = string.Empty;

            try
            {
                action = ParseInput(input);
            }
            catch (FormatException e)
            {
                ErrorMessage = e.Message;
            }

            return action;
        }


        private string ParseInput(string input)
        {
            if (input.Trim().Length == 0)
            {
                return string.Empty;
            }

            string[] elements = input.Trim().Split(" ");

            string action = elements[0];
            string[] args = elements.Skip(1).ToArray();

            ParseInputArgs(action, args);

            return action;
        }


        private void ParseInputArgs(string action, string[] args)
        {
            string arg = args switch
            {
                string[] when (args.Length > 1) => string.Join(" ", args),
                string[] when (args.Length == 1) => args[0],
                _ => string.Empty,
            };

            switch (action)
            {
                case "in":
                    try
                    {
                        InsertSum = (arg == string.Empty) ? 0 : Int32.Parse(arg);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Please use whole numbers to insert money.");
                    }
                    break;
                case "sel":
                case "rem":
                    try
                    {
                        ItemID = (arg == string.Empty) ? 0 : Int32.Parse(arg);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("Please use menu numbers to select or remove items.");
                    }
                    break;
                case "exam":
                case "use":
                    if (arg.All(char.IsDigit))
                    {
                        throw new FormatException("Enter item name to examine or use an item.");
                    }
                    PurchasedItemName = arg;
                    break;
            }
        }


        private void DoAction(string action)
        {
            try
            {
                TakeAction(action);
            }
            catch (ArgumentException e)
            {
                ErrorMessage = e.Message;
            }
            catch (VendingMachineException e)
            {
                ErrorMessage = e.Message;
            }
            catch (VendingMachineControllerException e)
            {
                ErrorMessage = e.Message;
            }
        }


        private void TakeAction(string action)
        {
            switch (action.ToLower())
            {
                case "m":
                case "menu":
                    ShowMenu();
                    break;
                case "in":
                    InsertMoney();
                    InsertSum = 0;
                    break;
                case "d":
                    ShowDenominations();
                    break;
                case "sel":
                    SelectItem();
                    ItemID = 0;
                    break;
                case "rem":
                    RemoveItem();
                    ItemID = 0;
                    break;
                case "p":
                case "purchase":
                    PurchaseItems();
                    break;
                case "r":
                case "return":
                    ReturnUnusedCredit();
                    break;
                case "exam":
                    ExamineItem();
                    PurchasedItemName = string.Empty;
                    break;
                case "use":
                    UseItem();
                    PurchasedItemName = string.Empty;
                    break;
                case "o":
                case "options":
                case "h":
                case "help":
                    ListOptions();
                    break;
                case "q":
                case "quit":
                    EndProgram = true;
                    Console.WriteLine("Program ended. Bye!\n");
                    break;
                default:
                    ErrorMessage = $"Ivalid option '{action}'. Enter 'o' for valid options.";
                    break;
            }
        }


        private void ShowMenu()
        {
            string menu = Vend.ShowAllProducts();
            Console.WriteLine("-----Menu-----");
            Console.WriteLine(menu.TrimEnd() + "\n");
        }


        private void InsertMoney()
        {
            if (InsertSum == 0)
            {
                throw new ArgumentException("Please specify an amount of money to insert.");
            }

            Vend.InsertMoney(InsertSum);
        }


        private void ShowDenominations()
        {
            string denominations = string.Join(", ", Vend.Denominations);
            Console.WriteLine($"Accepted money values: {denominations}\n");
        }


        private void SelectItem()
        {
            if (ItemID == 0)
            {
                throw new ArgumentException("Please select an item to add.");
            }

            Vend.AddToBasket(ItemID);
        }


        private void RemoveItem()
        {
            if (ItemID == 0)
            {
                throw new ArgumentException("Please select an item to remove.");
            }

            Vend.RemoveFromBasket(ItemID);
        }


        private void PurchaseItems()
        {
            Dictionary<Product, int> items = Vend.Purchase();
            UpdatePurchasedItems(items);
            Change = Vend.EndTransaction();
        }


        private void ReturnUnusedCredit()
        {
            Change = Vend.EndTransaction();

            if (Change.Count == 0)
            {
                Console.WriteLine("There is no unused credit to return.\n");
            }
        }


        private void Status()
        {
            Console.WriteLine("- - - - -\n");

            Console.WriteLine($"Current credit: {Vend.Credit}\n");

            ListSelectedItems();
            Console.WriteLine();

            ListPurchasedItems();
            Console.WriteLine();

            ListChange();

            Console.WriteLine("- - - - -\n");
        }


        private void ListSelectedItems()
        {
            string selectedItems = Vend.ViewBasket();

            string heading = (selectedItems == string.Empty) ? "Selected items: none" : "Selected items:";
            Console.WriteLine(heading);

            if (selectedItems != string.Empty)
            {
                Console.WriteLine(selectedItems);
                Console.WriteLine($"Total cost: {Vend.BasketTotal()} SEK");
            }
        }


        private void ListPurchasedItems()
        {
            string heading = PurchasedItems.Count > 0 ? $"Purchased items:" : "Purchased items: none";
            Console.WriteLine(heading);

            foreach (KeyValuePair<Product, int> item in PurchasedItems)
            {
                Product product = item.Key;
                int amount = item.Value;
                Console.WriteLine($"{amount} x {product.Name}");
            }
        }


        private void ListChange()
        {
            string change = string.Empty;
            int total = 0;

            if (Change.Count > 0)
            {
                foreach (KeyValuePair<int, int> item in Change)
                {
                    int denomination = item.Key;
                    int amount = item.Value;

                    change += $"{amount} x {denomination} SEK\n";
                    total += (denomination * amount);
                }

                Console.WriteLine($"Change returned ({total} SEK):");
                Console.WriteLine(change);

                Change.Clear();
            }
        }


        private void UpdatePurchasedItems(Dictionary<Product, int> items)
        {
            foreach (KeyValuePair<Product, int> item in items)
            {
                Product product = item.Key;
                int amount = item.Value;

                if (PurchasedItems.ContainsKey(product))
                {
                    PurchasedItems[product] += amount;
                }
                else
                {
                    PurchasedItems.Add(product, amount);
                }
            }
        }


        private void ExamineItem()
        {
            if (!InPurchasedItems(PurchasedItemName))
            {
                throw new VendingMachineControllerException($"There is no '{PurchasedItemName}' in your purchased items.");
            }

            Product item = GetItemFromPurchasedItems(PurchasedItemName);

            string result = item.Examine();

            Console.WriteLine(result + "\n");
        }


        private void UseItem()
        {
            if (!InPurchasedItems(PurchasedItemName))
            {
                throw new VendingMachineControllerException($"There is no '{PurchasedItemName}' in your purchased items.");
            }

            Product item = GetItemFromPurchasedItems(PurchasedItemName);

            string result = item.Use();

            Console.WriteLine(result + "\n");

            if (PurchasedItems[item] > 1)
            {
                PurchasedItems[item]--;
            }
            else
            {
                PurchasedItems.Remove(item);
            }
        }


        private bool InPurchasedItems(string itemName)
        {
            List<Product> items = PurchasedItems.Keys.ToList();

            return items.FindIndex(item => item.Name.ToLower() == itemName.ToLower()) > -1;
        }


        private Product GetItemFromPurchasedItems(string itemName)
        {
            return PurchasedItems.Where(item => item.Key.Name.ToLower() == itemName.ToLower()).First().Key;
        }
    }
}
