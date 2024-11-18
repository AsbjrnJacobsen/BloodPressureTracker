using Data_and_Models;
using Microsoft.AspNetCore.Mvc;

namespace PatientService;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
    {
        await _patientRepository.CreatePatientAsync(patient);
        return Ok();
    }

    [HttpGet("{SSN}")]
    public async Task<IActionResult> GetPatient(string SSN)
    {
        var patient = await _patientRepository.GetPatientAsync(SSN);
        return patient != null ? Ok(patient) : NotFound();
    }

    [HttpPut("{SSN}")]
    public async Task<IActionResult> UpdatePatient(string SSN, [FromBody] Patient patient)
    {
        var updatedPatient = await _patientRepository.UpdatePatientAsync(SSN, patient);
        return updatedPatient != null ? Ok(updatedPatient) : NotFound();
    }

    [HttpDelete("{SSN}")]
    public async Task<IActionResult> DeletePatient(string SSN)
    {
        var result = await _patientRepository.DeletePatientAsync(SSN);
        return result ? Ok() : NotFound();
    }
}


