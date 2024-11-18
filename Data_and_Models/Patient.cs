using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data_and_Models;

public class Patient
{
   [Key]
   public string SSN { get; set; }
   [Required]
   public string Email { get; set; }
   [Required]
   public string Name { get; set; }
   
   public ICollection<Measurement>? Measurements { get; set; }
}
