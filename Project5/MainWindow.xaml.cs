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
        List<Polygon> polygonList = new List<Polygon>();
        List<Polyline> polylineList = new List<Polyline>();
        List<Ellipse> pointList = new List<Ellipse>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openButtonClick(object sender, RoutedEventArgs e)
        {
            
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
                            polygonList.Add(tempPoly);
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
                                tempLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                            else if (line[1] == "road")
                                tempLine.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                            else
                                tempLine.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                            polylineList.Add(tempLine);
                            mapCanvas.Children.Add(tempLine);
                        }
                        else
                        {

                            Ellipse tempElip = new Ellipse();
                            tempElip.Width = 10;
                            tempElip.Height = tempElip.Width;
                            Canvas.SetLeft(tempElip, Convert.ToDouble(line[2]) - 5);
                            Canvas.SetTop(tempElip, Convert.ToDouble(line[3]) - 5);
                            if (line[1] == "city")
                                tempElip.Fill = new SolidColorBrush(Color.FromRgb(0,0,0));
                            else
                                tempElip.Fill = new SolidColorBrush(Color.FromRgb(150, 150, 0));
                            pointList.Add(tempElip);
                            mapCanvas.Children.Add(tempElip);
                        }
                        
                    }
                    foreach (var k in polygonList)
                    {
                        itemList.Items.Add(1);
                    }
                    foreach (var k in polylineList)
                    {
                        itemList.Items.Add(2);
                    }
                    foreach (var k in pointList)
                    {
                        itemList.Items.Add(3);
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
