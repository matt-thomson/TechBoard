using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Xml.Linq;
using SoundBoard.WPF;

namespace SoundBoard.Model
{
    public class SoundBlock : DependencyObject
    {
        #region Private members
        // TODO shouldn't be in the model
        private static DataTemplate mDataTemplate = null;
        private static DependencyProperty TitleProperty = DependencyProperty.Register("Title",
                                                                                      typeof(string),
                                                                                      typeof(SoundBlock));
        private static DependencyProperty VolumeProperty = DependencyProperty.Register("Volume",
                                                                                       typeof(double),
                                                                                       typeof(SoundBlock));
        private static DependencyProperty FileNameProperty = DependencyProperty.Register("FileName",
                                                                                         typeof(string),
                                                                                         typeof(SoundBlock));
        #endregion

        #region Public properties
        [FileNameEditorProperty]
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        [TextEditorProperty]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        [RangeEditorProperty]
        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }
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
        public SoundBlock()
        {
            Title = "New Sound";
            FileName = "";
            Volume = 0.5;
        }
        #endregion
    }
}
