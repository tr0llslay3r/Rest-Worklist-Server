namespace DicomCore
{
    public class DicomTagMap
    {
        public string AccessionNumber { get; set; } 
        public string AdmissionID { get; set; } 
        public string Allergies { get; set; } 
        public string CurrentPatientLocation { get; set; } 
        public string InstitutionalDepartmentName { get; set; } 
        public string MedicalAlerts { get; set; } 
        public string OtherPatientIDs { get; set; } 
        public string PatientComments { get; set; } 
        public string PatientID { get; set; } 
        public string PatientsBirthDate { get; set; } 
        public string PatientsName { get; set; } 
        public string PatientsSex { get; set; } 
        public string RequestedProcedureDescription { get; set; } 
        public string RequestedProcedureID { get; set; } 
        public string RequestingPhysician { get; set; } 
        public string SpecialNeeds { get; set; } 
        public string SpecificCharacterSet { get; set; } 
        public string StudyDate { get; set; } 
        public string StudyDescription { get; set; } 
        public string StudyInstanceUID { get; set; }

        public string ScheduledPerformingPhysiciansName { get; set; }
        public string ScheduledProcedureStepDescription { get; set; }
        public string ScheduledProcedureStepID { get; set; }
        public string ScheduledProcedureStepStartDate { get; set; }
        public string ScheduledProcedureStepStartTime { get; set; }
        public string ScheduledStationAETitle { get; set; }
        public string ScheduledStationName { get; set; }
        public string Modality { get; set; }
    }
}