using System.Collections.Generic;
using System.Linq;
using Dicom;

namespace DicomCore
{
    public class WorklistRepository
    {
        private readonly WorklistFileReader m_WorklistFileReader;

        public IEnumerable<DicomDataset> WorklistItems => ReadWorklistItems();

        public WorklistRepository()
        {
            m_WorklistFileReader = new WorklistFileReader();
        }

        private IEnumerable<DicomDataset> ReadWorklistItems()
        {
            var worklistItems = m_WorklistFileReader.ReadWorklistItems();
            return worklistItems;
        }

        public List<Dictionary<string, string>> GetReadableWorklistItems()
        {

            var myCollection = new List<Dictionary<string, string>>();
            foreach (var worklistItem in WorklistItems)
            {
                var wi = new Dictionary<string, string>();
                foreach (var w in worklistItem)
                {
                    if (w.ValueRepresentation != DicomVR.SQ)
                        wi.Add(w.Tag.DictionaryEntry.Keyword, worklistItem.Get<string>(w.Tag, ""));
                    else
                    {
                        var spsSequence = worklistItem.Get<DicomSequence>(DicomTag.ScheduledProcedureStepSequence).First();
                        foreach (var spsSequenceItem in spsSequence)
                        {
                            wi.Add(spsSequenceItem.Tag.DictionaryEntry.Keyword, spsSequence.Get<string>(spsSequenceItem.Tag, ""));
                        }
                    }
                }

                myCollection.Add(wi);
            }

            return myCollection;
        }
    }
}
