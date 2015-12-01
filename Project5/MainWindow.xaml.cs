using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Project5
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

        private void openButtonClick(object sender, RoutedEventArgs e)
        {
            Point tempPoint = new Point();
            Ellipse tempElip = new Ellipse();
            tempElip.Width = 10;
            tempElip.Height = tempElip.Width;
            string[] line;
            OpenFileDialog read = new OpenFileDialog();
            if (read.ShowDialog() == true)
            {
                try
                {
                    string[] text = File.ReadAllLines(read.FileName);
                    foreach (string item in text)
                    {
                        line = item.Split(' ');
                        if (line[0] == "polygon")
                        {

                            Polygon tempPoly = new Polygon();
                            for (int i = 2; i < line.Length - 1; i+=2)
                            {
                                tempPoly.Points.Add(new Point(Convert.ToDouble(line[i]), Convert.ToDouble(line[i + 1])));
                            }
                            if (line[1] == "land")
                                tempPoly.Fill = new SolidColorBrush(Color.FromRgb(100, 255, 100));
                            else
                                tempPoly.Fill = new SolidColorBrush(Color.FromRgb(100, 100, 255));
                                mapCanvas.Children.Add(tempPoly);
                        }
                        else if (line[0] == "line")
                        {
                            Polyline tempLine = new Polyline();
                            for (int i = 2; i < line.Length - 1; i += 2)
                            {
                                tempLine.Points.Add(new Point(Convert.ToDouble(line[i]), Convert.ToDouble(line[i + 1])));
                            }
                            if (line[1] == "river")
                                tempLine.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                            else if (line[1] == "road")
                                tempLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                            else
                                tempLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            mapCanvas.Children.Add(tempLine);
                        }
                        else
                        {
                            mapCanvas.Children.Add(tempElip);
                        }

                    }
                }
                catch (IOException i)
                {
                    Rectangle elip = new Rectangle();
                    elip.Height = 25;
                    elip.Width = 25;

                    Canvas.SetLeft(elip, 25);
                    Canvas.SetTop(elip, 25);

                    elip.Fill = new SolidColorBrush(Color.FromArgb(255, 155, 155, 155));

                    mapCanvas.Children.Add(elip);
                }
            }
        }
    }
}
