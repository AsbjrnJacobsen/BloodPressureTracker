using Data_and_Models;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService;

[ApiController]
[Route("api/[controller]")]
public class MeasurementController : ControllerBase
{
    private readonly IMeasurementRepository _measurementRepository;

    public MeasurementController(IMeasurementRepository measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddMeasurement([FromBody] Measurement measurement)
    {
        await _measurementRepository.AddMeasurementAsync(measurement);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMeasurement(int id)
    {
        var measurement = await _measurementRepository.GetMeasurementsAsync(id);
        return measurement != null ? Ok(measurement) : NotFound();
    }

    [HttpGet("patient/{patientSSN}")]
    public async Task<IActionResult> GetMeasurementsByPatientSSN(string patientSSN)
    {
        var measurements = await _measurementRepository.GetMeasurementsByPatientSSNAsync(patientSSN);
        //Maybe: if list.any 
        return measurements != null ? Ok(measurements) : NotFound();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] Measurement measurement)
    {
        var updatedMeasurement = await _measurementRepository.UpdateMeasurementAsync(id, measurement);
        return updatedMeasurement != null ? Ok(updatedMeasurement) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeasurement(int id)
    {
        var result = await _measurementRepository.DeleteMeasurementAsync(id);
        return result ? Ok(result) : NotFound();
    }
}