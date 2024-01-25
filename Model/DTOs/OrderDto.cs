namespace examen_web.Model.DTOs
{
    public class OrderDto
    {
        public Guid UserId { get; set; }
        public ICollection<Guid> ProductIds { get; set; }
    }
}
