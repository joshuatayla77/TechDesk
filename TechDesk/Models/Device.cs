using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TechDesk.Models
{
    [Index(nameof(AssetTag), IsUnique = true)]
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Asset tag is required")]
        [StringLength(10, ErrorMessage = "Asset tag cannot be longer than 10 characters")]
        public string AssetTag { get; set; }


        [Required(ErrorMessage = "Category is required")]
        [StringLength(20, ErrorMessage = "Category cannot be longer than 20 characters")]
        public string Category { get; set; } 

        [Range(2015, 2026, ErrorMessage = "Purchase year must be between 2015 and 2026")]
        public int PurchaseYear { get; set; }
        public string IsOnLoan { get; set; } = "no";


    }
}
