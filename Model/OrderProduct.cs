using System.Text.Json.Serialization;

namespace examen_web.Model
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }

        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
