using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.ViewModels
{
    public class Sepet
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamFiyat
        {
            get
            {
                return BirimFiyat * Miktar;
            }
        }
    }
}
