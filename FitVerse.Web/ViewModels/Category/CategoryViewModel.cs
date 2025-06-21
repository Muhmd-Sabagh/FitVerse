namespace FitVerse.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = new DateTime(2025, 01, 01);

        public DateTime UpdatedAt { get; set; } = new DateTime(2025, 01, 01);

    }
}
