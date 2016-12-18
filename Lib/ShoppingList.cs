using System;
using System.Collections.Generic;
using ShoppingListApi.Models;

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

        public static List<Item> Items => Instance._items;

        public static Item FindById(string id) => Instance._items.Find(i => i.Id == id);

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

        public static bool Remove(string id)
        {
            var item = FindById(id);

            return Instance._items.Remove(item);
        }

        // TODO: replace tuple with `out`
        // https://msdn.microsoft.com/en-us/library/t3c3bfhx.aspx
        public static Tuple<bool, Item> Update(string id, int quantity)
        {
            var alreadyInList = Contains(id);
            var item = FindById(id);

            if (alreadyInList)
            {
                item.Quantity = quantity;
            }

            return new Tuple<bool, Item>(alreadyInList, item);
        }

        public static bool Contains(string id)
        {
            return FindById(id) != null;
        }
    }
}