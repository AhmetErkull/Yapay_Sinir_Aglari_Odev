using System;
using System.IO;

namespace yapaysiniraglariodevi
{
    public class Algoritma
    {
        private int girdi = 35;
        private int ara = 10;
        private int cikti = 5;
        private double[,] girdi_ara_agirlik;
        private double[,] ara_cikti_agirlik;
        private double[,] girdi_ara_agirlik_degisim;
        private double[,] ara_cikti_agirlik_degisim;
        private double[] ara_bias_agirlik;
        private double[] cikti_bias_agirlik;
        private double[] ara_bias_degisim;
        private double[] cikti_bias_degisim;
        private double ogrenme_katsayisi;
        private double momentum;

        public Algoritma()
        {
            Random random = new Random();
            girdi_ara_agirlik = new double[girdi, ara];
            ara_cikti_agirlik = new double[ara, cikti];
            girdi_ara_agirlik_degisim = new double[girdi, ara];
            ara_cikti_agirlik_degisim = new double[ara, cikti];
            ara_bias_agirlik = new double[ara];
            cikti_bias_agirlik = new double[cikti];
            ara_bias_degisim = new double[ara];
            cikti_bias_degisim = new double[cikti];

          
            for (int i = 0; i < girdi; i++)
            {
                for (int j = 0; j < ara; j++)
                {
                    girdi_ara_agirlik[i, j] = random.NextDouble() * 2 - 1;
                    girdi_ara_agirlik_degisim[i, j] = 0;
                }
            }
            for (int i = 0; i < ara; i++)
            {
                ara_bias_agirlik[i] = random.NextDouble() * 2 - 1;
                ara_bias_degisim[i] = 0;
            }

            
            for (int i = 0; i < ara; i++)
            {
                for (int j = 0; j < cikti; j++)
                {
                    ara_cikti_agirlik[i, j] = random.NextDouble() * 2 - 1;
                    ara_cikti_agirlik_degisim[i, j] = 0;
                }
            }
            for (int i = 0; i < cikti; i++)
            {
                cikti_bias_agirlik[i] = random.NextDouble() * 2 - 1;
                cikti_bias_degisim[i] = 0;
            }
        }



        public double[] Tahmin(int[,] girisler)
        {
            double[] arakatmanciktisi = new double[ara];
            double[] ciktikatmanciktisi = new double[cikti];
            int[] giris = new int[girdi];

            for (int i = 0; i < girisler.GetLength(0); i++)
            {
                for (int j = 0; j < girisler.GetLength(1); j++)
                {
                    giris[i * 5 + j] = girisler[i, j];
                }
            }

            
            for (int i = 0; i < ara; i++)
            {
                double toplam = ara_bias_agirlik[i];
              
                for (int j = 0; j < girdi; j++)
                {
                    toplam += giris[j] * girdi_ara_agirlik[j, i];
                }
                arakatmanciktisi[i] = Sigmoid(toplam);
            }

            
            for (int i = 0; i < cikti; i++)
            {
                double toplam = cikti_bias_agirlik[i];
           
                for (int j = 0; j < ara; j++)
                {
                    toplam += arakatmanciktisi[j] * ara_cikti_agirlik[j, i];
                }
                ciktikatmanciktisi[i] = Sigmoid(toplam);
            }

            return ciktikatmanciktisi;
        }


        public void Egit(int[,,] girisler, int[,] hedef, double ogrenme_katsayisi, double momentum, double epsilon)
        {
            this.ogrenme_katsayisi = ogrenme_katsayisi;
            this.momentum = momentum;

            double toplamhata = double.MaxValue;
            int[] giris = new int[girdi];
            double[] arakatman_ciktisi = new double[ara];
            double[] ciktikatmani_ciktisi = new double[cikti];
            double[] ciktikatmani_hatasi = new double[cikti];
            double[] arakatman_hatasi = new double[ara];

            while (epsilon < toplamhata)
            {
                toplamhata = 0;
                for (int x = 0; x < girisler.GetLength(0); x++)
                {
                    for (int i = 0; i < girisler.GetLength(1); i++)
                    {
                        for (int j = 0; j < girisler.GetLength(2); j++)
                        {
                            giris[i * 5 + j] = girisler[x, i, j];
                        }
                    }

                    // Netleri hesapladım
                    for (int j = 0; j < ara; j++)
                    {
                        double toplam = ara_bias_agirlik[j];
                        for (int k = 0; k < girdi; k++)
                        {
                            toplam += giris[k] * girdi_ara_agirlik[k, j];
                        }
                        arakatman_ciktisi[j] = Sigmoid(toplam);
                    }

                    for (int j = 0; j < cikti; j++)
                    {
                        double toplam = cikti_bias_agirlik[j];
                        for (int k = 0; k < ara; k++)
                        {
                            toplam += arakatman_ciktisi[k] * ara_cikti_agirlik[k, j];
                        }
                        ciktikatmani_ciktisi[j] = Sigmoid(toplam);
                    }

                    // Hata değerlerini hesapladım
                    for (int j = 0; j < cikti; j++)
                    {
                        ciktikatmani_hatasi[j] = hedef[x, j] - ciktikatmani_ciktisi[j]; //ağın hatası
                        double hata = ciktikatmani_hatasi[j];
                        toplamhata += 0.5 * Math.Pow(hata, 2);
                    }

                    for (int j = 0; j < ara; j++)
                    {
                        double toplam = 0;
                        for (int k = 0; k < cikti; k++)
                        {
                            toplam += ciktikatmani_hatasi[k] * ara_cikti_agirlik[j, k];
                        }
                        // Sigmoidin türevi
                        arakatman_hatasi[j] = arakatman_ciktisi[j] * (1 - arakatman_ciktisi[j]) * toplam;
                    }

                    // Değişimleri ayarladım
                    for (int j = 0; j < ara; j++)
                    {
                        ara_bias_degisim[j] = momentum * ara_bias_degisim[j] + ogrenme_katsayisi * arakatman_hatasi[j];
                        ara_bias_agirlik[j] += ara_bias_degisim[j];

                        for (int k = 0; k < cikti; k++)
                        {
                            cikti_bias_degisim[k] = momentum * cikti_bias_degisim[k] + ogrenme_katsayisi * ciktikatmani_hatasi[k];
                            cikti_bias_agirlik[k] += cikti_bias_degisim[k];


                            double degisim = momentum * ara_cikti_agirlik_degisim[j, k] + ogrenme_katsayisi * ciktikatmani_hatasi[k] * (1 - ciktikatmani_ciktisi[k]) * ciktikatmani_ciktisi[k] * arakatman_ciktisi[j];

                            // double degisim = momentum * ara_cikti_agirlik_degisim[j, k] + ogrenme_katsayisi * ciktikatmani_hatasi[k] * arakatman_ciktisi[j];
                            ara_cikti_agirlik[j, k] += degisim;
                            ara_cikti_agirlik_degisim[j, k] = degisim;
                        }
                    }

                    for (int j = 0; j < girdi; j++)
                    {

                        for (int k = 0; k < ara; k++)
                        {
                            double degisim = momentum * girdi_ara_agirlik_degisim[j, k] + ogrenme_katsayisi * arakatman_hatasi[k] * giris[j];
                            girdi_ara_agirlik[j, k] += degisim;
                            girdi_ara_agirlik_degisim[j, k] = degisim;
                        }
                    }
                }
                Console.WriteLine(toplamhata);
                Console.WriteLine("Eğitiliyor...");
            }
            Console.WriteLine("Eğitim bitti.");
        }


        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }




        public void Kaydet(string dosyaAdi ="deneme.txt")
        {
            if (string.IsNullOrEmpty(dosyaAdi))
            {
                Random rnd = new Random();
                dosyaAdi = "deneme" + rnd.NextDouble()+ ".txt";
            }
            else
            {
                dosyaAdi += ".txt";
            }
            using (StreamWriter writer = new StreamWriter(dosyaAdi))
            {
                
                for (int i = 0; i < girdi; i++)
                {
                    for (int j = 0; j < ara; j++)
                    {
                        writer.WriteLine(girdi_ara_agirlik[i, j]);
                    }
                }

               
                for (int i = 0; i < ara; i++)
                {
                    for (int j = 0; j < cikti; j++)
                    {
                        writer.WriteLine(ara_cikti_agirlik[i, j]);
                    }
                }

                for (int i = 0; i < ara; i++)
                {
                    writer.WriteLine(ara_bias_agirlik[i]);
                }

                for (int i = 0; i < cikti; i++)
                {
                    writer.WriteLine(cikti_bias_agirlik[i]);
                }


                writer.Close();
            }
        }

        public void Yukle(string dosyaAdi)
        {
            using (StreamReader reader = new StreamReader(dosyaAdi))
            {
              
                for (int i = 0; i < girdi; i++)
                {
                    for (int j = 0; j < ara; j++)
                    {
                        girdi_ara_agirlik[i, j] = double.Parse(reader.ReadLine());
                    }
                }

               
                for (int i = 0; i < ara; i++)
                {
                    for (int j = 0; j < cikti; j++)
                    {
                        ara_cikti_agirlik[i, j] = double.Parse(reader.ReadLine());
                    }
                }

                for (int i = 0; i < ara; i++)
                {
                    ara_bias_agirlik[i] = double.Parse(reader.ReadLine());
                }

                // Ara-cikti bias değerlerini yükle
                for (int i = 0; i < cikti; i++)
                {
                    cikti_bias_agirlik[i] = double.Parse(reader.ReadLine());
                }

                reader.Close();
            }
        }


    }
}