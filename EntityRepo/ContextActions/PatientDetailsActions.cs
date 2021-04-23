using System.Linq;
using System.Threading.Tasks;
using EntityRepo.ContextInterfaces;
using EntityRepo.CovidAppModels;
using Microsoft.EntityFrameworkCore;

namespace EntityRepo.ContextActions
{
    public class PatientDetailsActions : CovidAppContext, IPatientDetailsActions
    {
        public async Task<PatientDetails> GetPatientHospitalAddressAndNextOfKinDetailsById(int id)
        {
            using (var context = new CovidAppContext())
            {
                var patDetails = context.PatientDetails.Include(hos => hos.PatientHospital).
                    Include(kin => kin.PatientNextOfKins).Include(add => add.PatientAddresses).
                    Where(a => a.Id == id).SingleOrDefaultAsync();
                return await patDetails;
            }
        }

        public async Task<PatientDetails> GetPatientDetailsByFirstNameAndLastName(string firstName, string lastName)
        {
            using (var context = new CovidAppContext())
            {
                var patDetails = context.PatientDetails.Include(hos => hos.PatientHospital).
                    Include(kin => kin.PatientNextOfKins).Include(add => add.PatientAddresses).
                    Where(a => a.FirstName == firstName && a.LastName == lastName).SingleOrDefaultAsync();
                return await patDetails;
            }
        }
    }
}
