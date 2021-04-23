using System.Collections.Generic;

namespace APICall.Model
{
    public interface IPatientDetailsAPI
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }

        public IList<PatientAddress> PatientAddresses { get; set; }

    }
}
