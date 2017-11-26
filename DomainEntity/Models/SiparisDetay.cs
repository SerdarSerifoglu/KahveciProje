using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.Models
{
    public class SiparisDetay
    {
        [Key]
        public int SiparisDetayID { get; set; }
        [ForeignKey("Siparis")]
        public int SiparisID { get; set; }
        [ForeignKey("Urun")]
        public int UrunID { get; set; }
        public int Miktar { get; set; }
        public decimal Tutar { get; set; }
        public virtual Urun Urun { get; set; }
        public virtual Siparis Siparis { get; set; }
    }
}
