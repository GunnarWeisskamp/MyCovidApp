namespace EntityRepo.CovidAppModels
{
    public partial class PatientAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int? PatientDetailsFk { get; set; }

        public PatientDetails PatientDetailsFkNavigation { get; set; }
    }
}
