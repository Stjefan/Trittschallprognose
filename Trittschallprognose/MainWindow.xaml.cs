﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace Trittschallprognose
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

        private void Label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lblFrom = e.Source as Label;

            if (e.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(lblFrom, lblFrom.Content, DragDropEffects.Copy);
        }

        private void Label1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            Label lblFrom = e.Source as Label;

            if (!e.KeyStates.HasFlag(DragDropKeyStates.LeftMouseButton))
                lblFrom.Content = "...";
        }

        private void Label2_Drop(object sender, DragEventArgs e)
        {
            string draggedText = (string)e.Data.GetData(DataFormats.StringFormat);

            Label toLabel = e.Source as Label;
            (sender as Label).Content = draggedText;
            // toLabel.Content = draggedText;
        }
    }

    public class Schalldaten : INotifyPropertyChanged
    {
        private int devlopmentDouble = 50;


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Uri Uri { get; set; }
        public ObservableCollection<ABauelementTreppenaufbau> Treppenaufbau { get; set; }

        public ObservableCollection<Einzelelement> Einzelelemente { get; set; }

        public Einzelelement SelectedEinzelelement { get => selectedEinzelelement; set { selectedEinzelelement = value; NotifyPropertyChanged(); } }
        private BitmapImage myImage;
        private Einzelelement selectedEinzelelement;
        private Einzelelement schichtC_Element;
        private Einzelelement schichtB_Element;
        private Einzelelement schichtA_Element;

        public BitmapImage MyImage { get => myImage; set { myImage = value; NotifyPropertyChanged(); } }

        public ImageCollection Images { get; set; }

        public Fliese Fliese { get; set; }
        public Estrich Estrich { get; set; }
        public Ditra Ditra { get; set; }

        public Beton Beton { get; set; }
        public Daemmung Daemmung { get; set; }
        public Schalldaemmung Schalldaemmung { get; set; }
        public int DevlopmentDouble { get => devlopmentDouble; set { devlopmentDouble = value; NotifyPropertyChanged(); } }


        public ObservableCollection<Einzelelement> SchichtA { get; set; }

        public Einzelelement SchichtA_Element { get => schichtA_Element; set { schichtA_Element = value; NotifyPropertyChanged(); } }
        public ObservableCollection<Einzelelement> SchichtB { get; set; }
        public Einzelelement SchichtB_Element { get => schichtB_Element; set { schichtB_Element = value; NotifyPropertyChanged(); } }

        public ObservableCollection<Einzelelement> SchichtC { get; set; }
        public Einzelelement SchichtC_Element { get => schichtC_Element; set { schichtC_Element = value; NotifyPropertyChanged(); } }
        private void ErstelleEinzelelmente()
        {
            Einzelelemente = new ObservableCollection<Einzelelement>()
            {
                new Einzelelement("Stahlbeton", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_beton_r_cropped.jpg"), null),
                new Einzelelement("BT 12fk (8 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_12fk_8mm_r_cropped.jpg"), 8),
                new Einzelelement("BT 23f Estrich (20 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_23f_estrich_20mm_r_cropped.jpg"), 20),
                new Einzelelement("BT 23f Estrich (8 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_23f_estrich_8mm_r_cropped.jpg"), 8),
                new Einzelelement("BT 2520 Estrich (20 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_2520_estrich_20mm_r_cropped.jpg"), 20),
                new Einzelelement("BT 2520 Estrich (8 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_2520_estrich_8mm_r_cropped.jpg"), 8),
                new Einzelelement("BT Ditra Heat Kleber (2 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_ditra_heat_kleber_2mm_r_cropped.jpg"), 2),
                new Einzelelement("BT Ditra25 Kleber (1 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_ditra25_kleber_1mm_r_cropped.jpg"), 1),
                new Einzelelement("BT Fliesenkleber (3 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_fliesen_kleber_3mm_r_cropped.jpg"), 3),
                new Einzelelement("Cemwood", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_cemwood_r_cropped.jpg"), null),
                new Einzelelement("EPS (15 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_15mm_r_cropped.jpg"), 15),
                new Einzelelement("EPS (30 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_30mm_r_cropped.jpg"), 30),
                new Einzelelement("EPS (35 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_35mm_r_cropped.jpg"), 35),
                new Einzelelement("Gutex Thermofloor", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_gutex_thermofloor_r_cropped.jpg"), null),
                new Einzelelement("Isover", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_isover_r_cropped.jpg"), null),
                new Einzelelement("Kerdi-Line-SR", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_kerdi-line-sr_r_cropped.jpg"), null),
                new Einzelelement("Rockwoll-HP", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_rockwoll-HP_r_cropped.jpg"), null),
                new Einzelelement("Rockwoll-te", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_rockwoll-te_r_cropped.jpg"), null),
                new Einzelelement("Thermowhite (55 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_thermowhite_55mm_r_cropped.jpg"), 55),
                new Einzelelement("Thermowhite (60 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_thermowhite_60mm_r_cropped.jpg"), 60),
                new Einzelelement("Thermowhite (70 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_thermowhite_70mm_r_cropped.jpg"), 70),
            };

            SchichtA = new ObservableCollection<Einzelelement>()
            {
                new Einzelelement("Thermowhite (55 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_thermowhite_55mm_r_cropped.jpg"), 55),
                new Einzelelement("Thermowhite (60 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_thermowhite_60mm_r_cropped.jpg"), 60),
                new Einzelelement("Thermowhite (70 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_thermowhite_70mm_r_cropped.jpg"), 70),
            };

            SchichtB = new ObservableCollection<Einzelelement>()
            {
                new Einzelelement("BT 12fk (8 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_12fk_8mm_r_cropped.jpg"), 8),
                new Einzelelement("BT 23f Estrich (20 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_23f_estrich_20mm_r_cropped.jpg"), 20),
                new Einzelelement("BT 23f Estrich (8 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_23f_estrich_8mm_r_cropped.jpg"), 8),
                new Einzelelement("BT 2520 Estrich (20 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_2520_estrich_20mm_r_cropped.jpg"), 20),
                new Einzelelement("BT 2520 Estrich (8 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_2520_estrich_8mm_r_cropped.jpg"), 8),
                new Einzelelement("BT Ditra Heat Kleber (2 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_ditra_heat_kleber_2mm_r_cropped.jpg"), 2),
                new Einzelelement("BT Ditra25 Kleber (1 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_ditra25_kleber_1mm_r_cropped.jpg"), 1),
                new Einzelelement("BT Fliesenkleber (3 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_fliesen_kleber_3mm_r_cropped.jpg"), 3),
            };

            SchichtC = new ObservableCollection<Einzelelement>()
            {
                new Einzelelement("EPS (15 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_15mm_r_cropped.jpg"), 15),
                new Einzelelement("EPS (30 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_30mm_r_cropped.jpg"), 30),
                new Einzelelement("EPS (35 mm)", new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_35mm_r_cropped.jpg"), 35),
            };
        }

        public Schalldaten()
        {


            ErstelleEinzelelmente();
            Fliese = new Fliese() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_fliesen_kleber_3mm_r_cropped.jpg") };
            Estrich = new Estrich() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_23f_estrich_20mm_r_cropped.jpg") };
            Ditra = new Ditra() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_ditra25_kleber_1mm_r_cropped.jpg") };
            Beton = new Beton() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_beton_r_cropped.jpg") };
            Daemmung = new Daemmung() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_30mm_r_cropped.jpg") };
            Schalldaemmung = new Schalldaemmung() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_rockwoll-te_r_cropped.jpg") };




            Treppenaufbau = new ObservableCollection<ABauelementTreppenaufbau>() {
                new Fliese(),
                Ditra,
                Estrich,
                new Bekotec(),
                Schalldaemmung,
                Daemmung,
                Beton,
            };

            Images = new ImageCollection();


            // var uri = new Uri("pack://application:,,,/Resources/ss_schn_beton_r.jpg");

            /*
            BitmapImage bmi = new BitmapImage(uri);
            var mybm = BitmapImage2Bitmap(bmi);
            var myModifiedBm = CropImage(mybm, new Rectangle(0, 411, mybm.Width, 734-411));
            myModifiedBm.Save("ss_schn_beton_r_cropped.jpg");

            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_12fk_8mm_r.jpg"), 458, "bt_12fk_8mm_cropped.jpg");
            */
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_23f_estrich_20mm_r.jpg"), 399, "ss_schn_bt_23f_estrich_20mm_r_cropped.jpg");
            /*
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_23f_estrich_8mm_r.jpg"), 433, "ss_schn_bt_23f_estrich_8mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_2520_estrich_20mm_r.jpg"), 352, "ss_schn_bt_2520_estrich_20mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_2520_estrich_8mm_r.jpg"), 389, "ss_schn_bt_2520_estrich_8mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_ditra_heat_kleber_2mm_r.jpg"), 476, "ss_schn_bt_ditra_heat_kleber_2mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_ditra25_kleber_1mm_r.jpg"), 479, "ss_schn_bt_ditra25_kleber_1mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_fliesen_kleber_3mm_r.jpg"), 467, "ss_schn_bt_fliesen_kleber_3mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_cemwood_r.jpg"), 342, "ss_schn_cemwood_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_eps_15mm_r.jpg"), 458, "ss_schn_eps_15mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_eps_30mm_r.jpg"), 434, "ss_schn_eps_30mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_eps_35mm_r.jpg"), 422, "ss_schn_eps_35mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_gutex_thermofloor_r.jpg"), 448, "ss_schn_gutex_thermofloor_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_isover_r.jpg"), 448, "ss_schn_isover_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_kerdi-line-sr_r.jpg"), 469, "ss_schn_kerdi-line-sr_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_rockwoll-HP_r.jpg"), 447, "ss_schn_rockwoll-HP_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_rockwoll-te_r.jpg"), 437, "ss_schn_rockwoll-te_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_thermowhite_55mm_r.jpg"), 376, "ss_schn_thermowhite_55mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_thermowhite_60mm_r.jpg"), 365, "ss_schn_thermowhite_60mm_r_cropped.jpg");
            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_thermowhite_70mm_r.jpg"), 339, "ss_schn_thermowhite_70mm_r_cropped.jpg");
            */
        }


        public static void ModifyPicture(Uri uri, int nonWhiteStart, string newName)
        {

            BitmapImage bmi = new BitmapImage(uri);
            var mybm = BitmapImage2Bitmap(bmi);
            var myModifiedBm = CropImage(mybm, new Rectangle(0, nonWhiteStart, mybm.Width, mybm.Height - nonWhiteStart));
            myModifiedBm.Save(newName);
        }


        private static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }




    }


    public class Einzelelement
    {
        public Uri Bild { get; set; }

        public string Bezeichnung { get; set; }

        public int? Dicke { get; set; }


        public Einzelelement()
        {

        }

        public Einzelelement(string bezeichnung, Uri bild, int? dicke)
        {
            Bild = bild;
            Bezeichnung = bezeichnung;
            Dicke = dicke;
        }
    }
    public abstract class ABauelementTreppenaufbau
    {
        public bool Vorhanden { get; set; }
        public double Daemmwirkung { get; set; }
        public Uri Bild { get; set; }
        public string Bezeichnung { get; set; }
        public double Dicke { get; set; }

        public string Variante { get; set; }
        public abstract string Typbezeichnung { get; }
        public double[] MoeglicheDicken { get; } = { 100, 200, 300 };

    }

    public class Beton : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Stahlbeton";

        

        public static string[] Varianten = { "a" };
    }

    public class Estrich : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Estrich";

        public void SetPassendesBild()
        {
            switch (Variante)
            {
                case "23f":
                    break;
                case "2520":
                    break;
                default:
                    break;
            }
            switch (Dicke)
            {
                case 20:
                    break;
                case 8:
                    break;
                default:
                    break;
            }
        }
    }

    public class Fliese : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Fliese";
    }

    public class Ditra : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Schlüter Ditra";

        public void SetPassendesBild()
        {
            switch (Dicke)
            {
                case 20:
                    break;
                case 8:
                    break;
                default:
                    break;
            }
        }
    }

    public class Daemmung : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Dämmung";
    }

    public class Schalldaemmung : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Schalldämmung";
    }
    public class Bekotec : ABauelementTreppenaufbau
    {
        public override string Typbezeichnung => "Schlüter Bekotec";
    }


    public class ImageCollection
    {

        public BitmapImage Beton { get; set; }
        public BitmapImage FK8mm { get; set; }
        public BitmapImage Estrich23f_20mm { get; set; }
        public BitmapImage Estrich23f_8mm { get; set; }
        public BitmapImage Estrich2520_20mm { get; set; }
        public BitmapImage Estrich2520_8mm { get; set; }
        public BitmapImage Ditra_heat_kleber_2mm { get; set; }
        public BitmapImage EPS_30mm { get; set; }

        public ImageCollection()
        {
            
            Bitmap source = new Bitmap(@"C:\Users\stsch\source\repos\Trittschallprognose\Trittschallprognose\Resources/ss_schn_bt_23f_estrich_20mm_r.jpg");
            Rectangle section = new Rectangle(0, 460, 734, 497 - 460);

            Bitmap CroppedImage = CropImage(source, section);

            Estrich23f_20mm = ToBitmapImage(CroppedImage);


            Bitmap source_Ditra_heat = new Bitmap(@"C:\Users\stsch\source\repos\Trittschallprognose\Trittschallprognose\Resources/ss_schn_bt_ditra_heat_kleber_2mm_r.jpg");
            Rectangle section_Ditra_heat = new Rectangle(0, 400, 734, 497 - 450);

            Bitmap croppedImage_Ditra_heat = CropImage(source_Ditra_heat, section_Ditra_heat);

            Ditra_heat_kleber_2mm = ToBitmapImage(croppedImage_Ditra_heat);


            Bitmap source_EPS_30mm = new Bitmap(@"C:\Users\stsch\source\repos\Trittschallprognose\Trittschallprognose\Resources/ss_schn_eps_30mm_r.jpg");
            Rectangle section_EPS_30mm = new Rectangle(0, 450, 734, 497 - 450);

            Bitmap croppedImage_EPS_30mm = CropImage(source_EPS_30mm, section_EPS_30mm);

            EPS_30mm = ToBitmapImage(croppedImage_EPS_30mm);

        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }
    }

}
