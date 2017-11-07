using System;

namespace BeerStorageLibrary.Models
{
    public class InventoryDetails : BeerEntity
    {
        public InventoryDetails(BeerEntity beer, InventoryEntity inventory)
        {
            this.ABV = beer.ABV;
            this.Availablity = beer.Availablity;
            this.Brewer = beer.Brewer;
            this.BrewSeason = beer.BrewSeason;
            this.BrewYear = beer.BrewYear;
            this.Description = beer.Description;
            this.Glass = beer.Glass;
            this.Id = beer.Id;
            this.LabelUrl = beer.LabelUrl;
            this.Name = beer.Name;


            this.LastAdded = inventory.LastAdded;
            this.Count = inventory.Count;
            this.Location = inventory.Location;
            this.Owner = inventory.Owner;
        }

        public string Owner { get; set; }

        public string Location { get; set; }

        public int Count { get; set; }

        public DateTime LastAdded { get; set; }

        public override string ToString()
        {
            return "[" + Name + "] " + Owner + " | " + Location + " : " + Count + "  Last added: " + LastAdded.ToString("MM/dd/yyyy");
        }
    }
}
