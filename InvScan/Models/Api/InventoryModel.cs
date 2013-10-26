using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace InvScan.Models.Api
{
    public class InventoryStack
    {
        [Key]
        public int Id { get; set; }
        public int Slot { get; set; }
        public int Size { get; set; }
        public int InventoryId { get; set; }
        public int Damage { get; set; }
        public string RawName { get; set; }
        public string DisplayName { get; set; }
        public bool IsEnchanted { get; set; }
    }

    public class InventoryStackDbContext : DbContext
    {
        public DbSet<InventoryStack> Stacks { get; set; }
    }

    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public bool IsPlayer { get; set; }
        public bool Compress { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public List<InventoryStack> Stacks { get; set; }
    }

    public class InventoryDbContext : DbContext
    {
        public DbSet<Inventory> Inventories { get; set; }
    }
}