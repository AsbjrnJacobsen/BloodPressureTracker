using Data_and_Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace PatientService;

public class PatientRepository : IPatientRepository
{
    private readonly BloodPressureContext _context;

    public PatientRepository(BloodPressureContext context)
    {
        _context = context;
    }

    public async Task CreatePatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient> GetPatientAsync(string SSN)
    {
        return await _context.Patients.Include(p => p.Measurements).Where(p => p.SSN == SSN).FirstAsync();
    }
    

    public async Task<Patient> UpdatePatientAsync(string SSN, Patient patient)
    {
        var existingPatient = await GetPatientAsync(SSN);
        if (existingPatient == null) 
            return null;

        existingPatient.SSN = patient.SSN;
        existingPatient.Name = patient.Name;
        existingPatient.Email = patient.Email;
        if (patient.Measurements != null)
            existingPatient.Measurements = patient.Measurements;

        _context.Patients.Update(existingPatient);
        await _context.SaveChangesAsync();

        return existingPatient;
    }

    public async Task<bool> DeletePatientAsync(string SSN)
    {
        var patient = await GetPatientAsync(SSN);
        if(patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
