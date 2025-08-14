using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTOS
{
    public class TaskUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
        public bool Provera { get; set; }
    }
}
