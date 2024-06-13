using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yapaysiniraglariodevi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Algoritma a = new Algoritma();
        private void Form1_Load(object sender, EventArgs e)
        {
            
          
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,,] egitim = new int[10, 7, 5] {
        {
            {0,0,1,0,0},
            {0,1,0,1,0},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1}
        },

        {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1}
        },
         {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1}
        },

        {
            {1,1,1,1,0},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,0},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,0}
        },
        {
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1}
        },
        {
            {0,0,1,1,1},
            {0,1,0,0,0},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {0,1,0,0,0},
            {0,0,1,1,1}
        },
           {
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,1,1,1,1}
        },
        {
            {1,1,1,0,0},
            {1,0,0,1,0},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,1,0},
            {1,1,1,0,0}
        },
         {
            {1,1,1,1, 1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,0,0,0,1},
            {1,1,1,1,1}
        },
        {
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,1,1,1,1},
            {1,0,0,0,0},
            {1,0,0,0,0},
            {1,1,1,1,1}
        }
    };

            int[,] istenenCıktı = new int[10, 5] {
        {0,0,0,0,1},
         {0,0,0,0,1},
          {0,0,0,0,1},
        {0,0,0,1,0},
         {0,0,0,1,0},
        {0,0,1,0,0},
          {0,0,1,0,0},
        {0,1,0,0,0},
          {0,1,0,0,0},
        {1,0,0,0,0}
    };

            a.Egit(egitim, istenenCıktı, 0.1, 0.9, (double)numericUpDown1.Value);
        }


        private void button2_Click(object sender, EventArgs e)
        {
           
            int[,] deneme =
         {
                   {1,1,1,1,1},
                   {1,1,0,1,1},
                   {1,0,0,0,1},
                   {1,1,1,1,1},
                   {1,0,0,0,1},
                   {1,0,0,0,1},
                   {1,0,0,0,1}
               };

            int[,] dizi = new int[7, 5];

            int index = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                  
                    Control button = Controls.Find("button" + (index+3), true).FirstOrDefault();
                    if (button != null && button.BackColor == Color.White)
                        dizi[i, j] = 0;
                    else
                        dizi[i, j] = 1;
                    index++;
                }

            }

            double enbuyuk = 0;
            double gecici;
            int enbuyukindex = 0;
            for (int i = 0; i < 5; i++)
            {
               
                Label lbl= Controls.Find("lbl" + (i+ 1), true).FirstOrDefault() as Label;
                lbl.Text = a.Tahmin(dizi)[i].ToString();
                gecici = Convert.ToDouble(lbl.Text);

                if (enbuyuk<gecici)
                {
                    enbuyuk = gecici;
                    enbuyukindex = i;
                }
            }
            Label label = Controls.Find("label" + (enbuyukindex + 7), true).FirstOrDefault() as Label;
            label1.Text = label.Text;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
     
            if (btn.BackColor == Color.White)
            {
                btn.BackColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.White;
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            a.Kaydet(textBox1.Text);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Metin Dosyaları (*.txt)|*.txt";
            dialog.Title = "Ağırlıkları ve Bias Değerlerini Yükle";

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string dosyaAdi = dialog.FileName;
                a.Yukle(dosyaAdi);

            }
        }
        }
}
