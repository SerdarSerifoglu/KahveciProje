using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.ViewModels
{
   public class KullaniciSatisViewModel
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public decimal ToplamSatisTutari { get; set; }
    }
}
