using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Xml.Linq;
using SoundBoard.WPF;

namespace SoundBoard.Model
{
    public class SoundBlock : INotifyPropertyChanged
    {
        #region Private members
        private string mTitle;
        private double mVolume;

        // TODO shouldn't be in the model
        private static DataTemplate mDataTemplate = null;
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

        #region Static properties
        // TODO shouldn't be in the model
        public static DataTemplate DataTemplate
        {
            get
            {
                if (mDataTemplate == null)
                {
                    mDataTemplate = new DataTemplate();
                    mDataTemplate.VisualTree = new FrameworkElementFactory(typeof(SoundBlockView));
                }

                return mDataTemplate;
            }
        }
        #endregion

        #region Constructors
        public SoundBlock(string xiTitle,
                          string xiFileName) 
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
        public static SoundBlock FromXElement(XElement xiXElement)
        {
            // Extract the soundBlock properties from the XML element.
            string title = xiXElement.Element("Title").Value;
            string fileName = xiXElement.Element("FileName").Value;

            // Create a new sound block.
            SoundBlock soundBlock = new SoundBlock(title, fileName);

            // Set the volume, if it was supplied.
            if (xiXElement.Element("Volume") != null)
            {
                soundBlock.Volume = double.Parse(xiXElement.Element("Volume").Value);
            }

            return soundBlock;
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
