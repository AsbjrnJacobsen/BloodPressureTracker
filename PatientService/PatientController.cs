using Data_and_Models;
using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;

namespace PatientService;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(FeatureFlagFilter))]
public class PatientController(IPatientRepository patientRepository) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
    {
        await patientRepository.CreatePatientAsync(patient);
        return Ok();
    }

    [HttpGet("{SSN}")]
    [SkipFeatureFlagFilter("isUserDoctor")] //Skips isUserDoctor Req from FeatureFlagFilter. 
    public async Task<IActionResult> GetPatient(string SSN)
    {
        var patient = await patientRepository.GetPatientAsync(SSN);
        return patient != null ? Ok(patient) : NotFound();
    }

    [HttpPut("{SSN}")]
    public async Task<IActionResult> UpdatePatient(string SSN, [FromBody] Patient patient)
    {
        var updatedPatient = await patientRepository.UpdatePatientAsync(SSN, patient);
        return updatedPatient != null ? Ok(updatedPatient) : NotFound();
    }

    [HttpDelete("{SSN}")]
    public async Task<IActionResult> DeletePatient(string SSN)
    {
        var result = await patientRepository.DeletePatientAsync(SSN);
        return result ? Ok() : NotFound();
    }
}