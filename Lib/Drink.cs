namespace ShoppingListApi.Lib
{
    public class Drink : Item
    {
        // how to avoid duplication in constructors?
//        public ItemTypes Type = ItemTypes.Drink;

        public Drink(string name, int quantity) : base(name, quantity)
        {
            this.Type = ItemTypes.Drink;
        }

        public Drink(string name) : base(name)
        {
            this.Type = ItemTypes.Drink;
        }

        public Drink() : base()
        {
            this.Type = ItemTypes.Drink;
        }
    }
}