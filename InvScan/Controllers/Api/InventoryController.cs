using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using InvScan.Models.Api;

namespace InvScan.Controllers.Api
{
    public class InventoryController : ApiController
    {
        private InventoryDbContext _inventoryDb = new InventoryDbContext();
        private InventoryStackDbContext _inventoryStackDb = new InventoryStackDbContext();

        // GET api/<controller>
        public Inventory[] Get()
        {
            return _inventoryDb.Inventories.ToArray();
        }

        // GET api/<controller>/5
        public Inventory Get(int id)
        {
            Inventory inventory = _inventoryDb.Inventories.Find(id);
            if (inventory != null)
            {
                inventory.Stacks = _inventoryStackDb.Stacks.Where(stack => stack.Id == inventory.Id).ToList();
            }
            return inventory;
        }

        // POST api/<controller>
        public int Post([FromBody]Inventory inventory)
        {
            _inventoryDb.Inventories.Add(inventory);
            _inventoryDb.SaveChanges();
            int id = _inventoryDb.Inventories.Max(inv => inv.Id);

            foreach (InventoryStack stack in inventory.Stacks)
            {
                stack.InventoryId = id;
                _inventoryStackDb.Stacks.Add(stack);
            }
            _inventoryStackDb.SaveChangesAsync();

            return id;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}