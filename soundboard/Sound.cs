using System;
using System.Xml.Linq;

namespace SoundBoard.Model
{
    #region Delegates
    public delegate void SoundDelegate(Sound xiSound);
    #endregion

    public class Sound
    {
        #region Public properties
        public string Title { get; private set; }
        public string FileName { get; private set; }
        #endregion

        #region Constructors
        public Sound(String xiTitle,
                     String xiFileName)
        {
            Title = xiTitle;
            FileName = xiFileName;
        }
        #endregion

        #region Public methods
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
