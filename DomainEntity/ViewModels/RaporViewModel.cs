using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity.ViewModels
{
    public class RaporViewModel
    {
        public decimal ToplamSatisTutari { get; set; }
        public int ToplamSatilanUrunSayisi { get; set; }
        public List<UrunSatisAdediViewModel> UrunSatisAdedi { get; set; }
        public List<KullaniciSatisViewModel> KullaniciBasiSatislar { get; set; }
    }
}
