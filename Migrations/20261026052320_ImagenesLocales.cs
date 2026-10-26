using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMundo.Migrations
{
    /// <inheritdoc />
    public partial class ImagenesLocales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagenUrl",
                value: "/img/productos/01-acer-nitro-v-16-ai-anv16-61-r8ue.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagenUrl",
                value: "/img/productos/02-hp-victus-15-fb3001nr.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagenUrl",
                value: "/img/productos/03-lenovo-thinkpad-x1-carbon-gen-13-aura-edition.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagenUrl",
                value: "/img/productos/04-apple-macbook-air-13-con-chip-m5.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagenUrl",
                value: "/img/productos/05-apple-mac-mini-con-chip-m6.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagenUrl",
                value: "/img/productos/07-asus-nuc-15-pro-mini-pc-core-7-240h.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Marca", "Nombre", "Precio" },
                values: new object[] { "Procesador: Broadcom BCM2712, 4 núcleos Cortex-A76 a 2,4 GHz\nMemoria: 8 GB LPDDR4X\nGráficos: VideoCore VII, dos salidas micro HDMI 4K60\nPuertos: 2 USB 3.0, 2 USB 2.0 y Ethernet Gigabit\nConectividad: Wi-Fi 5 doble banda y Bluetooth 5.0\nAlmacenamiento: microSD y PCIe 2.0 x1", "Computadora de placa única para aprender programación, electrónica y proyectos maker. Funciona como mini PC de escritorio con dos monitores 4K.", "/img/productos/08-raspberry-pi-5-8-gb.webp", "Raspberry Pi", "Raspberry Pi 5 (8 GB)", 123000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagenUrl",
                value: "/img/productos/09-iphone-16.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagenUrl",
                value: "/img/productos/10-samsung-galaxy-s25.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImagenUrl",
                value: "/img/productos/11-samsung-galaxy-a56-5g.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImagenUrl",
                value: "/img/productos/12-google-pixel-9.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImagenUrl",
                value: "/img/productos/13-xiaomi-14t.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImagenUrl",
                value: "/img/productos/15-samsung-galaxy-tab-s10-fe.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImagenUrl",
                value: "/img/productos/16-apple-ipad-air-11-m3.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImagenUrl",
                value: "/img/productos/17-logitech-mx-master-3s.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImagenUrl",
                value: "/img/productos/18-logitech-mx-keys-s.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImagenUrl",
                value: "/img/productos/19-logitech-mx-anywhere-3s.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImagenUrl",
                value: "/img/productos/20-logitech-c920s-pro-hd-webcam.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 21,
                column: "ImagenUrl",
                value: "/img/productos/21-logitech-brio-4k-webcam.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 22,
                column: "ImagenUrl",
                value: "/img/productos/22-sandisk-extreme-microsdxc-uhs-i-256gb.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 23,
                column: "ImagenUrl",
                value: "/img/productos/23-anker-nano-power-bank-10-000-mah-30w.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Nombre", "Precio" },
                values: new object[] { "Capacidad de 1 TB\nLectura secuencial de hasta 1.050 MB/s\nEscritura secuencial de hasta 1.000 MB/s\nInterfaz USB 3.2 Gen 2 (USB-C)\nResistencia IP65 al polvo y al agua\nResiste caídas de hasta 3 metros", "SSD externo robusto con recubrimiento de goma y protección contra polvo y agua. Velocidades de hasta 1.050 MB/s para creadores y gamers.", "/img/productos/24-samsung-portable-ssd-t7-shield-1tb.webp", "Samsung Portable SSD T7 Shield 1TB", 169000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 25,
                column: "ImagenUrl",
                value: "/img/productos/25-playstation-5-slim-con-lector-de-discos.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 26,
                column: "ImagenUrl",
                value: "/img/productos/26-xbox-series-x-1-tb.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 27,
                column: "ImagenUrl",
                value: "/img/productos/27-nintendo-switch-modelo-oled.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 28,
                column: "ImagenUrl",
                value: "/img/productos/28-razer-deathadder-v3.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Nombre", "Precio" },
                values: new object[] { "Formato tenkeyless (sin pad numérico)\nSwitches mecánicos GX Blue (clicky)\nRetroiluminación RGB LIGHTSYNC por tecla\nCable USB desmontable\nMemoria integrada para perfiles\nPatas de inclinación de 3 posiciones", "Teclado mecánico compacto sin pad numérico, diseñado junto a jugadores profesionales de esports. Ligero y con cable desmontable para llevar a torneos.", "/img/productos/30-logitech-g-pro-teclado-mecanico-gaming-tkl.webp", "Logitech G PRO Teclado mecánico gaming TKL", 200000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 31,
                column: "ImagenUrl",
                value: "/img/productos/31-mando-inalambrico-dualsense-para-ps5.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Marca", "Nombre", "Precio" },
                values: new object[] { "16 GB de memoria GDDR6X\n8448 núcleos CUDA\nDLSS 3 con generación de fotogramas\nTriple ventilador Axial-tech\nSalidas: 2 HDMI 2.1a y 3 DisplayPort 1.4a\nConsumo gráfico de 285 W", "Placa de video de alta gama para jugar en 1440p y 4K con trazado de rayos y DLSS 3. Refrigeración de triple ventilador y diseño ROG con iluminación Aura Sync.", "/img/productos/32-asus-rog-strix-geforce-rtx-4070-ti-super-oc-16gb.webp", "ASUS", "ASUS ROG Strix GeForce RTX 4070 Ti SUPER OC 16GB", 1386000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 33,
                column: "ImagenUrl",
                value: "/img/productos/33-tp-link-archer-t3u-plus-adaptador-usb-wifi-ac1300.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 34,
                column: "ImagenUrl",
                value: "/img/productos/34-tp-link-archer-t2u-nano-adaptador-usb-wifi-ac600.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImagenUrl",
                value: "/img/productos/35-tp-link-archer-tx20u-plus-adaptador-usb-wifi-6-ax1.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImagenUrl",
                value: "/img/productos/36-asus-usb-ac53-nano-adaptador-usb-wifi-ac1200.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImagenUrl",
                value: "/img/productos/37-tp-link-archer-ax55-router-wifi-6-ax3000.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 38,
                column: "ImagenUrl",
                value: "/img/productos/38-tp-link-deco-x20-sistema-mesh-wifi-6-pack-de-2.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 39,
                column: "ImagenUrl",
                value: "/img/productos/39-tp-link-tl-sg108-switch-gigabit-8-puertos.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 40,
                column: "ImagenUrl",
                value: "/img/productos/40-tp-link-re505x-extensor-wifi-6-ax1500.webp");

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Samsung Portable SSD T7 Shield 1TB", 169000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Samsung Portable SSD T7 Shield 1TB", 169000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Logitech G PRO Teclado mecánico gaming TKL", 200000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "ASUS ROG Strix GeForce RTX 4070 Ti SUPER OC 16GB", 1386000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Raspberry Pi 5 (8 GB)", 123000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "ASUS ROG Strix GeForce RTX 4070 Ti SUPER OC 16GB", 1386000m });

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Total",
                value: 1062000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 9,
                column: "Total",
                value: 1291000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 11,
                column: "Total",
                value: 724000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 13,
                column: "Total",
                value: 1534000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 14,
                column: "Total",
                value: 123000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 15,
                column: "Total",
                value: 2618000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagenUrl",
                value: "https://productimages.microcenter.com/0697599_888966.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagenUrl",
                value: "https://productimages.microcenter.com/0696088_871210.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagenUrl",
                value: "https://productimages.microcenter.com/0702642_940809.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagenUrl",
                value: "https://www.apple.com/v/macbook-air/z/images/meta/macbook_air_mx__ez5y0k5yy7au_og.png?202608191258");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagenUrl",
                value: "https://www.apple.com/v/mac-mini/ab/images/meta/mac-mini__dvce2jrm11w2_og.jpg?202609211742");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagenUrl",
                value: "https://www.bhphotovideo.com/images/fb/dell_u2725qe_ultrasharp_27_4k_uhd_1889853.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagenUrl",
                value: "https://productimages.microcenter.com/0704710_961219.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Marca", "Nombre", "Precio" },
                values: new object[] { "Procesador: Intel Core Ultra 9 275HX\nGráficos: NVIDIA GeForce RTX 5080 16GB\nRefrigeración: sistema de triple ventilador\nPantallas: soporta hasta cinco monitores 4K\nFormato: mini PC gaming compacto\nArquitectura: Intel Core Ultra Serie 2", "Mini PC gaming de alto rendimiento con Intel Core Ultra 9 275HX y GeForce RTX 5080 de 16GB en un chasis compacto con triple ventilador. Admite hasta cinco pantallas 4K.", "https://productimages.microcenter.com/0697039_883835.jpg", "ASUS", "ASUS ROG NUC (2025) con RTX 5080", 3850000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagenUrl",
                value: "https://store.storeimages.cdn-apple.com/1/as-images.apple.com/is/iphone-16-finish-select-202409-6-1inch_GEO_US?wid=1200&hei=630&fmt=jpeg&qlt=95&.v=1724710494447");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImagenUrl",
                value: "https://i02.appmifile.com/293_operatorx_operatorx_opx/27/09/2024/d714b3e4e95bd2d4817f5d3458b92c45.png");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImagenUrl",
                value: "https://images.samsung.com/is/image/samsung/assets/us/tablets/galaxy-tab-s10-fe/04082025/Tab-S10FE_HD01_KV_PC-V3.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImagenUrl",
                value: "https://www.apple.com/v/ipad-air/ah/images/meta/ipad-air_overview__bc2fd15uec0y_og.png?202609021131");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 17,
                column: "ImagenUrl",
                value: "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/mice/mx-master-3s/2025-update/mx-master-3s-bluetooth-edition-top-view-black-new-1.png");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 18,
                column: "ImagenUrl",
                value: "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/keyboards/mx-keys-s/migration-assets-for-delorean-2025/gallery/mx-keys-s-top-view-pale-gray-us.png");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 19,
                column: "ImagenUrl",
                value: "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/mice/mx-anywhere-3s/product-gallery/rose/mx-anywhere-3s-mouse-top-view-rose.png");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 20,
                column: "ImagenUrl",
                value: "https://resource.logitech.com/w_544,h_466,ar_7:6,c_pad,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/webcams/c920s/gallery/c920s-gallery-1.png");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 21,
                column: "ImagenUrl",
                value: "https://resource.logitech.com/w_692,c_lpad,ar_4:3,q_auto,f_auto,dpr_1.0/d_transparent.gif/content/dam/logitech/en/products/webcams/brio/gallery/brio-gallery-1.png?v=1");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 22,
                column: "ImagenUrl",
                value: "https://www.sandisk.com/content/dam/store/en-us/assets/products/memory-cards/extreme-uhs-i-microsd/featured/extreme-uhs-i-microsd-adapter.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 23,
                column: "ImagenUrl",
                value: "https://cdn.shopify.com/s/files/1/0493/9834/9974/files/5_a7a61bbb-8693-46f2-8d44-de814496fc62.png?v=1764230004");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Nombre", "Precio" },
                values: new object[] { "Capacidad de 1 TB\nLectura secuencial de hasta 1.050 MB/s\nEscritura secuencial de hasta 1.000 MB/s\nInterfaz USB 3.2 Gen 2\nResistente a golpes con control térmico\nDiseño ultraportátil en color gris", "Unidad de estado sólido externa, compacta y resistente a golpes, con velocidades de hasta 1.050 MB/s. Ideal para creadores y gamers.", null, "Samsung Portable SSD T7 1TB", 423000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 25,
                column: "ImagenUrl",
                value: "https://gmedia.playstation.com/is/image/SIEPDC/ps5-product-thumbnail-01-en-14sep21?$facebook$");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 26,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 27,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 28,
                column: "ImagenUrl",
                value: "https://assets2.razerzone.com/images/pnx.assets/b993b88204244288b62d053375f5be2e/razer-deathadder-v3-og-image.webp");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Nombre", "Precio" },
                values: new object[] { "Formato compacto 60 %\nConexión inalámbrica Lightspeed\nSwitches mecánicos GX Optical\nRetroiluminación RGB LIGHTSYNC\nHot-swap de switches\nHasta 65 horas de autonomía", "Teclado mecánico inalámbrico compacto al 60 % pensado para jugadores profesionales. Conexión Lightspeed de baja latencia y switches ópticos intercambiables.", null, "Logitech G PRO X 60 Lightspeed", 308000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 31,
                column: "ImagenUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Caracteristicas", "Descripcion", "ImagenUrl", "Marca", "Nombre", "Precio" },
                values: new object[] { "12 GB de memoria GDDR6X\n7168 núcleos CUDA\nDLSS 3 con generación de fotogramas\nTrazado de rayos de 3.ª generación\nConsumo gráfico de 220 W\nSalidas HDMI 2.1 y DisplayPort 1.4a", "Tarjeta gráfica de arquitectura Ada Lovelace para juegos a 1440p con trazado de rayos y DLSS 3. Combina alto rendimiento con un consumo contenido.", null, "NVIDIA", "NVIDIA GeForce RTX 4070 SUPER Founders Edition", 924000m });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 33,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/Archer_T3U_Plus(EU)_1.0_01_large_1583734764040g.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 34,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/Archer_T2U_Nano_EU_3.0_01_large_1536928386841s.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 35,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/upload/image-line/Archer_TX20U_Plus_2000px_large_20230809095804r.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 36,
                column: "ImagenUrl",
                value: "https://www.asus.com/media/global/gallery/sGgaGR1xdtbb5X06_setting_xxx_0_90_end_2000.png");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 37,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/upload/image-line/Overview_Archer_AX55_01_large_20210816012555f.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 38,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/Deco-X20-01_large_20220126092247t.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 39,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/upload/image-line/TL-SG108_UN_5.0_01_large_20231024111419x.jpg");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 40,
                column: "ImagenUrl",
                value: "https://static.tp-link.com/01_large_1620806188591r.jpg");

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Samsung Portable SSD T7 1TB", 423000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Samsung Portable SSD T7 1TB", 423000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "Logitech G PRO X 60 Lightspeed", 308000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "NVIDIA GeForce RTX 4070 SUPER Founders Edition", 924000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "ASUS ROG NUC (2025) con RTX 5080", 3850000m });

            migrationBuilder.UpdateData(
                table: "VentaItems",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "NombreProducto", "PrecioUnitario" },
                values: new object[] { "NVIDIA GeForce RTX 4070 SUPER Founders Edition", 924000m });

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Total",
                value: 1316000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 9,
                column: "Total",
                value: 1545000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 11,
                column: "Total",
                value: 832000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 13,
                column: "Total",
                value: 1072000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 14,
                column: "Total",
                value: 3850000m);

            migrationBuilder.UpdateData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 15,
                column: "Total",
                value: 2156000m);
        }
    }
}
