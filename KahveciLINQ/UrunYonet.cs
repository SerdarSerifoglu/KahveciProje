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
    public partial class UrunYonet : Form
    {
        public UrunYonet()
        {
            InitializeComponent();
        }
        //private void Yenile()
        //{
        //    listBox1.DataSource = null;
        //    listBox1.DataSource = db.Urunler;
        //}
        public void ListboxYenile()
        {
            listBox1.DataSource = db.Urunler.OrderBy(x => x.UrunAdi).Select(a => a.UrunAdi).ToList();
        }
        private void UrunYonet_Load(object sender, EventArgs e)
        {
            ListboxYenile();
            numericUpDown1.DecimalPlaces = 2;
        }
        KahveContext db = new KahveContext();
        private void button1_Click(object sender, EventArgs e)
        {//Ekle
            if (textBox1.Text != null && numericUpDown1.Text != null)
            {

                Urun u = new Urun();
                u.UrunAdi = textBox1.Text;
                u.Fiyat = numericUpDown1.Value;
                db.Urunler.Add(u);
            }
            db.SaveChanges();
            ListboxYenile();





        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Urun urun=db.Urunler.Where(x => x.UrunAdi == listBox1.SelectedItem.ToString()).FirstOrDefault();
            textBox1.Text = urun.UrunAdi;
            //maskedTextBox1.Text = urun.Fiyat.ToString().Replace(",",this.ToString());
            numericUpDown1.Text = urun.Fiyat.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {//Sil
            if (listBox1.SelectedItems.Count != 0)
            {
                var urun = db.Urunler.Where(x => x.UrunAdi == listBox1.SelectedItem.ToString()).First();
                db.Urunler.Remove(urun);
            }
            db.SaveChanges();
            ListboxYenile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var guncellenecekurun = db.Urunler.First(x => x.UrunAdi == listBox1.SelectedItem.ToString());
            guncellenecekurun.UrunAdi = textBox1.Text;
            guncellenecekurun.Fiyat = numericUpDown1.Value;
            db.SaveChanges();
            
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//Mouse'a sağ tıklandıysa
            {
                var index = listBox1.IndexFromPoint(e.Location);
                if (index == -1)//Birşey seçilmemişse değer -1'dir.
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = false;
                }
                else
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = true;
                }
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var urun = db.Urunler.Where(x => x.UrunAdi == listBox1.SelectedItem.ToString()).First();
            DialogResult sonuc = MessageBox.Show(urun.UrunAdi + "- silmek istediğinize emin misiniz?", "Ürün Sil", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                if (listBox1.SelectedItems.Count != 0)
                {
                    db.Urunler.Remove(urun);
                }
                db.SaveChanges();
                ListboxYenile();
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//Mouse'a sağ tıklandıysa
            {
                var index = listBox1.IndexFromPoint(e.Location);
                if (index == -1)//Birşey seçilmemişse değer -1'dir.
                {
                    contextMenuStrip1.Items[0].Enabled = false;
                    contextMenuStrip1.Items[1].Enabled = false;
                }
                else
                {
                    contextMenuStrip1.Items[0].Enabled = true;
                    contextMenuStrip1.Items[1].Enabled = true;
                }
            }
        }
    }
}
