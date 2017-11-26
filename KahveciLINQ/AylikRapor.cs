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
    public partial class AylikRapor : Form
    {
        public AylikRapor()
        {
            InitializeComponent();
        }

        private void AylikRapor_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
        }
        
        public void RaporGetir(DateTime secilenAy)
        {
            KahveContext db = new KahveContext();
            AylikRaporViewModel rapor = new AylikRaporViewModel();
            try//Boş ay geldiğinde hata vermemesi için yaptık
            {
                rapor.ToplamSatisTutari = db.Siparisler.Where(x => DbFunctions.TruncateTime(x.Tarih).Value.Month == secilenAy.Month && DbFunctions.TruncateTime(x.Tarih).Value.Year == secilenAy.Year).Sum(x => x.SiparistekiUrunler.Sum(a => a.Tutar));

                ////Diğer Yol
                //rapor.ToplamSatisTutari = (from x in db.Siparisler select x).Sum(x => x.SiparistekiUrunler.Sum(a => a.Tutar));
                rapor.ToplamSatilanUrunSayisi = db.Siparisler.Where(x => DbFunctions.TruncateTime(x.Tarih).Value.Month == secilenAy.Month && DbFunctions.TruncateTime(x.Tarih).Value.Year == secilenAy.Year).Sum(x => x.SiparistekiUrunler.Sum(a => a.Miktar));
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
                                           where DbFunctions.TruncateTime(sd.Tarih).Value.Month == secilenAy.Month
                                           group sd by k.KullaniciAdi into yeni
                                           select new KullaniciSatisViewModel()
                                           {
                                               KullaniciAdi = yeni.Key,
                                               ToplamSatisTutari = yeni.Sum(x => x.SiparistekiUrunler.Sum(a => a.Tutar))

                                           }
                                         ).ToList();
            dataGridView1.DataSource = rapor.KullaniciBasiSatislar;
            rapor.UrunSatisAdedi = (from sd in db.SiparisDetaylari
                                    where DbFunctions.TruncateTime(sd.Siparis.Tarih).Value.Month == secilenAy.Month
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RaporGetir(dateTimePicker1.Value);
        }
    }
}
