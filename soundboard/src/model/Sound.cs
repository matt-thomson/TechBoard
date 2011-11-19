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

        #region Static methods
        public static Sound FromXElement(XElement xiXElement)
        {
            // Extract the sound properties from the XML element.
            string title = xiXElement.Element("Title").Value;
            string fileName = xiXElement.Element("FileName").Value;

            // Create a new sound.
            Sound sound = new Sound(title, fileName);

            // Set the volume, if it was supplied.
            if (xiXElement.Element("Volume") != null)
            {
                sound.Volume = double.Parse(xiXElement.Element("Volume").Value);
            }

            return sound;
        }
        #endregion

        #region Public methods
        public XElement ToXElement()
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