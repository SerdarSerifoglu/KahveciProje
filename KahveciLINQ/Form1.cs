using DAL;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveciLINQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {          
            KahveContext db = new KahveContext();
            Kullanici k= db.Kullanicilar.Where(x => x.KullaniciAdi == textBox1.Text && x.Sifre == textBox2.Text).FirstOrDefault();
            if (k == null)
                MessageBox.Show("Hatalı Giriş");
            else
            {
                //// YAPILACAK !!!!!! Program.GirisYapanID = textBox1.Text;
                new SiparisEkrani().Show();
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //button focusu alır.Formun hala tuşları dinleyebilmesi için
            this.KeyPreview = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void günlükRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GunlukRapor().Show();
        }

        private void aylıkRaporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AylikRapor().Show();
        }

        private void ürünleriYönetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
