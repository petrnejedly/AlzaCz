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
        private const int DefaultPageSize = 10;

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
        public IList<Domain.Entities.Product> GetProducts(int page, int? pageSize)
        {
            int pageIndex = page - 1;
            if (pageIndex < 0) { pageIndex = 0; }
            int pageSizeNotNull = (int)((!pageSize.HasValue || pageSize == 0) ? pageSize : DefaultPageSize);

            List<Entities.Product> products = this.mockedProducts.Skip(pageIndex * pageSizeNotNull).Take(pageSizeNotNull).ToList();

            return this.mapper.Map<IEnumerable<Domain.Entities.Product>>(products).ToList();
        }

        /// <inheritdoc/>
        public bool UpdateProduct(Domain.Entities.Product product)
        {
            bool returnSuccess = true;
            // Act like the mocked product description has actually been modified.
            return returnSuccess;
        }

        #region private List<Entities.Product> GetMockedProducts()
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
                    Name = "JBL Club ONE",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JJ093aa3",
                    Price = 9790.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 5.0, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, frekvenční rozsah 10 Hz-40000 Hz, měnič 40 mm, odnímatelný kabel 1,5 m, výdrž baterie až 45 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "JBL Under Armour Sport Wireless Train černo-červená",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JJ094c5",
                    Price = 4990.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 4.1, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX4, frekvenční rozsah 16 Hz-20000 Hz, citlivost 101 dB/mW, impedance 32 Ohm, měnič 40 mm, odnímatelný kabel 1,25 m, výdrž baterie až 16 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "JBL Under Armour True Wireless Flash černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JJ094c1",
                    Price = 3390.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 4.2, přijímání hovorů, přepínání skladeb, certifikace IPX7 - voděodolná a potuodolná, frekvenční rozsah 20 Hz-20000 Hz, citlivost 113 dB/mW, impedance 14 Ohm, měnič 5,8 mm, výdrž baterie až 25 h (5 h+20 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Beats Solo Pro Wireless - More Matte Collection - červená",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=MON380a1a6",
                    Price = 6990.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth, aktivní potlačení hluku (ANC), přijímání hovorů, hlasový asistent, měnič 19,7 mm"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Beats Studio3 Wireless - matná černá",
                    ImgUri = "https://cdn.alza.cz",
                    Price = 6490.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 4.0, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, odnímatelný kabel 1,2 m, výdrž baterie až 40 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Beats PowerBeats Pro červená",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=MON303o81h",
                    Price = 5990.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless za uši, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX4, výdrž baterie až 24 h (9 h+15 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "BOSE Noise Cancelling Headphones 700 černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=BOS310k92",
                    Price = 7990.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth, aktivní potlačení hluku (ANC), přepínání skladeb, hlasový asistent, odnímatelný kabel 1,06 m, výdrž baterie až 20 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "BOSE QuietComfort 35 II černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=BOS310k9bc",
                    Price = 6289.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 4.1, NFC, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, hlasový asistent, odnímatelný kabel 1,2 m, výdrž baterie až 20 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "BOSE SoundLink AE wireless II - černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=BOS311r1",
                    Price = 5990.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth 4.0, NFC, s ovládáním hlasitosti, výdrž baterie až 15 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "BOSE SoundSport Free Wireless oranžová",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=BOS312g1b",
                    Price = 3990.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, certifikace IPX4, výdrž baterie až 15 h (5 h+10 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Koss BT/740iQZ černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JK983y2b",
                    Price = 3999.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth 5.0, podpora AAC a aptX, aktivní potlačení hluku (ANC), přijímání hovorů"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Koss TWS/150i černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JK997n1b",
                    Price = 1999.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless pecky, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Koss KSC/35 Wireless černá",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JK997n1a",
                    Price = 1899.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 4.2, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, frekvenční rozsah 15 Hz-25000 Hz, citlivost 101 dB/mW, výdrž baterie až 6 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Koss GMR/545 AIR (dožitvotní záruka)",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JK616a",
                    Price = 1799.00M,
                    Description = "Herní sluchátka drátová, s mikrofonem, přes hlavu, okolo uší, otevřená konstrukce, 3,5 mm Jack, pro PC, s ovládáním hlasitosti, frekvenční rozsah 15 Hz-22000 Hz, citlivost 102 dB/mW, impedance 35 Ohm, odnímatelný kabel 3,6 m"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Koss BT/539i černá (24 měsíců záruka)",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JK983y2",
                    Price = 1499.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, frekvenční rozsah 10 Hz-20000 Hz, citlivost 97 dB/mW, impedance 38 Ohm"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Koss PORTA PRO MIC (doživotní záruka)",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JK618set",
                    Price = 1490.00M,
                    Description = "Sluchátka s mikrofonem, přes hlavu, na uši, otevřená konstrukce, 3,5 mm Jack, s ovládáním hlasitosti, frekvenční rozsah 15 Hz-25000 Hz, citlivost 101 dB/mW, impedance 60 Ohm, kabel 1,22 m"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Sennheiser HD 660S",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JM162d1",
                    Price = 10590.00M,
                    Description = "Sluchátka přes hlavu, okolo uší, otevřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, frekvenční rozsah 10 Hz-40000 Hz, citlivost 104 dB/mW, impedance 150 Ohm, odnímatelný kabel 3 m"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Sennheiser RS 195 U",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JM171m",
                    Price = 9590.00M,
                    Description = "Bezdrátová sluchátka přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, radiofrekvenční připojení, s ovládáním hlasitosti, frekvenční rozsah 17 Hz-22000 Hz, citlivost 117 dB/mW, výdrž baterie až 18 h"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Sennheiser GSP 670",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JM196q",
                    Price = 8790.00M,
                    Description = "Herní sluchátka bezdrátová, s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth s donglem, aktivní potlačení hluku (ANC), prostorový zvuk 7.1, přijímání hovorů, frekvenční rozsah 10 Hz-23000 Hz, výdrž baterie až 20 h"
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
                    Name = "Sennheiser MOMENTUM True Wireless",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JM166t1",
                    Price = 6290.00M,
                    Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, podpora AAC a aptX, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX4, frekvenční rozsah 5 Hz-21000 Hz, citlivost 107 dB/mW, měnič 7 mm, výdrž baterie až 16 h (4 h+12 h)"
                },
                new Entities.Product()
                {
                    Id = i++,
                    Name = "Sennheiser HD 25 PLUS",
                    ImgUri = "/ImgW.ashx?fd=f3&cd=JM123g",
                    Price = 4899.00M,
                    Description = "Sluchátka přes hlavu, na uši, uzavřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, frekvenční rozsah 16 Hz-22000 Hz, citlivost 120 dB/mW, impedance 70 Ohm, odnímatelný kabel 3 m"
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
        #endregion
    }
}
