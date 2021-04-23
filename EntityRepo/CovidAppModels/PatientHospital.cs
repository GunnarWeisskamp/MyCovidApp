using System;
using System.Collections.Generic;

namespace EntityRepo.CovidAppModels
{
    public partial class PatientHospital
    {
        public PatientHospital()
        {
            PatientHospitalCovidTests = new HashSet<PatientHospitalCovidTest>();
        }
        public int Id { get; set; }
        public string HospitalName { get; set; }
        public DateTime? DateOfTest { get; set; }
        public string HealthCarerName { get; set; }
        public int? PatientDetailsIdFk { get; set; }

        public PatientDetails PatientDetailsIdFkNavigation { get; set; }
        public ICollection<PatientHospitalCovidTest> PatientHospitalCovidTests { get; set; }

    }
}
