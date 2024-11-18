using Data_and_Models;

namespace PatientService;

public interface IPatientRepository
{
    Task CreatePatientAsync(Patient patient);
    Task<Patient> GetPatientAsync(string SSN);
    Task<Patient> UpdatePatientAsync(string SSN, Patient patient);
    Task<bool> DeletePatientAsync(string SSN);
}