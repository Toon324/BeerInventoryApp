using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace BeerStorageLibrary.Models
{
    public class InventoryEntity : TableEntity
    {
        public InventoryEntity(String owner, String location, String Id)
        {
            PartitionKey = owner;
            RowKey = location + "|" + Id;

            Owner = owner;
            Location = location;
            this.Id = Id;
        }

        public InventoryEntity() { }

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
