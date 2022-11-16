using System;
using Taller.Api.Entities;
using Taller.Core.Extensions;
using Taller.Core.Identity;

namespace Taller.Web.Models
{
    public class ListCarModel
    {
       public PagedResult<CarModel> cars { get; set; }
       public IAspNetUser aspNetUser { get; set; }
    }

    public class CarModel
    {
        public Guid Id { get; set; }
        public string Make { get;  set; }
        public string Model { get;  set; }
        public int Year { get;  set; }
        public string Color { get;  set; }
        public decimal Price { get;  set; }
    }
}
