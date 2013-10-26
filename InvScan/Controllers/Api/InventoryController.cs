using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using InvScan.Models.Api;

namespace InvScan.Controllers.Api
{
    public class InventoryController : ApiController
    {
        private InventoryDbContext _inventoryDb = new InventoryDbContext();
        private  InventoryStackDbContext _inventoryStackDb = new InventoryStackDbContext();

        // GET api/<controller>
        public Inventory Get()
        {
            Inventory nInventory = new Inventory();
            nInventory.Compress = false;
            nInventory.Id = 1;
            nInventory.IsPlayer = true;
            nInventory.Name = "Banane9";
            InventoryStack stack = new InventoryStack();
            stack.Damage = 0;
            stack.DisplayName = "Stone";
            stack.RawName = "Stone";
            stack.InventoryId = 1;
            stack.IsEnchanted = false;
            stack.Size = 34;
            stack.Slot = 3;
            nInventory.Stacks = new List<InventoryStack>(new InventoryStack[]{stack});
            nInventory.X = 0;
            nInventory.Y = 0;
            nInventory.Z = 0;
            _inventoryDb.Inventories.Add(nInventory);
            _inventoryDb.SaveChanges();
            return nInventory;
        }

        // GET api/<controller>/5
        public Inventory Get(int id)
        {
            Inventory inventory = _inventoryDb.Inventories.Find(id);
            return inventory;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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