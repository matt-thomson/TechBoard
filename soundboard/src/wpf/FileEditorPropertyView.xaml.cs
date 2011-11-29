﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for FileEditorPropertyView.xaml
    /// </summary>
    public partial class FileEditorPropertyView : UserControl
    {
        public FileEditorPropertyView()
        {
            InitializeComponent();
        }

        private void HandleDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PropertyMapping context = DataContext as PropertyMapping;

            // Only set up the binding once.
            if ((context != null) && (e.OldValue == null))
            {
                Binding binding = new Binding
                {
                    Source = context.Target,
                    Path = new PropertyPath(context.Property),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                Field.SetBinding(TextBox.TextProperty, binding);
            }
        }

        private void HandleFileButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Sounds (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                Field.Text = openDialog.FileName;
            }
        }
    }
}
