using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace SoundBoard.Model
{
    #region Delegates
    public delegate void SoundDelegate(Sound xiSound);
    #endregion

    public class Sound : INotifyPropertyChanged
    {
        #region Private members
        private string mTitle;
        #endregion

        #region Public properties
        public string Title
        {
            get
            {
                return mTitle;
            }
            set
            {
                mTitle = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }
        }

        public string FileName { get; set; }
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
    }
}