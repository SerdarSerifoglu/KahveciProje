using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    [Table("tblKullanici")]//veritabanında oluşturulcak tablonun adını koyduk
    public class Kullanici
    {
        //[Key], [Required] bunların adları decorator olarak geçiyor.       
        [Key]//Primary key olmasını sağladık
        public int KullaniciID { get; set; }
        [Required]//Kullanıcı adını zorunlu hale getirdik
        [MaxLength(100)]//nvarchar(MAX) yerine nvarchar(100) haline getirdik
        public string KullaniciAdi { get; set; }
        [Required]//Şifrenin zorunlu olmasını sağladık
        [MaxLength(20)]//nvarchar(MAX) yerine nvarchar(20) haline getirdik
        public string Sifre { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public Kullanici()//Constructor metodları instance alındığında çalışan metodlardır.
        {
            EklenmeTarihi = DateTime.Now;// instance oluşturulduğu anda EklenmeTarihi propertysine o anın tarihi verilecek.
        }
    }
}
