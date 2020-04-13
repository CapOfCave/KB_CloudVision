using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace WpfAppGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Datei_auswählen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "jpg",
                Filter = "jpg files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Path.Text = openFileDialog1.FileName;
            }
        }
    

        private static void OpenExplorer(string path)
        {
            if (Directory.Exists("C:\temp"))
                Process.Start("explorer.exe", "C:\temp");
        }

        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Exit(0);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (___Gesichtseingrenzung.IsChecked)
            {

            }
            else if (Landmarks.IsChecked)
            {

            }
            else if (___Gesichtseingrenzung.IsChecked)
            {

            }
            else if (Signifikante_Punkte.IsChecked)
            {

            }
            else
            {
                Console.WriteLine("Anfrage verarbeitet!");
            }
        }
    }
}
