namespace Management.Models.DTO
{
    public class PostDTO
    {
        public string PostId { get; set; } = null!;

        public string? ArtworkId { get; set; }

        public string? Tittle { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? Status { get; set; }
        public ArtworkDTO? ArtworkDTO { get; set; }  
    }
}
