using System.Collections;
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

                //TODO: now add DicomDataSet from fileKeys and fileValues to datasets
                //key must be parsed to DicomTag enum and value must be added to DataSet...
                //dataSet.Add((DicomTag, value);
                //sps is another dataset in the dataset, sps items must be put in there

                dataSet.Add(new DicomSequence(DicomTag.ScheduledProcedureStepSequence, sps));
                datasets.Add(dataSet);
            }

            //TODO return datasets
            return RandomStuffFactory.CreateRandomDatasetList(10);
        }
    }
}
