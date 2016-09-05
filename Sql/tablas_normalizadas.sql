/*
SQLyog Ultimate v9.02 
MySQL - 5.5.0-m2-community : Database - ncsoftwa_re
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`ncsoftwa_re` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `ncsoftwa_re`;

/*Table structure for table `alicuotasiva` */

DROP TABLE IF EXISTS `alicuotasiva`;

CREATE TABLE `alicuotasiva` (
  `IdAlicuotaALI` tinyint(2) NOT NULL,
  `PorcentajeALI` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`IdAlicuotaALI`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `alicuotasiva` */

LOCK TABLES `alicuotasiva` WRITE;

insert  into `alicuotasiva`(`IdAlicuotaALI`,`PorcentajeALI`) values (4,'10.50'),(5,'21.00');

UNLOCK TABLES;

/*Table structure for table `articulos` */

DROP TABLE IF EXISTS `articulos`;

CREATE TABLE `articulos` (
  `IdArticuloART` char(10) NOT NULL,
  `IdItemART` smallint(3) DEFAULT NULL,
  `IdGeneroART` char(3) DEFAULT NULL,
  `IdColorART` tinyint(3) DEFAULT NULL,
  `IdAliculotaIvaART` tinyint(2) DEFAULT NULL,
  `TalleART` char(2) DEFAULT NULL,
  `IdProveedorART` smallint(4) NOT NULL,
  `DescripcionART` tinytext NOT NULL,
  `DescripcionWebART` tinytext,
  `PrecioCostoART` decimal(19,2) unsigned NOT NULL,
  `PrecioPublicoART` decimal(19,2) unsigned NOT NULL,
  `PrecioMayorART` decimal(19,2) unsigned NOT NULL,
  `FechaART` datetime DEFAULT NULL,
  `ImagenART` varchar(20) DEFAULT NULL,
  `ImagenBackART` varchar(20) DEFAULT NULL,
  `ImagenColorART` varchar(20) DEFAULT NULL,
  `ActivoWebART` tinyint(1) DEFAULT '0',
  `NuevoART` varchar(1) DEFAULT '1',
  PRIMARY KEY (`IdArticuloART`),
  KEY `FK_Color` (`IdColorART`),
  KEY `FK_Proveedor` (`IdProveedorART`),
  KEY `FK_Item` (`IdItemART`),
  KEY `FK_articulos_generos` (`IdGeneroART`),
  KEY `FK_articulos_alicuotasiva` (`IdAliculotaIvaART`),
  CONSTRAINT `FK_articulos` FOREIGN KEY (`IdGeneroART`) REFERENCES `generos` (`IdGeneroGEN`) ON DELETE CASCADE,
  CONSTRAINT `FK_articulosItems_articulos` FOREIGN KEY (`IdItemART`) REFERENCES `articulositems` (`IdItemITE`) ON DELETE CASCADE,
  CONSTRAINT `FK_articulos_colores` FOREIGN KEY (`IdColorART`) REFERENCES `colores` (`IdColorCOL`) ON DELETE CASCADE,
  CONSTRAINT `FK_articulos_proveedores` FOREIGN KEY (`IdProveedorART`) REFERENCES `proveedores` (`IdProveedorPRO`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `articulos` */

LOCK TABLES `articulos` WRITE;

UNLOCK TABLES;

/*Table structure for table `articulositems` */

DROP TABLE IF EXISTS `articulositems`;

CREATE TABLE `articulositems` (
  `IdItemITE` smallint(3) NOT NULL,
  `DescripcionITE` varchar(50) DEFAULT NULL,
  `DescripcionWebITE` varchar(50) DEFAULT NULL,
  `ActivoWebITE` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdItemITE`),
  KEY `IdItemITE` (`IdItemITE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `articulositems` */

LOCK TABLES `articulositems` WRITE;

insert  into `articulositems`(`IdItemITE`,`DescripcionITE`,`DescripcionWebITE`,`ActivoWebITE`) values (3,'BABUCHA','BABUCHAS',0);

UNLOCK TABLES;

/*Table structure for table `borrarmantenimiento` */

DROP TABLE IF EXISTS `borrarmantenimiento`;

CREATE TABLE `borrarmantenimiento` (
  `Descripcion` varchar(13) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `borrarmantenimiento` */

LOCK TABLES `borrarmantenimiento` WRITE;

UNLOCK TABLES;

/*Table structure for table `clientes` */

DROP TABLE IF EXISTS `clientes`;

CREATE TABLE `clientes` (
  `IdClienteCLI` int(11) NOT NULL,
  `NombreCLI` varchar(50) DEFAULT NULL,
  `ApellidoCLI` varchar(50) DEFAULT NULL,
  `RazonSocialCLI` varchar(50) DEFAULT NULL,
  `CUIT` varchar(50) DEFAULT NULL,
  `CondicionIvaCLI` varchar(50) DEFAULT NULL,
  `DireccionCLI` varchar(50) DEFAULT NULL,
  `LocalidadCLI` varchar(50) DEFAULT NULL,
  `ProvinciaCLI` varchar(50) DEFAULT NULL,
  `TransporteCLI` varchar(50) DEFAULT NULL,
  `ContactoCLI` varchar(50) DEFAULT NULL,
  `TelefonoCLI` varchar(50) DEFAULT NULL,
  `MovilCLI` varchar(50) DEFAULT NULL,
  `CorreoCLI` varchar(60) DEFAULT NULL,
  `FechaNacCLI` datetime DEFAULT NULL,
  `RecibeNewsCLI` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`IdClienteCLI`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `clientes` */

LOCK TABLES `clientes` WRITE;

insert  into `clientes`(`IdClienteCLI`,`NombreCLI`,`ApellidoCLI`,`RazonSocialCLI`,`CUIT`,`CondicionIvaCLI`,`DireccionCLI`,`LocalidadCLI`,`ProvinciaCLI`,`TransporteCLI`,`ContactoCLI`,`TelefonoCLI`,`MovilCLI`,`CorreoCLI`,`FechaNacCLI`,`RecibeNewsCLI`) values (1,'PUBLICO','','PUBLICO','',NULL,'','','','','','','',NULL,NULL,1);

UNLOCK TABLES;

/*Table structure for table `colores` */

DROP TABLE IF EXISTS `colores`;

CREATE TABLE `colores` (
  `IdColorCOL` tinyint(3) NOT NULL,
  `DescripcionCOL` varchar(50) DEFAULT NULL,
  `HexCOL` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`IdColorCOL`),
  KEY `IdColorCOL` (`IdColorCOL`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `colores` */

LOCK TABLES `colores` WRITE;

insert  into `colores`(`IdColorCOL`,`DescripcionCOL`,`HexCOL`) values (0,'NINGUNO',NULL),(1,'AERO','');

UNLOCK TABLES;

/*Table structure for table `comppesos` */

DROP TABLE IF EXISTS `comppesos`;

CREATE TABLE `comppesos` (
  `FechaMSTK` datetime DEFAULT NULL,
  `DestinoMSTK` int(11) DEFAULT NULL,
  `CompensaMSTK` tinyint(1) DEFAULT NULL,
  `PrecioCostoART` decimal(19,2) unsigned NOT NULL,
  `PrecioPublicoART` decimal(19,2) unsigned NOT NULL,
  `CantidadMSTKD` int(11) DEFAULT NULL,
  `SubtotalCosto` decimal(29,2) DEFAULT NULL,
  `SubtotalPublico` decimal(29,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `comppesos` */

LOCK TABLES `comppesos` WRITE;

UNLOCK TABLES;

/*Table structure for table `condicioniva` */

DROP TABLE IF EXISTS `condicioniva`;

CREATE TABLE `condicioniva` (
  `IdCondicionIvaCIVA` tinyint(2) NOT NULL,
  `DescripcionCIVA` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdCondicionIvaCIVA`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `condicioniva` */

LOCK TABLES `condicioniva` WRITE;

insert  into `condicioniva`(`IdCondicionIvaCIVA`,`DescripcionCIVA`) values (1,'RESPONSABLE INSCRIPTO'),(2,'RESPONSABLE MONOTRIBUTO'),(3,'CONSUMIDOR FINAL'),(4,'EXENTO'),(5,'NO RESPONSABLE');

UNLOCK TABLES;

/*Table structure for table `contador` */

DROP TABLE IF EXISTS `contador`;

CREATE TABLE `contador` (
  `ip` varchar(20) NOT NULL,
  `fecha` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `contador` */

LOCK TABLES `contador` WRITE;

UNLOCK TABLES;

/*Table structure for table `contador_historico` */

DROP TABLE IF EXISTS `contador_historico`;

CREATE TABLE `contador_historico` (
  `cantidad` bigint(20) NOT NULL DEFAULT '0',
  PRIMARY KEY (`cantidad`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `contador_historico` */

LOCK TABLES `contador_historico` WRITE;

UNLOCK TABLES;

/*Table structure for table `cupones` */

DROP TABLE IF EXISTS `cupones`;

CREATE TABLE `cupones` (
  `Nro_cupon` varchar(12) NOT NULL DEFAULT '0',
  `Mail` varchar(50) DEFAULT '',
  `Porcentaje` tinyint(2) DEFAULT '0',
  `FechaVencimiento` date DEFAULT '0000-00-00',
  `Utilizado` tinyint(1) DEFAULT '0',
  `Importe` double DEFAULT NULL,
  PRIMARY KEY (`Nro_cupon`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cupones` */

LOCK TABLES `cupones` WRITE;

UNLOCK TABLES;

/*Table structure for table `cupones_config` */

DROP TABLE IF EXISTS `cupones_config`;

CREATE TABLE `cupones_config` (
  `Porcentaje` tinyint(2) DEFAULT '0',
  `FechaVencimiento` date DEFAULT '0000-00-00',
  `Color` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cupones_config` */

LOCK TABLES `cupones_config` WRITE;

UNLOCK TABLES;

/*Table structure for table `empleados` */

DROP TABLE IF EXISTS `empleados`;

CREATE TABLE `empleados` (
  `IdEmpleadoEMP` int(11) NOT NULL,
  `DniEMP` varchar(10) DEFAULT NULL,
  `NombreEMP` varchar(50) DEFAULT NULL,
  `ApellidoEMP` varchar(25) DEFAULT NULL,
  `DireccionEMP` varchar(50) DEFAULT NULL,
  `TelefonoEMP` varchar(15) DEFAULT NULL,
  `FechaNacEMP` date DEFAULT NULL,
  `FechaIngresoEMP` date DEFAULT NULL,
  `IdLocalEMP` int(3) DEFAULT NULL,
  `SalarioEMP` double DEFAULT NULL,
  `CargasSocialesEMP` double DEFAULT NULL,
  `Activa` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdEmpleadoEMP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `empleados` */

LOCK TABLES `empleados` WRITE;

UNLOCK TABLES;

/*Table structure for table `empleadosmovimientos` */

DROP TABLE IF EXISTS `empleadosmovimientos`;

CREATE TABLE `empleadosmovimientos` (
  `IdMovEMOV` int(11) NOT NULL,
  `FechaEMOV` date DEFAULT NULL,
  `IdEmpleadoEMOV` int(11) DEFAULT NULL,
  `IdMovTipoEMOV` int(11) DEFAULT NULL,
  `CantidadEMOV` tinyint(3) DEFAULT NULL,
  `DetalleEMOV` varchar(50) DEFAULT NULL,
  `ImporteEMOV` double DEFAULT NULL,
  `LiquidadoEMOV` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdMovEMOV`),
  KEY `FK_empleadosmovimientos_movtipo` (`IdMovTipoEMOV`),
  KEY `FK_empleadosmovimientos_empleados` (`IdEmpleadoEMOV`),
  CONSTRAINT `FK_empleadosmovimientos_empleados` FOREIGN KEY (`IdEmpleadoEMOV`) REFERENCES `empleados` (`IdEmpleadoEMP`) ON DELETE CASCADE,
  CONSTRAINT `FK_empleadosmovimientos_movtipo` FOREIGN KEY (`IdMovTipoEMOV`) REFERENCES `empleadosmovtipos` (`IdMovETIP`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `empleadosmovimientos` */

LOCK TABLES `empleadosmovimientos` WRITE;

UNLOCK TABLES;

/*Table structure for table `empleadosmovtipos` */

DROP TABLE IF EXISTS `empleadosmovtipos`;

CREATE TABLE `empleadosmovtipos` (
  `IdMovETIP` int(11) NOT NULL,
  `DescripcionETIP` varchar(25) DEFAULT NULL,
  `RemuneracionETIP` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdMovETIP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `empleadosmovtipos` */

LOCK TABLES `empleadosmovtipos` WRITE;

insert  into `empleadosmovtipos`(`IdMovETIP`,`DescripcionETIP`,`RemuneracionETIP`) values (1,'VALE',NULL),(2,'HORAS EXTRAS',NULL),(3,'AGUINALDO',NULL),(4,'VACACIONES',NULL);

UNLOCK TABLES;

/*Table structure for table `exportar_fondo_caja` */

DROP TABLE IF EXISTS `exportar_fondo_caja`;

CREATE TABLE `exportar_fondo_caja` (
  `IdFondoFONP` int(11) DEFAULT NULL,
  `FechaFONP` date NOT NULL,
  `IdPcFONP` int(11) NOT NULL,
  `ImporteFONP` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `exportar_fondo_caja` */

LOCK TABLES `exportar_fondo_caja` WRITE;

UNLOCK TABLES;

/*Table structure for table `exportar_tesoreria_movimientos` */

DROP TABLE IF EXISTS `exportar_tesoreria_movimientos`;

CREATE TABLE `exportar_tesoreria_movimientos` (
  `IdMovTESM` int(11) NOT NULL,
  `FechaTESM` datetime DEFAULT NULL,
  `IdPcTESM` int(11) DEFAULT NULL,
  `DetalleTESM` varchar(200) DEFAULT NULL,
  `ImporteTESM` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `exportar_tesoreria_movimientos` */

LOCK TABLES `exportar_tesoreria_movimientos` WRITE;

UNLOCK TABLES;

/*Table structure for table `exportar_ventas` */

DROP TABLE IF EXISTS `exportar_ventas`;

CREATE TABLE `exportar_ventas` (
  `IdVentaVEN` int(11) NOT NULL,
  `IdPCVEN` int(11) DEFAULT NULL,
  `FechaVEN` datetime DEFAULT NULL,
  `IdClienteVEN` int(11) DEFAULT NULL,
  `NroCuponVEN` varchar(12) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `exportar_ventas` */

LOCK TABLES `exportar_ventas` WRITE;

UNLOCK TABLES;

/*Table structure for table `exportar_ventas_detalle` */

DROP TABLE IF EXISTS `exportar_ventas_detalle`;

CREATE TABLE `exportar_ventas_detalle` (
  `IdDVEN` int(11) NOT NULL,
  `IdVentaDVEN` int(11) DEFAULT NULL,
  `IdLocalDVEN` int(3) DEFAULT NULL,
  `IdArticuloDVEN` varchar(50) DEFAULT NULL,
  `DescripcionDVEN` varchar(50) DEFAULT NULL,
  `CantidadDVEN` int(11) DEFAULT NULL,
  `PrecioPublicoDVEN` double DEFAULT NULL,
  `PrecioCostoDVEN` double DEFAULT NULL,
  `PrecioMayorDVEN` double DEFAULT NULL,
  `IdFormaPagoDVEN` int(11) DEFAULT NULL,
  `NroCuponDVEN` int(11) DEFAULT NULL,
  `NroFacturaDVEN` int(11) DEFAULT NULL,
  `IdEmpleadoDVEN` int(11) DEFAULT NULL,
  `LiquidadoDVEN` bit(1) DEFAULT NULL,
  `EsperaDVEN` bit(1) DEFAULT NULL,
  `DevolucionDVEN` smallint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `exportar_ventas_detalle` */

LOCK TABLES `exportar_ventas_detalle` WRITE;

UNLOCK TABLES;

/*Table structure for table `fondocaja` */

DROP TABLE IF EXISTS `fondocaja`;

CREATE TABLE `fondocaja` (
  `IdFondoFONP` int(11) DEFAULT NULL,
  `FechaFONP` date NOT NULL,
  `IdPcFONP` int(11) NOT NULL,
  `ImporteFONP` double DEFAULT NULL,
  PRIMARY KEY (`FechaFONP`,`IdPcFONP`),
  KEY `FK_FondoCaja` (`IdPcFONP`),
  CONSTRAINT `FK_FondoCaja_Pc` FOREIGN KEY (`IdPcFONP`) REFERENCES `pc` (`IdPC`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `fondocaja` */

LOCK TABLES `fondocaja` WRITE;

UNLOCK TABLES;

/*Table structure for table `formaspago` */

DROP TABLE IF EXISTS `formaspago`;

CREATE TABLE `formaspago` (
  `IdFormaPagoFOR` int(11) NOT NULL,
  `DescripcionFOR` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdFormaPagoFOR`),
  KEY `IdFormaPagoFOR` (`IdFormaPagoFOR`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `formaspago` */

LOCK TABLES `formaspago` WRITE;

insert  into `formaspago`(`IdFormaPagoFOR`,`DescripcionFOR`) values (1,'EFECTIVO'),(2,'NARANJA'),(3,'PROVEN'),(4,'CORDOBESA'),(5,'KADICARD'),(6,'VISA'),(7,'VISA ELECTRON'),(8,'MASTERCARD'),(9,'MAESTRO'),(15,'AMERICAN EXPRESS'),(16,'NATIVA'),(99,'TODAS');

UNLOCK TABLES;

/*Table structure for table `generos` */

DROP TABLE IF EXISTS `generos`;

CREATE TABLE `generos` (
  `IdGeneroGEN` char(3) NOT NULL,
  `DescripcionGEN` varchar(50) DEFAULT NULL,
  `ActivoWebGEN` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdGeneroGEN`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `generos` */

LOCK TABLES `generos` WRITE;

insert  into `generos`(`IdGeneroGEN`,`DescripcionGEN`,`ActivoWebGEN`) values ('1','MUJER',1),('2','NIÑOS',1),('3','NIÑAS',1);

UNLOCK TABLES;

/*Table structure for table `locales` */

DROP TABLE IF EXISTS `locales`;

CREATE TABLE `locales` (
  `IdLocalLOC` int(11) NOT NULL AUTO_INCREMENT,
  `NombreLOC` varchar(50) DEFAULT NULL,
  `DireccionLOC` varchar(50) DEFAULT NULL,
  `TelefonoLOC` varchar(50) DEFAULT NULL,
  `ActivoWebLOC` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdLocalLOC`),
  UNIQUE KEY `NombreLOC` (`NombreLOC`),
  KEY `IdLocalLOC` (`IdLocalLOC`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

/*Data for the table `locales` */

LOCK TABLES `locales` WRITE;

insert  into `locales`(`IdLocalLOC`,`NombreLOC`,`DireccionLOC`,`TelefonoLOC`,`ActivoWebLOC`) values (1,'ENTRADAS',NULL,NULL,0),(2,'SALIDAS',NULL,NULL,0),(3,'LOCAL 1',NULL,NULL,0);

UNLOCK TABLES;

/*Table structure for table `pc` */

DROP TABLE IF EXISTS `pc`;

CREATE TABLE `pc` (
  `IdPC` int(11) NOT NULL,
  `IdLocalPC` int(11) DEFAULT NULL,
  `Detalle` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdPC`),
  KEY `IdLocalPC` (`IdLocalPC`),
  KEY `IdPC` (`IdPC`),
  CONSTRAINT `FK_Pc_Locales` FOREIGN KEY (`IdLocalPC`) REFERENCES `locales` (`IdLocalLOC`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `pc` */

LOCK TABLES `pc` WRITE;

insert  into `pc`(`IdPC`,`IdLocalPC`,`Detalle`) values (1,3,'Caja1'),(2,3,'Administracion');

UNLOCK TABLES;

/*Table structure for table `proveedores` */

DROP TABLE IF EXISTS `proveedores`;

CREATE TABLE `proveedores` (
  `IdProveedorPRO` smallint(4) NOT NULL,
  `RazonSocialPRO` varchar(50) DEFAULT NULL,
  `DireccionPRO` varchar(50) DEFAULT NULL,
  `CodigoPostalPRO` varchar(50) DEFAULT NULL,
  `TelefonoPRO` varchar(50) DEFAULT NULL,
  `ContactoPRO` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdProveedorPRO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `proveedores` */

LOCK TABLES `proveedores` WRITE;

insert  into `proveedores`(`IdProveedorPRO`,`RazonSocialPRO`,`DireccionPRO`,`CodigoPostalPRO`,`TelefonoPRO`,`ContactoPRO`) values (75,'OPERA ROCK',NULL,NULL,NULL,NULL);

UNLOCK TABLES;

/*Table structure for table `razonsocial` */

DROP TABLE IF EXISTS `razonsocial`;

CREATE TABLE `razonsocial` (
  `IdRazonSocialRAZ` int(11) NOT NULL,
  `RazonSocialRAZ` varchar(50) DEFAULT NULL,
  `NombreFantasiaRAZ` varchar(50) DEFAULT NULL,
  `DomicilioRAZ` varchar(50) DEFAULT NULL,
  `LocalidadRAZ` varchar(50) DEFAULT NULL,
  `ProvinciaRAZ` varchar(50) DEFAULT NULL,
  `IdCondicionIvaRAZ` tinyint(2) DEFAULT NULL,
  `CuitRAZ` varchar(15) DEFAULT NULL,
  `IngresosBrutosRAZ` varchar(15) DEFAULT NULL,
  `InicioActividadRAZ` datetime DEFAULT NULL,
  `ActualizarDatosRAZ` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`IdRazonSocialRAZ`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `razonsocial` */

LOCK TABLES `razonsocial` WRITE;

UNLOCK TABLES;

/*Table structure for table `stock` */

DROP TABLE IF EXISTS `stock`;

CREATE TABLE `stock` (
  `IdArticuloSTK` varchar(50) NOT NULL,
  `IdLocalSTK` int(11) NOT NULL,
  `CantidadSTK` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdArticuloSTK`,`IdLocalSTK`),
  KEY `FK_Stock_Local` (`IdLocalSTK`),
  CONSTRAINT `FK_Stock_Articulos` FOREIGN KEY (`IdArticuloSTK`) REFERENCES `articulos` (`IdArticuloART`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Stock_Locales` FOREIGN KEY (`IdLocalSTK`) REFERENCES `locales` (`IdLocalLOC`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `stock` */

LOCK TABLES `stock` WRITE;

UNLOCK TABLES;

/*Table structure for table `stockcomppesos` */

DROP TABLE IF EXISTS `stockcomppesos`;

CREATE TABLE `stockcomppesos` (
  `Prendas` decimal(32,0) DEFAULT NULL,
  `Costo` decimal(51,2) DEFAULT NULL,
  `Publico` decimal(51,2) DEFAULT NULL,
  `DestinoMSTK` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `stockcomppesos` */

LOCK TABLES `stockcomppesos` WRITE;

UNLOCK TABLES;

/*Table structure for table `stockmov` */

DROP TABLE IF EXISTS `stockmov`;

CREATE TABLE `stockmov` (
  `IdMovMSTK` int(11) NOT NULL,
  `FechaMSTK` datetime DEFAULT NULL,
  `OrigenMSTK` int(11) DEFAULT NULL,
  `DestinoMSTK` int(11) DEFAULT NULL,
  `CompensaMSTK` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdMovMSTK`),
  KEY `FK_StockMov_Origen` (`OrigenMSTK`),
  KEY `FK_StockMov_Destino` (`DestinoMSTK`),
  CONSTRAINT `FK_StockMov` FOREIGN KEY (`DestinoMSTK`) REFERENCES `locales` (`IdLocalLOC`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_StockMov_Origen` FOREIGN KEY (`OrigenMSTK`) REFERENCES `locales` (`IdLocalLOC`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `stockmov` */

LOCK TABLES `stockmov` WRITE;

UNLOCK TABLES;

/*Table structure for table `stockmovdetalle` */

DROP TABLE IF EXISTS `stockmovdetalle`;

CREATE TABLE `stockmovdetalle` (
  `ordenar` int(11) NOT NULL AUTO_INCREMENT,
  `IdMSTKD` int(11) NOT NULL,
  `IdMovMSTKD` int(11) DEFAULT NULL,
  `IdArticuloMSTKD` varchar(50) DEFAULT NULL,
  `CantidadMSTKD` int(11) DEFAULT NULL,
  `CompensaMSTKD` tinyint(1) DEFAULT '0',
  `OrigenMSTKD` tinyint(2) DEFAULT NULL,
  `DestinoMSTKD` tinyint(2) DEFAULT NULL,
  PRIMARY KEY (`IdMSTKD`),
  UNIQUE KEY `ordenar` (`ordenar`),
  KEY `FK_StockMovDetalle` (`IdMovMSTKD`),
  KEY `FK_StockMovDetalle_Articulos` (`IdArticuloMSTKD`),
  CONSTRAINT `FK_StockMovDetalle_Articulos` FOREIGN KEY (`IdArticuloMSTKD`) REFERENCES `articulos` (`IdArticuloART`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_StockMovDetalle_StockMov` FOREIGN KEY (`IdMovMSTKD`) REFERENCES `stockmov` (`IdMovMSTK`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16385 DEFAULT CHARSET=utf8;

/*Data for the table `stockmovdetalle` */

LOCK TABLES `stockmovdetalle` WRITE;

UNLOCK TABLES;

/*Table structure for table `stockprecioscons` */

DROP TABLE IF EXISTS `stockprecioscons`;

CREATE TABLE `stockprecioscons` (
  `NombreLOC` varchar(50) DEFAULT NULL,
  `IdArticuloSTK` varchar(50) DEFAULT NULL,
  `DescripcionART` varchar(55) DEFAULT NULL,
  `CantidadSTK` int(11) DEFAULT NULL,
  `PrecioCostoART` decimal(19,0) DEFAULT NULL,
  `PrecioPublicoART` decimal(19,0) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `stockprecioscons` */

LOCK TABLES `stockprecioscons` WRITE;

UNLOCK TABLES;

/*Table structure for table `tesoreria_mov_items` */

DROP TABLE IF EXISTS `tesoreria_mov_items`;

CREATE TABLE `tesoreria_mov_items` (
  `IdTESMI` int(11) NOT NULL,
  `DescripcionTESMI` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`IdTESMI`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

/*Data for the table `tesoreria_mov_items` */

LOCK TABLES `tesoreria_mov_items` WRITE;

UNLOCK TABLES;

/*Table structure for table `tesoreriamovimientos` */

DROP TABLE IF EXISTS `tesoreriamovimientos`;

CREATE TABLE `tesoreriamovimientos` (
  `IdMovTESM` int(11) NOT NULL,
  `FechaTESM` datetime DEFAULT NULL,
  `IdPcTESM` int(11) DEFAULT NULL,
  `DetalleTESM` varchar(200) DEFAULT NULL,
  `ImporteTESM` double DEFAULT NULL,
  PRIMARY KEY (`IdMovTESM`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tesoreriamovimientos` */

LOCK TABLES `tesoreriamovimientos` WRITE;

UNLOCK TABLES;

/*Table structure for table `usuarios` */

DROP TABLE IF EXISTS `usuarios`;

CREATE TABLE `usuarios` (
  `id_usuario` bigint(10) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `apellido` varchar(50) DEFAULT NULL,
  `correo` varchar(50) DEFAULT NULL,
  `clave` varchar(50) DEFAULT NULL,
  `nivel_seguridad` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `usuarios` */

LOCK TABLES `usuarios` WRITE;

UNLOCK TABLES;

/*Table structure for table `ventas` */

DROP TABLE IF EXISTS `ventas`;

CREATE TABLE `ventas` (
  `IdVentaVEN` int(11) NOT NULL,
  `IdPCVEN` int(11) DEFAULT NULL,
  `FechaVEN` datetime DEFAULT NULL,
  `IdClienteVEN` int(11) DEFAULT NULL,
  `NroCuponVEN` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`IdVentaVEN`),
  KEY `FK_Ventas` (`IdClienteVEN`),
  KEY `FK_Ventas_Pc` (`IdPCVEN`),
  CONSTRAINT `FK_Ventas` FOREIGN KEY (`IdPCVEN`) REFERENCES `pc` (`IdPC`),
  CONSTRAINT `FK_Ventas_Clientes` FOREIGN KEY (`IdClienteVEN`) REFERENCES `clientes` (`IdClienteCLI`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ventas` */

LOCK TABLES `ventas` WRITE;

UNLOCK TABLES;

/*Table structure for table `ventasdetalle` */

DROP TABLE IF EXISTS `ventasdetalle`;

CREATE TABLE `ventasdetalle` (
  `OrdenarDVEN` int(11) NOT NULL AUTO_INCREMENT,
  `IdDVEN` int(11) NOT NULL,
  `IdVentaDVEN` int(11) DEFAULT NULL,
  `IdLocalDVEN` int(3) DEFAULT NULL,
  `IdArticuloDVEN` varchar(50) DEFAULT NULL,
  `DescripcionDVEN` varchar(50) DEFAULT NULL,
  `CantidadDVEN` int(11) DEFAULT NULL,
  `PrecioPublicoDVEN` decimal(19,2) DEFAULT NULL,
  `PrecioCostoDVEN` decimal(19,2) DEFAULT NULL,
  `PrecioMayorDVEN` decimal(19,2) DEFAULT NULL,
  `IdFormaPagoDVEN` int(11) DEFAULT NULL,
  `NroCuponDVEN` int(11) DEFAULT NULL,
  `NroFacturaDVEN` int(11) DEFAULT NULL,
  `IdEmpleadoDVEN` int(11) DEFAULT NULL,
  `LiquidadoDVEN` bit(1) DEFAULT NULL,
  `EsperaDVEN` bit(1) DEFAULT NULL,
  `DevolucionDVEN` smallint(1) DEFAULT NULL,
  PRIMARY KEY (`IdDVEN`),
  UNIQUE KEY `Ordenar` (`OrdenarDVEN`),
  KEY `FK_VentasDetalle_Forma` (`IdFormaPagoDVEN`),
  KEY `FK_VentasDetalle` (`IdVentaDVEN`),
  KEY `FK_VentasDetalle_Articulos` (`IdArticuloDVEN`),
  CONSTRAINT `FK_VentasDetalle_Articulos` FOREIGN KEY (`IdArticuloDVEN`) REFERENCES `articulos` (`IdArticuloART`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_VentasDetalle_Ventas` FOREIGN KEY (`IdVentaDVEN`) REFERENCES `ventas` (`IdVentaVEN`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8774 DEFAULT CHARSET=utf8;

/*Data for the table `ventasdetalle` */

LOCK TABLES `ventasdetalle` WRITE;

UNLOCK TABLES;

/*Table structure for table `ventash` */

DROP TABLE IF EXISTS `ventash`;

CREATE TABLE `ventash` (
  `Fecha` date NOT NULL,
  `NombreLocal` varchar(50) NOT NULL,
  `Genero` varchar(50) DEFAULT NULL,
  `FormaPago` varchar(50) NOT NULL,
  `TotalPublico` decimal(10,0) DEFAULT NULL,
  `TotalCosto` decimal(10,0) DEFAULT NULL,
  `Prendas` int(11) DEFAULT NULL,
  PRIMARY KEY (`Fecha`,`NombreLocal`,`FormaPago`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `ventash` */

LOCK TABLES `ventash` WRITE;

UNLOCK TABLES;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
