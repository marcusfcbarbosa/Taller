using Taller.Core.DomainObjects;

namespace Taller.Api.Entities
{
    public class Car : Entity
    {
        public string Make { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public decimal Price { get; private set; }
        public Car(string make, string model, int year, string color, decimal price)
        {
            Make = make;
            Model = model;
            Year = year;
            Color = color;
            Price = price;
        }
        protected Car() { }


    }
}
