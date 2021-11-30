using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace TrittschallDotNetFramework
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
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



            Einzelelemente = new ObservableCollection<Einzelelement>();

        }

        public Schalldaten()
        {
            var db = new SimpleDatabase(@".\SampleDatabse.txt");


            Auswertung = new AuswertungMitError();

            ErstelleEinzelelmente();
            

            Betonschicht = new SchichtDefaultViewModel("Beton", new ObservableCollection<Einzelelement>(db.Betonschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));
            Fliesenschicht = new SchichtDefaultViewModel("Fließen", new ObservableCollection<Einzelelement>(db.Fliesenschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));
            Ditraschicht = new SchichtDefaultViewModel("Ditra", new ObservableCollection<Einzelelement>(db.Ditraschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));

            Bekotecschicht = new SchichtDefaultViewModel("Bekotec", new ObservableCollection<Einzelelement>(db.Bekotecschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(
                i.GetRelativePfadBild()), i.Dicke))));
            Daemmungschicht = new SchichtDaemmungViewModel("Dämmung",
                new ObservableCollection<Einzelelement>(db.Daemmungschicht.ZugeordneteElemente.Select(i => new Einzelelement(i.Bezeichnung, new Uri(i.GetRelativePfadBild()), i.Dicke))));


            foreach (var el in Betonschicht.MoeglicheElemente)
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
            }
            else
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
    }

}
