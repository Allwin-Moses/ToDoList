using System.ComponentModel.DataAnnotations;

namespace Github_project_1.Models
{
    public class Entity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(200, ErrorMessage = "Task name cannot exceed 200 characters.")]
        public string TaskName { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}