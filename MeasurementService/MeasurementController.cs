using Data_and_Models;
using FeatureHubSDK;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(FeatureFlagFilter))]
public class MeasurementController(IMeasurementRepository measurementRepository, IClientContext fhub) : ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> AddMeasurement([FromBody] Measurement measurement)
    {
        await measurementRepository.AddMeasurementAsync(measurement);
        return Ok();
    }

    [HttpGet("{id}")] // This could also benefit from FilterSkip. Not skipped for e.z testing - enjoy.
    public async Task<IActionResult> GetMeasurement(int id)
    {
        var measurement = await measurementRepository.GetMeasurementsAsync(id);
        return measurement != null ? Ok(measurement) : NotFound();
    }

    [HttpGet("patient/{patientSSN}")]
    [SkipFeatureFlagFilter("isUserDoctor")] //Skips isUserDoctor Req from FeatureFlagFilter.
    public async Task<IActionResult> GetMeasurementsByPatientSSN(string patientSSN)
    {
        // A patient should be able to see his or her own Measurements,
        // this will be possible after implementing Log-in functionality.

        var measurements = await measurementRepository.GetMeasurementsByPatientSSNAsync(patientSSN);
        return measurements != null ? Ok(measurements) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] Measurement measurement)
    {
        var updatedMeasurement = await measurementRepository.UpdateMeasurementAsync(id, measurement);
        return updatedMeasurement != null ? Ok(updatedMeasurement) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeasurement(int id)
    {
        var result = await measurementRepository.DeleteMeasurementAsync(id);
        return result ? Ok(result) : NotFound();
    }
}