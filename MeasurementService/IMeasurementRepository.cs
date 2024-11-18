using Data_and_Models;

namespace MeasurementService;

public interface IMeasurementRepository
{
    Task AddMeasurementAsync(Measurement measurement);
    Task<Measurement> GetMeasurementsAsync(int id);
    Task<List<Measurement>> GetMeasurementsByPatientSSNAsync(string patientSSN);
    Task<Measurement> UpdateMeasurementAsync(int id, Measurement measurement);
    Task<bool> DeleteMeasurementAsync(int id);
}