using System;

namespace BeerInventory.Models
{
    public class InventoryEntity
    {
        public string Id { get; set; }

        public string Owner { get; set; }

        public string Location { get; set; }

        public int Count { get; set; }

        public DateTime LastAdded { get; set; }

        public override string ToString()
        {
            return "[" + Id + "] " + Owner + " | " + Location + " : " + Count + "  Last added: " + LastAdded.ToString("MM/dd/yyyy");
        }
    }
}