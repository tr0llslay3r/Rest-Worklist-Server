using System.ComponentModel;

namespace DicomCore
{
    public class DicomConvertedItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private string m_Id;
        public string Id
        {
            get
            {
                return m_Id;
            }
            set
            {
                m_Id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string m_ScheduledStationName;
        public string ScheduledStationName
        {
            get
            {
                return m_ScheduledStationName;
            }
            set
            {
                m_ScheduledStationName = value;
                NotifyPropertyChanged("ScheduledStationName");
            }
        }

        private string m_Status;
        public string Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private string m_ScheduledDate;
        public string ScheduledDate
        {
            get
            {
                return m_ScheduledDate;
            }
            set
            {
                m_ScheduledDate = value;
                NotifyPropertyChanged("ScheduledDate");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}