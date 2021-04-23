using System.Collections.Generic;

namespace EntityRepo.CovidAppModels
{
    public partial class PatientDetails
    {
        public PatientDetails()
        {
            PatientAddresses = new HashSet<PatientAddress>();
            PatientHospital = new HashSet<PatientHospital>();
            PatientHospitalCovidTest = new HashSet<PatientHospitalCovidTest>();
            PatientNextOfKins = new HashSet<PatientNextOfKin>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }

        public ICollection<PatientAddress> PatientAddresses { get; set; }
        public ICollection<PatientHospital> PatientHospital { get; set; }
        public ICollection<PatientHospitalCovidTest> PatientHospitalCovidTest { get; set; }
        public ICollection<PatientNextOfKin> PatientNextOfKins { get; set; }
    }
}
