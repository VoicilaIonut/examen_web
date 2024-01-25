using examen_web.Model.One_to_Many;
using System.Text.Json.Serialization;

namespace examen_web.Model
{
    public class Order : Base
    {
        [JsonIgnore]
        public ICollection<OrderProduct> OrdersProducts { get; set; }

        // relation
        [JsonIgnore]
        public User User { get; set; }
       
        public Guid UserId { get; set; }
    }
}
