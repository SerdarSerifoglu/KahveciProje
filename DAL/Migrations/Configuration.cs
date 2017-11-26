namespace DAL.Migrations
{
    using DomainEntity.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.KahveContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.KahveContext context)
        {
            if (context.Kullanicilar.Count() == 0)
            {
                Kullanici k = new Kullanici();
                k.KullaniciAdi = "admin";
                k.Sifre = "123456";
                context.Kullanicilar.Add(k);
                context.SaveChanges();
            }

            //her update-database ile bu Seed metodu çalýþýr

            if (context.Urunler.Count() == 0)
            {
                context.Urunler.Add(new Urun()
                {
                    UrunAdi = "Latte",
                    Fiyat = 10
                });

                context.Urunler.Add(new Urun()
                {
                    UrunAdi = "Türk Kahvesi",
                    Fiyat = 5
                });

                context.Urunler.Add(new Urun()
                {
                    UrunAdi = "Filtre Kahve",
                    Fiyat = 6
                });

                context.Urunler.Add(new Urun()
                {
                    UrunAdi = "Mocha",
                    Fiyat = 11
                });

                context.Urunler.Add(new Urun()
                {
                    UrunAdi = "Espresso",
                    Fiyat = 20
                });
                context.SaveChanges();
            }
        }
    }
}
