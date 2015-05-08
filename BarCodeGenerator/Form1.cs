using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Barcode.Forms;
using ZXing;

namespace BarCodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }
        private void GenerateBarCode(string content)
        {
            //Create the Barcode Writer instance
            var writer = new BarcodeWriter
            {
                //sets the barcode formate is CODE 128 just for the example
                Format = BarcodeFormat.CODE_128
            };
            //writes the content of the barcod
            var bitmap = writer.Write(content);
            //shows the barcode in the form
            Pic_barcode.Image = bitmap;
            
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            if (txt_content.Text != string.Empty)
            {
                GenerateBarCode(txt_content.Text);
                btn_generate.Enabled = false;
                btn_save_as.Enabled = true;
                txt_content.Enabled = false;
            }
            else
                MessageBox.Show(this, "Please write the content of your barcode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CleanForm();
        }
        private void CleanForm()
        {
            Pic_barcode.Image = null;
            txt_content.Text = "";
            btn_save_as.Enabled = false;
            btn_generate.Enabled=txt_content.Enabled = true;
            txt_content.Focus();
        }
        private void SaveBarcodeAsImage()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            save.Title = "Save the Barcode as Image File";
            save.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (save.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)save.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                switch (save.FilterIndex)
                {
                    case 1:
                        this.Pic_barcode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.Pic_barcode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.Pic_barcode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }
        private void btn_save_as_Click(object sender, EventArgs e)
        {
            SaveBarcodeAsImage();
        }
        
    }
}
