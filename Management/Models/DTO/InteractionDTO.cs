namespace Management.Models.DTO
{
    public class InteractionDTO
    {
        public string InteractionId { get; set; } = null!;

        public string? Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? Like { get; set; }

        public string? Comments { get; set; }

        public string? PostId { get; set; }
        public PostDTO? PostDTO { get; set; }   
    }
}
