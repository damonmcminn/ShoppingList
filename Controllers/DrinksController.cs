using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ShoppingListApi.Lib;
using ShoppingListApi.Models;

namespace ShoppingListApi.Controllers
{
    [Route("ShoppingList/[controller]")]
    public class DrinksController : Controller
    {
        // GET api/values
        [HttpGet]
        public List<Item> Get()
        {
            return ShoppingList.Items;
        }

       [HttpGet("{id}")]
       public Item Get(string id)
       {
           return ShoppingList.FindById(id);
       }

        // POST api/values
        [HttpPost]
        // is [FromBody] a list of middleware to call?
        public IActionResult Post([FromBody] Drink drink)
        {
//            Console.WriteLine(drink.Name);
            return Json(drink);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}