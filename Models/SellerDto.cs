using System.ComponentModel.DataAnnotations;

namespace SellerMVC.Models
{
    public class SellerDto
    {

        // MaxLength is only used for string properties
        [MaxLength(100)]
        public string? seller_type_roles { get; set; } = "";

        [MaxLength(100)]
        public string? seller_level { get; set; } = "";

        // Removed MaxLength, since sellerBp is an integer
        public string? sellerbp { get; set; } 

        [MaxLength(100)]
        public string? fullname { get; set; } = "";

        // Removed MaxLength, since reportingTo is an integer
        public required string reporting_to { get; set; }
    }
}
