using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yigit.Properties;

namespace yigit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable sorular = new DataTable();
        int mevcutsatir = 0;
        int sorusayisi;
        int sorusatiri;
        int mevcutsoru = 0;
        SoundPlayer dogruses = new SoundPlayer(Resources.Doğru_Cevap_Ses_Efekti___Correct_Answer_Sound_Effect);
        SoundPlayer yanlisses = new SoundPlayer(Resources.Yanlış_cevap_sesi);

        private void button4_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            sorular = new DataTable();
            dt.Columns.Add("Soru");
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C");
            dt.Columns.Add("Cevap");
            dt.Columns.Add("Doğru Cevap");
            sorular.Columns.Add("satir");

            if (textBox1.Text != "")
            {
                StreamReader sr = new StreamReader("Sorular.txt");

                string secilisatirsoru;

                do
                {
                    secilisatirsoru = sr.ReadLine();
                    sorular.Rows.Add();
                    sorular.Rows[sorular.Rows.Count-1]["satir"] = secilisatirsoru;                    


                } while (secilisatirsoru != null);

                sorusayisi = sorular.Rows.Count / 5;


                

                for (int i_ = 0; i_ < sorusayisi; i_++)
                {
                    dt.Rows.Add();
                    for (int i = mevcutsatir; i < mevcutsatir+5; i++)
                    {
                        //secilisatir = mevcutsatir;                       

                        if (i == mevcutsatir)
                        {
                            dt.Rows[sorusatiri]["Soru"] = sorular.Rows[i]["satir"];
                        }
                        if (i == mevcutsatir+1)
                        {
                            dt.Rows[sorusatiri]["A"] = sorular.Rows[i]["satir"];
                        }
                        if (i == mevcutsatir+2)
                        {
                            dt.Rows[sorusatiri]["B"] = sorular.Rows[i]["satir"];                            
                        }
                        if (i == mevcutsatir+3)
                        {
                            dt.Rows[sorusatiri]["C"] = sorular.Rows[i]["satir"];                            
                        }
                        if (i == mevcutsatir+4)
                        {
                            dt.Rows[sorusatiri]["Doğru Cevap"] = sorular.Rows[i]["satir"];                            
                        }
                        
                    }
                    mevcutsatir += 5;
                    sorusatiri += 1;
                }


                sorugetir(true);

                textBox1.Enabled=false;
                button4.Enabled = false;
                panel1.Visible = true;


                //for (int i = 0; i < sorular.Rows.Count-1; i++)
                //{
                //    dt.Rows[i]["Soru"]= sorular.Rows[i][""];



                //}
                sr.Close();
            }
        }


        void  sorugetir(bool oncesonra)
        {
            if (oncesonra)
            {
                if (mevcutsoru + 1 <= sorusayisi)
                {
                    button1.BackColor = this.BackColor;
                    button2.BackColor = this.BackColor;
                    button3.BackColor = this.BackColor;


                    label1.Text = dt.Rows[mevcutsoru]["Soru"].ToString();
                    button1.Text = dt.Rows[mevcutsoru]["A"].ToString();
                    button2.Text = dt.Rows[mevcutsoru]["B"].ToString();
                    button3.Text = dt.Rows[mevcutsoru]["C"].ToString();
                    label5.Text = dt.Rows[mevcutsoru]["Doğru Cevap"].ToString();
                    //label6.Text = "Soru " + mevcutsoru + 1.ToString();
                    mevcutsoru++;
                }
                else
                {
                    MessageBox.Show("Sona Erdi", "Bilgi Yarışması", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt_ = new DataTable();
                    dt_.Columns.Add("Soru");
                    dt_.Columns.Add("Verilen Cevap");
                    dt_.Columns.Add("Doğru Cevap");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt_.Rows.Add(dt.Rows[i]["Soru"], dt.Rows[i]["Cevap"], dt.Rows[i]["Doğru Cevap"]);
                    }
                    int dogrucevap = 0;
                    for (int i = 0; i < dt_.Rows.Count; i++)
                    {
                        if (dt_.Rows[i]["Verilen Cevap"] == dt_.Rows[i]["Doğru Cevap"])
                        {
                            dogrucevap++;
                        }
                    }
                    dt_.Rows.Add("", "", dogrucevap.ToString() + " adet doğru cevap verdiniz");

                    dataGridView1.DataSource = dt_;
                    panel2.Visible = true;
                }
            }
            else
            {
                if (mevcutsoru > 0)
                {
                    button1.BackColor = this.BackColor;
                    button2.BackColor = this.BackColor;
                    button3.BackColor = this.BackColor;


                    label1.Text = dt.Rows[mevcutsoru - 1]["Soru"].ToString();
                    button1.Text = dt.Rows[mevcutsoru - 1]["A"].ToString();
                    button2.Text = dt.Rows[mevcutsoru - 1]["B"].ToString();
                    button3.Text = dt.Rows[mevcutsoru - 1]["C"].ToString();
                    label5.Text = dt.Rows[mevcutsoru - 1]["Doğru Cevap"].ToString();
                    //label6.Text = "Soru " + mevcutsoru + 1.ToString();
                    mevcutsoru--;
                }
                
            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dt.Columns.Add("Soru");
            //dt.Columns.Add("A");
            //dt.Columns.Add("B");
            //dt.Columns.Add("C");
            //dt.Columns.Add("Cevap");
            //dt.Columns.Add("Doğru Cevap");
            //sorular.Columns.Add("satir");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label5.Text == "A")
            {
                button1.BackColor = Color.Green;                
                dogruses.Play();
            }
            else
            {
                button1.BackColor = Color.Red;
                yanlisses.Play();
            }

            if (label5.Text == "B")
            {
                button2.BackColor = Color.Green;
            }

            if (label5.Text == "C")
            {
                button3.BackColor = Color.Green;
            }
            


            cevapver("A");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label5.Text == "A")
            {
                button1.BackColor = Color.Green;
            }

            if (label5.Text == "B")
            {
                button2.BackColor = Color.Green;
                dogruses.Play();
            }
            else
            {
                button2.BackColor = Color.Red;
                yanlisses.Play();
            }

            if (label5.Text == "C")
            {
                button3.BackColor = Color.Green;
            }           


            cevapver("B");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (label5.Text == "A")
            {
                button1.BackColor = Color.Green;
            }

            if (label5.Text == "B")
            {
                button2.BackColor = Color.Green;
            }

            if (label5.Text == "C")
            {
                button3.BackColor = Color.Green;                
                dogruses.Play();
            }
            else
            {
                button3.BackColor = Color.Red;
                yanlisses.Play();
            }           
            

            cevapver("C");
        }

        void cevapver(string cevap)
        {
            dt.Rows[mevcutsoru-1]["Cevap"] = cevap;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sorugetir(true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            textBox1.Text = "";
            textBox1.Enabled = true;
            button4.Enabled = true;
            panel2.Visible = false;
            dataGridView1.DataSource = null;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sorugetir(false);
        }
    }
}
