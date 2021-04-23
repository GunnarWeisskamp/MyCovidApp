using System;


namespace EntityRepo.CovidAppModels
{
    public class PatientNextOfKin
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public int? PatientDetailsFk { get; set; }

        public virtual PatientDetails PatientDetailsFkNavigation { get; set; }
    }
}
