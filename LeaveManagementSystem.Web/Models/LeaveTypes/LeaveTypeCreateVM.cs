using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(4, 150, ErrorMessage ="You have violated the length requirements")]
        public string Name { get; set; } = string.Empty; // to get rid of null warning
        [Required]
        [Range(1, 90)] // 1 min, 90 max
        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}
