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
using KB_CloudVision;

namespace WpfAppGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Services service;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Datei_auswaehlen_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog
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

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathBox.Text = @openFileDialog1.FileName;
            }
        }


        private void GesichtseingrenzungChanged(object sender, RoutedEventArgs e)
        {
            if (service != null)
                CalcImage();
        }

        private void LandmarksChanged(object sender, RoutedEventArgs e)
        {
            if (service != null)
                CalcImage();
        }

        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Labels.Text = "Loading...";
            service = new Services(@PathBox.Text);
            Labels.Text = "Loaded";
            CalcImage();

        }

        private void CalcImage()
        {
            service.Reset();
            if (___Gesichtseingrenzung.IsChecked.HasValue && ___Gesichtseingrenzung.IsChecked.Value)
            {
                service.drawBoundedPolygon();
            }
            if (Landmarks.IsChecked.HasValue && Landmarks.IsChecked.Value)
            {
                service.drawLandmarks();
            }

            Labels.Text = (String.Join("\n", service.getLabels().Select(o => o.Description)));
            System.Drawing.Image inputImage = System.Drawing.Image.FromFile(PathBox.Text);
            image.Source = service.Convert(inputImage);
            aimage.Source = service.ProcessedImage();

        }
    }

}
