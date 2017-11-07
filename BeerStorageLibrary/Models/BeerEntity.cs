using Microsoft.WindowsAzure.Storage.Table;

namespace BeerStorageLibrary.Models
{
    public class BeerEntity : TableEntity
    {
        public BeerEntity(string brewer, string id)
        {
            PartitionKey = brewer;
            RowKey = id;
            Brewer = brewer;
            Id = id;
        }

        public BeerEntity() { }

        public string Brewer { get; set; }

        public string Name { get; set; }

        public double ABV { get; set; }

        public string Type { get; set; }

        public string Glass { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public string LabelUrl { get; set; }

        public string Availablity { get; set; }

        public int BrewYear { get; set; }

        public string BrewSeason { get; set; }


        public override string ToString()
        {
            return "[" + Id + "] " + Brewer + " - " + Name + " " + ABV + "%" + "  " + Description;
        }
    }
}
