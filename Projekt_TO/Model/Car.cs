namespace Projekt_TO.Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int HorsePower { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public bool IsRent { get; set; }
    }
}
