namespace VendingMachine
{
    internal interface IVending
    {
        Dictionary<Product, int> Purchase();

        string ShowAllProducts();

        void InsertMoney(int amount);

        Dictionary<int, int> EndTransaction();
    }
}
