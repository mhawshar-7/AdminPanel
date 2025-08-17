using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Web.Areas.Admin.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string? ModifiedDate { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
