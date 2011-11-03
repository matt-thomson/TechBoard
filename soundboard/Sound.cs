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
        private double mVolume;
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

        public double Volume
        {
            get
            {
                return mVolume;
            }
            set
            {
                mVolume = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Volume"));
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
            Volume = 0.5;
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
                                   new XElement("FileName", FileName),
                                   new XElement("Volume", Volume));
            return element;
        }
        #endregion
    }
}