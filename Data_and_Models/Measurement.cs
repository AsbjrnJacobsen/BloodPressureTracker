using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data_and_Models;

public class Measurement
{
	[Key]
	public int Id { get; set; } 
	[Required]
	public DateOnly Date { get; set; }
	[Required]
	public int Systolic { get; set; }
	[Required]
	public int Diastolic { get; set; }
	[Required]
	public string PatientSSN { get; set; }
	[Required]
	public bool Seen { get; set; }
	[ForeignKey("PatientSSN")]
	[JsonIgnore]
	public Patient? Patient { get; set; }
}