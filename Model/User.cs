namespace examen_web.Model.One_to_Many

{
    public class User : Base
    {
        public string? Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
