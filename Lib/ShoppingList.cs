using System.Collections.Generic;

namespace ShoppingListApi.Lib
{
    public class ShoppingList
    {
        private readonly List<Item> _items = new List<Item>();
        private static ShoppingList _instance;

        private ShoppingList() { }

        private static ShoppingList Instance
        {
            get
            {
                _instance = _instance ?? new ShoppingList();

                return _instance;
            }
        }

        // an "expression body" (for gets only?)
        public static List<Item> Items => Instance._items;
        public static Item Find(string name) => Instance._items.Find(i => i.Id == name);

        public static bool Add(Item item)
        {
            // how to compare objects? On what basis? simple string equality shoudl suffice
            var alreadyInList = Contains(item.Id);

            if (alreadyInList)
            {
                return false;
            }

            Instance._items.Add(item);

            return true;
        }

        public static bool Remove(string name)
        {
            var item = Find(name);

            return Instance._items.Remove(item);
        }

        public static bool Update(string name, int quantity)
        {
            var alreadyInList = Contains(name);

            return true;
        }

        public static bool Contains(string name)
        {
            return Find(name) != null;
        }
    }
}