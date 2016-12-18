using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ShoppingListApi.Lib;
using ShoppingListApi.Models;

namespace ShoppingListApi.Controllers
{
    [Route("[controller]")]
    public class DrinksController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            StringValues name;
            var hasQuery = Request.Query.TryGetValue("name", out name);
            var items = ShoppingList.Items;

            // StringValues implicitly called ToString???
            var drink = ShoppingList.FindByName(name.ToString());

            if (drink == null)
            {
                return NotFound();
            }

            if (hasQuery)
            {
                items = ShoppingList.Items.Where(item => item == drink).ToList();
            }

            return Ok(items);
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
        public IActionResult Post([FromBody] Drink drink)
        {
            var successfullyAdded = ShoppingList.Add(drink);

            // TODO: build URI dynamically
            // currently doesn't have root domain and controller is hardcoded
            return successfullyAdded ? (IActionResult) Created($"/Drinks/{drink.Id}", drink) : BadRequest();
        }

        [HttpPut("{id}")]
        // need same validation as POST
        // validate id is a valid MD5 hex val
        // must be required quantity on body
        public IActionResult Put(string id, [FromBody] Drink data)
        {
            var updated = ShoppingList.Update(id, data.Quantity);
            var alreadyExists = updated.Item1;
            var drink = updated.Item2;

            return alreadyExists ? (IActionResult) Ok(drink) : NotFound();
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