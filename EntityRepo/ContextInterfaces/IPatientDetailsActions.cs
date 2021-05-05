using System.Collections.Generic;
using System.Threading.Tasks;
using EntityRepo.CovidAppModels;

namespace EntityRepo.ContextInterfaces
{
    public interface IPatientDetailsActions
    {
        Task<PatientDetails> GetPatientHospitalAddressAndNextOfKinDetailsById(int id);
        Task<PatientDetails> GetPatientDetailsByFirstNameAndLastName(string firstName, string lastName);
        Task<List<PatientDetails>> GetAllPatients();
    }
}
