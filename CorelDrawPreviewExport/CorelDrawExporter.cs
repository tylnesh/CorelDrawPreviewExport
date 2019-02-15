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
using System.IO;
using VGCore;

namespace CorelDrawPreviewExport
{
    class CorelDrawExporter
    {
        VGCore.Application appDraw;
     

        public CorelDrawExporter(object obj) {
            this.appDraw = (VGCore.Application) obj;


        }

        // Exports MSMX background with customizable text labels 

        public void ExportMSMXBackground(string variant, string number = null, string name = null)
        {
            VGCore.ShapeRange kit = this.appDraw.ActiveSelectionRange;
            if (CheckKitDimensions(kit))
            {

                VGCore.Shape msmx = ImportBackground("msmx.cdr");

                ArrangeExportAndCleanup(kit, msmx, cdrFilter.cdrJPEG, 1000, 779, variant, number, name);


            }

        }







        //Exports all FMR Backgrounds
        public void ExportAllFmrBackgrounds()
        {
            ShapeRange kit = this.appDraw.ActiveSelectionRange;

            if (CheckKitDimensions(kit))
            {

                String[] filePaths = Directory.GetFiles(@"C:\CorelDrawPreviewExport\CorelDrawPreviewExport\assets\bg\fmr\", "*.cdr",
                                             SearchOption.TopDirectoryOnly);

                for (int i = 0; i < filePaths.Length; i++)
                {
                    VGCore.Shape fmr = ImportBackground(filePaths[i].ToString().Substring(filePaths[i].Length - 9, 9));
                    ArrangeExportAndCleanup(kit, fmr, cdrFilter.cdrPNG, 2000, 1197);
                }

            }

        }


        //Export function for the F Background
        public void ExportFmrFBackground()
        {
            ShapeRange kit = this.appDraw.ActiveSelectionRange;

            if (CheckKitDimensions(kit))
            {
                VGCore.Shape fmrf = ImportBackground("FMR_F.cdr");
                ArrangeExportAndCleanup(kit, fmrf, cdrFilter.cdrPNG, 2000, 1197);
            }
        }
        //Export function for the M Background
        public void ExportFmrMBackground()
        {
            ShapeRange kit = this.appDraw.ActiveSelectionRange;

            if (CheckKitDimensions(kit))
            {
                VGCore.Shape fmrm = ImportBackground("FMR_M.cdr");
                ArrangeExportAndCleanup(kit, fmrm, cdrFilter.cdrPNG, 2000, 1197);
            }
        }
        //Export function for the K Background
        public void ExportFmrKBackground()
        {
            ShapeRange kit = this.appDraw.ActiveSelectionRange;
            if (CheckKitDimensions(kit))
            {
                VGCore.Shape fmrk = ImportBackground("FMR_K.cdr");
                ArrangeExportAndCleanup(kit, fmrk, cdrFilter.cdrPNG, 2000, 1197);
            }
        }

        //Export function for the White Background
        public void ExportFmrWBackground()
        {
            ShapeRange kit = this.appDraw.ActiveSelectionRange;
            if (CheckKitDimensions(kit))
            {
                VGCore.Shape fmrw = ImportBackground("FMR_W.cdr");
                ArrangeExportAndCleanup(kit, fmrw, cdrFilter.cdrPNG, 2000, 1197);
            }
        }


        // Checks whether the dimensions for the kit are valid and if not, shows an error message
        private bool CheckKitDimensions(VGCore.ShapeRange kit)
        {

            if (kit.SizeHeight > kit.SizeWidth)
            {

                MessageBox.Show("The kit's width must be bigger than it's height!");
                return false;
            }
            else
            {
                return true;
            }
        }

        //runs ArrangeBackground, ExportImage and deletes background to keep the file sizes low.

        private void ArrangeExportAndCleanup(VGCore.ShapeRange kit, VGCore.Shape background, cdrFilter filter, int Width, int Height, string variant = null, string number = null, string name = null)
        {


            ArrangeBackground(kit, background, variant, number, name);
            ExportImage(kit, background, filter, Width, Height, variant, number, name);
            background.Delete();

        }

        //exports image 

        private void ExportImage(VGCore.ShapeRange kit, VGCore.Shape background, cdrFilter imageType, int width, int height, string variant = null, string number = null, string name = null)
        {

            ShapeRange exportRange = kit;
            exportRange.Add(background);

            exportRange.CreateSelection();

            String fileName = this.appDraw.ActiveDocument.FileName;

            String filePath = this.appDraw.ActiveDocument.FilePath;
            fileName = fileName.Remove(fileName.Length - 4, 4);
            String suffix = "";
            ExportFilter expFil;

            if (variant == null)
            {

                if (background.Name != "FMR_W.cdr")
                {
                    suffix = " -" + background.Name.ElementAt(4);
                }

                if (imageType == cdrFilter.cdrPNG) suffix += ".png";

                expFil = this.appDraw.ActiveDocument.ExportBitmap(filePath + fileName + suffix, imageType, cdrExportRange.cdrSelection, cdrImageType.cdrRGBColorImage, width, height, 300, 300);
            }
            else
            {
                if (imageType == cdrFilter.cdrJPEG) suffix += ".jpg";
                if (name.Length > 0 || number.Length > 0)

                {
                    number = "#" + number;
                    fileName = variant + " " + number + " " + name + fileName.Substring(6);
                }
                else fileName = variant + fileName.Substring(6);
                expFil = this.appDraw.ActiveDocument.ExportBitmap(filePath + fileName + suffix, imageType, cdrExportRange.cdrSelection, cdrImageType.cdrRGBColorImage, width, height, 300, 300);

            }
            expFil.Finish();

        }

        // ImportBackgrounds - Imports Backgrounds from Install Folder.

        private VGCore.Shape ImportBackground(String fileName)
        {

            String prefix = fileName.Substring(0, 3);

            StructImportOptions impOpt = this.appDraw.CreateStructImportOptions();

            if (prefix == "FMR")
            {
                ImportFilter impFil = this.appDraw.ActiveLayer.ImportEx("C:\\CorelDrawPreviewExport\\CorelDrawPreviewExport\\assets\\bg\\fmr\\" + fileName, cdrFilter.cdrCDR, impOpt);
                impFil.Finish();
            }
            else
            {
                ImportFilter impFil = this.appDraw.ActiveLayer.ImportEx("C:\\CorelDrawPreviewExport\\CorelDrawPreviewExport\\assets\\bg\\msmx\\" + fileName, cdrFilter.cdrCDR, impOpt);
                impFil.Finish();
            }

            return this.appDraw.ActiveShape;

        }


        // ArrangeBackground - Arranges Backgrounds behind the decals kit. FMR_K needs to be moved to the bottom left and FMR_M needs to be moved down by a bit.

        private void ArrangeBackground(ShapeRange kit, VGCore.Shape background, string variant = null, string number = null, string name = null)
        {

            background.OrderToBack();

            if (background.Name == "msmx.cdr")
            {
                if (name.Length > 0 || number.Length > 0) background.Shapes.FindShape("custom", cdrShapeType.cdrTextShape).Text.Replace("#XX XXXXXXXXX", "#" + number + " " + name, true);
                else background.Shapes.FindShape("custom", cdrShapeType.cdrTextShape).Text.Replace("#XX XXXXXXXXX", "", true);

                background.Shapes.FindShape("design", cdrShapeType.cdrTextShape).Text.Replace("X XXXX -XX", variant, true);

            }

            if (background.Name != "FMR_K.cdr")
            {
                background.SetSize(kit.SizeWidth + 10);
                background.CenterX = kit.CenterX;
                background.CenterY = kit.CenterY;

                if (background.Name == "FMR_M.cdr")
                {
                    background.CenterY = kit.CenterY + 1;
                }
            }

            else
            {
                background.SetSize(kit.SizeWidth + 15);
                background.CenterX = kit.CenterX - 5;
                background.CenterY = kit.CenterY + 2;
            }


        }

    }
}


