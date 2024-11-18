using Data_and_Models;
using Microsoft.EntityFrameworkCore;

namespace MeasurementService;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly BloodPressureContext _context;

    public MeasurementRepository(BloodPressureContext context)
    {
        _context = context;
    }
    
    public async Task AddMeasurementAsync(Measurement measurement)
    {
        _context.Measurements.Add(measurement);
        await _context.SaveChangesAsync();
    }

    public async Task<Measurement> GetMeasurementsAsync(int id)
    {
        return await _context.Measurements.Include(m => m.Patient).FirstAsync(m => m.Id == id);
    }

    public async Task<List<Measurement>> GetMeasurementsByPatientSSNAsync(string patientSSN)
    {
        return await _context.Measurements
            .Where(m => m.PatientSSN == patientSSN)
            .ToListAsync();
    }

    public async Task<Measurement> UpdateMeasurementAsync(int id, Measurement measurement)
    {
        var existingMeasurement = await GetMeasurementsAsync(id);

        existingMeasurement.Id = measurement.Id;
        existingMeasurement.Date = measurement.Date;
        existingMeasurement.Systolic = measurement.Systolic;
        existingMeasurement.Diastolic = measurement.Diastolic;
        existingMeasurement.PatientSSN = measurement.PatientSSN;
        existingMeasurement.Seen = measurement.Seen;
        if (measurement.Patient != null)
            existingMeasurement.Patient = measurement.Patient;
        
        _context.Measurements.Update(existingMeasurement);
        await _context.SaveChangesAsync();
        return existingMeasurement;
    }

    public async Task<bool> DeleteMeasurementAsync(int id)
    {
        var measurement = await GetMeasurementsAsync(id);
        if (measurement != null)
        {
            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

}