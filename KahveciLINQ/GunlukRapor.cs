using DAL;
using DomainEntity.Models;
using DomainEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveciLINQ
{
    public partial class GunlukRapor : Form
    {
        public GunlukRapor()
        {
            InitializeComponent();
        }
        KahveContext db = new KahveContext();


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //Günlük Toplam Satış Tutarı
        //    List<SiparisDetay> kayitlar = db.SiparisDetaylari.Where(x => DbFunctions.TruncateTime(x.Siparis.Tarih) == dateTimePicker1.Value.Date).ToList();
        //    decimal tutar = kayitlar.Sum(a => a.Tutar);
        //    label2.Text = tutar.ToString();
        //    //Toplam Satılan Ürün Sayısı
        //    List<SiparisDetay> kayitlar1 = db.SiparisDetaylari.Where(x => DbFunctions.TruncateTime(x.Siparis.Tarih) == dateTimePicker1.Value.Date).ToList();
        //    int adet = kayitlar1.Sum(a => a.Miktar);
        //    label3.Text = adet.ToString();

        //    //Kullanıcı Başı Satış Tutarı
        //    ////Benim Yaptığım
        //    //var kayitlar2 = db.SiparisDetaylari.Where(x => x.Siparis.KaydedenKullaniciID.ToString() == comboBox1.SelectedValue.ToString()).ToList();
        //    //decimal kisiseltoplam = kayitlar2.Sum(x => x.Tutar);
        //    //label6.Text = kisiseltoplam.ToString();
        //    //Kullanıcı Başı Satış Tutarı
        //    ////Benim Yaptığım
        //    //var kayitlar3 = db.SiparisDetaylari.Where(x => x.Siparis.KaydedenKullaniciID.ToString() == comboBox1.SelectedValue.ToString()).ToList();
        //    //label7.Text = kayitlar3.GroupBy(a => a.SiparisID).Count().ToString();


        //    //Hangi Üründen Kaç Tane Satılmış
        //    List<UrunSatisAdediViewModel> liste= db.SiparisDetaylari.Select(x => new UrunSatisAdediViewModel()
        //    {
        //        UrunID = x.UrunID,
        //        UrunAdi = x.Urun.UrunAdi,
        //        Adet = x.Miktar
        //    }).ToList();



        //}

        //private void GunlukRapor_Load(object sender, EventArgs e)
        //{
        //    comboBox1.DataSource = db.Siparisler.Select(x => x.KaydedenKullaniciID).ToList();
        //}


        ////Hocanın Yaptığı

        public void RaporGetir(DateTime secilenGun)
        {
            KahveContext db = new KahveContext();
            GunlukRaporViewModel rapor = new GunlukRaporViewModel();
            try//kayıt olmayan gün seçildiğindede çalışmasını sağlıyoruz.
            {
                rapor.ToplamSatisTutari = db.Siparisler.Where(a => DbFunctions.TruncateTime(a.Tarih).Value.Day == secilenGun.Day).Sum(x => x.SiparistekiUrunler.Sum(a => a.Tutar));

                ////Diğer Yol
                //rapor.ToplamSatisTutari = (from x in db.Siparisler select x).Sum(x => x.SiparistekiUrunler.Sum(a => a.Tutar));
                rapor.ToplamSatilanUrunSayisi = db.Siparisler.Where(a => DbFunctions.TruncateTime(a.Tarih).Value.Day == secilenGun.Day).Sum(x => x.SiparistekiUrunler.Sum(a => a.Miktar));
            }
            catch { }
            label2.Text = rapor.ToplamSatisTutari.ToString("C");
            label3.Text = rapor.ToplamSatilanUrunSayisi.ToString();
            ////SQL'deki karşılığı
            //SELECT k.KullaniciAdi, COUNT(*) AS SiparisSayisi, SUM(sd.Tutar) AS SatisTutari
            //FROM Kullanicis k
            //INNER JOIN Siparis s
            //ON k.kullaniciID=s.EkleyenKullaniciID
            //INNER JOIN SiparisDetay sd
            //ON s.SiparisID=sd.SiparisID
            //GROUP BY k.KullaniciAdi
            rapor.KullaniciBasiSatislar = (from sd in db.Siparisler
                                           join k in db.Kullanicilar
                                           on sd.KaydedenKullaniciID equals k.KullaniciID
                                           where DbFunctions.TruncateTime(sd.Tarih).Value.Day == secilenGun.Day
                                           group sd by k.KullaniciAdi into yeni
                                           select new KullaniciSatisViewModel()
                                           {
                                               KullaniciAdi = yeni.Key,
                                               ToplamSatisTutari = yeni.Sum(x => x.SiparistekiUrunler.Sum(a => a.Tutar))

                                           }
                                         ).ToList();
            dataGridView1.DataSource = rapor.KullaniciBasiSatislar;
            rapor.UrunSatisAdedi = (from sd in db.SiparisDetaylari
                                    where DbFunctions.TruncateTime(sd.Siparis.Tarih).Value.Day == secilenGun.Day
                                    group sd by sd.UrunID into yeni
                                    select new UrunSatisAdediViewModel()
                                    {
                                        UrunID = yeni.Key,
                                        UrunAdi = yeni.Max(x => x.Urun.UrunAdi),
                                        Adet = yeni.Sum(x => x.Miktar)
                                    }).ToList();
            dataGridView3.DataSource = rapor.UrunSatisAdedi;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            RaporGetir(dateTimePicker1.Value);
        }

        private void GunlukRapor_Load(object sender, EventArgs e)
        {
           
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            RaporGetir(dateTimePicker1.Value);
        }
    }
}
