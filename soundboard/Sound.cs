using System;
using System.Xml.Linq;
using System.ComponentModel;

namespace SoundBoard.Model
{
    #region Delegates
    public delegate void SoundDelegate(Sound xiSound);
    #endregion

    public class Sound : INotifyPropertyChanged
    {
        #region Private members
        private string mTitle;
        private string mFileName;
        #endregion

        #region Public properties
        public string Title
        {
            get
            {
                return mTitle;
            }
            private set
            {
                mTitle = value;
                OnPropertyChanged("Title");
            }
        }

        public string FileName
        {
            get
            {
                return mFileName;
            }
            private set
            {
                mFileName = value;
                OnPropertyChanged("FileName");
            }
        }
        #endregion

        #region Constructors
        public Sound(String xiTitle,
                     String xiFileName)
        {
            Title = xiTitle;
            FileName = xiFileName;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Internal methods
        internal XElement ToXElement()
        {
            XElement element = new XElement("Sound",
                                   new XElement("Title", Title),
                                   new XElement("FileName", FileName));
            return element;
        }
        #endregion

        #region Private methods
        private void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}