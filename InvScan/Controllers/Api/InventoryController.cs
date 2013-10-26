using System.Data.Entity;
using InvScan.Models.Api;
using System.Linq;
using System.Web.Http;

namespace InvScan.Controllers.Api
{
    public class InventoryController : ApiController
    {
        private readonly InventoryDbContext _inventoryDb = new InventoryDbContext();
        private readonly InventoryStackDbContext _inventoryStackDb = new InventoryStackDbContext();

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
                inventory.Stacks = _inventoryStackDb.Stacks.Where(stack => stack.InventoryId == inventory.Id).ToList();
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
                System.Diagnostics.Debug.WriteLine("Stack: " + stack.DisplayName);
                stack.InventoryId = id;
                _inventoryStackDb.Stacks.Add(stack);
            }
            _inventoryStackDb.SaveChangesAsync();

            return id;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Inventory inventory)
        {
            _inventoryDb.Entry(inventory).State = EntityState.Modified;
            _inventoryDb.SaveChangesAsync();

            _inventoryStackDb.Stacks.RemoveRange(_inventoryStackDb.Stacks.Where(stack => stack.InventoryId == id));

            foreach (InventoryStack stack in inventory.Stacks)
            {
                stack.InventoryId = id;
                _inventoryStackDb.Stacks.Add(stack);
            }

            _inventoryStackDb.SaveChangesAsync();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            _inventoryDb.Inventories.Remove(_inventoryDb.Inventories.Find(id));
            _inventoryDb.SaveChangesAsync();

            _inventoryStackDb.Stacks.RemoveRange(_inventoryStackDb.Stacks.Where(stack => stack.InventoryId == id));
            _inventoryStackDb.SaveChangesAsync();
        }
    }
}