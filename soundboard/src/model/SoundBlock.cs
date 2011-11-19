using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace SoundBoard.Model
{
    public class SoundBlock
    {
        #region Public properties
        public ObservableCollection<Sound> Sounds { get; private set; }
        #endregion
        
        #region Constructors
        public SoundBlock() 
        {
            Sounds = new ObservableCollection<Sound>();
        }
        #endregion

        #region Static methods
        public static SoundBlock FromXElement(XElement xiXElement)
        {
            // Create a new sound block.
            SoundBlock soundBlock = new SoundBlock();

            // Extract the sounds from the XML element.
            foreach (XElement soundElement in xiXElement.Descendants("Sound"))
            {
                Sound sound = Sound.FromXElement(soundElement);
                soundBlock.Sounds.Add(sound);
            }

            return soundBlock;
        }
        #endregion

        #region Public methods
        public XElement ToXElement()
        {
            // Create an XML element for this sound block.
            XElement blockElement = new XElement("Sounds");

            foreach (Sound sound in Sounds)
            {
                blockElement.Add(sound.ToXElement());
            }

            return blockElement;
        }
        #endregion
    }
}
