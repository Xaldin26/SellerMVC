using System.ComponentModel.DataAnnotations;

namespace SellerMVC.Models
{
    public class SellerDto
    {

        // MaxLength is only used for string properties
        [MaxLength(100)]

        public int Id { get; set; }
        public string? seller_type_roles { get; set; } = "";

        [MaxLength(100)]
        public int seller_level { get; set; } 

        // Removed MaxLength, since sellerBp is an integer
        public string? seller_buyer_reference { get; set; } 

        [MaxLength(100)]
        public string? fullname { get; set; } = "";

        // Removed MaxLength, since reportingTo is an integer
        public int? reporting_to { get; set; }
    }
}
