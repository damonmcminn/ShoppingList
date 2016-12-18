using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ShoppingListApi.Lib;
using ShoppingListApi.Models;

namespace ShoppingListApi.Controllers
{
    [Route("[controller]")]
    public class DrinksController : Controller
    {
        // GET api/values
        [HttpGet]
        public List<Item> Get()
        {
            return ShoppingList.Items;
        }

       [HttpGet("{id}")]
       public IActionResult Get(string id)
       {
           var drink = ShoppingList.FindById(id);

           return drink != null ? (IActionResult) Ok(drink) : NotFound();
       }

        [HttpPost]
        // need to validate quantity not exist or >= 1
        // ideally this would be shared across methods: decorator?
        // name must be required
        public dynamic Post([FromBody] Drink drink)
        {
            var successfullyAdded = ShoppingList.Add(drink);

            return new
            {
                success = successfullyAdded,
                message = "",
                result = drink
            };
        }

        [HttpPut("{id}")]
        // need same validation as POST
        // validate id is a valid MD5 hex val
        // must be required quantity on body
        // TODO:
        public dynamic Put(string id, [FromBody] Drink drink)
        {
            var updated = ShoppingList.Update(id, drink.Quantity);

            return new
            {
                success = updated.Item1,
                message = "",
                result = updated.Item2
            };
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            // explicit cast required
            // http://stackoverflow.com/a/27822029
            return ShoppingList.Remove(id) ? (IActionResult) Ok() : NotFound();
        }
    }
}