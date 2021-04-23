using System;
using System.Threading.Tasks;

namespace EntityRepo.ContextInterfaces
{
    public interface IPatientStoredProcedureActions
    {
        Task<string> sp_InsertNewPatientDetailsAndAddress(string FirstName, string LastName, int Age, string Street, string Suburb, string State, string HospitalName, DateTime DateOfTest, string HealthCarerName, Boolean Result, string Notes);

        Task<string> sp_UpdatePatientAddress(int id, string Street, string Suburb, string State);
        Task<string> sp_UpdatePatientNextOfKin(Guid id, string PhoneNumber, string Name, string Relationship);
    }
}
