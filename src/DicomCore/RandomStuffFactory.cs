using System;
using System.Collections.Generic;
using Dicom;

namespace DicomCore
{
    public class RandomStuffFactory
    {
        private static readonly Random m_Random = new Random();

        public static IEnumerable<DicomDataset> CreateRandomDatasetList(int count)
        {
            var dataSetList = new List<DicomDataset>();

            for (int i = 0; i < count; i++)
            {
                dataSetList.Add(CreateRandomDataset(i, Configuration.StationName, true));
            }

            return dataSetList;
        }

        private static string CreateRandomGender()
        {
            return m_Random.Next(2) == 0 ? "M" : "F";
        }

        private static string CreateStationName()
        {
            return m_Random.Next(2) == 0 ? Configuration.StationName : "OTHER_STATION";
        }

        private static string CreateRandomPregnancyStatus()
        {
            return m_Random.Next(5).ToString();
        }

        private static DicomDataset CreateRandomDataset(int index, string stationName, bool isToday)
        {
            var dataSet = new DicomDataset();

            dataSet.Add(DicomTag.PatientID, string.Format("ID_{0}", index));
            dataSet.Add(DicomTag.PatientName, NameGenerator.GenerateDicomFirstAndLastName());
            dataSet.Add(DicomTag.OtherPatientIDs, string.Format("OTHER_ID_{0}", index));
            dataSet.Add(DicomTag.PatientSex, CreateRandomGender());
            dataSet.Add(new DicomDate(DicomTag.PatientBirthDate, DateTime.Now));
            dataSet.Add(DicomTag.MedicalAlerts, "");
            dataSet.Add(DicomTag.PregnancyStatus, CreateRandomPregnancyStatus());
            dataSet.Add(DicomTag.Allergies, "");

            dataSet.Add(DicomTag.PatientComments, string.Format("Comment_{0}", index));
            dataSet.Add(DicomTag.SpecialNeeds, "");
            dataSet.Add(DicomTag.CurrentPatientLocation, NameGenerator.GenerateNew());
            dataSet.Add(DicomTag.InstitutionalDepartmentName, NameGenerator.GenerateNew());
            dataSet.Add(DicomTag.AdmissionID, "111");
            dataSet.Add(DicomTag.AccessionNumber, "123");
            dataSet.Add(DicomTag.ReferringPhysicianName, NameGenerator.GenerateDicomFirstAndLastName());
            dataSet.Add(DicomTag.RequestingPhysician, "Schorsch Dr.");
            dataSet.Add(DicomTag.StudyInstanceUID, "1223325235");
            dataSet.Add(DicomTag.StudyDescription, NameGenerator.GenerateNew());
            dataSet.Add(DicomTag.StudyID, string.Format("StudyID_{0}", index));
            dataSet.Add(DicomTag.StudyDate, "20160728");
            dataSet.Add(DicomTag.StudyTime, "210000");
            dataSet.Add(DicomTag.RequestedProcedureID, "TODO");
            dataSet.Add(DicomTag.RequestedProcedureDescription, NameGenerator.GenerateNew());


            var sps = new DicomDataset();
            sps.Add(DicomTag.ScheduledStationAETitle, NameGenerator.GenerateNew());
            sps.Add(DicomTag.ScheduledStationName, CreateStationName());
            sps.Add(new DicomDate(DicomTag.ScheduledProcedureStepStartDate, DateTime.Now));
            sps.Add(DicomTag.ScheduledProcedureStepStartTime, "175821");
            sps.Add(DicomTag.Modality, "XC");
            sps.Add(DicomTag.ScheduledPerformingPhysicianName, "TODO");
            sps.Add(DicomTag.ScheduledProcedureStepDescription, "TODO");
            sps.Add(DicomTag.ScheduledProcedureStepLocation, "TODO");
            sps.Add(DicomTag.ScheduledProcedureStepID, "TODO");
            dataSet.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));

            return dataSet;


            //unnötiges, was wir abfragen:
            //new DicomDataset
            //           {
            //               { DicomTag.SeriesDate, "" },
            //               { DicomTag.SeriesTime, "" },

            //               { DicomTag.AccessionNumber, index.ToString() },
            //               { DicomTag.PerformingPhysicianName, NameGenerator.GenerateDicomFirstAndLastName() },
            //               { DicomTag.NameOfPhysiciansReadingStudy, NameGenerator.GenerateDicomFirstAndLastName() },

            //               { DicomTag.SeriesInstanceUID, "1223325235" },
            //               { DicomTag.SeriesNumber, string.Format("SeriesNumber_{0}", index) },
            
            //           };

        }

        private static DicomDate GenerateDate(bool isToday)
        {
            var today = DateTime.Now;
            DicomDate date = new DicomDate(DicomTag.PatientBirthDate, today);
            
            return date;
        }
    }
}
