using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace DesignEngineering
{
    public partial class Form3 : Form
    {
        public string qrstr = null;
        public Form3()
        {
            InitializeComponent();
            BarcodeLib.Barcode.QRCode qrbarcode = new BarcodeLib.Barcode.QRCode();
           // Form3 fm = new Form3();
            //MessageBox.Show(label11.Text);
            /*
            string str1,str2,str3;
            str1 = fm.textBox1.Text;
            str2 = fm.;
            str3 = label11.Text;
            str=str1+str2+str3;
          //  fm.Close();*/
     // Select QR Code data encoding type: numeric, alphanumeric, byte, and Kanji to select from.
         //   qrstr = textBox1.Text.ToString();
          // textBox1.Dispose();
            String str1 = "RFID NO.:"+Form1.rfid.ToString()+"\n"+"Name:"+Form1.str+"\n"+"Date:"+DateTime.Now.ToString()+"\n"+"to:"+Form1.toStation+"\n"+"From:"+Form1.fromStation+"";
     qrbarcode.Encoding = BarcodeLib.Barcode.QRCodeEncoding.Auto;
     qrbarcode.Data = str1;

     // Adjusting QR Code barcode module size and quiet zones on four sides.
     qrbarcode.ModuleSize = 4;
     qrbarcode.LeftMargin = 0;
     qrbarcode.RightMargin = 0;
     qrbarcode.TopMargin = 0;
     qrbarcode.BottomMargin = 0;

     // Select QR Code Version (Symbol Size), available from V1 to V40, i.e. 21 x 21 to 177 x 177 modules.
     qrbarcode.Version = BarcodeLib.Barcode.QRCodeVersion.V1;

     // Set QR-Code bar code Reed Solomon Error Correction Level: L(7%), M (15%), Q(25%), H(30%)
     qrbarcode.ECL = BarcodeLib.Barcode.QRCodeErrorCorrectionLevel.L;
     qrbarcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
   
     // More barcode settings here, like ECI, FNC1, Structure Append, etc.

     // save barcode image into your system
     qrbarcode.drawBarcode("D:/qrcode.png");
     pictureBox1.Image = Image.FromFile("D:/qrcode.png");
        }
        
        
         
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private Bitmap memoryImage;

        private void PrintScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }  
        
        
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintScreen();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.printDocument1_PrintPage;

            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            if(dlgSettings.ShowDialog()==DialogResult.OK)
                doc.Print();

            //printPreviewDialog1.ShowDialog();
            //printPreviewDialog1.Hide();
            //    this.Hide();*/
                Form1 fm = new Form1();
                fm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

    }
}
