using System.Text.Json.Serialization;

namespace examen_web.Model
{
    public class Product : Base
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        [JsonIgnore]
        public ICollection<OrderProduct> OrdersProducts { get; set; }
    }
}
