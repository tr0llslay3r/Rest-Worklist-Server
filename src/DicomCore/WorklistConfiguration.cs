using System.Collections.Generic;
using System.IO;
using Dicom;
using Newtonsoft.Json;

namespace DicomCore
{
    public class WorklistConfiguration
    {
        public Dictionary<string, DicomTag> WorklistTags { get; set; }
        public Dictionary<string, DicomTag> SpsTags { get; set; }

        public WorklistConfiguration()
        {
            var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "tagmap.json"));
            var map = JsonConvert.DeserializeObject<DicomTagMap>(json);

            WorklistTags = new Dictionary<string, DicomTag>
            {
                {map.AccessionNumber, DicomTag.AccessionNumber},
                {map.AdmissionID, DicomTag.AdmissionID},
                {map.Allergies, DicomTag.Allergies},
                {map.CurrentPatientLocation, DicomTag.CurrentPatientLocation},
                {map.InstitutionalDepartmentName, DicomTag.InstitutionalDepartmentName},
                {map.MedicalAlerts, DicomTag.MedicalAlerts},
                {map.OtherPatientIDs, DicomTag.OtherPatientIDs},
                {map.PatientComments, DicomTag.PatientComments},
                {map.PatientID, DicomTag.PatientID},
                {map.PatientsBirthDate, DicomTag.PatientBirthDate},
                {map.PatientsName, DicomTag.PatientName},
                {map.PatientsSex, DicomTag.PatientSex},
                {map.RequestedProcedureDescription, DicomTag.RequestedProcedureDescription},
                {map.RequestedProcedureID, DicomTag.RequestedProcedureID},
                {map.RequestingPhysician, DicomTag.RequestingPhysician},
                {map.SpecialNeeds, DicomTag.SpecialNeeds},
                {map.SpecificCharacterSet, DicomTag.SpecificCharacterSet},
                {map.StudyDate, DicomTag.StudyDate},
                {map.StudyDescription, DicomTag.StudyDescription},
                {map.StudyInstanceUID, DicomTag.StudyInstanceUID}
            };

            SpsTags = new Dictionary<string, DicomTag>
            {
                {map.ScheduledPerformingPhysiciansName, DicomTag.ScheduledPerformingPhysicianName},
                {map.ScheduledProcedureStepDescription, DicomTag.ScheduledProcedureStepDescription},
                {map.ScheduledProcedureStepID, DicomTag.ScheduledProcedureStepID},
                {map.ScheduledProcedureStepStartDate, DicomTag.ScheduledProcedureStepStartDate},
                {map.ScheduledProcedureStepStartTime, DicomTag.ScheduledProcedureStepStartTime},
                {map.ScheduledStationAETitle, DicomTag.ScheduledStationAETitle},
                {map.ScheduledStationName, DicomTag.ScheduledStationName},
                {map.Modality, DicomTag.Modality}
            };
        }
    }
}