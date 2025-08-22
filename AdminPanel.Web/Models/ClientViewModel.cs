using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Web.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string? ModifiedDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
