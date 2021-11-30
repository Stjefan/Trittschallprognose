using System;
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
    }
    public class Schalldaten : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Einzelelement> Einzelelemente { get; set; }

        public Einzelelement SelectedEinzelelement { get => selectedEinzelelement; set { selectedEinzelelement = value; NotifyPropertyChanged(); } }
        private Einzelelement selectedEinzelelement;
        private AAuswertung auswertung;


        public SchichtDefaultViewModel Betonschicht { get; set; }
        public SchichtDefaultViewModel Fliesenschicht { get; set; }
        public SchichtDefaultViewModel Ditraschicht { get; set; }
        public SchichtDefaultViewModel Bekotecschicht { get; set; }
        public SchichtDaemmungViewModel Daemmungschicht { get; set; }

        public AAuswertung Auswertung { get => auswertung; set { auswertung = value; NotifyPropertyChanged(); } }
        public ICommand ErstelleAuswertungCommand { get; set; }


        private void ErstelleEinzelelmente()
        {



            Einzelelemente = new ObservableCollection<Einzelelement>()
            /*{
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
            }*/;

            
        }

        public Schalldaten()
        {
            var db = new SimpleDatabase();
            

            Auswertung = new AuswertungMitError();

            ErstelleEinzelelmente();
            /*
            Fliese = new Fliese() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_fliesen_kleber_3mm_r_cropped.jpg") };
            Estrich = new Estrich() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_23f_estrich_20mm_r_cropped.jpg") };
            Ditra = new Ditra() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_bt_ditra25_kleber_1mm_r_cropped.jpg") };
            Beton = new Beton() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_beton_r_cropped.jpg") };
            Daemmung = new Daemmung() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_eps_30mm_r_cropped.jpg") };
            Schalldaemmung = new Schalldaemmung() { Bild = new Uri("pack://application:,,,/Resources/CroppedImages/ss_schn_rockwoll-te_r_cropped.jpg") };

            var betonModel = new EinzelelementModel()
            {
                PfadAufbereitetesBild = "./Resources/CroppedImages/ss_schn_beton_r_cropped.jpg",
                Bezeichnung = "Stahlbeton"
            };
            */


            Betonschicht = new SchichtDefaultViewModel("Beton", new ObservableCollection<Einzelelement>(db.Betonschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));
            Fliesenschicht = new SchichtDefaultViewModel("Fließen", new ObservableCollection<Einzelelement>(db.Fliesenschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));
            Ditraschicht = new SchichtDefaultViewModel("Ditra", new ObservableCollection<Einzelelement>(db.Ditraschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));
            
            Bekotecschicht = new SchichtDefaultViewModel("Bekotec", new ObservableCollection<Einzelelement>(db.Bekotecschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(
                i.GetRelativePfadBild()), i.Dicke))));
            Daemmungschicht = new SchichtDaemmungViewModel("Dämmung",
                new ObservableCollection<Einzelelement>(db.Daemmungschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));


            // var uri = new Uri("pack://application:,,,/Resources/ss_schn_beton_r.jpg");

            
            foreach(var el in Betonschicht.MoeglicheElemente)
            {
                Einzelelemente.Add(el);
            }
            foreach (var el in Ditraschicht.MoeglicheElemente)
            {
                Einzelelemente.Add(el);
            }
            foreach (var el in Fliesenschicht.MoeglicheElemente)
            {
                Einzelelemente.Add(el);
            }
            foreach (var el in Bekotecschicht.MoeglicheElemente)
            {
                Einzelelemente.Add(el);
            }
            foreach (var el in Daemmungschicht.MoeglicheElemente)
            {
                Einzelelemente.Add(el);
            }


            ErstelleAuswertungCommand = new DefaultCommand()
            {
                Action = new Action(() =>
                {
                    var bodenaufbau = new ObservableCollection<Einzelelement>();


                    if (Betonschicht.GewaehltesElement != null && Betonschicht.Vorhanden)
                    {
                        bodenaufbau.Add(Betonschicht.GewaehltesElement);
                    }
                    if (Fliesenschicht.GewaehltesElement != null && Fliesenschicht.Vorhanden)
                    {
                        bodenaufbau.Add(Fliesenschicht.GewaehltesElement);
                    }
                    if (Ditraschicht.GewaehltesElement != null && Ditraschicht.Vorhanden)
                    {
                        bodenaufbau.Add(Ditraschicht.GewaehltesElement);
                    }
                    if (Bekotecschicht.GewaehltesElement != null && Bekotecschicht.Vorhanden)
                    {
                        bodenaufbau.Add(Bekotecschicht.GewaehltesElement);
                    }
                    if (Daemmungschicht.SchichtA.GewaehltesElement != null && Daemmungschicht.SchichtA.Vorhanden)
                    {
                        bodenaufbau.Add(Daemmungschicht.SchichtA.GewaehltesElement);
                    }
                    if (Daemmungschicht.SchichtB.GewaehltesElement != null && Daemmungschicht.SchichtB.Vorhanden)
                    {
                        bodenaufbau.Add(Daemmungschicht.SchichtB.GewaehltesElement);
                    }
                    if (Daemmungschicht.SchichtC.GewaehltesElement != null && Daemmungschicht.SchichtC.Vorhanden)
                    {
                        bodenaufbau.Add(Daemmungschicht.SchichtC.GewaehltesElement);
                    }
                    Auswertung = AAuswertung.CreateAuswertung(bodenaufbau);




                })
            };
        }





    }


    public class Einzelelement
    {
        public Uri Bild { get; set; }

        public string Bezeichnung { get; set; }

        public int? Dicke { get; set; }

        public string ZugeordneteSchicht { get; set; }

        public int[] MoeglicheDicken { get; set; }

        public Einzelelement()
        {

        }

        public Einzelelement(string bezeichnung, Uri bild, int? dicke)
        {
            Bild = bild;
            Bezeichnung = bezeichnung;
            Dicke = dicke;
        }

        public Einzelelement MakeCopy()
        {
            return new Einzelelement()
            {
                Bezeichnung = Bezeichnung,
                Bild = Bild,
                Dicke = Dicke
            };
        }

        public static Einzelelement CreateFromModel(EinzelelementModel arg)
        {
            return new Einzelelement(arg.Bezeichnung, new Uri(arg.PfadAufbereitetesBild, UriKind.Relative), arg.Dicke != 0 ? arg.Dicke : new int?());
        }
    }

    public class EinzelelementVariableDicke : Einzelelement
    {
        public EinzelelementVariableDicke(string bezeichnung, Uri bild, int? dicke) : base(bezeichnung, bild, dicke)
        {
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

    public class SchichtDefaultViewModel : INotifyPropertyChanged
    {
        public bool Vorhanden { get => vorhanden; set { vorhanden = value; NotifyPropertyChanged(); } }

        private Einzelelement gewaehltesElement;
        private bool vorhanden;

        public string Schichtbezeichnung { get; set; }

        public ObservableCollection<Einzelelement> MoeglicheElemente { get; set; }

        public SchichtDefaultViewModel(string schichtbezeichnung, ObservableCollection<Einzelelement> moeglicheElemente)
        {
            Schichtbezeichnung = schichtbezeichnung;
            MoeglicheElemente = moeglicheElemente;
            foreach (var i in MoeglicheElemente)
            {
                i.ZugeordneteSchicht = Schichtbezeichnung;
            };
        }

        public Einzelelement GewaehltesElement { get => gewaehltesElement; set { gewaehltesElement = value; NotifyPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SchichtDefaultViewModel()
        {

        }
    }

    public class SchichtDaemmungViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Einzelelement> MoeglicheElemente { get; set; }
        public string Schichtbezeichnung { get; set; }

        public SchichtDaemmungViewModel(string schichtbezeichnung, ObservableCollection<Einzelelement> moeglicheElemente)
        {
            Schichtbezeichnung = schichtbezeichnung;
            MoeglicheElemente = moeglicheElemente;
            foreach (var i in MoeglicheElemente)
            {
                i.ZugeordneteSchicht = Schichtbezeichnung;
            };
            SchichtA = new SchichtDefaultViewModel("Dämmschicht A", MoeglicheElemente);
            SchichtB = new SchichtDefaultViewModel("Dämmschicht B", MoeglicheElemente);
            SchichtC = new SchichtDefaultViewModel("Dämmschicht C", MoeglicheElemente);
        }

        public SchichtDefaultViewModel SchichtA { get; set; }
        public SchichtDefaultViewModel SchichtB { get; set; }
        public SchichtDefaultViewModel SchichtC { get; set; }
        /*
        private Einzelelement gewaehltesElementA;
        private Einzelelement gewaehltesElementB;
        private Einzelelement gewaehltesElementC;

        private bool vorhandenA;
        private bool vorhandenB;
        private bool vorhandenC;
        public Einzelelement GewaehltesElementA { get => gewaehltesElementA; set { gewaehltesElementA = value; NotifyPropertyChanged(); } }

        public Einzelelement GewaehltesElementB { get => gewaehltesElementB; set { gewaehltesElementB = value; NotifyPropertyChanged(); } }

        public Einzelelement GewaehltesElementC { get => gewaehltesElementC; set { gewaehltesElementC = value; NotifyPropertyChanged(); } }

        public bool VorhandenA { get => vorhandenA; set { vorhandenA = value; NotifyPropertyChanged(); } }
        public bool VorhandenB { get => vorhandenB; set { vorhandenB = value; NotifyPropertyChanged(); } }
        public bool VorhandenC { get => vorhandenC; set { vorhandenC = value; NotifyPropertyChanged(); } }
        */

    }

    public abstract class AAuswertung : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public static AAuswertung CreateAuswertung(ObservableCollection<Einzelelement> arg)
        {
            if (arg.Count < 2)
            {
                return new AuswertungMitError("Kein valider Bodenaufbau eingegeben.");
            } else
            {
                return new Auswertung(arg);
            }
        }
    }
    public class Auswertung : AAuswertung
    {
        private double prognostizierterPegel;

        public ObservableCollection<Einzelelement> ZugehoerigerAufbau { get; set; }

        public double PrognostizierterPegel { get => prognostizierterPegel; set { prognostizierterPegel = value; NotifyPropertyChanged(); } }

        public ICommand BerechnePrognositiziertenPegel { get; set; }


        public DateTime ErstelltAm { get => erstelltAm; set { erstelltAm = value; NotifyPropertyChanged(); } }

        private Random r = new Random();
        private DateTime erstelltAm;

        public Auswertung()
        {
        }

        public Auswertung(ObservableCollection<Einzelelement> arg)
        {
            PrognostizierterPegel = r.NextDouble() * 100;
            ErstelltAm = DateTime.Now;
            ZugehoerigerAufbau = arg;
        }
    }

    public class AuswertungMitError : AAuswertung
    {
        public string Informationstext { get; set; } = "Es wurden keine Daten eingegeben.";

        public AuswertungMitError()
        {

        }

        public AuswertungMitError(string anzeigetext)
        {
            Informationstext = anzeigetext;
        }
    }

    public class DefaultCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Action?.Invoke();
        }

        public Action Action { get; set; }
    }
}
