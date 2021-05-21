using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Rectangle = System.Drawing.Rectangle;

namespace Trittschallprognose
{
    public class BilderVorbereitung
    {
        public static void PrepareBilder()
        {
            /*
            BitmapImage bmi = new BitmapImage(uri);
            var mybm = BitmapImage2Bitmap(bmi);
            var myModifiedBm = CropImage(mybm, new Rectangle(0, 411, mybm.Width, 734-411));
            myModifiedBm.Save("ss_schn_beton_r_cropped.jpg");

            ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_12fk_8mm_r.jpg"), 458, "bt_12fk_8mm_cropped.jpg");
            */
            // ModifyPicture(new Uri("pack://application:,,,/Resources/ss_schn_bt_23f_estrich_20mm_r.jpg"), 399, "ss_schn_bt_23f_estrich_20mm_r_cropped.jpg");
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
}
