using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechMundo.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteNombre = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    ClienteEmail = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Caracteristicas = table.Column<string>(type: "TEXT", maxLength: 1500, nullable: true),
                    ImagenUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    StockMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Disponible = table.Column<bool>(type: "INTEGER", nullable: false),
                    Destacado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VentaItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: true),
                    NombreProducto = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaItems_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VentaItems_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Computadoras" },
                    { 2, "Celulares" },
                    { 3, "Accesorios" },
                    { 4, "Gaming" },
                    { 5, "Redes" }
                });

            migrationBuilder.InsertData(
                table: "Ventas",
                columns: new[] { "Id", "ClienteEmail", "ClienteNombre", "Fecha", "Total" },
                values: new object[,]
                {
                    { 1, "mateo.diaz@gmail.com", "Mateo Díaz", new DateTime(2026, 9, 8, 10, 15, 0, 0, DateTimeKind.Unspecified), 1001000m },
                    { 2, "martin.gomez@hotmail.com", "Martín Gómez", new DateTime(2026, 9, 10, 18, 40, 0, 0, DateTimeKind.Unspecified), 1316000m },
                    { 3, "lucia.fernandez@gmail.com", "Lucía Fernández", new DateTime(2026, 9, 12, 12, 5, 0, 0, DateTimeKind.Unspecified), 216000m },
                    { 4, "joaquin.perez@gmail.com", "Joaquín Pérez", new DateTime(2026, 9, 14, 20, 20, 0, 0, DateTimeKind.Unspecified), 76000m },
                    { 5, "nicolas.torres@gmail.com", "Nicolás Torres", new DateTime(2026, 9, 17, 9, 50, 0, 0, DateTimeKind.Unspecified), 770000m },
                    { 6, "nicolas.torres@gmail.com", "Nicolás Torres", new DateTime(2026, 9, 19, 16, 35, 0, 0, DateTimeKind.Unspecified), 3463000m },
                    { 7, "agustina.sosa@hotmail.com", "Agustina Sosa", new DateTime(2026, 9, 22, 11, 10, 0, 0, DateTimeKind.Unspecified), 278000m },
                    { 8, "agustina.sosa@hotmail.com", "Agustina Sosa", new DateTime(2026, 9, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), 123000m },
                    { 9, "martin.gomez@gmail.com", "Martín Gómez", new DateTime(2026, 9, 27, 14, 25, 0, 0, DateTimeKind.Unspecified), 1545000m },
                    { 10, "nicolas.torres@yahoo.com.ar", "Nicolás Torres", new DateTime(2026, 9, 30, 10, 45, 0, 0, DateTimeKind.Unspecified), 77000m },
                    { 11, "julieta.alvarez@outlook.com", "Julieta Álvarez", new DateTime(2026, 10, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), 832000m },
                    { 12, "valentina.lopez@hotmail.com", "Valentina López", new DateTime(2026, 10, 2, 13, 15, 0, 0, DateTimeKind.Unspecified), 1230000m },
                    { 13, "martin.gomez@outlook.com", "Martín Gómez", new DateTime(2026, 10, 3, 21, 5, 0, 0, DateTimeKind.Unspecified), 1072000m },
                    { 14, "nicolas.torres@yahoo.com.ar", "Nicolás Torres", new DateTime(2026, 10, 4, 12, 40, 0, 0, DateTimeKind.Unspecified), 3850000m },
                    { 15, "camila.rodriguez@yahoo.com.ar", "Camila Rodríguez", new DateTime(2026, 10, 5, 9, 20, 0, 0, DateTimeKind.Unspecified), 2156000m }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Caracteristicas", "CategoriaId", "CreadoEn", "Descripcion", "Destacado", "Disponible", "ImagenUrl", "Marca", "Nombre", "Precio", "Stock", "StockMinimo", "Version" },
                values: new object[,]
                {
                    { 1, "Procesador: AMD Ryzen AI 7 350\nGráficos: NVIDIA GeForce RTX 5060 8GB GDDR7\nMemoria RAM: 16GB DDR5\nAlmacenamiento: SSD PCIe Gen 4 de 1TB\nPantalla: 16\" WUXGA IPS 180Hz\nSistema: Windows 11 Copilot+ PC", 1, new DateTime(2026, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portátil gaming de 16 pulgadas con Ryzen AI 7 350 y GeForce RTX 5060, certificado Copilot+ PC. Alto rendimiento en juegos y creación de contenido con pantalla de 180 Hz.", true, true, "https://productimages.microcenter.com/0697599_888966.jpg", "Acer", "Acer Nitro V 16 AI (ANV16-61-R8UE)", 1463000m, 6, 3, 0 },
                    { 2, "Procesador: AMD Ryzen AI 7 350\nGráficos: NVIDIA GeForce RTX 5060 8GB GDDR7\nMemoria RAM: 16GB DDR5\nAlmacenamiento: SSD NVMe de 1TB\nPantalla: 15.6\" Full HD IPS 144Hz\nColor: Mica Silver", 1, new DateTime(2026, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portátil gaming de 15.6 pulgadas para quienes inician en el mundo gamer, con Ryzen AI 7 350 y RTX 5060. Incluye el centro de control OMEN Gaming Hub.", false, true, "https://productimages.microcenter.com/0696088_871210.jpg", "HP", "HP Victus 15-fb3001nr", 1386000m, 5, 3, 0 },
                    { 3, "Procesador: Intel Core Ultra 7 258V\nPantalla: 14\" 2.8K (2880 x 1800) OLED\nMemoria RAM: 32GB LPDDR5x\nAlmacenamiento: SSD PCIe Gen 4 (hasta 2TB)\nConectividad: Wi-Fi 7 y Thunderbolt 4\nPeso: aprox. 1 kg, Windows 11 Pro", 1, new DateTime(2026, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portátil profesional ultraligero de 14 pulgadas con pantalla OLED 2.8K y Intel Core Ultra 7 de la serie 2. Pesa alrededor de 1 kg y ofrece gran autonomía y seguridad empresarial.", false, true, "https://productimages.microcenter.com/0702642_940809.jpg", "Lenovo", "Lenovo ThinkPad X1 Carbon Gen 13 Aura Edition", 2926000m, 3, 2, 0 },
                    { 4, "Chip: Apple M5 (CPU de 10 núcleos)\nPantalla: Liquid Retina de 13.6\"\nMemoria unificada: 16GB (base)\nAlmacenamiento: SSD de 512GB (base)\nPuertos: 2 Thunderbolt 4 y MagSafe\nBatería: hasta 18 horas, Wi-Fi 7 y Touch ID", 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ultrabook delgada y silenciosa con el chip Apple M5, pantalla Liquid Retina de 13.6 pulgadas y hasta 18 horas de batería. Incluye Wi-Fi 7 y carga MagSafe.", true, true, "https://www.apple.com/v/macbook-air/z/images/meta/macbook_air_mx__ez5y0k5yy7au_og.png?202608191258", "Apple", "Apple MacBook Air 13\" con chip M5", 1692000m, 7, 3, 0 },
                    { 5, "Chip: Apple M6 (CPU y GPU de 12 núcleos)\nMemoria unificada: 16GB (hasta 32GB)\nAlmacenamiento: SSD de 256GB (base)\nPuertos: 5 Thunderbolt, 2 USB-C, HDMI\nRed: Ethernet 2.5Gb, Wi-Fi 7, Bluetooth 6\nTamaño: 5\" x 5\"", 1, new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mini PC ultracompacto de 5 x 5 pulgadas con chip M6 de 12 núcleos, ideal para oficina, edición y flujos de trabajo con IA. Soporta hasta dos pantallas 6K y una 5K.", false, true, "https://www.apple.com/v/mac-mini/ab/images/meta/mac-mini__dvce2jrm11w2_og.jpg?202609211742", "Apple", "Apple Mac mini con chip M6", 1384000m, 4, 2, 0 },
                    { 6, "Pantalla: 27\" IPS Black 4K UHD (3840 x 2160)\nFrecuencia: 120Hz\nColor: 99% DCI-P3, contraste 3000:1\nBrillo: hasta 600 nits\nConectividad: Thunderbolt 4 (140W), DP 1.4, HDMI 2.1\nExtras: Ethernet 2.5GbE y soporte ajustable", 1, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monitor profesional de 27 pulgadas 4K con panel IPS Black de 120 Hz y hub Thunderbolt 4 integrado. Colores precisos y conexión de un solo cable con carga de hasta 140W.", false, true, "https://www.bhphotovideo.com/images/fb/dell_u2725qe_ultrasharp_27_4k_uhd_1889853.jpg", "Dell", "Dell UltraSharp 27 4K Thunderbolt Hub U2725QE", 1232000m, 2, 3, 0 },
                    { 7, "Procesador: Intel Core 7 240H\nGráficos: Intel integrados\nMemoria: DDR5 SODIMM (hasta 96GB)\nAlmacenamiento: diseño dual con M.2 PCIe 4.0\nConectividad: Wi-Fi 7, Bluetooth 5.4, Thunderbolt 4\nSoporte para hasta cuatro pantallas 4K", 1, new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mini PC barebone compacto con Intel Core 7 240H, Wi-Fi 7 y Thunderbolt 4, pensado para oficina, negocios y aplicaciones de IA. Se vende sin RAM ni almacenamiento.", false, true, "https://productimages.microcenter.com/0704710_961219.jpg", "ASUS", "ASUS NUC 15 Pro Mini PC (Core 7 240H)", 693000m, 9, 4, 0 },
                    { 8, "Procesador: Intel Core Ultra 9 275HX\nGráficos: NVIDIA GeForce RTX 5080 16GB\nRefrigeración: sistema de triple ventilador\nPantallas: soporta hasta cinco monitores 4K\nFormato: mini PC gaming compacto\nArquitectura: Intel Core Ultra Serie 2", 1, new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mini PC gaming de alto rendimiento con Intel Core Ultra 9 275HX y GeForce RTX 5080 de 16GB en un chasis compacto con triple ventilador. Admite hasta cinco pantallas 4K.", false, false, "https://productimages.microcenter.com/0697039_883835.jpg", "ASUS", "ASUS ROG NUC (2025) con RTX 5080", 3850000m, 0, 2, 0 },
                    { 9, "Pantalla: 6,1\" Super Retina XDR OLED 60 Hz\nChip: Apple A18\nCámara trasera: dual 48 MP + 12 MP ultra gran angular\nCámara frontal: 12 MP TrueDepth\nAlmacenamiento: 128/256/512 GB\nConectividad: 5G, USB-C, Wi-Fi 7", 2, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartphone con chip A18, botón de Control de cámara y Apple Intelligence. Ofrece un sistema de cámara dual de 48 MP y gran autonomía.", true, true, "https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/iphone-16-finish-select-202409-6-1inch_GEO_US?wid=1200&hei=630&fmt=jpeg&qlt=95&.v=1724710494447", "Apple", "iPhone 16", 1230000m, 12, 5, 0 },
                    { 10, "Pantalla: 6,2\" Dynamic AMOLED 2X 120 Hz\nProcesador: Snapdragon 8 Elite for Galaxy\nCámara trasera: triple 50 MP + 12 MP + 10 MP\nRAM: 12 GB\nBatería: 4.000 mAh, carga rápida 25 W\nAlmacenamiento: 128/256/512 GB", 2, new DateTime(2026, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teléfono compacto de gama alta con Snapdragon 8 Elite for Galaxy y funciones Galaxy AI. Combina una pantalla brillante con cámara triple versátil.", true, true, null, "Samsung", "Samsung Galaxy S25", 1232000m, 10, 4, 0 },
                    { 11, "Pantalla: 6,7\" Super AMOLED 120 Hz\nProcesador: Exynos 1580\nCámara trasera: triple 50 MP OIS + 12 MP + 5 MP\nBatería: 5.000 mAh, carga 45 W\nRAM/Almacenamiento: 8 GB / 128-256 GB\nResistencia: IP67", 2, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gama media con pantalla AMOLED fluida y cámara de 50 MP con estabilización óptica. Incluye funciones de IA y varios años de actualizaciones.", false, true, null, "Samsung", "Samsung Galaxy A56 5G", 770000m, 14, 5, 0 },
                    { 12, "Pantalla: 6,3\" Actua OLED 120 Hz\nProcesador: Google Tensor G4\nCámara trasera: dual 50 MP + 48 MP ultra gran angular\nRAM: 12 GB\nBatería: 4.700 mAh, carga 27 W\nActualizaciones: 7 años", 2, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smartphone con chip Tensor G4 y funciones de IA de Google como Gemini. Destaca por su fotografía computacional y siete años de actualizaciones.", false, true, null, "Google", "Google Pixel 9", 1230000m, 2, 4, 0 },
                    { 13, "Pantalla: 6,67\" AMOLED 144 Hz\nProcesador: MediaTek Dimensity 8300-Ultra\nCámara trasera: triple Leica 50 MP + 50 MP tele + 12 MP\nBatería: 5.000 mAh, carga 67 W\nRAM/Almacenamiento: 12 GB / 256-512 GB\nResistencia: IP68", 2, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Móvil de gama alta con óptica Leica y pantalla AMOLED de 144 Hz. Ofrece carga rápida de 67 W y gran rendimiento con Dimensity 8300-Ultra.", false, true, "https://i02.appmifile.com/293_operatorx_operatorx_opx/27/09/2024/d714b3e4e95bd2d4817f5d3458b92c45.png", "Xiaomi", "Xiaomi 14T", 999000m, 9, 4, 0 },
                    { 14, "Pantalla: 6,7\" pOLED 120 Hz\nProcesador: MediaTek Dimensity 7400\nCámara trasera: 50 MP + 50 MP ultra gran angular + 10 MP tele\nBatería: 5.200 mAh, carga 68 W\nRAM/Almacenamiento: 8 GB / 256 GB\nResistencia: IP68/IP69", 2, new DateTime(2026, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teléfono de gama media-alta con pantalla pOLED curva y diseño delgado con acabado Pantone. Incluye carga rápida de 68 W y cámara de 50 MP.", false, true, null, "Motorola", "Motorola Edge (2025)", 847000m, 8, 4, 0 },
                    { 15, "Pantalla: 10,9\" LCD 90 Hz\nProcesador: Exynos 1580\nCámara trasera: 13 MP; frontal 12 MP\nBatería: 8.000 mAh, carga 45 W\nRAM/Almacenamiento: 6-8 GB / 128-256 GB\nIncluye S Pen; resistencia IP68", 2, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tableta de 10,9\" con pantalla de 90 Hz y S Pen incluido. Ideal para estudio, entretenimiento y productividad con resistencia IP68.", false, true, "https://images.samsung.com/is/image/samsung/assets/us/tablets/galaxy-tab-s10-fe/04082025/Tab-S10FE_HD01_KV_PC-V3.jpg", "Samsung", "Samsung Galaxy Tab S10 FE", 770000m, 6, 3, 0 },
                    { 16, "Pantalla: 11\" Liquid Retina IPS 60 Hz\nChip: Apple M3\nCámara trasera: 12 MP; frontal 12 MP Center Stage\nAlmacenamiento: 128/256/512 GB/1 TB\nConectividad: Wi-Fi 6E, 5G opcional, USB-C\nCompatible con Apple Pencil Pro", 2, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tableta con chip M3 y pantalla Liquid Retina de 11 pulgadas. Compatible con Apple Pencil Pro y Magic Keyboard para trabajo y creación.", false, true, "https://www.apple.com/v/ipad-air/ah/images/meta/ipad-air_overview__bc2fd15uec0y_og.png?202609021131", "Apple", "Apple iPad Air 11\" (M3)", 922000m, 5, 3, 0 },
                    { 17, "Sensor óptico de 8.000 DPI\nClics silenciosos (90 % menos ruido)\nRueda MagSpeed electromagnética de 1.000 líneas por segundo\nSeguimiento incluso sobre vidrio\nSoftware Logi Options+ con Flow multidispositivo\nFabricado con plástico reciclado", 3, new DateTime(2026, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mouse inalámbrico ergonómico para productividad avanzada, con clics silenciosos y rueda MagSpeed. Funciona incluso sobre superficies de vidrio.", true, true, "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/mice/mx-master-3s/2025-update/mx-master-3s-bluetooth-edition-top-view-black-new-1.png", "Logitech", "Logitech MX Master 3S", 154000m, 18, 6, 0 },
                    { 18, "Teclas esféricas con forma cóncava para los dedos\nRetroiluminación inteligente que se activa al acercar las manos\nCambio entre 3 dispositivos\nCompatible con Windows, macOS, Linux, ChromeOS e iPadOS\nTeclas programables y Smart Actions\nFabricado con plástico reciclado", 3, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teclado inalámbrico de perfil bajo con teclas esféricas y retroiluminación inteligente. Ideal para escribir con comodidad en varios dispositivos.", false, true, "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/keyboards/mx-keys-s/migration-assets-for-delorean-2025/gallery/mx-keys-s-top-view-pale-gray-us.png", "Logitech", "Logitech MX Keys S", 185000m, 11, 5, 0 },
                    { 19, "Sensor de 8.000 DPI para cualquier superficie\nRueda MagSpeed de 1.000 líneas por segundo\nClics silenciosos\nBluetooth para hasta 3 dispositivos\nBatería de hasta 70 días con carga USB-C\nFabricado con plástico reciclado", 3, new DateTime(2026, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mouse inalámbrico compacto y silencioso para trabajar en cualquier lugar. Sensor preciso en cualquier superficie y hasta 70 días de batería.", false, true, "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/mice/mx-anywhere-3s/product-gallery/rose/mx-anywhere-3s-mouse-top-view-rose.png", "Logitech", "Logitech MX Anywhere 3S", 139000m, 4, 5, 0 },
                    { 20, "Video Full HD 1080p a 30 fps\nCampo de visión diagonal de 78°\nLente de vidrio de cinco elementos con autoenfoque\nDos micrófonos con sonido estéreo\nCorrección de luz automática RightLight 2\nCubierta de privacidad integrada", 3, new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cámara web Full HD con enfoque automático, audio estéreo y cubierta de privacidad integrada. Perfecta para videollamadas y transmisiones.", false, true, "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/webcams/c920s/gallery/c920s-gallery-1.png", "Logitech", "Logitech C920s Pro HD Webcam", 108000m, 15, 5, 0 },
                    { 21, "Resolución 4K a 30 fps o 1080p a 60 fps\nSensor de imagen de 13 MP\nCampo de visión ajustable de 65°, 78° o 90°\nZoom digital 5x y autoenfoque\nSensor infrarrojo para Windows Hello\nConexión USB con obturador de privacidad", 3, new DateTime(2026, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cámara web Ultra HD 4K con HDR y reconocimiento facial Windows Hello. Ofrece imagen nítida en cualquier condición de luz.", false, true, "https://resource.logitech.com/w_692,c_lpad,ar_4:3,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/webcams/brio/gallery/brio-gallery-1.png?v=1", "Logitech", "Logitech BRIO 4K Webcam", 308000m, 6, 3, 0 },
                    { 22, "Capacidad de 256 GB (microSDXC)\nLectura de hasta 190 MB/s\nEscritura de hasta 130 MB/s\nClases U3, V30 y A2 para video 4K y apps\nResistente a golpes, agua, temperatura y rayos X\nGarantía limitada de por vida", 3, new DateTime(2026, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tarjeta microSD de alto rendimiento para grabar video 4K, cámaras de acción, drones y teléfonos. Incluye adaptador SD.", false, true, "https://www.sandisk.com/content/dam/store/en-us/assets/products/memory-cards/extreme-uhs-i-microsd/featured/extreme-uhs-i-microsd-adapter.jpg", "SanDisk", "SanDisk Extreme microSDXC UHS-I 256GB", 54000m, 25, 8, 0 },
                    { 23, "Capacidad de 10.000 mAh\nCarga rápida de 30 W por USB-C\nCable USB-C integrado, puerto USB-C y puerto USB-A\nPantalla a color\nCarga bidireccional\nPeso de 215 g", 3, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Batería externa compacta de 10.000 mAh con cable USB-C integrado y carga rápida de 30 W. Incluye pantalla a color para ver el nivel de carga.", false, true, "https://cdn.shopify.com/s/files/1/0493/9834/9974/files/5_a7a61bbb-8693-46f2-8d44-de814496fc62.png?v=1764230004", "Anker", "Anker Nano Power Bank 10.000 mAh (30W)", 77000m, 20, 6, 0 },
                    { 24, "Capacidad de 1 TB\nLectura secuencial de hasta 1.050 MB/s\nEscritura secuencial de hasta 1.000 MB/s\nInterfaz USB 3.2 Gen 2\nResistente a golpes con control térmico\nDiseño ultraportátil en color gris", 3, new DateTime(2026, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unidad de estado sólido externa, compacta y resistente a golpes, con velocidades de hasta 1.050 MB/s. Ideal para creadores y gamers.", false, true, null, "Samsung", "Samsung Portable SSD T7 1TB", 423000m, 9, 4, 0 },
                    { 25, "SSD personalizado de 1 TB\nResolución 4K hasta 120 fps\nTrazado de rayos por hardware\nLector Ultra HD Blu-ray\nAudio 3D Tempest\nMando DualSense con respuesta háptica", 4, new DateTime(2026, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consola de nueva generación con SSD ultrarrápido, gráficos 4K y soporte para juegos de hasta 120 fps. Incluye lector Ultra HD Blu-ray y mando DualSense.", true, true, "https://gmedia.playstation.com/is/image/SIEPDC/ps5-product-thumbnail-01-en-14sep21?$facebook$", "Sony", "PlayStation 5 Slim (con lector de discos)", 847000m, 8, 3, 0 },
                    { 26, "SSD NVMe de 1 TB\n12 teraflops de potencia gráfica\n4K nativo hasta 120 fps\nTrazado de rayos acelerado por hardware\nLector Ultra HD Blu-ray 4K\nCompatible con Game Pass y Quick Resume", 4, new DateTime(2026, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "La consola Xbox más potente, con 12 teraflops de rendimiento gráfico y tiempos de carga mínimos. Compatible con miles de juegos de cuatro generaciones.", false, false, null, "Microsoft", "Xbox Series X 1 TB", 1001000m, 0, 3, 0 },
                    { 27, "Pantalla OLED de 7 pulgadas\n64 GB de almacenamiento interno\nModo TV, sobremesa y portátil\nAltavoces mejorados\nBase con puerto LAN integrado\nAutonomía de 4,5 a 9 horas", 4, new DateTime(2026, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consola híbrida para jugar en casa o en cualquier lugar, con una vibrante pantalla OLED de 7 pulgadas. Incorpora base con puerto LAN y soporte ajustable.", false, true, null, "Nintendo", "Nintendo Switch Modelo OLED", 539000m, 10, 4, 0 },
                    { 28, "Sensor Focus Pro de 30K DPI\nPeso de solo 59 g\nHasta 8000 Hz de polling rate (con HyperPolling)\nSwitches ópticos Gen-3\nCable Speedflex\nDiseño ergonómico para diestros", 4, new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mouse ergonómico ultraligero para esports con sensor óptico de alta precisión y cable flexible. Ideal para juegos competitivos de ritmo rápido.", false, true, "https://assets2.razerzone.com/images/pnx.assets/b993b88204244288b62d053375f5be2e/razer-deathadder-v3-og-image.webp", "Razer", "Razer DeathAdder V3", 108000m, 13, 5, 0 },
                    { 29, "Sonido envolvente virtual 7.1\nDrivers de 53 mm\nEstructura de aluminio\nMicrófono con cancelación de ruido desmontable\nEspuma viscoelástica con memoria\nCompatible con PC, PS5, Xbox y Switch", 4, new DateTime(2026, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Headset gaming con sonido envolvente virtual 7.1 y almohadillas de espuma viscoelástica muy cómodas. Estructura de aluminio resistente y micrófono desmontable.", false, true, null, "HyperX", "HyperX Cloud II", 154000m, 12, 5, 0 },
                    { 30, "Formato compacto 60 %\nConexión inalámbrica Lightspeed\nSwitches mecánicos GX Optical\nRetroiluminación RGB LIGHTSYNC\nHot-swap de switches\nHasta 65 horas de autonomía", 4, new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teclado mecánico inalámbrico compacto al 60 % pensado para jugadores profesionales. Conexión Lightspeed de baja latencia y switches ópticos intercambiables.", false, true, null, "Logitech G", "Logitech G PRO X 60 Lightspeed", 308000m, 5, 3, 0 },
                    { 31, "Retroalimentación háptica\nGatillos adaptativos\nMicrófono integrado y salida de audio de 3,5 mm\nTouchpad y barra de luz LED\nBatería recargable por USB-C\nCompatible con PS5 y PC", 4, new DateTime(2026, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mando con retroalimentación háptica y gatillos adaptativos que aumentan la inmersión. Incluye micrófono integrado y barra de luz LED.", false, true, null, "Sony", "Mando inalámbrico DualSense para PS5", 115000m, 16, 6, 0 },
                    { 32, "12 GB de memoria GDDR6X\n7168 núcleos CUDA\nDLSS 3 con generación de fotogramas\nTrazado de rayos de 3.ª generación\nConsumo gráfico de 220 W\nSalidas HDMI 2.1 y DisplayPort 1.4a", 4, new DateTime(2026, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tarjeta gráfica de arquitectura Ada Lovelace para juegos a 1440p con trazado de rayos y DLSS 3. Combina alto rendimiento con un consumo contenido.", false, true, null, "NVIDIA", "NVIDIA GeForce RTX 4070 SUPER Founders Edition", 924000m, 3, 2, 0 },
                    { 33, "Velocidad: AC1300 (867 Mbps en 5 GHz + 400 Mbps en 2,4 GHz)\nAntena externa de alta ganancia\nInterfaz: USB 3.0\nEstándares: 802.11a/n/ac (5 GHz) y 802.11b/g/n (2,4 GHz)\nSeguridad: WEP, WPA/WPA2, WPA-PSK/WPA2-PSK\nDoble banda 2,4 GHz y 5 GHz", 5, new DateTime(2026, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adaptador USB WiFi de doble banda AC1300 con antena externa de alta ganancia y conexión USB 3.0. Ideal para dar WiFi rápido y estable a PCs de escritorio.", true, true, "https://static.tp-link.com/Archer_T3U_Plus(EU)_1.0_01_large_1583734764040g.jpg", "TP-Link", "TP-Link Archer T3U Plus (Adaptador USB WiFi AC1300)", 38000m, 30, 8, 0 },
                    { 34, "Velocidad: AC600 (433 Mbps en 5 GHz + 200 Mbps en 2,4 GHz)\nDiseño nano de 15 × 7,1 × 18,6 mm\nInterfaz: USB 2.0\nEstándares: 802.11ac/a/n/g/b\nSeguridad: WEP 64/128 bits, WPA/WPA2, WPA-PSK/WPA2-PSK\nDoble banda 2,4 GHz y 5 GHz", 5, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adaptador USB WiFi nano de doble banda AC600, diseño ultracompacto que casi no sobresale del puerto. Perfecto para laptops y uso cotidiano.", false, true, "https://static.tp-link.com/Archer_T2U_Nano_EU_3.0_01_large_1536928386841s.jpg", "TP-Link", "TP-Link Archer T2U Nano (Adaptador USB WiFi AC600)", 20000m, 40, 10, 0 },
                    { 35, "Velocidad: AX1800 (1201 Mbps en 5 GHz + 574 Mbps en 2,4 GHz)\n2 antenas de doble banda de alta ganancia (ajustables 180°)\nInterfaz: USB 3.0 SuperSpeed con cable de 1 m\nWiFi 6 (802.11ax) con MU-MIMO y OFDMA\nSeguridad: WPA3 y WPA/WPA2\nCompatible con Windows 10/11", 5, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adaptador USB WiFi 6 AX1800 con dos antenas de alta ganancia ajustables y USB 3.0. Mayor velocidad, menor latencia y seguridad WPA3.", true, true, "https://static.tp-link.com/upload/image-line/Archer_TX20U_Plus_2000px_large_20230809095804r.jpg", "TP-Link", "TP-Link Archer TX20U Plus (Adaptador USB WiFi 6 AX1800)", 62000m, 14, 6, 0 },
                    { 36, "Velocidad: AC1200 (867 Mbps en 5 GHz + 300 Mbps en 2,4 GHz)\nDiseño nano ultracompacto\nInterfaz: USB 2.0\nDoble banda 2,4 GHz y 5 GHz\nAntenas internas MIMO 2x2\nCompatible con Windows y Linux (según versión)", 5, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adaptador USB WiFi nano de doble banda AC1200 con diseño muy compacto, pensado para laptops y PCs que necesitan conexión rápida y portátil.", false, true, "https://www.asus.com/media/global/gallery/sGgaGR1xdtbb5X06_setting_xxx_0_90_end_2000.png", "ASUS", "ASUS USB-AC53 Nano (Adaptador USB WiFi AC1200)", 38000m, 3, 5, 0 },
                    { 37, "Velocidad: AX3000 (2402 Mbps en 5 GHz + 574 Mbps en 2,4 GHz)\nPuertos: 1 WAN Gigabit + 4 LAN Gigabit + 1 USB 3.0\nProcesador de doble núcleo y 4 antenas externas\nWiFi 6 con OFDMA, beamforming y ancho de banda de 160 MHz\nSeguridad: WPA3 y TP-Link HomeShield\nCompatible con OneMesh y app Tether", 5, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Router WiFi 6 AX3000 de doble banda con puertos Gigabit y USB 3.0. Alta velocidad, baja latencia y compatibilidad con OneMesh.", false, true, "https://static.tp-link.com/upload/image-line/Overview_Archer_AX55_01_large_20210816012555f.jpg", "TP-Link", "TP-Link Archer AX55 (Router WiFi 6 AX3000)", 123000m, 11, 4, 0 },
                    { 38, "Velocidad: AX1800 (1201 Mbps en 5 GHz + 574 Mbps en 2,4 GHz)\n2 puertos Gigabit por unidad (WAN/LAN autodetectables)\nCobertura: pack de 2 hasta 370 m²\nHasta 150 dispositivos conectados\nAI-Driven Mesh, OFDMA y MU-MIMO\nSeguridad WPA3 y HomeShield con control parental", 5, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sistema WiFi mesh AX1800 con tecnología WiFi 6 y gestión por IA para cobertura uniforme y roaming sin cortes en todo el hogar.", false, true, "https://static.tp-link.com/Deco-X20-01_large_20220126092247t.jpg", "TP-Link", "TP-Link Deco X20 (Sistema Mesh WiFi 6, pack de 2)", 200000m, 7, 3, 0 },
                    { 39, "8 puertos RJ45 10/100/1000 Mbps con Auto-MDI/MDIX\nTasa de reenvío de paquetes: 11,9 Mpps\nCarcasa metálica de acero\nQoS e IGMP Snooping para multicast\nTecnología Green Ethernet y sin ventilador\nMontaje en escritorio o pared, plug-and-play", 5, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Switch de escritorio Gigabit de 8 puertos con carcasa metálica, instalación plug-and-play y funcionamiento silencioso sin ventilador.", false, true, "https://static.tp-link.com/upload/image-line/TL-SG108_UN_5.0_01_large_20231024111419x.jpg", "TP-Link", "TP-Link TL-SG108 (Switch Gigabit 8 puertos)", 31000m, 22, 6, 0 },
                    { 40, "Velocidad: AX1500 (1200 Mbps en 5 GHz + 300 Mbps en 2,4 GHz)\n1 puerto Ethernet Gigabit\n2 antenas externas\nModos de operación: extensor de rango y punto de acceso\nCompatible con OneMesh\nConsumo máximo: 10 W", 5, new DateTime(2026, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Extensor de rango WiFi 6 AX1500 de doble banda con puerto Gigabit y modo punto de acceso. Compatible con OneMesh para una red más amplia.", false, false, "https://static.tp-link.com/01_large_1620806188591r.jpg", "TP-Link", "TP-Link RE505X (Extensor WiFi 6 AX1500)", 77000m, 0, 4, 0 }
                });

            migrationBuilder.InsertData(
                table: "VentaItems",
                columns: new[] { "Id", "Cantidad", "NombreProducto", "PrecioUnitario", "ProductoId", "VentaId" },
                values: new object[,]
                {
                    { 1, 1, "Xbox Series X 1 TB", 1001000m, 26, 1 },
                    { 2, 1, "ASUS NUC 15 Pro Mini PC (Core 7 240H)", 693000m, 7, 2 },
                    { 3, 1, "Samsung Portable SSD T7 1TB", 423000m, 24, 2 },
                    { 4, 1, "TP-Link Deco X20 (Sistema Mesh WiFi 6, pack de 2)", 200000m, 38, 2 },
                    { 5, 2, "Razer DeathAdder V3", 108000m, 28, 3 },
                    { 6, 2, "ASUS USB-AC53 Nano (Adaptador USB WiFi AC1200)", 38000m, 36, 4 },
                    { 7, 1, "Samsung Galaxy Tab S10 FE", 770000m, 15, 5 },
                    { 8, 1, "Xbox Series X 1 TB", 1001000m, 26, 6 },
                    { 9, 1, "Apple MacBook Air 13\" con chip M5", 1692000m, 4, 6 },
                    { 10, 1, "Samsung Galaxy Tab S10 FE", 770000m, 15, 6 },
                    { 11, 2, "Logitech MX Anywhere 3S", 139000m, 19, 7 },
                    { 12, 1, "TP-Link Archer AX55 (Router WiFi 6 AX3000)", 123000m, 37, 8 },
                    { 13, 1, "TP-Link Archer AX55 (Router WiFi 6 AX3000)", 123000m, 37, 9 },
                    { 14, 1, "Xiaomi 14T", 999000m, 13, 9 },
                    { 15, 1, "Samsung Portable SSD T7 1TB", 423000m, 24, 9 },
                    { 16, 1, "TP-Link RE505X (Extensor WiFi 6 AX1500)", 77000m, 40, 10 },
                    { 17, 2, "Razer DeathAdder V3", 108000m, 28, 11 },
                    { 18, 1, "Logitech BRIO 4K Webcam", 308000m, 21, 11 },
                    { 19, 1, "Logitech G PRO X 60 Lightspeed", 308000m, 30, 11 },
                    { 20, 1, "Google Pixel 9", 1230000m, 12, 12 },
                    { 21, 1, "Logitech C920s Pro HD Webcam", 108000m, 20, 13 },
                    { 22, 2, "TP-Link Archer T2U Nano (Adaptador USB WiFi AC600)", 20000m, 34, 13 },
                    { 23, 1, "NVIDIA GeForce RTX 4070 SUPER Founders Edition", 924000m, 32, 13 },
                    { 24, 1, "ASUS ROG NUC (2025) con RTX 5080", 3850000m, 8, 14 },
                    { 25, 1, "Samsung Galaxy S25", 1232000m, 10, 15 },
                    { 26, 1, "NVIDIA GeForce RTX 4070 SUPER Founders Edition", 924000m, 32, 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Nombre",
                table: "Productos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_VentaItems_ProductoId",
                table: "VentaItems",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaItems_VentaId",
                table: "VentaItems",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Fecha",
                table: "Ventas",
                column: "Fecha");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentaItems");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
