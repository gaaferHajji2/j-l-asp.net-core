using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Authentication___Authorization.DTOs.Claims
{
    public class ClaimRequest
    {
        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
