namespace VendingMachine
{
    internal abstract class Product
    {
        public abstract string Name { get; }

        public abstract int Price { get; }

        public abstract string Description { get; }

        public abstract string Examine();

        public abstract string Use();
    }



    internal class Drink : Product
    {
        public Drink(string name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public override string Name { get; }

        public override int Price { get; }

        public override string Description { get; }

        public override string Examine()
        {
            return $"Name: {Name}\nPrice: {Price} SEK\nDescription: {Description}";
        }

        public override string Use()
        {
            return $"You drank the {Name}.";
        }
    }



    internal class Sweets : Product
    {
        public Sweets(string name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public override string Name { get; }

        public override int Price { get; }

        public override string Description { get; }

        public override string Examine()
        {
            return $"Name: {Name}\nPrice: {Price} SEK\nDescription: {Description}";
        }

        public override string Use()
        {
            return $"You ate the {Name}.";
        }
    }



    internal class Snacks : Product
    {
        public Snacks(string name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public override string Name { get; }

        public override int Price { get; }

        public override string Description { get; }

        public override string Examine()
        {
            return $"Name: {Name}\nPrice: {Price} SEK\nDescription: {Description}";
        }

        public override string Use()
        {
            return $"You ate the {Name}.";
        }
    }
}
