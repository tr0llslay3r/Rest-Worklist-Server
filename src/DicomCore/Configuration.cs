using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dicom;

namespace DicomCore
{
    public static class Configuration
    {
        public static string StoragePath { get; set; }
        public static string StationName { get; set; }
        public static IEnumerable<DicomDataset> WorklistItems { get; set; }
        public static ObservableCollection<DicomConvertedItem> MyCollection { get; set; }
        public static ObservableCollection<LogItem> LogList { get; set; }
    }
}
