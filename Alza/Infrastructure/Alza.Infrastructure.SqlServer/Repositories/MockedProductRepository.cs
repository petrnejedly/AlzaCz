using Alza.Domain.Abstractions.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Alza.Infrastructure.SqlServer.Repositories
{
    /// <summary>
    /// Mocked product repository.
    /// </summary>
    public class MockedProductRepository : IMockedProductRepository
    {
        private readonly IMapper mapper;
        private List<Entities.Product> mockedProducts;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedProductRepository"/> class.
        /// </summary>
        /// <param name="mapper">An instance of the Automapper class.</param>
        public MockedProductRepository(IMapper mapper)
        {
            this.mapper = mapper;
            this.mockedProducts = this.GetMockedProducts();
        }

        /// <inheritdoc/>
        public Domain.Entities.Product GetProduct(int id)
        {
            Entities.Product product = this.mockedProducts.Where(x => x.Id == id).FirstOrDefault();

            return this.mapper.Map<Domain.Entities.Product>(product);
        }

        /// <inheritdoc/>
        public IList<Domain.Entities.Product> GetProducts()
        {
            List<Entities.Product> products = this.mockedProducts;

            return this.mapper.Map<IEnumerable<Domain.Entities.Product>>(products).ToList();
        }

        /// <inheritdoc/>
        public IList<Domain.Entities.Product> GetProducts(int page, int pageSize)
        {
            int pageIndex = page - 1;
            if (pageIndex < 0) { pageIndex = 0; }

            List<Entities.Product> products = this.mockedProducts.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return this.mapper.Map<IEnumerable<Domain.Entities.Product>>(products).ToList();
        }

        /// <summary>
        /// Gets a collection of the mocked products.
        /// </summary>
        /// <returns></returns>
        private List<Entities.Product> GetMockedProducts()
        {
            int i = 1;
            List<Entities.Product> products = new List<Entities.Product>()
            {
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Niceboy HIVE pods 2",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=NCB050c4a",
                    Price = 1499.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, přepínání skladeb, certifikace IPX5, frekvenční rozsah 20 Hz-15000 Hz, citlivost 94 dB/mW, výdrž baterie až 35 h (7 h+28 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Apple AirPods 2019",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JA940i1a",
                    Price = 4790.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless pecky, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, přepínání skladeb, hlasový asistent, výdrž baterie až 24 h (5 h+19 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "LAMAX Dots2 Wireless Charging",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=LMA031dot2",
                    Price = 1190.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, do uší, uzavřená, hlasový asistent, Bluetooth 5.0, bezdrátové + USB-C nabíjení, výdrž baterie až 36 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Niceboy HIVE podsie",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=NCB050c6",
                    Price = 999.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přepínání skladeb, certifikace IPX4, frekvenční rozsah 20 Hz-20000 Hz, citlivost 110 dB/mW, měnič 8 mm, výdrž baterie až 15 h (3,5 h+11,5 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Sony Hi-Res WH-1000XM4, černá, model 2020",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=RO972j0b1",
                    Price = 10599.00M,
                    Description = "Bezdrátová sluchátka přes hlavu, s aktivním odrušením okolního hluku (ANC), Bluetooth 5.0, NFC, frekvenční odezva 4-40000 Hz, Hi-Res audio a 360 Reality Audio, formáty LDAC/AAC/SBC, technologie DSEE Extreme, hands-free telefonování komfortnější díky funkcím HD Voice, Echo Cancellation, více mikrofonů snímající hlas, možnost připojení ke dvěma Bluetooth zařízením současně díky funkci Multi-point connection, hlasový průvodce, Siri/Alexa/Google Assistant, připojení i přes jack-jack 3.5mm kabel, výdrž baterie 30h s podporou ANC, plné nabití 3h, funkce Quick charge, nabíjení přes USB-C, stand-by čas 200h, váha 254g, balení obsahuje kabel pro nabíjení/audio kabel/pouzdro a adaptér pro připojení v letadle"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Xiaomi Mi True Wireless Earbuds Basic",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=XIAhe17",
                    Price = 699.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, hlasový asistent, certifikace IPX4, frekvenční rozsah 20 Hz-20000 Hz, citlivost 93 dB/mW, impedance 16 Ohm, měnič 7,2 mm, výdrž baterie až 9 h (3 h+6 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "AlzaPower Shpunty černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=APWBTE040",
                    Price = 490.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, do uší, True Wireless, frekvenční rozsah 20Hz-20kHz, podpora A2DP, výdrž baterie 4h (s pouzdrem až 15h), EasyPairing, Bluetooth 5.0, podpora hlasových asistentů, citlivost 98dB/mW, impedance 16 Ohm, měnič 6mm, váha pouze 4g"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Samsung Galaxy Buds Live Black",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=SAAW0010c1",
                    Price = 5499.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless pecky, otevřená konstrukce, Bluetooth 5.0, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX2, frekvenční rozsah 20 Hz-20000 Hz, citlivost 92 dB/mW, impedance 50 Ohm, měnič 12 mm, výdrž baterie až 29 h (8 h+21 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "JBL Tune 500BT černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JJ092q1g5",
                    Price = 1290.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 4.1, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, frekvenční rozsah 20 Hz-20000 Hz, měnič 32 mm, výdrž baterie až 16 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Sony True Wireless WF-SP800N, černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=RO980d9a",
                    Price = 4199.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, do uší, True Wireless, Bluetooth, aktivní potlačení okolního hluku (Noise Cancelling), obě sluchátka ve funkci master, možnost nošení pouze jednoho sluchátka, bezpečné uchycení pro sportování, podpora audio formátu SBC/AAC, vestavěný mikrofon pro hlasové ovládání a handsfree volání, výdrž na baterii až 9 hodin + dalších 9 hodin při dobíjení z pouzdra, odolnost vůči prachu a vodě s normou IP55, váha sluchátek 2x 9,8g, váha pouzdra cca 59 gramů, funkce quick charge pro rychlé nabíjení, barva černá"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "xxx",
                    ImgUri = "",
                    Price = 0.00M,
                    Description = ""
                }
            };

            return products;
        }
    }
}
