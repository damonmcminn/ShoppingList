using System;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingListApi.Models
{

    public enum ItemTypes { Drink }

    public abstract class Item
    {
        // what does protected mean?
        // I want only to be able to assign the type at initialization
        // not allow changes to it
        protected ItemTypes Type;
        // are fields readonly implicitly unless I do get; set?

        private string _id;
        public string Id
        {
            get
            {
                var id = _id ?? CalculateId();
                this._id = id;

                return id;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }

        }

        public int Quantity { get; set; }

        // need parameterless constructor to allow ASP model binding
        protected Item() { }

        // why protected and not public?
        protected Item(string name, int quantity)
        {
            this.Name = name.Trim();
            this.Quantity = quantity;
        }

        protected Item(string name) : this(name, 0) { }

        private string CalculateId()
        {
            var md5Hash = MD5.Create();
            var nameBytes = Encoding.UTF8.GetBytes(Name.ToLower());
            var hashed = md5Hash.ComputeHash(nameBytes);

            return BitConverter.ToString(hashed).Replace("-", "").ToLower();
        }

    }
}