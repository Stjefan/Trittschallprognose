using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace TrittschallDotNetFramework
{
    public enum RecordType
    {
        None = 0,
        Fliese,
        Ditra,
        Bekotec,
        Daemmung,
        Beton
    }
    public class EinzelelementModel
    {
        private string pfadAufbereitetesBild;

        public EinzelelementModel()
        {

        }

        public EinzelelementModel(string bezeichnung, string pfadAufbereitetesBild, int dicke) : this(bezeichnung, pfadAufbereitetesBild)
        {
            Dicke = dicke;
        }

        public EinzelelementModel(string bezeichnung, string pfadAufbereitetesBild)
        {
            Bezeichnung = bezeichnung;
            PfadAufbereitetesBild = pfadAufbereitetesBild;
            var uri = new System.Uri(pfadAufbereitetesBild);

            var uri_curr_dir = new System.Uri(".");
            var converted = uri.MakeRelativeUri(uri_curr_dir);
        }

        public string Bezeichnung { get; set; }
        public string PfadAufbereitetesBild { get => pfadAufbereitetesBild; set { 
                pfadAufbereitetesBild = value;
                
                
            } }
        public int Dicke { get; set; }

        public string GetRelativePfadBild()
        {
            var fullpath = System.IO.Path.GetFullPath(pfadAufbereitetesBild);
            var uri_curr_dir = new Uri("pack://application:,,,");
            var uri = new System.Uri(fullpath);
            var converted = uri_curr_dir.MakeRelativeUri(uri);
            return fullpath;
        }
    }


    public abstract class ASchichtModel
    {

    }

    public class DitraschichtModel
    {
        public List<EinzelelementModel> ZugeordneteElemente { get; set; }

        public DitraschichtModel()
        {
            ZugeordneteElemente = new List<EinzelelementModel>();
        }
        public void CreateInitialData()
        {
            ZugeordneteElemente = new List<EinzelelementModel>() {

            new EinzelelementModel("BT Ditra Heat Kleber (2 mm)", "/Resources/CroppedImages/ss_schn_bt_ditra_heat_kleber_2mm_r_cropped.jpg", 2),
                new EinzelelementModel("BT Ditra25 Kleber (1 mm)", "/Resources/CroppedImages/ss_schn_bt_ditra25_kleber_1mm_r_cropped.jpg", 1),
                new EinzelelementModel("BT Fliesenkleber (3 mm)", "/Resources/CroppedImages/ss_schn_bt_fliesen_kleber_3mm_r_cropped.jpg", 3),
        };
            }

    }

    public class BekotecschichtModel
    {
        public List<EinzelelementModel> ZugeordneteElemente { get; set; }

        public BekotecschichtModel()
        {
            ZugeordneteElemente = new List<EinzelelementModel>();
        }

        public void CreateInitialData()
        {
            ZugeordneteElemente = new List<EinzelelementModel>()
            {
                new EinzelelementModel("BT 12fk (8 mm)", "/Resources/CroppedImages/ss_schn_bt_12fk_8mm_r_cropped.jpg", 8),
                new EinzelelementModel("BT 23f Estrich (20 mm)", "/Resources/CroppedImages/ss_schn_bt_23f_estrich_20mm_r_cropped.jpg", 20),
                new EinzelelementModel("BT 23f Estrich (8 mm)", "/Resources/CroppedImages/ss_schn_bt_23f_estrich_8mm_r_cropped.jpg", 8),
                new EinzelelementModel("BT 2520 Estrich (20 mm)", "/Resources/CroppedImages/ss_schn_bt_2520_estrich_20mm_r_cropped.jpg", 20),
                new EinzelelementModel("BT 2520 Estrich (8 mm)", "/Resources/CroppedImages/ss_schn_bt_2520_estrich_8mm_r_cropped.jpg", 8),
            };
        }
    }

    public class DaemmungschichtModel
    {
        public List<EinzelelementModel> ZugeordneteElemente { get; set; }

        public DaemmungschichtModel()
        {
            ZugeordneteElemente = new List<EinzelelementModel>();
        }
        public void CreateInitialData()
        {
            ZugeordneteElemente = new List<EinzelelementModel>()
            {
                new EinzelelementModel("Cemwood", "/Resources/CroppedImages/ss_schn_cemwood_r_cropped.jpg"),
                new EinzelelementModel("EPS (15 mm)", "/Resources/CroppedImages/ss_schn_eps_15mm_r_cropped.jpg", 15),
                new EinzelelementModel("EPS (30 mm)", "/Resources/CroppedImages/ss_schn_eps_30mm_r_cropped.jpg", 30),
                new EinzelelementModel("EPS (35 mm)", "/Resources/CroppedImages/ss_schn_eps_35mm_r_cropped.jpg", 35),
                new EinzelelementModel("Gutex Thermofloor", "/Resources/CroppedImages/ss_schn_gutex_thermofloor_r_cropped.jpg"),
                new EinzelelementModel("Isover", "/Resources/CroppedImages/ss_schn_isover_r_cropped.jpg"),
                new EinzelelementModel("Kerdi-Line-SR", "/Resources/CroppedImages/ss_schn_kerdi-line-sr_r_cropped.jpg"),
                new EinzelelementModel("Rockwoll-HP", "/Resources/CroppedImages/ss_schn_rockwoll-HP_r_cropped.jpg"),
                new EinzelelementModel("Rockwoll-te", "/Resources/CroppedImages/ss_schn_rockwoll-te_r_cropped.jpg"),
                new EinzelelementModel("Thermowhite (55 mm)", "/Resources/CroppedImages/ss_schn_thermowhite_55mm_r_cropped.jpg", 55),
                new EinzelelementModel("Thermowhite (60 mm)", "/Resources/CroppedImages/ss_schn_thermowhite_60mm_r_cropped.jpg", 60),
                new EinzelelementModel("Thermowhite (70 mm)", "/Resources/CroppedImages/ss_schn_thermowhite_70mm_r_cropped.jpg", 70),
            };
        }
    }
    public class BetonschichtModel
    {
        public List<EinzelelementModel> ZugeordneteElemente { get; set; }

        public BetonschichtModel()
        {
            ZugeordneteElemente = new List<EinzelelementModel>();
        }
        public void CreateInitialData()
        {
            ZugeordneteElemente = new List<EinzelelementModel>()
            {
                new EinzelelementModel("Stahlbeton", "./Resources/CroppedImages/ss_schn_beton_r_cropped.jpg")
            };
        }
    }


    public class FliesenschichtModel
    {
        public List<EinzelelementModel> ZugeordneteElemente { get; set; }

        public void CreateInitialData()
        {
            ZugeordneteElemente = new List<EinzelelementModel>()
            {
            };
        }

    }
    public class SimpleDatabase
    {
        public FliesenschichtModel Fliesenschicht { get; set; }
        public DitraschichtModel Ditraschicht { get; set; }
        public BekotecschichtModel Bekotecschicht { get; set; }
        public DaemmungschichtModel Daemmungschicht { get; set; }
        public BetonschichtModel Betonschicht { get; set; }

        public ObservableCollection<EinzelelementModel> EinzelementeCollection { get; private set; }

        public SimpleDatabase(string filepath)
        {
            

            Fliesenschicht = new FliesenschichtModel();
            Fliesenschicht.CreateInitialData();

            Ditraschicht = new DitraschichtModel();

            Bekotecschicht = new BekotecschichtModel();

            Daemmungschicht = new DaemmungschichtModel();

            Betonschicht = new BetonschichtModel();

            ReadFromCSV(filepath);
            EinzelementeCollection = new ObservableCollection<EinzelelementModel>();
            foreach (var el in Fliesenschicht.ZugeordneteElemente)
            {
                EinzelementeCollection.Add(el);
            }
            foreach (var el in Ditraschicht.ZugeordneteElemente)
            {
                EinzelementeCollection.Add(el);
            }
            foreach (var el in Bekotecschicht.ZugeordneteElemente)
            {
                EinzelementeCollection.Add(el);
            }
            foreach (var el in Daemmungschicht.ZugeordneteElemente)
            {
                EinzelementeCollection.Add(el);
            }
            foreach (var el in Betonschicht.ZugeordneteElemente)
            {
                EinzelementeCollection.Add(el);
            }
        }

        public void ReadFromCSV(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                IgnoreBlankLines = false,
                Delimiter = ";",
                AllowComments = true,

            };

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                
                csv.Context.RegisterClassMap<EinzelelementMapping>();
                var type = RecordType.None;
                var container = new List<EinzelelementModel>();
                var isHeader = true;
                while (csv.Read())
                {
                    if (isHeader)
                    {
                        csv.ReadHeader();
                        isHeader = false;
                        continue;
                    }

                    var typ = csv.GetField(1);
                    switch (typ)
                    {
                        case "Betonschicht":
                        case "Beton":
                            Betonschicht.ZugeordneteElemente.Add(csv.GetRecord<EinzelelementModel>());
                            break;
                        case "Fliese":
                        case "Fliesen":
                        case "Fliesenschicht":
                            Fliesenschicht.ZugeordneteElemente.Add(csv.GetRecord<EinzelelementModel>());
                            break;
                        case "Bekotec":
                        case "Bekotecschicht":
                            Bekotecschicht.ZugeordneteElemente.Add(csv.GetRecord<EinzelelementModel>());
                            break;
                        case "Ditra":
                            Ditraschicht.ZugeordneteElemente.Add(csv.GetRecord<EinzelelementModel>());
                            break;
                        case "Dämmungsschicht":
                        case "Dämmung":
                            Daemmungschicht.ZugeordneteElemente.Add(csv.GetRecord<EinzelelementModel>());
                            break;
                        default:
                            throw new Exception("Unbekannter Schichttyp");
                    }
                        
                }





            }
        }
    }

    public sealed class EinzelelementMapping : ClassMap<EinzelelementModel>
    {
        public EinzelelementMapping()
        {
            Map(m => m.Bezeichnung).Name("Bezeichnung");
            Map(m => m.Dicke);
            Map(m => m.PfadAufbereitetesBild).Index(3);
            
        }
    }
}
