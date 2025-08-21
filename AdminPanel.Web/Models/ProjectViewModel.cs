using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Web.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string? ModifiedDate { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Status")]
        public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]        
        public decimal? Budget { get; set; }
        public int? ClientId { get; set; }
        public IEnumerable<SelectListItem>? Clients { get; set; }
    }
}
