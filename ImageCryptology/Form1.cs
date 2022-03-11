using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageCryptology
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
            button3.Text = "Save EncryptedImage";  //Here we making some adjustments to the Save button
            button3.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
           

            
        }
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
        #region String To Binary converter.
        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0'))); // Burada ne yaptığını ben de çözemedim
            // Stackoverflowlayıp geçtim teşekkürler :D
            // Anlamaya çalışmaya gerek yok sonuçta araba yapmak için tekeri yeniden icat etmiyoruz.
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //storing variables on the form.
            textBox1.Text.ToLower(); //writing the text in lowercase letters.
            string Encrypt_Text = textBox1.Text.ToString();
            var bytes = ConvertToByteArray(Encrypt_Text, Encoding.UTF8);
            string Encrypted_Text = ToBinary(bytes).ToString();
            label1.Text = Encrypted_Text;
            MessageBox.Show("Message has been converted ...(^_^)... ");
            
        }
        #endregion
        #region Encryption
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Properties.Resources.ac9176a4a23bf54cc9747b7010765a8b); // creating a bitmap


            //checking the pixels on bitmap - one by one for i,k
            for (int i = 0; i < bitmap.Width-1; i++)
            {
                for (int k = 0; k < bitmap.Height-1; k++)
                {
                    var color = bitmap.GetPixel(i, k);
                    var red = color.R;
                    var green = color.G;
                    var blue = color.B;
                    string metin = label1.Text;
                    int uzunluk=0; // creating a control variable so that there are no infinite loops
                    uzunluk++;// Since an operation has been performed, increasing the control variable by one
                    if (metin[uzunluk]=='1' &&  uzunluk!=metin.Length-1) // if the value contained in the text is 1, then
                                                                         // increasing the red value of the bitmap pixel value by one.
                    {
                        red++; // increasing the red's value
                        bitmap.SetPixel(i, k,Color.FromArgb(255,red,green,blue)); // Processing into bitmap

                    }
                    if(uzunluk==metin.Length-2)
                    {
                        // End of point 
                        //We are preparing it so that we will use it to decrypt
                        bitmap.SetPixel(i, k,Color.FromArgb(255,255,255,255));
                    }
                    
                    
                    
                    
                    
                }
            }
            MessageBox.Show("Message has been encrypted and processed into image ...(^_^)..."); // Show messagebox
            var image = bitmap;
            // throw the processed image in the bitmap into the variable for get it out.
            pictureBox1.BackgroundImage = image;
            button3.Visible = true; // Image saver.
            
            
            
        }
        #endregion

        private void button3_Click(object sender, EventArgs e) //Registration procedures
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "jpeg dosyası(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp";

            sfd.Title = "Kayıt";

            sfd.FileName = "resim";

            DialogResult sonuç = sfd.ShowDialog();

            if (sonuç == DialogResult.OK)
            {
                pictureBox1.Image.Save(sfd.FileName);
            }
        }
    }
}
