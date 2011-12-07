using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for FileBlockPropertyEditor.xaml
    /// </summary>
    public partial class FileBlockPropertyEditor : UserControl
    {
        public FileBlockPropertyEditor()
        {
            InitializeComponent();
        }

        private void HandleFileButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            // TODO needs to be a parameter
            openDialog.Filter = "Sounds (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                Field.Text = openDialog.FileName;
            }
        }
    }
}
