using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTOS
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title must be at most 100 characters")]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
