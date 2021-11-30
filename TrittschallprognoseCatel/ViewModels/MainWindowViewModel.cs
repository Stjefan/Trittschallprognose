using Catel.MVVM;
using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TrittschallprognoseCatel.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Einzelelement SelectedEinzelelement { get; set; }
        public ObservableCollection<Einzelelement> Einzelelemente { get; set; }
        public ASchichtViewModel Betonschicht { get; set; }
        public ASchichtViewModel Fliesenschicht { get; set; }
        public ASchichtViewModel Ditraschicht { get; set; }
        public ASchichtViewModel Bekotecschicht { get; set; }
        public DaemmungSchichtViewModel Daemmungschicht { get; set; }

        public AAuswertung Auswertung { get; set; }

        public bool BoolProp { get; set; }
        public MainWindowViewModel()
        {
            Dictionary<string, List<Einzelelement>> myDict;
            List<Einzelelement> allEinzelelemente;
            Einzelelement.ReadFromExcel("", out myDict,  out allEinzelelemente);
            BoolProp = true;


            var imageFolderPath = @"C:\Users\stsch\source\repos\Trittschallprognose\TrittschallDotNetFramework\bin\Debug\ZugeschnitteneBilder";
            var useReadFromExcel = true;
            if (!useReadFromExcel)
            {
                Einzelelemente = new ObservableCollection<Einzelelement>()
            {
                new Einzelelement("Cemwood", $"{imageFolderPath}/ss_schn_cemwood_r_cropped.jpg"),
                new Einzelelement("EPS (15 mm)", $"{imageFolderPath}/ss_schn_eps_15mm_r_cropped.jpg", 15),
                new Einzelelement("EPS (30 mm)", $"{imageFolderPath}/ss_schn_eps_30mm_r_cropped.jpg", 30),
                new Einzelelement("EPS (35 mm)", $"{imageFolderPath}/ss_schn_eps_35mm_r_cropped.jpg", 35),
                new Einzelelement("Gutex Thermofloor", $"{imageFolderPath}/ss_schn_gutex_thermofloor_r_cropped.jpg"),
                new Einzelelement("Isover", $"{imageFolderPath}/ss_schn_isover_r_cropped.jpg"),
                new Einzelelement("Kerdi-Line-SR", $"{imageFolderPath}/ss_schn_kerdi-line-sr_r_cropped.jpg"),
                new Einzelelement("Rockwoll-HP", $"{imageFolderPath}/ss_schn_rockwoll-HP_r_cropped.jpg"),
                new Einzelelement("Rockwoll-te", $"{imageFolderPath}/ss_schn_rockwoll-te_r_cropped.jpg"),
                new Einzelelement("Thermowhite (55 mm)", $"{imageFolderPath}/ss_schn_thermowhite_55mm_r_cropped.jpg", 55),
                new Einzelelement("Thermowhite (60 mm)", $"{imageFolderPath}/ss_schn_thermowhite_60mm_r_cropped.jpg", 60),
                new Einzelelement("Thermowhite (70 mm)", $"{imageFolderPath}/ss_schn_thermowhite_70mm_r_cropped.jpg", 70),
            };

                Betonschicht = new DefaultSchichtViewModel("Betonschicht", Einzelelemente) { };
                Fliesenschicht = new DefaultSchichtViewModel("Fliesen", Einzelelemente);
                Ditraschicht = new DefaultSchichtViewModel("Ditra", Einzelelemente);
                Bekotecschicht = new DefaultSchichtViewModel("Bekotec", Einzelelemente);
                Daemmungschicht = new DaemmungSchichtViewModel("Dämmung", Einzelelemente);
            } else
            {
                Einzelelemente = new ObservableCollection<Einzelelement>(allEinzelelemente);

                Betonschicht = new DefaultSchichtViewModel("Betonschicht", new ObservableCollection<Einzelelement>(myDict["Beton"]));
                Fliesenschicht = new DefaultSchichtViewModel("Fliesen", new ObservableCollection<Einzelelement>(myDict["Fliesen"]));
                Ditraschicht = new DefaultSchichtViewModel("Ditra", new ObservableCollection<Einzelelement>(myDict["Ditra"]));
                Bekotecschicht = new DefaultSchichtViewModel("Bekotec", new ObservableCollection<Einzelelement>(myDict["Bekotec"]));
                Daemmungschicht = new DaemmungSchichtViewModel("Dämmung", new ObservableCollection<Einzelelement>(myDict["Dämmung"]));
            }

            CreateAuswertung = new Command(OnCreateAuswertungExecute);


        }

        public override string Title { get { return "Trittschallprognose"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets


        public Command CreateAuswertung { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        private void OnCreateAuswertungExecute()
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
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }

    public abstract class ASchichtViewModel : ViewModelBase
    {
        public ObservableCollection<Einzelelement> MoeglicheElemente { get; set; }
        public Einzelelement GewaehltesElement { get; set; }
        public string Schichtbezeichnung { get; set; }

        public bool Vorhanden { get; set; }

        public ASchichtViewModel(string schichtbezeichnung, ObservableCollection<Einzelelement> moeglicheElemente)
        {
            Schichtbezeichnung = schichtbezeichnung;
            MoeglicheElemente = moeglicheElemente;
        }
    }
    public class DefaultSchichtViewModel : ASchichtViewModel
    {
        public DefaultSchichtViewModel(string schichtbezeichnung, ObservableCollection<Einzelelement> moeglicheElemente) : base(schichtbezeichnung, moeglicheElemente)
        {

        }
    }

    public class DaemmungSchichtViewModel : ASchichtViewModel
    {
        public DefaultSchichtViewModel SchichtA { get; set; }
        public DefaultSchichtViewModel SchichtB { get; set; }
        public DefaultSchichtViewModel SchichtC { get; set; }
        public DaemmungSchichtViewModel(string schichtbezeichnung, ObservableCollection<Einzelelement> moeglicheElemente) : base(schichtbezeichnung, moeglicheElemente)
        {
            SchichtA = new DefaultSchichtViewModel("Dämmschicht A", MoeglicheElemente);
            SchichtB = new DefaultSchichtViewModel("Dämmschicht B", MoeglicheElemente);
            SchichtC = new DefaultSchichtViewModel("Dämmschicht C", MoeglicheElemente);
        }
    }

    public class Einzelelement : ViewModelBase
    {
        [Ignore]
        public Uri Bild { get; set; }

        public string Bezeichnung { get; set; }

        public int? Dicke { get; set; }

        [Ignore]
        public string ZugeordneteSchicht { get; set; }

        [Ignore]
        public int[] MoeglicheDicken { get; set; }

        public Einzelelement(string bezeichnung, string pfadAufbereitetesBild, int dicke) : this(bezeichnung, pfadAufbereitetesBild)
        {
            Dicke = dicke;
        }

        public Einzelelement(string bezeichnung, string pfadAufbereitetesBild)
        {
            Bezeichnung = bezeichnung;
            // PfadAufbereitetesBild = pfadAufbereitetesBild;
            var uri = new System.Uri(pfadAufbereitetesBild);

            Bild = uri;
            // var uri_curr_dir = new System.Uri(".");
            // var converted = uri.MakeRelativeUri(uri_curr_dir);
        }


        public static void ReadFromExcel(string path, out Dictionary<string, List<Einzelelement>> schichtenMitMoeglichenElementen, out List<Einzelelement> alleElemente)
        {
            var mapper = new ExcelMapper(@"C:\Users\stsch\Desktop\ProdukteTrittschallprognose.xlsx");


            /*
            mapper.AddMapping<EinzelelementPOCO>("Mögliche Dicken", p => p.MoeglicheDicken)
                .SetPropertyUsing((v) =>
                {
                    
                    if (v is double)
                    {
                        // Defaultmäßig kommen einzelne Zahlen als Double aus dem Mapper
                        return new List<int>() { Convert.ToInt32(v) };
                    }
                    var val = v as string;
                    if (string.IsNullOrEmpty(val))
                        return null;
                    else
                    {
                        
                        return val.Split(";").Select(i => int.Parse(i)).ToList();
                    }
                });
            */
            mapper.AddMapping<EinzelelementPOCO>("Pfad zugeordnetes Bild", p => p.PfadZuZugeschnittemBild).AsFormulaResult();

            mapper.AddMapping<ZuordnungselementDefault>("Zugeordnete Elemente", i => i.ZugeordneteElemente);
            var elementsInExcel = mapper.Fetch<EinzelelementPOCO>();
            alleElemente = elementsInExcel.Select(i =>
            {
                if (!i.Dicke.HasValue)
                {
                    return new Einzelelement(i.Bezeichnung, i.PfadZuZugeschnittemBild);
                }
                else
                {
                    return new Einzelelement(i.Bezeichnung, i.PfadZuZugeschnittemBild, i.Dicke.Value);
                }
            }).ToList();



            var allSchichten = new Dictionary<string, List<ZuordnungselementDefault>>();
            var myKeys = new string[] { "Bekotec", "Beton", "Dämmung", "Ditra", "Fliesen" };

            foreach(var k in myKeys)
            {
                allSchichten.Add(k, mapper.Fetch<ZuordnungselementDefault>(k).ToList());
            }
            schichtenMitMoeglichenElementen = new Dictionary<string, List<Einzelelement>>();

            foreach(var l in allSchichten)
            {
                var zuordnung = (from b in l.Value join el in alleElemente on b.ZugeordneteElemente equals el.Bezeichnung select el).ToList();
                schichtenMitMoeglichenElementen.Add(l.Key, zuordnung);
            }


            ;
        }
    }

    public class EinzelelementPOCO
    {
        public string Bezeichnung { get; set; }
        public int? Dicke { get; set; }
        // public List<int> MoeglicheDicken { get; set; }
        public string PfadZuZugeschnittemBild { get; set; }
    }

    public class ZuordnungselementDefault
    {
        public string ZugeordneteElemente { get; set; }
    }
    public abstract class AAuswertung : ViewModelBase
    {

        public static AAuswertung CreateAuswertung(ObservableCollection<Einzelelement> arg)
        {
            return new AuswertungViewModel(arg);
        }
    }
    public class AuswertungViewModel : AAuswertung
    {

        public ObservableCollection<Einzelelement> ZugehoerigerAufbau { get; set; }

        public double PrognostizierterPegel { get; set; }


        public DateTime ErstelltAm { get; set; }

        private Random r = new Random();

        public AuswertungViewModel(ObservableCollection<Einzelelement> arg)
        {
            PrognostizierterPegel = r.NextDouble() * 100;
            ErstelltAm = DateTime.Now;
            ZugehoerigerAufbau = arg;
        }
    }

}
