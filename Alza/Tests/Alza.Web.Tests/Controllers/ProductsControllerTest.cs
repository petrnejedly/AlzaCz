﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alza.Application.Web.Facades;
using Alza.Application.Web.MappingProfiles;
using Alza.Domain.Abstractions.Repositories;
using Alza.Domain.Entities;
using Alza.Domain.Services;
using Alza.Web.Controllers;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Alza.Web.Tests.Controllers
{
    public class ProductsControllerTest
    {
        [Theory]
        [InlineData(1)]
        public async Task Get_Single_When_FoundAsync(int productId)
        {
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new ProductMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductAsync(productId)).Returns(() => Task.FromResult(new Product() { Id = productId, Description = "Some product description" }));

            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get(productId);

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1234)]
        public async Task Get_Single_When_NotFoundAsync(int productId)
        {
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new ProductMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductAsync(productId)).Returns(() => Task.FromResult<Product>(null));

            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get(productId);

            // assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Get_Single_When_InvalidIdAsync(int productId)
        {
            // arrange
            var mapperConfiguration = new MapperConfiguration(new MapperConfigurationExpression());
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get(productId);

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_All_When_Exists_Async()
        {
            // arrange
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new ProductMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductsAsync()).Returns(() => Task.FromResult(mockedProducts));
            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get();

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_All_When_Empty_Async()
        {
            // arrange
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new ProductMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductsAsync()).Returns(() => Task.FromResult<IList<Product>>(new List<Product>()));

            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get();

            // assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Get_Paged_When_Exists_Async()
        {
            int page = 1;
            int pageSize = 10;

            // arrange
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new ProductMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductsAsync(page, pageSize)).Returns(() => Task.FromResult(mockedProducts));
            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get(page, pageSize);

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_Paged_When_Empty_Async()
        {
            int page = 10;
            int pageSize = 10;
            int pageIndex = page - 1;

            // arrange
            var mapperConfiguration = new MapperConfiguration(config => config.AddProfile(new ProductMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var mockLogger = new Mock<ILogger<ProductsController>>();
            var logger = mockLogger.Object;
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetProductsAsync(page, pageSize)).Returns(() => Task.FromResult((IList<Product>)this.mockedProducts.Skip(pageIndex * pageSize).Take(pageSize)));
            var productRepository = mockProductRepository.Object;
            var productService = new ProductService(productRepository);
            var productFacade = new ProductFacade(productService);
            var productsController = new ProductsController(productFacade, this.Configuration, mapper, logger);

            // act
            IActionResult result = await productsController.Get(page, pageSize);

            // assert
            Assert.IsType<NoContentResult>(result);
        }

        #region private IConfigurationRoot Configuration
        private IConfigurationRoot Configuration
        {
            get
            {
                MemoryConfigurationSource source = new MemoryConfigurationSource
                {
                    InitialData = new Dictionary<string, string>()
                    {
                        { "ImageUrlPrefix" , "https://cdn.alza.cz/" },
                        { "ImageNull", "https://satyr.io/1024x768/lightgreen?text=Alza+rulez" }
                    }
                };
                //var memoryConfigurationProvider = new MemoryConfigurationProvider(source);
                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.Add(source);
                IConfigurationRoot configurationRoot = configurationBuilder.Build();
                return configurationRoot;
            }
        }
        #endregion

        #region private IList<Product> mockedProducts = new List<Product>()
        private IList<Product> mockedProducts = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Name = "Niceboy HIVE pods 2",
                ImgUri = "/ImgW.ashx?fd=f3&cd=NCB050c4a",
                Price = 1499.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, přepínání skladeb, certifikace IPX5, frekvenční rozsah 20 Hz-15000 Hz, citlivost 94 dB/mW, výdrž baterie až 35 h (7 h+28 h)"
            },
            new Product()
            {
                Id = 2,
                Name = "Apple AirPods 2019",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JA940i1a",
                Price = 4790.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless pecky, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, přepínání skladeb, hlasový asistent, výdrž baterie až 24 h (5 h+19 h)"
            },
            new Product()
            {
                Id = 3,
                Name = "LAMAX Dots2 Wireless Charging",
                ImgUri = "/ImgW.ashx?fd=f3&cd=LMA031dot2",
                Price = 1190.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, do uší, uzavřená, hlasový asistent, Bluetooth 5.0, bezdrátové + USB-C nabíjení, výdrž baterie až 36 h"
            },
            new Product()
            {
                Id = 4,
                Name = "Niceboy HIVE podsie",
                ImgUri = "/ImgW.ashx?fd=f3&cd=NCB050c6",
                Price = 999.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přepínání skladeb, certifikace IPX4, frekvenční rozsah 20 Hz-20000 Hz, citlivost 110 dB/mW, měnič 8 mm, výdrž baterie až 15 h (3,5 h+11,5 h)"
            },
            new Product()
            {
                Id = 5,
                Name = "Sony Hi-Res WH-1000XM4, černá, model 2020",
                ImgUri = "/ImgW.ashx?fd=f3&cd=RO972j0b1",
                Price = 10599.00M,
                Description = "Bezdrátová sluchátka přes hlavu, s aktivním odrušením okolního hluku (ANC), Bluetooth 5.0, NFC, frekvenční odezva 4-40000 Hz, Hi-Res audio a 360 Reality Audio, formáty LDAC/AAC/SBC, technologie DSEE Extreme, hands-free telefonování komfortnější díky funkcím HD Voice, Echo Cancellation, více mikrofonů snímající hlas, možnost připojení ke dvěma Bluetooth zařízením současně díky funkci Multi-point connection, hlasový průvodce, Siri/Alexa/Google Assistant, připojení i přes jack-jack 3.5mm kabel, výdrž baterie 30h s podporou ANC, plné nabití 3h, funkce Quick charge, nabíjení přes USB-C, stand-by čas 200h, váha 254g, balení obsahuje kabel pro nabíjení/audio kabel/pouzdro a adaptér pro připojení v letadle"
            },
            new Product()
            {
                Id = 6,
                Name = "Xiaomi Mi True Wireless Earbuds Basic",
                ImgUri = "/ImgW.ashx?fd=f3&cd=XIAhe17",
                Price = 699.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, hlasový asistent, certifikace IPX4, frekvenční rozsah 20 Hz-20000 Hz, citlivost 93 dB/mW, impedance 16 Ohm, měnič 7,2 mm, výdrž baterie až 9 h (3 h+6 h)"
            },
            new Product()
            {
                Id = 7,
                Name = "AlzaPower Shpunty černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=APWBTE040",
                Price = 490.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, do uší, True Wireless, frekvenční rozsah 20Hz-20kHz, podpora A2DP, výdrž baterie 4h (s pouzdrem až 15h), EasyPairing, Bluetooth 5.0, podpora hlasových asistentů, citlivost 98dB/mW, impedance 16 Ohm, měnič 6mm, váha pouze 4g"
            },
            new Product()
            {
                Id = 8,
                Name = "Samsung Galaxy Buds Live Black",
                ImgUri = "/ImgW.ashx?fd=f3&cd=SAAW0010c1",
                Price = 5499.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless pecky, otevřená konstrukce, Bluetooth 5.0, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX2, frekvenční rozsah 20 Hz-20000 Hz, citlivost 92 dB/mW, impedance 50 Ohm, měnič 12 mm, výdrž baterie až 29 h (8 h+21 h)"
            },
            new Product()
            {
                Id = 9,
                Name = "JBL Tune 500BT černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JJ092q1g5",
                Price = 1290.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 4.1, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, frekvenční rozsah 20 Hz-20000 Hz, měnič 32 mm, výdrž baterie až 16 h"
            },
            new Product()
            {
                Id = 10,
                Name = "Sony True Wireless WF-SP800N, černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=RO980d9a",
                Price = 4199.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, do uší, True Wireless, Bluetooth, aktivní potlačení okolního hluku (Noise Cancelling), obě sluchátka ve funkci master, možnost nošení pouze jednoho sluchátka, bezpečné uchycení pro sportování, podpora audio formátu SBC/AAC, vestavěný mikrofon pro hlasové ovládání a handsfree volání, výdrž na baterii až 9 hodin + dalších 9 hodin při dobíjení z pouzdra, odolnost vůči prachu a vodě s normou IP55, váha sluchátek 2x 9,8g, váha pouzdra cca 59 gramů, funkce quick charge pro rychlé nabíjení, barva černá"
            },
            new Product()
            {
                Id = 11,
                Name = "JBL Club ONE",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JJ093aa3",
                Price = 9790.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 5.0, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, frekvenční rozsah 10 Hz-40000 Hz, měnič 40 mm, odnímatelný kabel 1,5 m, výdrž baterie až 45 h"
            },
            new Product()
            {
                Id = 12,
                Name = "JBL Under Armour Sport Wireless Train černo-červená",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JJ094c5",
                Price = 4990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 4.1, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX4, frekvenční rozsah 16 Hz-20000 Hz, citlivost 101 dB/mW, impedance 32 Ohm, měnič 40 mm, odnímatelný kabel 1,25 m, výdrž baterie až 16 h"
            },
            new Product()
            {
                Id = 13,
                Name = "JBL Under Armour True Wireless Flash černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JJ094c1",
                Price = 3390.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 4.2, přijímání hovorů, přepínání skladeb, certifikace IPX7 - voděodolná a potuodolná, frekvenční rozsah 20 Hz-20000 Hz, citlivost 113 dB/mW, impedance 14 Ohm, měnič 5,8 mm, výdrž baterie až 25 h (5 h+20 h)"
            },
            new Product()
            {
                Id = 14,
                Name = "Beats Solo Pro Wireless - More Matte Collection - červená",
                ImgUri = "/ImgW.ashx?fd=f3&cd=MON380a1a6",
                Price = 6990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth, aktivní potlačení hluku (ANC), přijímání hovorů, hlasový asistent, měnič 19,7 mm"
            },
            new Product()
            {
                Id = 15,
                Name = "Beats Studio3 Wireless - matná černá",
                ImgUri = "https://cdn.alza.cz",
                Price = 6490.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 4.0, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, odnímatelný kabel 1,2 m, výdrž baterie až 40 h"
            },
            new Product()
            {
                Id = 16,
                Name = "Beats PowerBeats Pro červená",
                ImgUri = "/ImgW.ashx?fd=f3&cd=MON303o81h",
                Price = 5990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless za uši, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX4, výdrž baterie až 24 h (9 h+15 h)"
            },
            new Product()
            {
                Id = 17,
                Name = "BOSE Noise Cancelling Headphones 700 černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=BOS310k92",
                Price = 7990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth, aktivní potlačení hluku (ANC), přepínání skladeb, hlasový asistent, odnímatelný kabel 1,06 m, výdrž baterie až 20 h"
            },
            new Product()
            {
                Id = 18,
                Name = "BOSE QuietComfort 35 II černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=BOS310k9bc",
                Price = 6289.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 4.1, NFC, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, hlasový asistent, odnímatelný kabel 1,2 m, výdrž baterie až 20 h"
            },
            new Product()
            {
                Id = 19,
                Name = "BOSE SoundLink AE wireless II - černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=BOS311r1",
                Price = 5990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth 4.0, NFC, s ovládáním hlasitosti, výdrž baterie až 15 h"
            },
            new Product()
            {
                Id = 20,
                Name = "BOSE SoundSport Free Wireless oranžová",
                ImgUri = "/ImgW.ashx?fd=f3&cd=BOS312g1b",
                Price = 3990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, certifikace IPX4, výdrž baterie až 15 h (5 h+10 h)"
            },
            new Product()
            {
                Id = 21,
                Name = "Koss BT/740iQZ černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JK983y2b",
                Price = 3999.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth 5.0, podpora AAC a aptX, aktivní potlačení hluku (ANC), přijímání hovorů"
            },
            new Product()
            {
                Id = 22,
                Name = "Koss TWS/150i černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JK997n1b",
                Price = 1999.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless pecky, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů"
            },
            new Product()
            {
                Id = 23,
                Name = "Koss KSC/35 Wireless černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JK997n1a",
                Price = 1899.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 4.2, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, frekvenční rozsah 15 Hz-25000 Hz, citlivost 101 dB/mW, výdrž baterie až 6 h"
            },
            new Product()
            {
                Id = 24,
                Name = "Koss GMR/545 AIR (dožitvotní záruka)",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JK616a",
                Price = 1799.00M,
                Description = "Herní sluchátka drátová, s mikrofonem, přes hlavu, okolo uší, otevřená konstrukce, 3,5 mm Jack, pro PC, s ovládáním hlasitosti, frekvenční rozsah 15 Hz-22000 Hz, citlivost 102 dB/mW, impedance 35 Ohm, odnímatelný kabel 3,6 m"
            },
            new Product()
            {
                Id = 25,
                Name = "Koss BT/539i černá (24 měsíců záruka)",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JK983y2",
                Price = 1499.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, frekvenční rozsah 10 Hz-20000 Hz, citlivost 97 dB/mW, impedance 38 Ohm"
            },
            new Product()
            {
                Id = 26,
                Name = "Koss PORTA PRO MIC (doživotní záruka)",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JK618set",
                Price = 1490.00M,
                Description = "Sluchátka s mikrofonem, přes hlavu, na uši, otevřená konstrukce, 3,5 mm Jack, s ovládáním hlasitosti, frekvenční rozsah 15 Hz-25000 Hz, citlivost 101 dB/mW, impedance 60 Ohm, kabel 1,22 m"
            },
            new Product()
            {
                Id = 27,
                Name = "Sennheiser HD 660S",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM162d1",
                Price = 10590.00M,
                Description = "Sluchátka přes hlavu, okolo uší, otevřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, frekvenční rozsah 10 Hz-40000 Hz, citlivost 104 dB/mW, impedance 150 Ohm, odnímatelný kabel 3 m"
            },
            new Product()
            {
                Id = 28,
                Name = "Sennheiser RS 195 U",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM171m",
                Price = 9590.00M,
                Description = "Bezdrátová sluchátka přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, radiofrekvenční připojení, s ovládáním hlasitosti, frekvenční rozsah 17 Hz-22000 Hz, citlivost 117 dB/mW, výdrž baterie až 18 h"
            },
            new Product()
            {
                Id = 29,
                Name = "Sennheiser GSP 670",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM196q",
                Price = 8790.00M,
                Description = "Herní sluchátka bezdrátová, s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth s donglem, aktivní potlačení hluku (ANC), prostorový zvuk 7.1, přijímání hovorů, frekvenční rozsah 10 Hz-23000 Hz, výdrž baterie až 20 h"
            },
            new Product()
            {
                Id = 30,
                Name = "Soundpeats Truewings",
                ImgUri = "/ImgW.ashx?fd=f3&cd=SOUND001c",
                Price = 1690.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless za uši, uzavřená konstrukce, Bluetooth 5.0, podpora SBC, s ovládáním hlasitosti, přijímání hovorů, certifikace IPX7 - voděodolná a potuodolná, frekvenční rozsah 20 Hz-20000 Hz, citlivost 103 dB/mW, impedance 32 Ohm, měnič 13,6 mm, výdrž baterie až 22 h (4 h+18 h)"
            },
            new Product()
            {
                Id = 31,
                Name = "Sennheiser MOMENTUM True Wireless",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM166t1",
                Price = 6290.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, podpora AAC a aptX, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX4, frekvenční rozsah 5 Hz-21000 Hz, citlivost 107 dB/mW, měnič 7 mm, výdrž baterie až 16 h (4 h+12 h)"
            },
            new Product()
            {
                Id = 32,
                Name = "Sennheiser HD 25 PLUS",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM123g",
                Price = 4899.00M,
                Description = "Sluchátka přes hlavu, na uši, uzavřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, frekvenční rozsah 16 Hz-22000 Hz, citlivost 120 dB/mW, impedance 70 Ohm, odnímatelný kabel 3 m"
            },
            new Product()
            {
                Id = 33,
                Name = "Sennheiser HD 450BT Black",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM300a2a1",
                Price = 4390.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, Bluetooth 5.0, podpora AAC a aptX, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, s vibracemi, frekvenční rozsah 18 Hz-22000 Hz, výdrž baterie až 30 h"
            },
            new Product()
            {
                Id = 34,
                Name = "Sennheiser HD 25",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM123e",
                Price = 4059.00M,
                Description = "Sluchátka přes hlavu, na uši, uzavřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, frekvenční rozsah 16 Hz-22000 Hz, citlivost 120 dB/mW, impedance 70 Ohm, odnímatelný kabel 1,5 m"
            },
            new Product()
            {
                Id = 35,
                Name = "Sennheiser HD 569",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM158g",
                Price = 3990.00M,
                Description = "Sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, 6,3 mm Jack, frekvenční rozsah 10 Hz-28000 Hz, citlivost 115 dB/mW, impedance 23 Ohm, odnímatelný kabel 1,2 m"
            },
            new Product()
            {
                Id = 36,
                Name = "Sennheiser CX 350BT černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JM201g1",
                Price = 2590.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, špunty, uzavřená konstrukce, Bluetooth 5.0, podpora AAC, aptX a SBC, přijímání hovorů, přepínání skladeb, frekvenční rozsah 17 Hz-20000 Hz, impedance 28 Ohm, výdrž baterie až 10 h"
            },
            new Product()
            {
                Id = 37,
                Name = "Pioneer SE-E9TW-P růžová",
                ImgUri = "/ImgW.ashx?fd=f3&cd=PIO633f44c",
                Price = 4190.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, přijímání hovorů, přepínání skladeb, hlasový asistent, frekvenční rozsah 20 Hz-20000 Hz, měnič 6 mm, výdrž baterie až 20 h (5 h+15 h)"
            },
            new Product()
            {
                Id = 38,
                Name = "Pioneer SE-MS7BT-T hnědá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=PIO633g9g",
                Price = 3690.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth, NFC, frekvenční rozsah 9 Hz-40000 Hz, impedance 32 Ohm, měnič 40 mm"
            },
            new Product()
            {
                Id = 39,
                Name = "Pioneer SE-S6BN-B černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=PIO633h2",
                Price = 3490.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth 5.0, aktivní potlačení hluku (ANC), s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, frekvenční rozsah 20 Hz-22000 Hz, měnič 40 mm, odnímatelný kabel 1,2 m, výdrž baterie až 30 h"
            },
            new Product()
            {
                Id = 40,
                Name = "Pioneer SE-MS5T-K černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=PIO633g9a",
                Price = 1390.00M,
                Description = "Sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, přijímání hovorů, frekvenční rozsah 9 Hz-40000 Hz, citlivost 96 dB/mW, impedance 32 Ohm, měnič 40 mm, odnímatelný kabel 1,2 m"
            },
            new Product()
            {
                Id = 41,
                Name = "Pioneer SE-MJ503-K černá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=PIO630m6",
                Price = 649.00M,
                Description = "Sluchátka přes hlavu, na uši, uzavřená konstrukce, 3,5 mm Jack, frekvenční rozsah 10 Hz-24000 Hz, citlivost 100 dB/mW, impedance 32 Ohm, měnič 30 mm, kabel 1,2 m"
            },
            new Product()
            {
                Id = 42,
                Name = "Soundpeats Truengine SE",
                ImgUri = "/ImgW.ashx?fd=f3&cd=SOUND001a",
                Price = 1249.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, True Wireless špunty, uzavřená konstrukce, Bluetooth 5.0, podpora aptX a SBC, s ovládáním hlasitosti, přijímání hovorů, certifikace IPX5, frekvenční rozsah 20 Hz-20000 Hz, citlivost 97 dB/mW, impedance 16 Ohm, výdrž baterie až 27 h (6 h+21 h)"
            },
            new Product()
            {
                Id = 43,
                Name = "Acer Predator Gaming Headset Galea 350",
                ImgUri = "/ImgW.ashx?fd=f3&cd=NC472j9k",
                Price = 2199.00M,
                Description = "Herní sluchátka drátová, s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, USB-A, pro PC, s ovládáním hlasitosti, prostorový zvuk 7.1, frekvenční rozsah 20 Hz-20000 Hz, citlivost 116 dB/mW, impedance 32 Ohm, měnič 50 mm"
            },
            new Product()
            {
                Id = 44,
                Name = "Acer Predator Gaming Headset Galea 311",
                ImgUri = "/ImgW.ashx?fd=f3&cd=NC472j9a",
                Price = 1669.00M,
                Description = "Herní sluchátka drátová, s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, pro PC, s ovládáním hlasitosti, frekvenční rozsah 20 Hz-20000 Hz, citlivost 115 dB/mW, impedance 32 Ohm, měnič 50 mm"
            },
            new Product()
            {
                Id = 45,
                Name = "Acer Nitro Gaming Headset",
                ImgUri = "/ImgW.ashx?fd=f3&cd=NC472j8",
                Price = 699.00M,
                Description = "Herní sluchátka drátová, s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, 3,5 mm Jack, pro PC, frekvenční rozsah 20 Hz-20000 Hz, citlivost 100 dB/mW, impedance 21 Ohm, měnič 50 mm, kabel 1,2 m"
            },
            new Product()
            {
                Id = 46,
                Name = "AfterShokz Aeropex černá",
                ImgUri = string.Empty,
                Price = 4290.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, na lícní kosti, uzavřená konstrukce, Bluetooth 5.0, aktivní potlačení hluku (ANC), přijímání hovorů, přepínání skladeb, certifikace IPX7 - voděodolná a potuodolná, frekvenční rozsah 20 Hz-20000 Hz, citlivost 105 dB/mW, výdrž baterie až 8 h"
            },
            new Product()
            {
                Id = 47,
                Name = "AfterShokz Trekz Air šedá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=AKZ120o41",
                Price = 3090.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, za uši, Bluetooth 4.2, přijímání hovorů, přepínání skladeb, certifikace IPX5, frekvenční rozsah 20 Hz-20000 Hz, citlivost 100 dB/mW, výdrž baterie až 6 h"
            },
            new Product()
            {
                Id = 48,
                Name = "AfterShokz Trekz Titanium šedá",
                ImgUri = "/ImgW.ashx?fd=f3&cd=AKZ120k3",
                Price = 1990.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, na lícní kosti, uzavřená konstrukce, Bluetooth 4.1, s ovládáním hlasitosti, přijímání hovorů, přepínání skladeb, hlasový asistent, certifikace IPX5, frekvenční rozsah 20 Hz-20000 Hz, citlivost 100 dB/mW, výdrž baterie až 6 h"
            },
            new Product()
            {
                Id = 49,
                Name = "JVC HA-S90BN-Z ",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JVC274l",
                Price = 2339.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, okolo uší, uzavřená konstrukce, Bluetooth, podpora SBC, NFC, aktivní potlačení hluku (ANC), frekvenční rozsah 8 Hz-25000 Hz, citlivost 100 dB/mW, měnič 40 mm, odnímatelný kabel 1,2 m, výdrž baterie až 27 h"
            },
            new Product()
            {
                Id = 50,
                Name = "JVC HA-S30BT R",
                ImgUri = "/ImgW.ashx?fd=f3&cd=JVC274b",
                Price = 989.00M,
                Description = "Bezdrátová sluchátka s mikrofonem, přes hlavu, na uši, uzavřená konstrukce, Bluetooth, s ovládáním hlasitosti, frekvenční rozsah 20 Hz-20000 Hz, měnič 30 mm, výdrž baterie až 17 h"
            }
        };
        #endregion
    }
}
