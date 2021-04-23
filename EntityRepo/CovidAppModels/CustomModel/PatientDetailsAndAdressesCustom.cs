using System.Collections.Generic;

namespace EntityRepo.CovidAppModels.CustomModel
{
    public class PatientDetailsAndAdressesCustom
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }

        public List<PatientAddressCustom> PatientAddresses { get; set; }
    }
}
