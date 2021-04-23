namespace EntityRepo.CovidAppModels
{
    public partial class PatientHospitalCovidTest
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public string Notes { get; set; }
        public int? PatientHospitalIdFk { get; set; }
        public int? PatientDetailsIdFk { get; set; }

        public PatientDetails PatientDetailsIdFkNavigation { get; set; }
        public PatientHospital PatientHospitalIdFkNavigation { get; set; }
    }
}
