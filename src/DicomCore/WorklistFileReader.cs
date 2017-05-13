using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Dicom;

namespace DicomCore
{
    public class WorklistFileReader
    {
        public IEnumerable<DicomDataset> ReadWorklistItems()
        {
            List<DicomDataset> datasets = new List<DicomDataset>();
            foreach (string fileName in Directory.GetFiles(@"K:\worklist", "*.*", SearchOption.TopDirectoryOnly))
            {
                Debug.WriteLine("Reading file: " + fileName);
                var fileLines = File.ReadAllLines(fileName);
                if (fileLines.Length != 2)
                {
                    Debug.WriteLine("{0} could not be read. file must contain 2 lines. LineCount: {1}", fileName, fileLines.Length);
                }

                var fileKeys = fileLines[0].Split('\t');
                var fileValues = fileLines[1].Split('\t');

                var keysCount = fileKeys.Length;
                var valuesCount = fileValues.Length;
                if (keysCount != valuesCount)
                {
                    Debug.WriteLine("{0} could not be read, illegal count of fields. Keys: {1}, Value :{2}", fileName, keysCount, valuesCount);
                    continue;
                }

                var dataSet = new DicomDataset();
                var sps = new DicomDataset();
                
                var dictDataset = new Dictionary<string, DicomTag>()
                {
                    {"AccessionNumber", DicomTag.AccessionNumber},
                    {"AdmissionID", DicomTag.AdmissionID},
                    {"Allergies", DicomTag.Allergies},
                    {"CurrentPatientLocation", DicomTag.CurrentPatientLocation},
                    {"InstitutionalDepartmentName", DicomTag.InstitutionalDepartmentName},
                    {"MedicalAlerts", DicomTag.MedicalAlerts},
                    {"OtherPatientIDs", DicomTag.OtherPatientIDs},
                    {"PatientComments", DicomTag.PatientComments},
                    {"PatientID", DicomTag.PatientID},
                    {"PatientsBirthDate", DicomTag.PatientBirthDate},
                    {"PatientsName", DicomTag.PatientName},
                    {"PatientsSex", DicomTag.PatientSex},
                    {"RequestedProcedureDescription", DicomTag.RequestedProcedureDescription},
                    {"RequestedProcedureID", DicomTag.RequestedProcedureID},
                    {"RequestingPhysician", DicomTag.RequestingPhysician},
                    {"SpecialNeeds", DicomTag.SpecialNeeds},
                    {"SpecificCharacterSet", DicomTag.SpecificCharacterSet},
                    {"StudyDate", DicomTag.StudyDate},
                    {"StudyDescription", DicomTag.StudyDescription},
                    {"StudyInstanceUID", DicomTag.StudyInstanceUID},
                };

                var dictSps = new Dictionary<string, DicomTag>()
                {
                    {"ScheduledPerformingPhysiciansName", DicomTag.ScheduledPerformingPhysicianName},
                    {"ScheduledProcedureStepDescription", DicomTag.ScheduledProcedureStepDescription},
                    {"ScheduledProcedureStepID", DicomTag.ScheduledProcedureStepID},
                    {"ScheduledProcedureStepStartDate", DicomTag.ScheduledProcedureStepStartDate},
                    {"ScheduledProcedureStepStartTime", DicomTag.ScheduledProcedureStepStartTime},
                    {"ScheduledStationAETitle", DicomTag.ScheduledStationAETitle},
                    {"ScheduledStationName", DicomTag.ScheduledStationName},
                    {"Modality", DicomTag.Modality},

                };

                for (int i = 0; i < fileKeys.Length -1; i++)
                {
                    if (dictDataset.ContainsKey(fileKeys[i]))
                    {
                        var tag = dictDataset[fileKeys[i]];
                        dataSet.Add(tag, fileValues[i]);
                    }
                    else if (dictSps.ContainsKey(fileKeys[i]))
                    {
                        var tag = dictSps[fileKeys[i]];
                        sps.Add(tag, fileValues[i]);
                    }
                    else
                    {
                        Debug.WriteLine("ERROR: Key is not in Dictionary: {0}", fileKeys[i]);
                    }
                }
                
                dataSet.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));
                datasets.Add(dataSet);
            }

            return datasets;
        }
    }
}
