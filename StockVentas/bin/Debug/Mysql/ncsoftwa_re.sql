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

insert  into `proveedores`(`IdProveedorPRO`,`RazonSocialPRO`,`DireccionPRO`,`CodigoPostalPRO`,`TelefonoPRO`,`ContactoPRO`) values (1,'OPERA ROCK',NULL,NULL,NULL,NULL);

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

insert  into `razonsocial`(`IdRazonSocialRAZ`,`RazonSocialRAZ`,`NombreFantasiaRAZ`,`DomicilioRAZ`,`LocalidadRAZ`,`ProvinciaRAZ`,`IdCondicionIvaRAZ`,`CuitRAZ`,`IngresosBrutosRAZ`,`InicioActividadRAZ`,`ActualizarDatosRAZ`) values (1043057025,'','','','','',NULL,'','',NULL,0);

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

/* Procedure structure for procedure `AlicuotasIva_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `AlicuotasIva_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `AlicuotasIva_Actualizar`(
IN p_id TINYINT(3),
IN p_old_id TINYINT(3),
IN p_porcentaje VARCHAR(50))
BEGIN
UPDATE alicuotasiva SET IdAlicuotaALI = p_id, PorcentajeALI = REPLACE(p_porcentaje, ",",".")
WHERE IdAlicuotaALI = p_old_id 
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1;
END */$$
DELIMITER ;

/* Procedure structure for procedure `AlicuotasIva_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `AlicuotasIva_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `AlicuotasIva_Borrar`(
IN p_id tinyint(3))
BEGIN
DELETE
FROM alicuotasiva
WHERE IdAlicuotaALI= p_id
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `AlicuotasIva_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `AlicuotasIva_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `AlicuotasIva_Insertar`(
IN p_id TINYINT(3),
IN p_porcentaje VARCHAR(50)
)
BEGIN
INSERT INTO alicuotasiva(IdAlicuotaALI, PorcentajeALI)
VALUES(p_id, REPLACE(p_porcentaje, ",","."))
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1;
END */$$
DELIMITER ;

/* Procedure structure for procedure `ArticulosItems_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `ArticulosItems_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `ArticulosItems_Actualizar`(
in p_id int(11),
IN p_descripcion varchar(50),
IN p_descripcionWeb VARCHAR(50),
IN p_activoWeb INT(1)
)
BEGIN
update `articulositems` set DescripcionITE = p_descripcion, DescripcionWebITE = p_descripcionWeb,
ActivoWebITE = p_activoWeb
where IdItemITE = p_id 
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `ArticulosItems_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `ArticulosItems_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `ArticulosItems_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM `articulositems`
WHERE IdItemITE= p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `ArticulosItems_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `ArticulosItems_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `ArticulosItems_Insertar`(
in p_id int,
IN p_descripcion VARCHAR(50),
IN p_descripcionWeb VARCHAR(50),
IN p_activoWeb INT(1)
)
BEGIN
INSERT INTO `articulositems`(IdItemITE, DescripcionITE, DescripcionWebITE, ActivoWebITE)
VALUES(p_id, p_descripcion, p_descripcionWeb, p_activoWeb)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_Actualizar`(
IN p_id VARCHAR(50),
IN p_idItem INT(11),
IN p_idGenero INT(3),
IN p_idColor INT(11),
IN p_idAlicuota TINYINT(3),
IN p_talle VARCHAR(50),
IN p_idProveedor INT(11),
IN p_descripcion VARCHAR(100),
IN p_descripcionWeb VARCHAR(100),
IN p_precioCosto VARCHAR(19),
IN p_precioPublico VARCHAR(19),
IN p_precioMayor VARCHAR(19),
IN p_fecha DATETIME,
IN p_imagen VARCHAR(20),
IN p_imagenBack VARCHAR(20),
IN p_imagenColor VARCHAR(20),
IN p_activoWeb INT(1),
IN p_nuevo INT(1)
)
BEGIN
START TRANSACTION;
UPDATE articulos SET IdItemART = p_idItem, IdGeneroART = p_idGenero, IdColorART = p_idColor, IdAliculotaIvaART = p_idAlicuota, TalleART = p_talle ,IdProveedorART = p_idProveedor, 
DescripcionART = p_descripcion, DescripcionWebART = p_descripcionWeb, PrecioCostoART = REPLACE(p_precioCosto, ",","."), 
PrecioPublicoART = REPLACE(p_precioPublico, ",","."), PrecioMayorART = REPLACE(p_precioMayor, ",","."), FechaART = p_fecha, ImagenART = p_imagen, 
ImagenBackART = p_imagenBack, ImagenColorART = p_imagenColor, ActivoWebART = p_activoWeb, NuevoART = p_nuevo
WHERE IdArticuloART = p_id;
COMMIT;
IF p_imagen IS NOT NULL THEN
	UPDATE articulos SET ImagenART = p_imagen, ImagenColorART = p_imagenColor 
		WHERE IdArticuloART LIKE CONCAT(LEFT(p_id,8),'%');	
	UPDATE articulos SET ImagenBackART = p_imagenBack 
		WHERE IdArticuloART LIKE CONCAT(LEFT(p_id,6),'%');		
END IF;	
update articulos set DescripcionWebART = p_descripcionWeb where IdArticuloART like concat(LEFT(p_id,6),'%');
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_Actualizar_Precio` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_Actualizar_Precio` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_Actualizar_Precio`(
in p_id varchar(50),
IN p_precioPublico DECIMAL(19)
)
BEGIN
update articulos set PrecioPublicoART = p_precioPublico
where IdArticuloART like concat(p_id, '%')
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_Borrar`(IN p_id varchar(50))
BEGIN
DELETE
FROM articulos
WHERE IdArticuloART = p_id
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_BorrarCons` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_BorrarCons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_BorrarCons`()
BEGIN
SELECT
  `articulos`.`IdArticuloART`    AS `IdArticuloART`,
  `articulos`.`DescripcionART`   AS `DescripcionART`,
  `articulos`.`PrecioCostoART`   AS `PrecioCostoART`,
  `articulos`.`PrecioPublicoART` AS `PrecioPublicoART`,
  `articulos`.`PrecioMayorART`   AS `PrecioMayorART`,
  SUM(`stock`.`CantidadSTK`)     AS `Stock`
FROM (`articulos`
   LEFT JOIN `stock`
     ON ((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)))
GROUP BY `articulos`.`IdArticuloART`,`articulos`.`DescripcionART`,`articulos`.`PrecioCostoART`,`articulos`.`PrecioPublicoART`,`articulos`.`PrecioMayorART`
ORDER BY `articulos`.`DescripcionART`
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_Insertar`(
IN p_id VARCHAR(50),
IN p_idItem INT(11),
IN p_idGenero INT(3),
IN p_idColor INT(11),
IN p_idAlicuota TINYINT(3),
IN p_talle VARCHAR(50),
IN p_idProveedor INT(11),
IN p_descripcion VARCHAR(100),
IN p_descripcionWeb VARCHAR(100),
IN p_precioCosto VARCHAR(19),
IN p_precioPublico VARCHAR(19),
IN p_precioMayor VARCHAR(19),
IN p_fecha DATETIME,
IN p_imagen VARCHAR(20),
IN p_imagenBack VARCHAR(20),
IN p_imagenColor VARCHAR(20),
IN p_activoWeb INT(1),
IN p_nuevo INT(1)
)
BEGIN
INSERT INTO articulos(IdArticuloART, IdItemART, IdGeneroART, IdColorART, IdAliculotaIvaART, TalleART, IdProveedorART, DescripcionART,
DescripcionWebART, PrecioCostoART, PrecioPublicoART, PrecioMayorART, FechaART, 
ImagenART, ImagenBackART, ImagenColorART, ActivoWebART, NuevoART)
VALUES(p_id, p_idItem, p_idGenero, p_idColor, p_idAlicuota, p_talle, p_idProveedor, p_descripcion, p_descripcionWeb,
REPLACE(p_precioCosto, ",","."), REPLACE(p_precioPublico, ",","."), REPLACE(p_precioMayor, ",","."), p_fecha, p_imagen, p_imagenBack, p_imagenColor, p_activoWeb, p_nuevo)
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_Listar`()
BEGIN
SELECT *
FROM articulos
ORDER BY DescripcionART
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Articulos_Precios_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Articulos_Precios_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Articulos_Precios_Actualizar`()
BEGIN
	DECLARE done INT DEFAULT 0;
	DECLARE codigo_articulo VARCHAR(16);	  
	DECLARE precio_publico DECIMAL(19);
	DECLARE cur1 CURSOR FOR SELECT IdArticuloART, PrecioPublicoART FROM articulos;
	DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;
	OPEN cur1;
	  REPEAT
		FETCH cur1 INTO codigo_articulo, precio_publico;
		IF NOT done THEN
			UPDATE articulos SET PrecioMayorART = precio_publico
			WHERE IdArticuloART = codigo_articulo;
		END IF;
	  UNTIL done END REPEAT;
	  CLOSE cur1;
	  UPDATE `razonsocial` SET ActualizarDatosRAZ = 1;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Clientes_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Clientes_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Clientes_Actualizar`(
in p_id int(11),
IN p_razon varchar(50),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(50),
IN p_cuit VARCHAR(50),
IN p_condicion VARCHAR(50),
IN p_direccion VARCHAR(50),
IN p_localidad VARCHAR(50),
IN p_provincia VARCHAR(50),
IN p_transporte VARCHAR(50),
IN p_contacto VARCHAR(50),
IN p_telefono VARCHAR(50),
IN p_movil VARCHAR(50),
IN p_correo VARCHAR(50),
IN p_fecha VARCHAR(50)
)
BEGIN
update clientes set RazonSocialCLI = p_razon, NombreCLI = p_nombre, ApellidoCLI = p_apellido, CUIT = p_cuit, CondicionIvaCLI = p_condicion, DireccionCLI = p_direccion, 
LocalidadCLI = p_localidad, ProvinciaCLI = p_provincia, TransporteCLI = p_transporte,
ContactoCLI = p_contacto, TelefonoCLI = p_telefono, MovilCLI = p_movil, CorreoCLI = p_correo,
FechaNacCLI = p_fecha
where IdClienteCLI = p_id 
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Clientes_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Clientes_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Clientes_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM clientes
WHERE IdClienteCLI = p_id
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Clientes_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Clientes_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Clientes_Insertar`(
IN p_id INT(11),
IN p_razon VARCHAR(50),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(50),
IN p_cuit VARCHAR(50),
IN p_condicion VARCHAR(50),
IN p_direccion VARCHAR(50),
IN p_localidad VARCHAR(50),
IN p_provincia VARCHAR(50),
IN p_transporte VARCHAR(50),
IN p_contacto VARCHAR(50),
IN p_telefono VARCHAR(50),
IN p_movil VARCHAR(50),
IN p_correo VARCHAR(50),
IN p_fecha VARCHAR(50)
)
BEGIN
INSERT INTO clientes(IdClienteCLI, RazonSocialCLI, NombreCLI, ApellidoCLI, CUIT, CondicionIvaCLI, DireccionCLI, LocalidadCLI, ProvinciaCLI,
TransporteCLI, ContactoCLI, TelefonoCLI, MovilCLI, CorreoCLI, FechaNacCLI)
VALUES(p_id, p_razon, p_nombre, p_apellido, p_cuit, p_condicion, p_direccion, p_localidad, p_provincia, p_transporte, p_contacto,
p_telefono, p_movil, p_correo, p_fecha)
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Clientes_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Clientes_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Clientes_Listar`(IN p_frm TINYINT)
BEGIN
IF(p_frm = 1) THEN
	SELECT `IdClienteCLI`,`NombreCLI`,`ApellidoCLI`,`RazonSocialCLI`,`CUIT`,`CondicionIvaCLI`,`DireccionCLI`,`LocalidadCLI`,
	`ProvinciaCLI`,`TransporteCLI`,`ContactoCLI`,`TelefonoCLI`,`MovilCLI`,`CorreoCLI`,`FechaNacCLI`,
	CONCAT(NombreCLI, ' ' , ApellidoCLI) AS NombreCompletoCLI
	FROM `clientes`
	WHERE IdClienteCLI <>1
	ORDER BY RazonSocialCLI
	;
ELSE
	SELECT `IdClienteCLI`,`NombreCLI`,`ApellidoCLI`,`RazonSocialCLI`,`CUIT`,`CondicionIvaCLI`,`DireccionCLI`,`LocalidadCLI`,
	`ProvinciaCLI`,`TransporteCLI`,`ContactoCLI`,`TelefonoCLI`,`MovilCLI`,`CorreoCLI`,`FechaNacCLI`,
	CONCAT(NombreCLI, ' ' , ApellidoCLI) AS NombreCompletoCLI
	FROM `clientes`
	ORDER BY RazonSocialCLI
	;
END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Colores_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Colores_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Colores_Actualizar`(
IN p_id INT(11),
IN p_descripcion VARCHAR(50),
IN p_hex VARCHAR(50)
)
BEGIN
UPDATE colores SET DescripcionCOL = p_descripcion, HexCOL = p_hex
WHERE IdColorCOL = p_id 
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Colores_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Colores_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Colores_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM colores
WHERE IdColorCOL= p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Colores_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Colores_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Colores_Insertar`(
IN p_id INT,
IN p_descripcion VARCHAR(50),
IN p_hex VARCHAR(50)
)
BEGIN
INSERT INTO colores(IdColorCOL, DescripcionCOL, HexCOL)
VALUES(p_id, p_descripcion, p_hex)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `CondicionIva_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `CondicionIva_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `CondicionIva_Actualizar`(
in p_id tinyint(2),
IN p_descripcion varchar(50))
BEGIN
	UPDATE `condicioniva`
	SET `DescripcionCIVA` = p_descripcion
	WHERE `IdCondicionIvaCIVA` = p_id;
END */$$
DELIMITER ;

/* Procedure structure for procedure `CondicionIva_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `CondicionIva_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `CondicionIva_Borrar`(IN p_id tinyint)
BEGIN
DELETE
FROM condicioniva
WHERE `IdCondicionIvaCIVA` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `CondicionIva_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `CondicionIva_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `CondicionIva_Insertar`(
in p_id tinyint(2),
IN p_descripcion VARCHAR(50)
)
BEGIN
INSERT INTO condicioniva(IdCondicionIvaCIVA, DescripcionCIVA)
VALUES(p_id, p_descripcion)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `CondicionIva_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `CondicionIva_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `CondicionIva_Listar`()
BEGIN
		select * from condicioniva;
    END */$$
DELIMITER ;

/* Procedure structure for procedure `Cupones_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Cupones_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Cupones_Actualizar`(
in p_cupon varchar(10),
in p_importe double
)
BEGIN
	update `cupones` set `Utilizado` = 1, `Importe` = p_importe where `Nro_cupon` = p_cupon;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Cupones_GetByPk` */

/*!50003 DROP PROCEDURE IF EXISTS  `Cupones_GetByPk` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Cupones_GetByPk`(
in p_cupon varchar(10)
)
BEGIN
	select * from `cupones` where `Nro_cupon` = p_cupon;
END */$$
DELIMITER ;

/* Procedure structure for procedure `DatosPos_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `DatosPos_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `DatosPos_Listar`()
BEGIN
SELECT * FROM articulos;
SELECT * FROM clientes;
SELECT * FROM formaspago;
SELECT * FROM alicuotasiva;
select * from generos;
END */$$
DELIMITER ;

/* Procedure structure for procedure `DatosServer_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `DatosServer_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `DatosServer_Borrar`()
BEGIN
DECLARE	fecha DATE;
DECLARE	fechaStockMov DATE;
SET fecha = DATE_SUB(CURDATE(), INTERVAL 60 DAY);
SET fechaStockMov = DATE_SUB(CURDATE(), INTERVAL 365 DAY);
INSERT INTO borrarmantenimiento(Descripcion) VALUES('Mantenimiento');
DELETE FROM ventas WHERE FechaVEN < fecha;
DELETE FROM stockmov WHERE FechaMSTK < fechaStockMov;
DELETE FROM fondocaja WHERE `FechaFONP` < fecha;
DELETE FROM tesoreriamovimientos WHERE `FechaTESM` < fecha;
DELETE FROM borrarmantenimiento;
END */$$
DELIMITER ;

/* Procedure structure for procedure `DatosServer_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `DatosServer_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `DatosServer_Listar`()
BEGIN
SELECT * FROM `articuloscons`;
SELECT * FROM `articulositems` ORDER BY `DescripcionITE`;
SELECT * FROM `clientes` ORDER BY `RazonSocialCLI`;
SELECT * FROM `colores` where IdColorCOL <>0 ORDER BY `DescripcionCOL` ;
SELECT * FROM `formaspago` ORDER BY `DescripcionFOR`;
SELECT * FROM `locales` ORDER BY `NombreLOC`;
SELECT * FROM `pc`;
SELECT * FROM `proveedores` ORDER BY `RazonSocialPRO`;
SELECT * FROM `generos` ORDER BY `DescripcionGEN`;
SELECT IdAlicuotaALI, REPLACE(CAST(PorcentajeALI AS CHAR), '.', ',') as PorcentajeALI 
	FROM `alicuotasiva` ORDER BY `IdAlicuotaALI`;
SELECT * from `condicioniva` order by DescripcionCIVA;	
select * from razonsocial;
SELECT * FROM empleados;
SELECT * FROM empleadosmovtipos;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosLiquidacion` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosLiquidacion` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosLiquidacion`()
BEGIN
select * from empleados_liquidacion_cons
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovimientos_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovimientos_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovimientos_Actualizar`(
in p_id int,
IN p_fecha date,
IN p_idEmpleado INT,
in p_idTipoMov int,
in p_cantidad tinyint(3),
in p_detalle varchar(50),
in p_importe double,
in p_liquidado tinyint(1)
)
BEGIN
UPDATE `empleadosmovimientos`
SET   `FechaEMOV` = p_fecha,
	  `IdEmpleadoEMOV` = p_idEmpleado,
	  `IdMovTipoEMOV` = p_idTipoMov,
	  `CantidadEMOV` = p_cantidad,
	  `DetalleEMOV` = p_detalle,
	  `ImporteEMOV` = p_importe,
	  `LiquidadoEMOV` = p_liquidado
WHERE `IdMovEMOV` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovimientos_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovimientos_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovimientos_Borrar`(
in p_id int
)
BEGIN
	DELETE
	FROM `empleadosmovimientos`
	WHERE `IdMovEMOV` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovimientos_Cons` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovimientos_Cons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovimientos_Cons`(
IN p_fechaDesde DATE,
IN p_fechaHasta DATE,
IN p_idEmpleado INT,
IN p_liquidado INT
)
BEGIN
	IF p_idEmpleado<>0 THEN
		SELECT * FROM `empleados_mov_cons`
		WHERE `FechaEMOV` >= p_fechaDesde AND `FechaEMOV` <= p_fechaHasta 
		AND `IdEmpleadoEMOV` = p_idEmpleado AND LiquidadoEMOV = p_liquidado;
	ELSE
		SELECT * FROM `empleados_mov_cons`
		WHERE `FechaEMOV` >= p_fechaDesde AND `FechaEMOV` <= p_fechaHasta AND LiquidadoEMOV = p_liquidado;
	END IF;	
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovimientos_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovimientos_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovimientos_Insertar`(
in p_id int,
IN p_fecha date,
IN p_idEmpleado INT,
in p_idTipoMov int,
in p_cantidad tinyint(3),
in p_detalle varchar(50),
in p_importe double,
in p_liquidado tinyint(1)
)
BEGIN
INSERT INTO `empleadosmovimientos`
            (`IdMovEMOV`,
             `FechaEMOV`,
             `IdEmpleadoEMOV`,
             `IdMovTipoEMOV`,
             `CantidadEMOV`,
             `DetalleEMOV`,
             `ImporteEMOV`,
             `LiquidadoEMOV`)
VALUES (p_id,
        p_fecha,
        p_idEmpleado,
        p_idTipoMov,
        p_cantidad,
        p_detalle,
        p_importe,
        p_liquidado)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovTipos_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovTipos_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovTipos_Actualizar`(
IN p_id INT,
IN p_descripcion VARCHAR(50),
IN p_remumeracion INT(1)
)
BEGIN
UPDATE `empleadosmovtipos` 
SET `DescripcionETIP` = p_descripcion, `RemuneracionETIP` = p_remumeracion
WHERE `IdMovETIP` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovTipos_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovTipos_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovTipos_Borrar`(
IN p_id INT,
IN p_descripcion VARCHAR(50),
IN p_remumeracion INT(1)
)
BEGIN
DELETE FROM `empleadosmovtipos` 
WHERE `IdMovETIP` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `EmpleadosMovTipos_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `EmpleadosMovTipos_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `EmpleadosMovTipos_Insertar`(
IN p_id INT,
IN p_descripcion VARCHAR(50),
IN p_remumeracion INT(1)
)
BEGIN
INSERT INTO `empleadosmovtipos`(`IdMovETIP`, `DescripcionETIP`, `RemuneracionETIP`)
VALUES(p_id, p_descripcion, p_remumeracion)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Empleados_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Empleados_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Empleados_Actualizar`(
IN p_id INT(11),
IN p_dni VARCHAR(10),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(25),
IN p_direccion VARCHAR(50),
IN p_telefono VARCHAR(15),
IN p_fechaNac DATETIME,
IN p_fechaIngreso DATETIME,
IN p_idLocal INT(3),
IN p_salario DOUBLE,
IN p_cargas DOUBLE,
IN p_activo int(1)
)
BEGIN
UPDATE `empleados`
SET `DniEMP` = p_dni,
  `NombreEMP` = p_nombre,
  `ApellidoEMP` = p_apellido,
  `DireccionEMP` = p_direccion,
  `TelefonoEMP` = p_telefono,
  `FechaNacEMP` = p_fechaNac,
  `FechaIngresoEMP` = p_fechaIngreso,
  `IdLocalEMP` = p_idLocal,
  `SalarioEMP` = REPLACE(p_salario, ",","."),
  `CargasSocialesEMP` = REPLACE(p_cargas, ",","."),
  `Activa` = p_activo
WHERE `IdEmpleadoEMP` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Empleados_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Empleados_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Empleados_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM empleados
WHERE `IdEmpleadoEMP`= p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Empleados_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Empleados_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Empleados_Insertar`(
IN p_id INT(11),
IN p_dni VARCHAR(10),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(25),
IN p_direccion VARCHAR(50),
IN p_telefono VARCHAR(15),
IN p_fechaNac DATETIME,
IN p_fechaIngreso DATETIME,
IN p_idLocal INT(3),
IN p_salario DOUBLE,
IN p_cargas DOUBLE,
IN p_activo INT(1)
)
BEGIN
INSERT INTO `empleados`
            (`IdEmpleadoEMP`,
             `DniEMP`,
             `NombreEMP`,
             `ApellidoEMP`,
             `DireccionEMP`,
             `TelefonoEMP`,
             `FechaNacEMP`,
             `FechaIngresoEMP`,
             `IdLocalEMP`,
             `SalarioEMP`,
             `CargasSocialesEMP`,
             `Activa`)
VALUES (p_id,
        p_dni,
        p_nombre,
        p_apellido,
        p_direccion,
        p_telefono,
        p_fechaNac,
        p_fechaIngreso,
        p_idLocal,
        REPLACE(p_salario, ",","."),
        REPLACE(p_cargas, ",","."),
        p_activo)    
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Empleados_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Empleados_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Empleados_Listar`()
BEGIN
		SELECT * FROM empleados;
    END */$$
DELIMITER ;

/* Procedure structure for procedure `Exportar_Controlar_Restore` */

/*!50003 DROP PROCEDURE IF EXISTS  `Exportar_Controlar_Restore` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Exportar_Controlar_Restore`(
p_fecha VARCHAR(50),
p_pc INT)
BEGIN
	SELECT COUNT(*) FROM exportar_fondo_caja
	WHERE `FechaFONP` = p_fecha AND `IdPcFONP` = p_pc;
 
END */$$
DELIMITER ;

/* Procedure structure for procedure `Exportar_Insertar_Movimientos` */

/*!50003 DROP PROCEDURE IF EXISTS  `Exportar_Insertar_Movimientos` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Exportar_Insertar_Movimientos`()
BEGIN
SET @fecha = (SELECT `FechaFONP` FROM exportar_fondo_caja);
SET @pc = (SELECT `IdPcFONP` FROM exportar_fondo_caja);
DELETE FROM fondocaja WHERE FechaFONP = @fecha AND IdPcFONP = @pc;
DELETE FROM tesoreriamovimientos WHERE FechaTESM = @fecha AND IdPcTESM = @pc;
DELETE FROM `ventas` WHERE (`FechaVEN` >= @fecha AND `FechaVEN` < DATE_SUB(@fecha, INTERVAL -1 DAY))
	AND (IdPCVEN = @pc);
INSERT INTO tesoreriamovimientos SELECT * FROM exportar_tesoreria_movimientos;
INSERT INTO ventas SELECT * FROM exportar_ventas;
INSERT INTO ventasdetalle(`IdDVEN`,`IdVentaDVEN`,`IdLocalDVEN`,`IdArticuloDVEN`,`DescripcionDVEN`,`CantidadDVEN`,
	`PrecioPublicoDVEN`,`PrecioCostoDVEN`,`PrecioMayorDVEN`,`IdFormaPagoDVEN`,`NroCuponDVEN`,`NroFacturaDVEN`,
	`IdEmpleadoDVEN`,`LiquidadoDVEN`,`EsperaDVEN`,`DevolucionDVEN`)
	 SELECT `IdDVEN`,`IdVentaDVEN`,`IdLocalDVEN`,`IdArticuloDVEN`,`DescripcionDVEN`,`CantidadDVEN`,
	`PrecioPublicoDVEN`,`PrecioCostoDVEN`,`PrecioMayorDVEN`,`IdFormaPagoDVEN`,`NroCuponDVEN`,`NroFacturaDVEN`,
	`IdEmpleadoDVEN`,`LiquidadoDVEN`,`EsperaDVEN`,`DevolucionDVEN`
	 FROM exportar_ventas_detalle;
INSERT INTO fondocaja SELECT * FROM exportar_fondo_caja;	 
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Actualizar`(
in p_fecha VARCHAR(50),
IN p_pc int(11),
in p_importe double
)
BEGIN
update fondocaja set `FechaFONP` = p_fecha, `IdPcFONP` = p_pc, `ImporteFONP` = p_importe
where `FechaFONP` = p_fecha AND `IdPcFONP` = p_pc
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Actualizar2` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Actualizar2` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Actualizar2`(
IN p_id INT,
IN p_fecha VARCHAR(50),
IN p_pc INT(11),
IN p_importe DOUBLE
)
BEGIN
UPDATE `fondocaja` SET `IdFondoFONP` = p_id, `FechaFONP` = p_fecha, `IdPcFONP` = p_pc, `ImporteFONP` = p_importe
WHERE `IdFondoFONP` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Borrar`(
IN p_fecha varchar(50),
IN p_pc INT(11)
)
BEGIN
DELETE
FROM fondocaja
WHERE `FechaFONP` = p_fecha and `IdPcFONP` = p_pc
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_BorrarByPk` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_BorrarByPk` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_BorrarByPk`(
IN p_id INT
)
BEGIN
DELETE
FROM `fondocaja`
WHERE `IdFondoFONP` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Inicial_Final` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Inicial_Final` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Inicial_Final`(
in p_fecha varchar(50),
in p_idPc int(11)
)
BEGIN
SELECT `ImporteFONP` FROM fondocaja
WHERE `FechaFONP` = (SELECT MAX(`FechaFONP`) FROM FondoCaja WHERE `FechaFONP` < p_fecha)
AND `IdPcFONP` = p_idPc
;
SELECT `ImporteFONP` FROM fondocaja
WHERE `FechaFONP` = p_fecha
AND `IdPcFONP` = p_idPc
;
SELECT * FROM `tesoreriamovimientos`
WHERE `FechaTESM` = p_fecha
AND `IdPcTESM` = p_idPc
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Insertar`(
IN p_fecha DATETIME,
IN p_pc INT(11),
IN p_importe DOUBLE
)
BEGIN
INSERT INTO fondocaja(`FechaFONP`, `IdPcFONP`, `ImporteFONP`)
VALUES(p_fecha, p_pc, p_importe)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Insertar2` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Insertar2` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Insertar2`(
IN p_id INT,
IN p_fecha DATETIME,
IN p_pc INT(11),
IN p_importe DOUBLE
)
BEGIN
INSERT INTO `fondocaja`(`IdFondoFONP`, `FechaFONP`, `IdPcFONP`, `ImporteFONP`)
VALUES(p_id, p_fecha, p_pc, p_importe)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_Listar`()
BEGIN
SELECT *
FROM fondocaja
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FondoCaja_ListarCons` */

/*!50003 DROP PROCEDURE IF EXISTS  `FondoCaja_ListarCons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FondoCaja_ListarCons`()
BEGIN
SELECT *
FROM `fondocajacons`
order by `FechaFONP` desc
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FormasPago_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FormasPago_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FormasPago_Actualizar`(
in p_id int(11),
IN p_descripcion varchar(50))
BEGIN
update `formaspago` set `DescripcionFOR` = p_descripcion
where `IdFormaPagoFOR` = p_id 
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FormasPago_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FormasPago_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FormasPago_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM formaspago
WHERE IdFormaPagoFOR= p_id
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `FormasPago_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `FormasPago_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `FormasPago_Insertar`(
in p_id int(11),
IN p_descripcion VARCHAR(50)
)
BEGIN
INSERT INTO formaspago(IdFormaPagoFOR, DescripcionFOR)
VALUES(p_id, p_descripcion)
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Generos_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Generos_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Generos_Actualizar`(
in p_id int(11),
IN p_descripcion varchar(50),
IN p_activoWeb INT(1)
)
BEGIN
update `generos` set DescripcionGEN = p_descripcion, ActivoWebGEN = p_activoWeb
where `IdGeneroGEN` = p_id 
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Generos_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Generos_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Generos_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM `generos`
WHERE `IdGeneroGEN`= p_id
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Generos_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Generos_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Generos_Insertar`(
in p_id int,
IN p_descripcion VARCHAR(50),
IN p_activoWeb INT(1)
)
BEGIN
INSERT INTO `generos`(`IdGeneroGEN`, `DescripcionGEN`, `ActivoWebGEN`)
VALUES(p_id, p_descripcion, p_activoWeb)
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Locales_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Locales_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Locales_Actualizar`(
IN p_id INT(11),
IN p_nombre VARCHAR(50),
IN p_direccion VARCHAR(50),
IN p_telefono VARCHAR(50),
IN p_activoWeb TINYINT(1)
)
BEGIN
UPDATE `locales`
SET `NombreLOC` = p_nombre,
  `DireccionLOC` = p_direccion,
  `TelefonoLOC` = p_telefono,
  `ActivoWebLOC` = p_activoWeb
WHERE `IdLocalLOC` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Locales_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Locales_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Locales_Borrar`(
IN p_id INT(11)
)
BEGIN
DELETE
FROM `locales`
WHERE `IdLocalLOC` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Locales_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Locales_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Locales_Insertar`(
IN p_id INT(11),
IN p_nombre VARCHAR(50),
IN p_direccion VARCHAR(50),
IN p_telefono VARCHAR(50),
IN p_activoWeb TINYINT(1)
)
BEGIN
INSERT INTO `locales`
            (`IdLocalLOC`,
             `NombreLOC`,
             `DireccionLOC`,
             `TelefonoLOC`,
             `ActivoWebLOC`)
		VALUES (p_id,
				p_nombre,
				p_direccion,
				p_telefono,
				p_activoWeb)
		;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Locales_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Locales_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Locales_Listar`()
BEGIN
select *
FROM locales
where NombreLOC not like 'Entradas' or NombreLOC NOT LIKE 'Salidas'
order by IdLocalLOC
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Pcs_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Pcs_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Pcs_Listar`()
BEGIN
select *
FROM pc
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Pedido_Cons` */

/*!50003 DROP PROCEDURE IF EXISTS  `Pedido_Cons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Pedido_Cons`(
IN p_fechaDesde VARCHAR(50),
IN p_genero VARCHAR(3)
)
BEGIN
DROP TABLE IF EXISTS `pedidoventas`;
IF p_genero = '99' THEN
	CREATE TEMPORARY TABLE pedidoventas
	AS SELECT
		`ventasdetallepedidocons`.`IdArticuloDVEN`
	    , SUM(`ventasdetallepedidocons`.`CantidadDVEN`) AS Venta
	FROM
	    `ventasdetallepedidocons`
	    INNER JOIN `ventas` 
		ON (`ventasdetallepedidocons`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`)
	WHERE FechaVEN >=p_fechaDesde       
	GROUP BY `ventasdetallepedidocons`.`IdArticuloDVEN`
	;
ELSE
	CREATE TEMPORARY TABLE pedidoventas
	AS SELECT
		`ventasdetallepedidocons`.`IdArticuloDVEN`
	    , SUM(`ventasdetallepedidocons`.`CantidadDVEN`) AS Venta
	FROM
	    `ventasdetallepedidocons`
	    INNER JOIN `ventas` 
		ON (`ventasdetallepedidocons`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`)
	WHERE FechaVEN >=p_fechaDesde AND IdGeneroART = p_genero        
	GROUP BY `ventasdetallepedidocons`.`IdArticuloDVEN`
	;
END IF;		
DROP TABLE IF EXISTS pedido;
IF p_genero = '99' THEN
	CREATE TEMPORARY TABLE pedido
	AS SELECT
		`pedidostockcons`.`RazonSocialPRO` AS Proveedor
		, `pedidostockcons`.`IdArticuloSTK` AS Articulo
		, `pedidostockcons`.`DescripcionART` AS Descripcion
		, `pedidoventas`.`Venta`
		, `pedidostockcons`.`Stock`
		, `pedidostockcons`.`Costo`
		, `pedidostockcons`.`Publico`
	FROM
	    `pedidoventas`
	    RIGHT JOIN `pedidostockcons` 
		ON (`pedidoventas`.`IdArticuloDVEN` = `pedidostockcons`.`IdArticuloSTK`)    
	    ORDER BY `pedidostockcons`.`DescripcionART`    
	;
ELSE
	CREATE TEMPORARY TABLE pedido
	AS SELECT
		`pedidostockcons`.`RazonSocialPRO` AS Proveedor
		, `pedidostockcons`.`IdArticuloSTK` AS Articulo
		, `pedidostockcons`.`DescripcionART` AS Descripcion
		, `pedidoventas`.`Venta`
		, `pedidostockcons`.`Stock`
		, `pedidostockcons`.`Costo`
		, `pedidostockcons`.`Publico`
	FROM
	    `pedidoventas`
	    RIGHT JOIN `pedidostockcons` 
		ON (`pedidoventas`.`IdArticuloDVEN` = `pedidostockcons`.`IdArticuloSTK`)
	    WHERE IdGeneroART = p_genero       
	    ORDER BY `pedidostockcons`.`DescripcionART`    
	;
END IF;		
SELECT * FROM pedido ORDER BY Descripcion;
SELECT LEFT(Articulo, 6) AS IdArticulo FROM pedido GROUP BY LEFT(Articulo, 6);
END */$$
DELIMITER ;

/* Procedure structure for procedure `Proveedores_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Proveedores_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Proveedores_Actualizar`(
in p_id int(11),
IN p_razon varchar(50),
IN p_direccion VARCHAR(50),
IN p_codigo VARCHAR(50),
IN p_telefono VARCHAR(50),
IN p_contacto VARCHAR(50)
)
BEGIN
update proveedores set RazonSocialPRO = p_razon, DireccionPRO = p_direccion, CodigoPostalPRO = p_codigo,
TelefonoPRO = p_telefono, ContactoPRO = p_contacto
where IdProveedorPRO = p_id 
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Proveedores_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Proveedores_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Proveedores_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM proveedores
WHERE IdProveedorPRO = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Proveedores_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Proveedores_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Proveedores_Insertar`(
IN p_id int,
IN p_razon VARCHAR(50),
IN p_direccion VARCHAR(50),
IN p_codigo VARCHAR(50),
IN p_telefono VARCHAR(50),
IN p_contacto VARCHAR(50)
)
BEGIN
INSERT INTO proveedores(IdProveedorPRO, RazonSocialPRO, DireccionPRO , CodigoPostalPRO, TelefonoPRO, ContactoPRO)
VALUES(p_id, p_razon, p_direccion, p_codigo, p_telefono, p_contacto)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `RazonSocial_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `RazonSocial_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `RazonSocial_Actualizar`(
in p_id int,
IN p_razon varchar(50),
IN p_nombre VARCHAR(50),
IN p_domicilio VARCHAR(50),
IN p_localidad VARCHAR(50),
IN p_provincia VARCHAR(50),
IN p_idCondicionIva tinyint(2),
IN p_cuit VARCHAR(50),
IN p_ingresosB VARCHAR(50),
IN p_inicio datetime
)
BEGIN
UPDATE `razonsocial`
SET `RazonSocialRAZ` = p_razon, 
  `NombreFantasiaRAZ` = p_nombre,
  `DomicilioRAZ` = p_domicilio,
  `LocalidadRAZ` = p_localidad,
  `ProvinciaRAZ` = p_provincia,
  `IdCondicionIvaRAZ` = p_idCondicionIva,
  `CuitRAZ` = p_cuit,
  `IngresosBrutosRAZ` = p_ingresosB,
  `InicioActividadRAZ` = p_inicio
WHERE `IdRazonSocialRAZ` = p_id
;
UPDATE `razonsocial` SET ActualizarDatosRAZ = 1
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `RazonSocial_ActualizarDatos` */

/*!50003 DROP PROCEDURE IF EXISTS  `RazonSocial_ActualizarDatos` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `RazonSocial_ActualizarDatos`()
BEGIN
UPDATE `razonsocial` SET ActualizarDatosRAZ = 0
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `RazonSocial_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `RazonSocial_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `RazonSocial_Borrar`(
IN p_id int
)
BEGIN
delete from `razonsocial`
where IdRazonSocialRAZ = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `RazonSocial_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `RazonSocial_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `RazonSocial_Insertar`(
IN p_id int(11),
IN p_razon VARCHAR(50),
IN p_nombre VARCHAR(50),
IN p_domicilio VARCHAR(50),
IN p_localidad VARCHAR(50),
IN p_provincia VARCHAR(50),
IN p_idCondicionIva TINYINT(2),
IN p_cuit VARCHAR(50),
IN p_ingresosB VARCHAR(50),
IN p_inicio DATETIME
)
BEGIN
INSERT INTO `razonsocial`
            (`IdRazonSocialRAZ`,
             `RazonSocialRAZ`,
             `NombreFantasiaRAZ`,
             `DomicilioRAZ`,
             `LocalidadRAZ`,
             `ProvinciaRAZ`,
             `IdCondicionIvaRAZ`,
             `CuitRAZ`,
             `IngresosBrutosRAZ`,
             `InicioActividadRAZ`)
VALUES (p_id,
        p_razon,
        p_nombre,
        p_domicilio,
        p_localidad,
        p_provincia,
        p_idCondicionIva,
        p_cuit,
        p_ingresosB,
        p_inicio)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `RazonSocial_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `RazonSocial_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `RazonSocial_Listar`()
BEGIN
select * from `razonsocial`
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMovDetalle_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMovDetalle_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMovDetalle_Actualizar`(
in p_id_detalle int,
in p_articulo varchar(50),
in p_cantidad int,
in p_compensa tinyint
)
BEGIN
UPDATE stockmovdetalle SET IdArticuloMSTKD = p_articulo, CantidadMSTKD = p_cantidad, CompensaMSTKD = p_compensa
WHERE IdMSTKD = p_id_detalle
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMovDetalle_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMovDetalle_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMovDetalle_Borrar`(IN p_id_detalle INT)
BEGIN
DELETE
FROM stockmovdetalle
WHERE IdMSTKD= p_id_detalle
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMovDetalle_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMovDetalle_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMovDetalle_Insertar`(
IN p_id_detalle INT,
IN p_id_mov INT,
IN p_articulo VARCHAR(50),
IN p_cantidad INT,
IN p_compensa TINYINT,
IN p_origen TINYINT,
IN p_destino TINYINT
)
BEGIN
INSERT INTO stockmovdetalle(IdMSTKD, IdMovMSTKD, IdArticuloMSTKD, CantidadMSTKD, CompensaMSTKD,
OrigenMSTKD, DestinoMSTKD)
VALUES(p_id_detalle, p_id_mov, p_articulo, p_cantidad, p_compensa, p_origen, p_destino)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMov_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMov_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMov_Actualizar`(
in p_id int,
IN p_fecha datetime,
in p_origen int,
in p_destino int,
IN p_compensa TINYINT
)
BEGIN
update stockmov set FechaMSTK = p_fecha, OrigenMSTK = p_origen, DestinoMSTK = p_destino, CompensaMSTK = p_compensa
where IdMovMSTK = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMov_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMov_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMov_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM stockmov
WHERE IdMovMSTK= p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMov_CompPesos` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMov_CompPesos` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMov_CompPesos`(
IN p_fecha_desde VARCHAR(50),
IN p_fecha_hasta VARCHAR(50),
IN p_id_local INT(11)
)
BEGIN
DROP TABLE IF EXISTS comppesos;
CREATE TABLE comppesos
AS SELECT
  `stockmov`.`FechaMSTK`              AS `FechaMSTK`,
  `stockmov`.`DestinoMSTK`            AS `DestinoMSTK`,
  `stockmov`.`CompensaMSTK`           AS `CompensaMSTK`,
  `articulos`.`PrecioCostoART`        AS `PrecioCostoART`,
  `articulos`.`PrecioPublicoART`      AS `PrecioPublicoART`,
  `stockmovdetalle`.`CantidadMSTKD`   AS `CantidadMSTKD`,
  `articulos`.`PrecioCostoART` * `CantidadMSTKD` AS SubtotalCosto,
  `articulos`.`PrecioPublicoART` * `CantidadMSTKD` AS SubtotalPublico
FROM ((`stockmovdetalle`
    JOIN `stockmov`
      ON ((`stockmovdetalle`.`IdMovMSTKD` = `stockmov`.`IdMovMSTK`)))
   JOIN `articulos`
     ON ((`stockmovdetalle`.`IdArticuloMSTKD` = `articulos`.`IdArticuloART`)))
WHERE DestinoMSTK = p_id_local AND CompensaMSTK =1 AND FechaMSTK>=p_fecha_desde  AND FechaMSTK<=p_fecha_hasta
;
DROP TABLE IF EXISTS `stockcomppesos`;
CREATE TABLE stockcomppesos
AS SELECT SUM(CantidadMSTKD) AS Prendas, SUM(SubtotalCosto) AS Costo, 
	SUM(SubtotalPublico) AS Publico, DestinoMSTK	
FROM `comppesos`
GROUP BY DestinoMSTK
;
SELECT * FROM stockcomppesos;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMov_Cons` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMov_Cons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMov_Cons`(
IN p_fecha_desde VARCHAR(50),
IN p_fecha_hasta VARCHAR(50),
IN p_id_local INT(3),
IN p_tipo_mov VARCHAR(15),
IN p_movimiento VARCHAR(8),
IN p_articulo VARCHAR(10),
IN p_descripcion VARCHAR(55)
)
BEGIN
IF p_tipo_mov ='movimientos' THEN
	IF p_id_local<>99 THEN
		IF p_movimiento ='entradas' THEN
			IF p_articulo = "" THEN
				SELECT * FROM stockmovcons
				WHERE ((`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
				AND `DestinoMSTK` = p_id_local AND `CompensaMSTK` = 0) 
				AND (DescripcionART LIKE CONCAT('%',p_descripcion,'%'))
				ORDER BY ordenar;
			ELSE
				SELECT * FROM stockmovcons
				WHERE (`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
				AND `DestinoMSTK` = p_id_local AND `CompensaMSTK` = 0 
				AND IdArticuloMSTKD LIKE CONCAT(p_articulo,'%')
				ORDER BY ordenar;	
			END IF;		
		ELSEIF p_movimiento ='salidas' THEN
			IF p_articulo = "" THEN
				SELECT * FROM stockmovcons
				WHERE ((`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
				AND `OrigenMSTK` = p_id_local AND `CompensaMSTK` = 0) 
				AND (DescripcionART LIKE CONCAT('%',p_descripcion,'%'))
				ORDER BY ordenar;
			ELSE
				SELECT * FROM stockmovcons
				WHERE (`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
				AND `OrigenMSTK` = p_id_local AND `CompensaMSTK` = 0 
				AND IdArticuloMSTKD LIKE CONCAT(p_articulo,'%')
				ORDER BY ordenar;	
			END IF;			
		ELSE
			IF p_articulo = "" THEN
				SELECT * FROM stockmovcons
				WHERE (`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
				AND (`OrigenMSTK` = p_id_local OR `DestinoMSTK` = p_id_local) AND `CompensaMSTK` = 0 
				AND (DescripcionART LIKE CONCAT('%',p_descripcion,'%'))
				ORDER BY ordenar;
			ELSE
				SELECT * FROM stockmovcons
				WHERE (`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
				AND (`OrigenMSTK` = p_id_local OR `DestinoMSTK` = p_id_local) AND `CompensaMSTK` = 0 
				AND IdArticuloMSTKD LIKE CONCAT(p_articulo,'%')
				ORDER BY ordenar;	
			END IF;			
		END IF;
	END IF;
ELSE
	IF p_articulo = "" THEN
		SELECT * FROM stockmovcons
		WHERE (`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
		AND (`DestinoMSTK` = p_id_local) AND `CompensaMSTK` = 1
		AND (DescripcionART LIKE CONCAT('%',p_descripcion,'%'))
		ORDER BY ordenar;
	ELSE
		SELECT * FROM stockmovcons
		WHERE (`FechaMSTK` >= p_fecha_desde AND `FechaMSTK` <= p_fecha_hasta) 
		AND (`DestinoMSTK` = p_id_local) AND `CompensaMSTK` = 1
		AND IdArticuloMSTKD LIKE CONCAT(p_articulo,'%')
		ORDER BY ordenar;	
	END IF;	
END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMov_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMov_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMov_Insertar`(
in p_id int,
IN p_fecha datetime,
in p_origen int,
in p_destino int,
IN p_compensa tinyint
)
BEGIN
INSERT INTO stockmov(IdMovMSTK, FechaMSTK, OrigenMSTK, DestinoMSTK, CompensaMSTK)
VALUES(p_id, p_fecha, p_origen, p_destino, p_compensa)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `StockMov_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `StockMov_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `StockMov_Listar`(
)
BEGIN
select * from stockmov;
select * from stockmovdetalle;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Stock_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Stock_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Stock_Actualizar`(
in p_id_articulo varchar(50),
IN p_id_local int,
in p_cantidad int
)
BEGIN
update stock set CantidadSTK = p_cantidad
where IdArticuloSTK = p_id_articulo and IdLocalSTK = p_id_local
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Stock_Cons` */

/*!50003 DROP PROCEDURE IF EXISTS  `Stock_Cons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Stock_Cons`(
IN p_locales VARCHAR(200),
IN p_genero char(3),
IN p_proveedor SMALLINT(4),
IN p_articulo char(10),
IN p_descripcion VARCHAR(200),
in p_activoWeb tinyint(1)
)
BEGIN
	DROP TABLE IF EXISTS stockcons;
	CREATE TEMPORARY TABLE stockcons
	AS SELECT
	  `stock`.`IdArticuloSTK`        AS `Articulo`,
	  `articulos`.`IdGeneroART`      AS `IdGeneroART`,
	  `articulos`.`DescripcionART`   AS `Descripcion`,
	  `articulos`.`ActivoWebART`     AS `ActivoWebART`,
	  `articulos`.`ImagenART`        AS `ImagenART`,
	  `stock`.`CantidadSTK`          AS `Cantidad`,
	  `proveedores`.`IdProveedorPRO` AS `IdProveedorPRO`,
	  `proveedores`.`RazonSocialPRO` AS `Proveedor`,
	  `locales`.`NombreLOC`          AS `NombreLOC`,
	  `stock`.`IdLocalSTK`           AS `IdLocalSTK`
	FROM (((`stock`
	     JOIN `articulos`
	       ON ((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)))
	    JOIN `proveedores`
	      ON ((`articulos`.`IdProveedorART` = `proveedores`.`IdProveedorPRO`)))
	   JOIN `locales`
	     ON ((`stock`.`IdLocalSTK` = `locales`.`IdLocalLOC`)))
	WHERE (`stock`.`CantidadSTK` <> 0) 
	;  
	IF p_proveedor<>0 THEN
		if p_activoWeb = 0 then
			SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
			WHERE ((",p_locales,") AND IdProveedorPRO ='",p_proveedor,"') AND (Articulo LIKE '",p_articulo,"%'
				AND Descripcion LIKE '%",p_descripcion,"%')
			ORDER BY Descripcion, NombreLOC");	
		else
			SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
			WHERE ((",p_locales,") AND IdProveedorPRO ='",p_proveedor,"') AND (Articulo LIKE '",p_articulo,"%'
				AND Descripcion LIKE '%",p_descripcion,"%' AND ActivoWebART = 1 AND ImagenART<>'')
			ORDER BY Descripcion, NombreLOC");			
		end if;
	ELSE
		IF p_activoWeb = 0 THEN
			SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
			WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%'
				AND Descripcion LIKE '%",p_descripcion,"%'))
			ORDER BY Descripcion, NombreLOC");
		ELSE
			SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
			WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%'
				AND Descripcion LIKE '%",p_descripcion,"%' AND ActivoWebART = 1 AND ImagenART<>''))
			ORDER BY Descripcion, NombreLOC");		
		END IF;
	END IF;
  
  PREPARE smpt FROM @QUERY;
  EXECUTE smpt;
  
END */$$
DELIMITER ;

/* Procedure structure for procedure `Stock_Cons2` */

/*!50003 DROP PROCEDURE IF EXISTS  `Stock_Cons2` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Stock_Cons2`(
IN p_locales VARCHAR(200),
IN p_genero char(3),
IN p_proveedor SMALLINT(4),
IN p_articulo char(10),
IN p_descripcion VARCHAR(200),
in p_activoWeb tinyint(1)
)
BEGIN
	DROP TABLE IF EXISTS stockcons;
	CREATE TEMPORARY TABLE stockcons
	AS SELECT
	  `stock`.`IdArticuloSTK`        AS `Articulo`,
	  `articulos`.`IdGeneroART`      AS `IdGeneroART`,
	  `articulos`.`DescripcionART`   AS `Descripcion`,
	  `articulos`.`ActivoWebART`     AS `ActivoWebART`,
	  `articulos`.`ImagenART`        AS `ImagenART`,
	  `stock`.`CantidadSTK`          AS `Cantidad`,
	  `proveedores`.`IdProveedorPRO` AS `IdProveedorPRO`,
	  `proveedores`.`RazonSocialPRO` AS `Proveedor`,
	  `locales`.`NombreLOC`          AS `NombreLOC`,
	  `stock`.`IdLocalSTK`           AS `IdLocalSTK`
	FROM (((`stock`
	     JOIN `articulos`
	       ON ((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)))
	    JOIN `proveedores`
	      ON ((`articulos`.`IdProveedorART` = `proveedores`.`IdProveedorPRO`)))
	   JOIN `locales`
	     ON ((`stock`.`IdLocalSTK` = `locales`.`IdLocalLOC`)))
	WHERE (`stock`.`CantidadSTK` <> 0) 
	;  
	IF p_genero ='' and p_proveedor = 0 and p_activoWeb = 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%'
			AND Descripcion LIKE '%",p_descripcion,"%'))
		ORDER BY Descripcion");
	elseif p_genero<>'' AND p_proveedor = 0 AND p_activoWeb = 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%'
			AND Descripcion LIKE '%",p_descripcion,"%' AND IdGeneroART = '", p_genero, "'))
		ORDER BY Descripcion");
	ELSEIF p_genero<>'' AND p_proveedor <> 0 AND p_activoWeb = 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%' AND Descripcion LIKE '%",p_descripcion,"%' 
		AND IdGeneroART = '", p_genero, "' AND IdProveedorPRO = '", p_proveedor, "'))
		ORDER BY Descripcion");	
	ELSEIF p_genero<>'' AND p_proveedor = 0 AND p_activoWeb <> 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%' AND Descripcion LIKE '%",p_descripcion,"%' 
		AND IdGeneroART = '", p_genero, "' AND ActivoWebART = '", p_activoWeb, "'))
		ORDER BY Descripcion");	
	ELSEIF p_genero<>'' AND p_proveedor <> 0 AND p_activoWeb <> 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%' AND Descripcion LIKE '%",p_descripcion,"%' 
		AND IdGeneroART = '", p_genero, "' AND IdProveedorPRO = '", p_proveedor, "' AND ActivoWebART = '", p_activoWeb, "'))
		ORDER BY Descripcion");		
	ELSEIF p_genero ='' AND p_proveedor <> 0 AND p_activoWeb = 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%' AND Descripcion LIKE '%",p_descripcion,"%' 
		AND IdProveedorPRO = '", p_proveedor, "'))
		ORDER BY Descripcion");	
	ELSEIF p_genero ='' AND p_proveedor <> 0 AND p_activoWeb <> 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%' AND Descripcion LIKE '%",p_descripcion,"%' 
		AND IdProveedorPRO = '", p_proveedor, "' AND ActivoWebART = '", p_activoWeb, "'))
		ORDER BY Descripcion");	
	ELSEIF p_genero ='' AND p_proveedor = 0 AND p_activoWeb <> 0 THEN
		SET @QUERY = CONCAT("SELECT Articulo, Descripcion, Cantidad, Proveedor, NombreLOC FROM stockcons 
		WHERE ((",p_locales,") AND (Articulo LIKE '",p_articulo,"%' AND Descripcion LIKE '%",p_descripcion,"%' 
		AND ActivoWebART = '", p_activoWeb, "'))
		ORDER BY Descripcion");												
	END IF;
  
  PREPARE smpt FROM @QUERY;
  EXECUTE smpt;
  
END */$$
DELIMITER ;

/* Procedure structure for procedure `Stock_Cons2_prueba` */

/*!50003 DROP PROCEDURE IF EXISTS  `Stock_Cons2_prueba` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Stock_Cons2_prueba`()
BEGIN
CREATE TEMPORARY TABLE pedido
AS SELECT
  `locales`.`IdLocalLOC`            AS `IdLocalLOC`,
  `locales`.`NombreLOC`             AS `NombreLOC`,
  `ventas`.`FechaVEN`               AS `FechaVEN`,
  `articulos`.`IdGeneroART`         AS `IdGeneroART`,
  `ventasdetalle`.`IdFormaPagoDVEN` AS `IdFormaPagoDVEN`,
  `ventasdetalle`.`CantidadDVEN`    AS `Prendas`,
  (`ventasdetalle`.`PrecioCostoDVEN` * `ventasdetalle`.`CantidadDVEN`) AS `Costo`,
  (`ventasdetalle`.`PrecioPublicoDVEN` * `ventasdetalle`.`CantidadDVEN`) AS `Venta`
FROM ((((`ventasdetalle`
      JOIN `articulos`
        ON ((`articulos`.`IdArticuloART` = `ventasdetalle`.`IdArticuloDVEN`)))
     JOIN `ventas`
       ON ((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`)))
    JOIN `pc`
      ON ((`ventas`.`IdPCVEN` = `pc`.`IdPC`)))
   JOIN `locales`
     ON ((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`)))  
;
   SET @OCOTE = "SELECT * FROM pedido";	
  PREPARE smpt FROM @OCOTE;
  EXECUTE smpt;
  
END */$$
DELIMITER ;

/* Procedure structure for procedure `Stock_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Stock_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Stock_Insertar`(
IN p_id_articulo VARCHAR(50),
IN p_id_local INT,
IN p_cantidad INT
)
BEGIN
INSERT INTO `ncsoftwa_re`.`stock`
            (`IdArticuloSTK`,
             `IdLocalSTK`,
             `CantidadSTK`)
VALUES (p_id_articulo,
        p_id_local,
        p_cantidad)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Stock_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Stock_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Stock_Listar`(
)
BEGIN
select *
FROM stock
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Tablas_Normalizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Tablas_Normalizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Tablas_Normalizar`()
BEGIN
DELETE FROM articulos WHERE IdArticuloART NOT LIKE '000000000%';
DELETE FROM articulositems WHERE IdItemITE NOT LIKE '3';
DELETE FROM clientes WHERE IdClienteCLI NOT LIKE '1';
DELETE FROM colores WHERE IdColorCOL >1;
DELETE FROM `comppesos`;
DELETE FROM contador;
DELETE FROM contador_historico;
DELETE FROM cupones;
DELETE FROM cupones_config;
DELETE FROM empleados;
DELETE FROM empleadosmovimientos;
DELETE FROM `exportar_fondo_caja`;
DELETE FROM `exportar_tesoreria_movimientos`;
DELETE FROM `exportar_ventas`;
DELETE FROM `exportar_ventas_detalle`;
DELETE FROM `fondocaja`;
DELETE FROM proveedores WHERE `RazonSocialPRO` NOT LIKE 'OPERA ROCK';
DELETE FROM `razonsocial`;
DELETE FROM `stock`;
DELETE FROM `stockcomppesos`;
DELETE FROM `stockmov`;
DELETE FROM `tesoreriamovimientos`;
DELETE FROM `ventas`;
DELETE FROM `ventasdetalle`;
DELETE FROM `ventash`;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMovFechaLocal_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMovFechaLocal_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMovFechaLocal_Listar`(
in p_fecha datetime,
in p_pc int(11)
)
BEGIN
select *
FROM tesoreriamovimientos
where `FechaTESM` = p_fecha and `IdPcTESM` = p_pc
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMovItems_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMovItems_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMovItems_Actualizar`(
in p_id int(11),
IN p_descripcion varchar(50))
BEGIN
	UPDATE `tesoreria_mov_items`
	SET `DescripcionTESMI` = p_descripcion
	WHERE `IdTESMI` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMovItems_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMovItems_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMovItems_Borrar`(
in p_id int(11)
)
BEGIN
	delete from `tesoreria_mov_items`
	WHERE `IdTESMI` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMovItems_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMovItems_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMovItems_Insertar`(
in p_id int(11),
in p_descripcion varchar(50)
)
BEGIN
	insert into `tesoreria_mov_items`(IdTESMI, DescripcionTESMI)
	values(p_id, p_descripcion)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMov_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMov_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMov_Actualizar`(
in p_id int(11),
in p_fecha datetime,
IN p_pc int(11),
IN p_detalle varchar(50),
in p_importe double
)
BEGIN
update tesoreriamovimientos set `FechaTESM` = p_fecha, `IdPcTESM` = p_pc, 
`DetalleTESM` = p_detalle, `ImporteTESM` = p_importe
where `IdMovTESM` = p_id 
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMov_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMov_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMov_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM tesoreriamovimientos
WHERE `IdMovTESM` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMov_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMov_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMov_Insertar`(
IN p_id INT(11),
IN p_fecha DATETIME,
IN p_pc INT(11),
IN p_detalle VARCHAR(50),
IN p_importe DOUBLE
)
BEGIN
INSERT INTO tesoreriamovimientos(`IdMovTESM`, `FechaTESM`, `IdPcTESM`, `DetalleTESM`, `ImporteTESM`)
VALUES(p_id, p_fecha, p_pc, p_detalle, p_importe)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `TesoreriaMov_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `TesoreriaMov_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `TesoreriaMov_Listar`()
BEGIN
select *
FROM tesoreriamovimientos
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Usuarios_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Usuarios_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Usuarios_Actualizar`(
IN p_id INT(11),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(50),
IN p_correo VARCHAR(50),
IN p_clave VARCHAR(50),
IN p_nivel tinyint(1)
)
BEGIN
UPDATE usuarios SET `nombre` = p_nombre, `apellido` = p_apellido, `correo` = p_correo, 
`clave` = p_clave, nivel_seguridad = p_nivel
WHERE `id_usuario` = p_id 
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Usuarios_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Usuarios_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Usuarios_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM usuarios
WHERE `id_usuario`= p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Usuarios_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Usuarios_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Usuarios_Insertar`(
IN p_id INT(11),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(50),
IN p_correo VARCHAR(50),
IN p_clave VARCHAR(50),
IN p_nivel TINYINT(1)
)
BEGIN
INSERT INTO usuarios(`id_usuario`, `nombre`, `apellido`, `correo`, `clave`, `nivel_seguridad`)
VALUES(p_id, p_nombre, p_apellido, p_correo, p_clave, p_nivel )
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasDetalleCons_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasDetalleCons_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasDetalleCons_Listar`(
IN p_local INT(2),
IN p_forma INT(2),
IN p_fechaDesde VARCHAR(50),
IN p_fechaHasta VARCHAR(50),
IN p_opcion INT(1),
IN p_parametros VARCHAR(200)
)
BEGIN
IF forma =99 THEN	
	SELECT NombreLOC, FechaVEN, IdArticuloDVEN, DescripcionART, CantidadDVEN, 
	PrecioPublicoDVEN, DescripcionFOR
	FROM ventasdetallecons 
	WHERE IdLocalLOC = p_local AND FechaVEN>= p_fechaDesde AND FechaVEN< p_fechaHasta
	AND DescripcionART LIKE CONCAT('%',p_parametros,'%') AND IdArticuloDVEN LIKE CONCAT(p_parametros,'%');
ELSE
	SELECT NombreLOC, FechaVEN, IdArticuloDVEN, DescripcionART, CantidadDVEN, 
	PrecioPublicoDVEN, DescripcionFOR
	FROM ventasdetallecons 
	WHERE IdLocalLOC = p_local AND FechaVEN>= p_fechaDesde AND FechaVEN< p_fechaHasta
	AND DescripcionART LIKE CONCAT('%',p_parametros,'%') AND IdArticuloDVEN LIKE CONCAT(p_parametros,'%')
	AND IdFormaPagoFOR = p_forma;	
END IF;  
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasDetalle_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasDetalle_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasDetalle_Actualizar`(
in p_id_detalle int,
in p_articulo varchar(50),
in p_cantidad int,
IN p_publico DECIMAL,
IN p_costo DECIMAL,
IN p_mayor DECIMAL,
in p_forma_pago int,
in p_nro_cupon int,
in p_nro_factura int,
in p_id_empleado int,
in p_liquidado bit,
in p_devolucion bit
)
BEGIN
UPDATE ventasdetalle SET `IdArticuloDVEN` = p_articulo, `CantidadDVEN` = p_cantidad, 
`PrecioPublicoDVEN` = REPLACE(p_publico, ",","."), `PrecioCostoDVEN` = REPLACE(p_costo, ",","."), 
`PrecioMayorDVEN` = REPLACE(p_mayor, ",","."), `IdFormaPagoDVEN` = p_forma_pago, 
`NroCuponDVEN` = p_nro_cupon, `NroFacturaDVEN` = p_nro_factura, `IdEmpleadoDVEN` = p_id_empleado,
`LiquidadoDVEN` = p_liquidado, `DevolucionDVEN` = p_devolucion
WHERE `IdDVEN` = p_id_detalle
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasDetalle_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasDetalle_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasDetalle_Borrar`(IN p_id_detalle INT)
BEGIN
DELETE
FROM ventasdetalle
WHERE `IdDVEN`= p_id_detalle
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasDetalle_Cons` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasDetalle_Cons` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasDetalle_Cons`(
IN p_local INT(2),
IN p_forma INT(2),
IN p_fechaDesde VARCHAR(50),
IN p_fechaHasta VARCHAR(50),
IN p_parametros VARCHAR(200)
)
BEGIN
IF p_forma =99 THEN	
	SELECT NombreLOC, FechaVEN, IdArticuloDVEN, DescripcionART, CantidadDVEN, 
	PrecioPublicoDVEN, DescripcionFOR
	FROM ventasdetallecons 
	WHERE (IdLocalLOC = p_local AND FechaVEN>= p_fechaDesde AND FechaVEN< p_fechaHasta)
	AND (DescripcionART LIKE CONCAT('%',p_parametros,'%') OR IdArticuloDVEN LIKE CONCAT(p_parametros,'%'));
ELSE
	SELECT NombreLOC, FechaVEN, IdArticuloDVEN, DescripcionART, CantidadDVEN, 
	PrecioPublicoDVEN, DescripcionFOR
	FROM ventasdetallecons 
	WHERE (IdLocalLOC = p_local AND FechaVEN>= p_fechaDesde AND FechaVEN< p_fechaHasta AND IdFormaPagoFOR = p_forma)
	AND (DescripcionART LIKE CONCAT('%',p_parametros,'%') OR IdArticuloDVEN LIKE CONCAT(p_parametros,'%'))
	;	
END IF;  
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasDetalle_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasDetalle_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasDetalle_Insertar`(
IN p_id_detalle INT,
IN p_id_venta INT,
IN p_id_local INT,
IN p_articulo VARCHAR(50),
IN p_cantidad INT,
IN p_publico DECIMAL,
IN p_costo decimal,
IN p_mayor DECIMAL,
IN p_forma_pago INT,
IN p_nro_cupon INT,
IN p_nro_factura INT,
IN p_id_empleado INT,
IN p_liquidado BIT,
IN p_devolucion BIT
)
BEGIN
INSERT INTO ventasdetalle(`IdDVEN`, `IdVentaDVEN`, `IdLocalDVEN`, `IdArticuloDVEN`, `CantidadDVEN`, `PrecioPublicoDVEN`,
`PrecioCostoDVEN`, `PrecioMayorDVEN`, `IdFormaPagoDVEN`, `NroCuponDVEN`, `NroFacturaDVEN`, 
`IdEmpleadoDVEN`, `LiquidadoDVEN`, `DevolucionDVEN`)
VALUES(p_id_detalle, p_id_venta, p_id_local, p_articulo, p_cantidad, REPLACE(p_publico, ",","."), REPLACE(p_costo, ",","."),
REPLACE(p_mayor, ",","."), p_forma_pago, p_nro_cupon, p_nro_factura, p_id_empleado, p_liquidado, p_devolucion)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasDetalle_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasDetalle_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasDetalle_Listar`()
BEGIN
select *
FROM ventasdetalle
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasHistoricas_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasHistoricas_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasHistoricas_Insertar`(
IN p_fecha date,
IN p_nombre varchar(50),
IN p_forma varchar(50),
IN p_total_publico decimal,
IN p_total_costo decimal,
IN p_prendas INT
)
BEGIN
INSERT INTO ventash(`Fecha`, `NombreLocal`, `FormaPago`, `TotalPublico`, `TotalCosto`, `Prendas`)
VALUES(p_fecha, p_nombre, p_forma, p_total_publico, p_total_costo, p_prendas)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasH_Mantener` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasH_Mantener` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasH_Mantener`()
BEGIN
  DECLARE v_Fecha DATE;
  DECLARE v_NombreLOC, v_DescripcionFOR VARCHAR(255);
  DECLARE v_PublicoDiarias, v_CostoDiarias, v_PrendasDiarias INT;  
  DECLARE done INT DEFAULT FALSE;
  DECLARE cur1 CURSOR FOR SELECT Fecha, NombreLOC, DescripcionFOR, PublicoDiarias, CostoDiarias, PrendasDiarias
  FROM `ventasdiariasdifhistoricas`;
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
  OPEN cur1;
 
  
  the_loop: LOOP
    FETCH cur1 INTO v_Fecha, v_NombreLOC, v_DescripcionFOR, v_PublicoDiarias, v_CostoDiarias, v_PrendasDiarias;
    IF done THEN
      LEAVE the_loop;
    END IF;
    
    UPDATE ventash SET `TotalPublico` = v_PublicoDiarias, `TotalCosto` = v_CostoDiarias, `Prendas` = v_PrendasDiarias
    WHERE `Fecha`= v_Fecha AND `NombreLocal`= v_NombreLOC AND `FormaPago`= v_DescripcionFOR;
  END LOOP the_loop; 
  CLOSE cur1;
  
  INSERT INTO ventash(`Fecha`,`NombreLocal`,`FormaPago`,`TotalPublico`,`TotalCosto`,`Prendas`)
			(SELECT
				  `ventasdiariascons`.`Fecha`
				, `ventasdiariascons`.`NombreLOC`
				, `ventasdiariascons`.`DescripcionFOR`
				, `ventasdiariascons`.`TotalPublico`
				, `ventasdiariascons`.`TotalCosto`
				, `ventasdiariascons`.`Prendas`
			FROM
				`ncsoftwa_re`.`ventasdiariascons`
				LEFT JOIN `ncsoftwa_re`.`ventash` 
					ON (`ventasdiariascons`.`Fecha` = `ventash`.`Fecha`) 
					AND (`ventasdiariascons`.`NombreLOC` = `ventash`.`NombreLocal`) 
					AND (`ventasdiariascons`.`DescripcionFOR` = `ventash`.`FormaPago`)
					WHERE `ventash`.`Fecha` IS NULL)
					;
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasPesosCons_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasPesosCons_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasPesosCons_Listar`(
in p_locales varchar(200),
in p_forma int(2),
in p_fechaDesde varchar(50),
IN p_fechaHasta VARCHAR(50),
IN p_genero VARCHAR(3)
)
BEGIN
  DECLARE locales VARCHAR(50);
  DECLARE forma INT(2);
  DECLARE fechaDesde VARCHAR(50);
  DECLARE fechaHasta VARCHAR(50);
  DECLARE genero VARCHAR(3);
  SET locales = p_locales;
  SET forma = p_forma;
  SET fechaDesde = p_fechaDesde;
  SET fechaHasta = p_fechaHasta;
  SET genero = p_genero;
drop table if exists ventaspesoscons;
CREATE TEMPORARY TABLE ventaspesoscons
AS SELECT
  `locales`.`IdLocalLOC`            AS `IdLocalLOC`,
  `locales`.`NombreLOC`             AS `NombreLOC`,
  `ventas`.`FechaVEN`               AS `FechaVEN`,
  `articulos`.`IdGeneroART`         AS `IdGeneroART`,
  `ventasdetalle`.`IdFormaPagoDVEN` AS `IdFormaPagoDVEN`,
  `ventasdetalle`.`CantidadDVEN`    AS `Prendas`,
  (`ventasdetalle`.`PrecioCostoDVEN` * `ventasdetalle`.`CantidadDVEN`) AS `Costo`,
  (`ventasdetalle`.`PrecioPublicoDVEN` * `ventasdetalle`.`CantidadDVEN`) AS `Venta`
FROM ((((`ventasdetalle`
      JOIN `articulos`
        ON ((`articulos`.`IdArticuloART` = `ventasdetalle`.`IdArticuloDVEN`)))
     JOIN `ventas`
       ON ((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`)))
    JOIN `pc`
      ON ((`ventas`.`IdPCVEN` = `pc`.`IdPC`)))
   JOIN `locales`
     ON ((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`)))  
;  
  
if forma =99 then
	IF genero ='' THEN
		SET @QUERY = CONCAT("SELECT 
		NombreLOC AS Local, SUM(Venta) AS Venta, SUM(Costo) AS Costo, SUM(Prendas) AS Prendas, 
		(SUM(Venta) - SUM(Costo)) AS 'Utilidad bruta', (((SUM(Venta)/SUM(Costo))-1)*100) AS 'Valor agregado'
		FROM ventaspesoscons 
		WHERE (",locales,") AND (FechaVEN >='",fechaDesde,"' AND FechaVEN <'",fechaHasta,"')
		GROUP BY NombreLOC");	
	else
		SET @QUERY = CONCAT("SELECT 
		NombreLOC AS Local, SUM(Venta) AS Venta, SUM(Costo) AS Costo, SUM(Prendas) AS Prendas, 
		(SUM(Venta) - SUM(Costo)) AS 'Utilidad bruta', (((SUM(Venta)/SUM(Costo))-1)*100) AS 'Valor agregado'
		FROM ventaspesoscons 
		WHERE (",locales,") AND (FechaVEN >='",fechaDesde,"' 
		AND FechaVEN <'",fechaHasta,"') AND IdGeneroART ='",genero,"' 
		GROUP BY NombreLOC");	
	END IF;
else
	IF genero ='' THEN
		SET @QUERY = CONCAT("SELECT 
		NombreLOC AS Local, SUM(Venta) AS Venta, SUM(Costo) AS Costo, SUM(Prendas) AS Prendas, 
		(SUM(Venta) - SUM(Costo)) AS 'Utilidad bruta', (((SUM(Venta)/SUM(Costo))-1)*100) AS 'Valor agregado'
		FROM ventaspesoscons 
		WHERE (",locales,") AND (FechaVEN >='",fechaDesde,"' AND FechaVEN <'",fechaHasta,"') AND IdFormaPagoDVEN =",p_forma,"
		GROUP BY NombreLOC");
	ELSE
		SET @QUERY = CONCAT("SELECT 
		NombreLOC AS Local, SUM(Venta) AS Venta, SUM(Costo) AS Costo, SUM(Prendas) AS Prendas, 
		(SUM(Venta) - SUM(Costo)) AS 'Utilidad bruta', (((SUM(Venta)/SUM(Costo))-1)*100) AS 'Valor agregado'
		FROM ventaspesoscons 
		WHERE (",locales,") AND (FechaVEN >='",fechaDesde,"' AND FechaVEN <'",fechaHasta,"') 
		AND IdFormaPagoDVEN =",p_forma," AND IdGeneroART ='",genero,"'
		GROUP BY NombreLOC");
	END IF;
END IF;
  
  PREPARE smpt FROM @QUERY;
  EXECUTE smpt;
  DEALLOCATE PREPARE smpt;
  
END */$$
DELIMITER ;

/* Procedure structure for procedure `VentasPesosCons_Listar_Prueba` */

/*!50003 DROP PROCEDURE IF EXISTS  `VentasPesosCons_Listar_Prueba` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `VentasPesosCons_Listar_Prueba`()
BEGIN
	SET @QUERY = "SELECT * FROM articulos";	
  
  PREPARE smpt FROM @QUERY;
  EXECUTE smpt;
  
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Actualizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Actualizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Actualizar`(
in p_id int,
IN p_id_pc INT,
IN p_fecha datetime,
in p_cliente int,
IN p_cupon VARCHAR(12)
)
BEGIN
update ventas set IdPCVEN = p_id_pc, FechaVEN = p_fecha, IdClienteVEN = p_cliente, NroCuponVEN = p_cupon
where `IdVentaVEN` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Arqueo` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Arqueo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Arqueo`(
in p_fecha_desde varchar(50),
IN p_fecha_hasta VARCHAR(50),
in p_pc int(11)
)
BEGIN
SELECT * FROM ventas
WHERE (`FechaVEN` >= p_fecha_desde AND `FechaVEN` < p_fecha_hasta) AND `IdPCVEN` = p_pc
;
SELECT * FROM ventasarqueocons
WHERE (`FechaVEN` >= p_fecha_desde AND `FechaVEN` < p_fecha_hasta) AND `IdPCVEN` = p_pc
;
SELECT `ImporteFONP` FROM fondocaja
WHERE `FechaFONP` = (SELECT MAX(`FechaFONP`) FROM fondocaja WHERE `FechaFONP` < p_fecha_desde)
AND `IdPcFONP` = p_pc
;
SELECT `ImporteFONP` FROM fondocaja
WHERE `FechaFONP` = p_fecha_desde
AND `IdPcFONP` = p_pc
;
SELECT * FROM `tesoreriamovimientos`
WHERE `FechaTESM` >= p_fecha_desde AND `FechaTESM` < p_fecha_hasta
AND `IdPcTESM` = p_pc
order by `FechaTESM`
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Arqueo2` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Arqueo2` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Arqueo2`(
IN p_fecha_desde VARCHAR(50),
IN p_fecha_hasta VARCHAR(50),
IN p_pc INT(11)
)
BEGIN
	SELECT IdVentaVEN, FechaVEN, IdPCVEN, IdClienteVEN
	FROM arqueocons2
	WHERE (`FechaVEN` >= p_fecha_desde AND `FechaVEN` < p_fecha_hasta) AND `IdPCVEN` = p_pc
	GROUP BY IdVentaVEN, FechaVEN, IdPCVEN, IdClienteVEN
	;
	SELECT * FROM arqueocons2
	WHERE (`FechaVEN` >= p_fecha_desde AND `FechaVEN` < p_fecha_hasta) AND `IdPCVEN` = p_pc
	;
	SELECT `ImporteFONP` FROM fondocaja
	WHERE `FechaFONP` = (SELECT MAX(`FechaFONP`) FROM fondocaja WHERE `FechaFONP` < p_fecha_desde)
	AND `IdPcFONP` = p_pc
	;
	SELECT `ImporteFONP` FROM fondocaja
	WHERE `FechaFONP` = p_fecha_desde
	AND `IdPcFONP` = p_pc
	;
	SELECT FechaTESM, IdMovTESM, DetalleTESM, ImporteTESM, IdPcTESM FROM `tesoreriamovimientos`
	WHERE `FechaTESM` >= p_fecha_desde AND `FechaTESM` < p_fecha_hasta
	AND `IdPcTESM` = p_pc
	ORDER BY FechaTESM
	;
	SELECT SUM(Subtotal) FROM arqueocons2
	WHERE (`FechaVEN` >= p_fecha_desde AND `FechaVEN` < p_fecha_hasta) AND `IdPCVEN` = p_pc AND `Forma pago` = 'EFECTIVO'
	;
	SELECT SUM(Subtotal) FROM arqueocons2
	WHERE (`FechaVEN` >= p_fecha_desde AND `FechaVEN` < p_fecha_hasta) AND `IdPCVEN` = p_pc AND `Forma pago` <> 'EFECTIVO'
	;
	SELECT SUM(ImporteTESM) FROM `tesoreriamovimientos`
	WHERE `FechaTESM` >= p_fecha_desde AND `FechaTESM` < p_fecha_hasta
	AND `IdPcTESM` = p_pc
	;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Borrar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Borrar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Borrar`(IN p_id INT)
BEGIN
DELETE
FROM ventas
WHERE `IdVentaVEN` = p_id
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Diarias` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Diarias` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Diarias`(
IN p_fecha_desde VARCHAR(50),
IN p_fecha_hasta VARCHAR(50),
IN p_local INT(11),
IN p_forma VARCHAR(50)
)
BEGIN
DROP TABLE IF EXISTS ventassubtotal;
IF p_forma = "TODAS" THEN
	CREATE TEMPORARY TABLE ventassubtotal
	AS SELECT
		LEFT(`ventas`.`FechaVEN`, 10) AS Fecha
		, `ventasdetalle`.`CantidadDVEN`
		, `ventasdetalle`.`PrecioPublicoDVEN`
		, `pc`.`IdLocalPC`
		, `formaspago`.`DescripcionFOR`
		, `ventasdetalle`.`CantidadDVEN` * `ventasdetalle`.`PrecioPublicoDVEN` AS Subtotal
	FROM
		`pc`
		INNER JOIN `locales` 
			ON (`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`)
		INNER JOIN `ventas` 
			ON (`ventas`.`IdPCVEN` = `pc`.`IdPC`)
		INNER JOIN `ventasdetalle` 
			ON (`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`)
		INNER JOIN `formaspago` 
			ON (`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`)
			WHERE `ventas`.`FechaVEN`>= p_fecha_desde AND `ventas`.`FechaVEN`< p_fecha_hasta
			AND IdLocalPC = p_local
			ORDER BY `ventas`.`FechaVEN`
			;
		SELECT DATE_FORMAT(Fecha,'%d-%m-%Y') AS Fecha, SUM(Subtotal) AS Total, SUM(CantidadDVEN) AS Unidades
		FROM ventassubtotal
		GROUP BY Fecha
		ORDER BY Fecha ASC	
		;		
ELSE
	CREATE TEMPORARY TABLE ventassubtotal
	AS SELECT
		LEFT(`ventas`.`FechaVEN`, 10) AS Fecha
		, `ventasdetalle`.`CantidadDVEN`
		, `ventasdetalle`.`PrecioPublicoDVEN`
		, `pc`.`IdLocalPC`
		, `formaspago`.`DescripcionFOR`
		, `ventasdetalle`.`CantidadDVEN` * `ventasdetalle`.`PrecioPublicoDVEN` AS Subtotal
	FROM
		`pc`
		INNER JOIN `locales` 
			ON (`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`)
		INNER JOIN `ventas` 
			ON (`ventas`.`IdPCVEN` = `pc`.`IdPC`)
		INNER JOIN `ventasdetalle` 
			ON (`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`)
		INNER JOIN `formaspago` 
			ON (`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`)
			WHERE `ventas`.`FechaVEN`>= p_fecha_desde AND `ventas`.`FechaVEN`< p_fecha_hasta
			AND IdLocalPC = p_local AND `formaspago`.`DescripcionFOR` = p_forma
			ORDER BY `ventas`.`FechaVEN`
			;	
		SELECT DATE_FORMAT(Fecha,'%d-%m-%Y') AS Fecha, SUM(Subtotal) AS Total, SUM(CantidadDVEN) AS Unidades, DescripcionFOR AS 'Forma pago'
		FROM ventassubtotal
		GROUP BY Fecha, DescripcionFOR
		ORDER BY Fecha ASC
		;				
END IF;			     
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Insertar`(
IN p_id INT,
IN p_id_pc INT,
IN p_fecha DATETIME,
IN p_cliente INT,
IN p_cupon varchar(12)
)
BEGIN
INSERT INTO ventas(`IdVentaVEN`, `IdPCVEN`, `FechaVEN`, `IdClienteVEN`,`NroCuponVEN`)
VALUES(p_id, p_id_pc, p_fecha, p_cliente,p_cupon)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Ventas_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Ventas_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Ventas_Listar`(
)
BEGIN
select * from ventas;
select * from ventasdetalle;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulo` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulo`(IN p_articulo VARCHAR(10))
BEGIN
	DECLARE articulo_left VARCHAR(10);
	SET articulo_left = LEFT(p_articulo,6);
	
	SELECT IdArticuloART, DescripcionWebART, PrecioPublicoART, ImagenART, ImagenBackART, ImagenColorART, HexCOL, DescripcionCOL, TalleART, CantidadSTK
	FROM `web_stockcons`
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'')) 
	ORDER BY DescripcionCOL ASC
	;
	SELECT ImagenART
	FROM web_stockcons
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'')) 
	group by ImagenART
	;
	SELECT ImagenBackART
	FROM web_stockcons
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'' AND ImagenBackART<>'')) 
	GROUP BY ImagenBackART
	;
	SELECT TalleART
	FROM web_stockcons
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'')) 
	GROUP BY TalleART
	;	
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulos` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulos` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulos`(
IN p_item VARCHAR(20), 
IN p_genero VARCHAR(20)
)
BEGIN 
IF p_item = "Lo_nuevo" THEN
	SELECT
	    LEFT(`articulos`.`IdArticuloART`,6)
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`ImagenART`
	    , SUM(`stock`.`CantidadSTK`) AS sumStock
	    ,`generos`.`DescripcionGEN`
	FROM
	    `ncsoftwa_re`.`articulos`
	    INNER JOIN `ncsoftwa_re`.`articulositems` 
		ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
	    INNER JOIN `ncsoftwa_re`.`stock` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
	WHERE ((`articulos`.`ActivoWebART` = 1)
		       AND (`stock`.`CantidadSTK` > 0)
		       AND (`articulos`.`NuevoART` = 1)
		       AND (`articulos`.`ImagenART` <> '')
		       AND (`generos`.`DescripcionGEN` = p_genero))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,6), `articulos`.`DescripcionWebART`, 
		`articulos`.`ImagenART`;
ELSE
	SELECT
	    LEFT(`articulos`.`IdArticuloART`,6)
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`ImagenART`
	    , SUM(`stock`.`CantidadSTK`) AS sumStock
	    ,`generos`.`DescripcionGEN`
	FROM
	    `ncsoftwa_re`.`articulos`
	    INNER JOIN `ncsoftwa_re`.`articulositems` 
		ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
	    INNER JOIN `ncsoftwa_re`.`stock` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
		WHERE ((`articulos`.`ActivoWebART` = 1)
		       AND (`stock`.`CantidadSTK` > 0)
		       AND (`articulos`.`ImagenART` <> '')
		       AND (`generos`.`DescripcionGEN` = p_genero)
		       AND (`articulositems`.`DescripcionWebITE` = p_item))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,6), `articulos`.`DescripcionWebART`, 
		`articulos`.`ImagenART`;
END IF;		
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulos_by_Item` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulos_by_Item` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulos_by_Item`(
IN p_idItem INT, 
IN p_idGenero INT
)
BEGIN 
IF p_idItem = 0 THEN
	SELECT
	    `articulos`.`IdArticuloART`
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`PrecioPublicoART`
	    , `articulos`.`ImagenART`
	    , `articulos`.`ActivoWebART`
	    , `articulos`.`NuevoART`
	    , SUM(`stock`.`CantidadSTK`)
	FROM
	    `ncsoftwa_re`.`stock`
	    INNER JOIN `ncsoftwa_re`.`articulos` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`articulositems` 
		ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
	WHERE ((`articulos`.`ActivoWebART` = 1)
	       AND (`stock`.`CantidadSTK` > 0)
	       AND (`articulos`.`NuevoART` = 1)
	       AND (`articulos`.`ImagenART` <> '')
	       AND (`generos`.`IdGeneroGEN` = p_idGenero))	       
	GROUP BY LEFT(`articulos`.`IdArticuloART`,7),`articulos`.`PrecioPublicoART`,
	`articulos`.`ImagenART`,`articulos`.`ActivoWebART`,`articulos`.`NuevoART`,`stock`.`IdLocalSTK`; 
ELSE
	SELECT
	    `articulos`.`IdArticuloART`
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`PrecioPublicoART`
	    , `articulos`.`ImagenART`
	    , `articulos`.`ActivoWebART`
	    , `articulos`.`NuevoART`
	    , SUM(`stock`.`CantidadSTK`)
	FROM
	    `ncsoftwa_re`.`stock`
	    INNER JOIN `ncsoftwa_re`.`articulos` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`articulositems` 
		ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
	WHERE ((`articulos`.`ActivoWebART` = 1)
	       AND (`stock`.`CantidadSTK` > 0)
	       AND (`articulos`.`ImagenART` <> '')
	       AND (`generos`.`IdGeneroGEN` = p_idGenero)
	       AND (`articulositems`.`IdItemITE` = p_idItem))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,7),`articulos`.`PrecioPublicoART`,
	`articulos`.`ImagenART`,`articulos`.`ActivoWebART`,`articulos`.`NuevoART`,`stock`.`IdLocalSTK`; 
END IF;		
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulos_by_Item2` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulos_by_Item2` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulos_by_Item2`(
IN p_idItem INT, 
IN p_idGenero INT
)
BEGIN 
IF p_idItem = 0 THEN
	SELECT
	    `articulos`.`IdArticuloART`
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`PrecioPublicoART`
	    , `articulos`.`ImagenART`
	    , `articulos`.`ActivoWebART`
	    , `articulos`.`NuevoART`
	    , SUM(`stock`.`CantidadSTK`)
	FROM
	    `ncsoftwa_re`.`stock`
	    INNER JOIN `ncsoftwa_re`.`articulos` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
	WHERE ((`articulos`.`ActivoWebART` = 1)
	       AND (`stock`.`CantidadSTK` > 0)
	       AND (`articulos`.`NuevoART` = 1)
	       AND (`articulos`.`ImagenART` <> '')
	       AND (`generos`.`IdGeneroGEN` = p_idGenero))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,7),`articulos`.`PrecioPublicoART`,
	`articulos`.`ImagenART`,`articulos`.`ActivoWebART`,`articulos`.`NuevoART`,`stock`.`IdLocalSTK`; 
ELSE
	SELECT
	    `articulos`.`IdArticuloART`
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`PrecioPublicoART`
	    , `articulos`.`ImagenART`
	    , `articulos`.`ActivoWebART`
	    , `articulos`.`NuevoART`
	    , SUM(`stock`.`CantidadSTK`)
	FROM
	    `ncsoftwa_re`.`stock`
	    INNER JOIN `ncsoftwa_re`.`articulos` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
	WHERE ((`articulos`.`ActivoWebART` = 1)
	       AND (`stock`.`CantidadSTK` > 0)
	       AND (`articulos`.`ImagenART` <> '')
	       AND (`generos`.`IdGeneroGEN` = p_idGenero))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,7),`articulos`.`PrecioPublicoART`,
	`articulos`.`ImagenART`,`articulos`.`ActivoWebART`,`articulos`.`NuevoART`,`stock`.`IdLocalSTK`; 
END IF;		
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulos_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulos_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulos_Listar`(
IN p_articulo INT,
IN p_local INT
)
BEGIN
SELECT COUNT(*) FROM Web_ArticulosCons 
WHERE (CONCAT("IdArticuloART LIKE '", p_articulo,"%'") AND IdLocalLOC = p_local)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulo_beta` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulo_beta` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulo_beta`(IN p_local INT, IN p_articulo VARCHAR(10))
BEGIN
	DECLARE articuloLeft VARCHAR(10);
	DECLARE tieneImagenColor INT(2);
	DECLARE tieneTalle VARCHAR(3);
	
	SET articuloLeft = LEFT(p_articulo,5);
	DROP TEMPORARY TABLE IF EXISTS ArticuloTemp;
	CREATE TEMPORARY TABLE `ArticuloTemp` (
	`IdArticuloART` VARCHAR(50) NOT NULL,
	`DescripcionWebART` VARCHAR(50) DEFAULT NULL,
	`PrecioPublicoART` DECIMAL(19,0) DEFAULT NULL,
	`ImagenART` VARCHAR(20) DEFAULT NULL,
	`ImagenBackART` VARCHAR(20) DEFAULT NULL,
	`ImagenColorART` VARCHAR(20) DEFAULT NULL,		
	`HexCOL` VARCHAR(20) DEFAULT NULL,		
	`DescripcionCOL` VARCHAR(50) DEFAULT NULL,	
	`TalleART` VARCHAR(2) DEFAULT NULL,	
	`CantidadSTK` INT(11) DEFAULT NULL
	);  	
	INSERT INTO ArticuloTemp (IdArticuloART, DescripcionWebART, PrecioPublicoART, ImagenART, ImagenBackART, ImagenColorART, HexCOL, 
			DescripcionCOL, TalleART, CantidadSTK) 
		SELECT IdArticuloART, DescripcionWebART, PrecioPublicoART, ImagenART, 
				ImagenBackART, ImagenColorART, HexCOL, DescripcionCOL, TalleART, CantidadSTK
		FROM Web_StockCons
		WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articuloLeft,'%') AND IdLocalSTK = p_local))
		ORDER BY DescripcionCOL ASC;	
	SET tieneImagenColor = (SELECT COUNT(*) FROM ArticuloTemp WHERE ImagenColorART IS NOT NULL);
	IF tieneImagenColor = 0 THEN
		SELECT * FROM ArticuloTemp;
	ELSE
		SELECT * FROM ArticuloTemp WHERE ImagenColorART IS NOT NULL;
	END IF;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulo_beta_2` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulo_beta_2` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulo_beta_2`(IN p_local INT, IN p_articulo VARCHAR(10))
BEGIN
	DECLARE articuloLeft VARCHAR(10);
	DECLARE tieneImagenColor INT(2);
	DECLARE tieneTalle VARCHAR(3);
	
	SET articuloLeft = LEFT(p_articulo,5);
	DROP TEMPORARY TABLE IF EXISTS ArticuloTemp;
	CREATE TEMPORARY TABLE `ArticuloTemp` (	
	`ImagenART` VARCHAR(20) DEFAULT NULL,
	`ImagenBackART` VARCHAR(20) DEFAULT NULL
	);  	
	INSERT INTO ArticuloTemp (ImagenART, ImagenBackART) 
		SELECT ImagenART, ImagenBackART
		FROM Web_StockCons
		WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articuloLeft,'%') AND IdLocalSTK = p_local))
		group BY ImagenART, ImagenBackART;
		ALTER IGNORE TABLE ArticuloTemp ADD UNIQUE INDEX(ImagenART, ImagenBackART);		
		select * from ArticuloTemp;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulo_Maximizar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulo_Maximizar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulo_Maximizar`(IN p_articulo VARCHAR(10))
BEGIN
	DECLARE articulo_left VARCHAR(10);
	SET articulo_left = LEFT(p_articulo,6);
	
	SELECT ImagenART
	FROM `web_stockcons`
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'')) 
	group by ImagenART
	;
	SELECT ImagenBackART
	FROM web_stockcons
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'' AND ImagenBackART<>'')) 
	GROUP BY ImagenBackART
	;
	SELECT DescripcionWebART
	FROM web_stockcons
	WHERE ((CantidadSTK>0 AND IdArticuloART LIKE CONCAT(articulo_left,'%') AND ImagenART<>'')) 
	GROUP BY DescripcionWebART
	;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Articulo_prueba` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Articulo_prueba` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Articulo_prueba`()
BEGIN
	
	SELECT *
	FROM `generos`
	;	
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Clientes_GetByCorreo` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Clientes_GetByCorreo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Clientes_GetByCorreo`(
IN p_correo VARCHAR(50)
)
BEGIN
SELECT CorreoCLI FROM clientes WHERE CorreoCLI LIKE p_correo
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Clientes_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Clientes_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Clientes_Insertar`(
IN p_id INT(11),
IN p_nombre VARCHAR(50),
IN p_apellido VARCHAR(50),
IN p_correo VARCHAR(50)
)
BEGIN
INSERT INTO clientes(IdClienteCLI, NombreCLI, ApellidoCLI, CorreoCLI)
VALUES(p_id, p_nombre, p_apellido, p_correo)
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Clientes_UnsuscribeNews` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Clientes_UnsuscribeNews` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Clientes_UnsuscribeNews`(
IN p_correo VARCHAR(50)
)
BEGIN
UPDATE `clientes`
SET `RecibeNewsCLI` = 0
WHERE `CorreoCLI` = p_correo
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Contador_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Contador_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Contador_Insertar`(
in p_ip VARCHAR(50)
)
BEGIN
INSERT INTO `ncsoftwa_re`.`contador`
            (`ip`,
             `fecha`)
VALUES (p_ip,
        now());
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Contador_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Contador_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Contador_Listar`(p_ip VARCHAR(30))
BEGIN
DECLARE maxTime DATETIME;
SET maxTime = (SELECT MAX(`fecha`) FROM contador);
SELECT TIMEDIFF(NOW(), fecha) AS diferencia FROM contador
WHERE ip = p_ip AND fecha = maxTime;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Cupon_Buscar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Cupon_Buscar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Cupon_Buscar`(
IN p_correo VARCHAR(50),
IN p_fecha date
)
BEGIN
	select * from cupones
	where `Mail` like p_correo and `FechaVencimiento` = p_fecha and `Utilizado` = 0;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Cupon_Config_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Cupon_Config_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Cupon_Config_Listar`()
BEGIN
SELECT * from cupones_config;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Cupon_Insertar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Cupon_Insertar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Cupon_Insertar`(
IN p_nro int(11),
IN p_correo VARCHAR(50),
IN p_porcentaje varchar(2),
IN p_fecha date
)
BEGIN
insert into cupones(`Nro_cupon`,`Mail`,`Porcentaje`,`FechaVencimiento`) 
			values(p_nro,p_correo,p_porcentaje,p_fecha);
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Generos_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Generos_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Generos_Listar`()
BEGIN
SELECT * FROM generos where ActivoWebGEN = 1
ORDER BY `DescripcionGEN` ASC
;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Items_Listar` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Items_Listar` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Items_Listar`(p_genero VARCHAR(20))
BEGIN
SELECT
    `articulositems`.`IdItemITE`
    , `articulositems`.`DescripcionWebITE`
    , articulos.`IdGeneroART`
FROM
    `ncsoftwa_re`.`articulos`
    INNER JOIN `ncsoftwa_re`.`articulositems` 
        ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
    INNER JOIN `ncsoftwa_re`.`stock` 
        ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
    INNER JOIN `ncsoftwa_re`.`generos` 
        ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
WHERE `stock`.`CantidadSTK` > 0 AND `articulositems`.`DescripcionWebITE` NOT LIKE ''
	 AND articulos.`ImagenART` <>'' AND generos.`DescripcionGEN` = p_genero
GROUP BY `articulositems`.`IdItemITE`, `articulositems`.`DescripcionWebITE`, articulos.`IdGeneroART`
ORDER BY DescripcionWebITE;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Items_y_Articulos` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Items_y_Articulos` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Items_y_Articulos`(
IN p_idItem INT, 
IN p_idGenero INT
)
BEGIN 
IF p_idItem = 0 THEN
	SELECT
	    LEFT(`articulos`.`IdArticuloART`,6)
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`PrecioPublicoART`
	    , `articulos`.`ImagenART`
	    , SUM(`stock`.`CantidadSTK`) AS sumStock
	FROM
	    `ncsoftwa_re`.`articulos`
	    INNER JOIN `ncsoftwa_re`.`articulositems` 
		ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
	    INNER JOIN `ncsoftwa_re`.`stock` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
	WHERE ((`articulos`.`ActivoWebART` = 1)
		       AND (`stock`.`CantidadSTK` > 0)
		       AND (`articulos`.`NuevoART` = 1)
		       AND (`articulos`.`ImagenART` <> '')
		       AND (`generos`.`IdGeneroGEN` = p_idGenero))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,6), `articulos`.`DescripcionWebART`, 
		`articulos`.`PrecioPublicoART`, `articulos`.`ImagenART`;
ELSE
	SELECT
	    LEFT(`articulos`.`IdArticuloART`,6)
	    , `articulos`.`DescripcionWebART`
	    , `articulos`.`PrecioPublicoART`
	    , `articulos`.`ImagenART`
	    , SUM(`stock`.`CantidadSTK`) AS sumStock
	FROM
	    `ncsoftwa_re`.`articulos`
	    INNER JOIN `ncsoftwa_re`.`articulositems` 
		ON (`articulos`.`IdItemART` = `articulositems`.`IdItemITE`)
	    INNER JOIN `ncsoftwa_re`.`stock` 
		ON (`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`)
	    INNER JOIN `ncsoftwa_re`.`generos` 
		ON (`articulos`.`IdGeneroART` = `generos`.`IdGeneroGEN`)
		WHERE ((`articulos`.`ActivoWebART` = 1)
		       AND (`stock`.`CantidadSTK` > 0)
		       AND (`articulos`.`ImagenART` <> '')
		       AND (`generos`.`IdGeneroGEN` = p_idGenero)
		       AND (`articulositems`.`IdItemITE` = p_idItem))
	GROUP BY LEFT(`articulos`.`IdArticuloART`,6), `articulos`.`DescripcionWebART`, 
		`articulos`.`PrecioPublicoART`, `articulos`.`ImagenART`;
END IF;		
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Lo_Nuevo` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Lo_Nuevo` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Lo_Nuevo`()
BEGIN
  SELECT * FROM Web_LoNuevoCons;
END */$$
DELIMITER ;

/* Procedure structure for procedure `Web_Lo_Nuevo_Copia` */

/*!50003 DROP PROCEDURE IF EXISTS  `Web_Lo_Nuevo_Copia` */;

DELIMITER $$

/*!50003 CREATE DEFINER=`ncsoftwa_re`@`%` PROCEDURE `Web_Lo_Nuevo_Copia`(IN p_local INT)
BEGIN
  DECLARE done INT DEFAULT 0;
  DECLARE codigo_articulo VARCHAR(16);
  DECLARE codigo_5 VARCHAR(5);
   DECLARE codigo_7 VARCHAR(7);
  DECLARE imagen_color VARCHAR(16);
  DECLARE erroneos INT DEFAULT 0;
  DECLARE tiene_stock INT DEFAULT 0;
  DECLARE cur1 CURSOR FOR SELECT IdArticuloART, ImagenColorART FROM Articulos WHERE ImagenART<>'' AND NuevoART = 1 AND ActivoWebART = 1;
  DECLARE CONTINUE HANDLER FOR SQLSTATE '02000' SET done = 1;
  
  DROP TEMPORARY TABLE IF EXISTS ArticulosTemp;
  CREATE TEMPORARY TABLE `ArticulosTemp` (
    `IdArticuloART` VARCHAR(50) NOT NULL,
    `DescripcionART` VARCHAR(50) DEFAULT NULL,
    `DescripcionWebART` VARCHAR(50) DEFAULT NULL,
    `PrecioPublicoART` DECIMAL(19,0) DEFAULT NULL,
    `ImagenART` VARCHAR(20) DEFAULT NULL,
	`IdLocalSTK` INT(2) DEFAULT NULL    
  );  
  
  OPEN cur1;
  REPEAT
    FETCH cur1 INTO codigo_articulo, imagen_color;
    IF NOT done THEN
	IF imagen_color<>'' THEN
		SET codigo_7 = LEFT(codigo_articulo,7);
		SET tiene_stock=(SELECT COUNT(IdArticuloART) FROM Web_StockCons
							WHERE (IdArticuloART LIKE CONCAT(codigo_7,'%')
							AND CantidadSTK>0 AND IdLocalSTK = p_local));
		IF tiene_stock>0 THEN	
			INSERT INTO ArticulosTemp (IdArticuloART, DescripcionART, DescripcionWebART, PrecioPublicoART, ImagenART, IdLocalSTK ) 
			SELECT IdArticuloART, DescripcionART, DescripcionWebART, PrecioPublicoART, ImagenART, IdLocalSTK 
			FROM Web_StockCons
			WHERE IdArticuloART LIKE codigo_articulo AND IdLocalSTK = p_local AND ActivoWebITE = 1;
		END IF;
	ELSE
		SET codigo_5 = LEFT(codigo_articulo,5);
		SET erroneos=(SELECT COUNT(IdArticuloART) FROM Articulos
			WHERE IdArticuloART LIKE CONCAT(codigo_5,'%' AND ImagenColorART<>''));
		IF erroneos=0 THEN
			SET tiene_stock=(SELECT COUNT(IdArticuloART) FROM Web_StockCons
								WHERE (IdArticuloART LIKE CONCAT(codigo_5,'%')
								AND ImagenColorART ='' AND CantidadSTK>0
								AND IdLocalSTK = p_local));
			IF tiene_stock>0 THEN		
				INSERT INTO ArticulosTemp (IdArticuloART, DescripcionART, DescripcionWebART, PrecioPublicoART, ImagenART, IdLocalSTK ) 
				SELECT IdArticuloART, DescripcionART, DescripcionWebART, PrecioPublicoART, ImagenART, IdLocalSTK 
				FROM Web_StockCons
				WHERE IdArticuloART LIKE codigo_articulo AND IdLocalSTK = p_local  AND ActivoWebITE = 1;
			END IF;		
		END IF;
	END IF;
    END IF;
  UNTIL done END REPEAT;
  CLOSE cur1;
  SELECT * FROM ArticulosTemp;
END */$$
DELIMITER ;

/*Table structure for table `arqueocons` */

DROP TABLE IF EXISTS `arqueocons`;

/*!50001 DROP VIEW IF EXISTS `arqueocons` */;
/*!50001 DROP TABLE IF EXISTS `arqueocons` */;

/*!50001 CREATE TABLE  `arqueocons`(
 `IdVentaVEN` int(11) ,
 `IdPCVEN` int(11) ,
 `FechaVEN` datetime ,
 `IdClienteVEN` int(11) ,
 `IdDVEN` int(11) ,
 `IdVentaDVEN` int(11) ,
 `IdArticuloDVEN` varchar(50) ,
 `DescripcionDVEN` varchar(50) ,
 `CantidadDVEN` int(11) ,
 `PrecioPublicoDVEN` decimal(19,2) ,
 `PrecioCostoDVEN` decimal(19,2) ,
 `PrecioMayorDVEN` decimal(19,2) ,
 `IdFormaPagoDVEN` int(11) ,
 `NroCuponDVEN` int(11) ,
 `NroFacturaDVEN` int(11) ,
 `IdEmpleadoDVEN` int(11) ,
 `LiquidadoDVEN` bit(1) ,
 `EsperaDVEN` bit(1) ,
 `DevolucionDVEN` smallint(1) ,
 `DescripcionART` tinytext ,
 `DescripcionFOR` varchar(50) 
)*/;

/*Table structure for table `arqueocons2` */

DROP TABLE IF EXISTS `arqueocons2`;

/*!50001 DROP VIEW IF EXISTS `arqueocons2` */;
/*!50001 DROP TABLE IF EXISTS `arqueocons2` */;

/*!50001 CREATE TABLE  `arqueocons2`(
 `IdVentaVEN` int(11) ,
 `FechaVEN` datetime ,
 `IdPCVEN` int(11) ,
 `IdClienteVEN` int(11) ,
 `IdDVEN` int(11) ,
 `IdVentaDVEN` int(11) ,
 `IdLocalDVEN` int(3) ,
 `IdArticuloDVEN` varchar(50) ,
 `Descripcion` tinytext ,
 `CantidadDVEN` int(11) ,
 `PrecioPublicoDVEN` decimal(19,2) ,
 `PrecioCostoDVEN` decimal(19,2) ,
 `PrecioMayorDVEN` decimal(19,2) ,
 `IdFormaPagoDVEN` int(11) ,
 `NroCuponDVEN` int(11) ,
 `NroFacturaDVEN` int(11) ,
 `IdEmpleadoDVEN` int(11) ,
 `LiquidadoDVEN` bit(1) ,
 `EsperaDVEN` bit(1) ,
 `DevolucionDVEN` smallint(1) ,
 `Forma pago` varchar(50) ,
 `Subtotal` decimal(29,2) ,
 `OrdenarDVEN` int(11) 
)*/;

/*Table structure for table `articuloscons` */

DROP TABLE IF EXISTS `articuloscons`;

/*!50001 DROP VIEW IF EXISTS `articuloscons` */;
/*!50001 DROP TABLE IF EXISTS `articuloscons` */;

/*!50001 CREATE TABLE  `articuloscons`(
 `IdArticuloART` char(10) ,
 `IdItemART` smallint(3) ,
 `IdGeneroART` char(3) ,
 `IdColorART` tinyint(3) ,
 `TalleART` char(2) ,
 `IdProveedorART` smallint(4) ,
 `DescripcionART` tinytext ,
 `DescripcionWebART` tinytext ,
 `PrecioCostoART` varchar(20) ,
 `PrecioPublicoART` varchar(20) ,
 `PrecioMayorART` varchar(20) ,
 `FechaART` datetime ,
 `ImagenART` varchar(20) ,
 `ImagenBackART` varchar(20) ,
 `ImagenColorART` varchar(20) ,
 `ActivoWebART` tinyint(1) ,
 `NuevoART` varchar(1) ,
 `RazonSocialPRO` varchar(50) ,
 `IdAliculotaIvaART` tinyint(2) 
)*/;

/*Table structure for table `empleados_liquidacion_cons` */

DROP TABLE IF EXISTS `empleados_liquidacion_cons`;

/*!50001 DROP VIEW IF EXISTS `empleados_liquidacion_cons` */;
/*!50001 DROP TABLE IF EXISTS `empleados_liquidacion_cons` */;

/*!50001 CREATE TABLE  `empleados_liquidacion_cons`(
 `FechaEMOV` date ,
 `IdEmpleadoEMOV` int(11) ,
 `ApellidoEMP` varchar(25) ,
 `NombreEMP` varchar(50) ,
 `SalarioEMP` double ,
 `CantidadEMOV` tinyint(3) ,
 `DetalleEMOV` varchar(50) ,
 `ImporteEMOV` double ,
 `RemuneracionETIP` tinyint(1) ,
 `LiquidadoEMOV` tinyint(1) 
)*/;

/*Table structure for table `empleados_mov_cons` */

DROP TABLE IF EXISTS `empleados_mov_cons`;

/*!50001 DROP VIEW IF EXISTS `empleados_mov_cons` */;
/*!50001 DROP TABLE IF EXISTS `empleados_mov_cons` */;

/*!50001 CREATE TABLE  `empleados_mov_cons`(
 `IdMovEMOV` int(11) ,
 `FechaEMOV` date ,
 `IdEmpleadoEMOV` int(11) ,
 `NombreEMP` varchar(50) ,
 `ApellidoEMP` varchar(25) ,
 `IdMovTipoEMOV` int(11) ,
 `CantidadEMOV` tinyint(3) ,
 `DetalleEMOV` varchar(50) ,
 `ImporteEMOV` double ,
 `LiquidadoEMOV` tinyint(1) 
)*/;

/*Table structure for table `fondocajacons` */

DROP TABLE IF EXISTS `fondocajacons`;

/*!50001 DROP VIEW IF EXISTS `fondocajacons` */;
/*!50001 DROP TABLE IF EXISTS `fondocajacons` */;

/*!50001 CREATE TABLE  `fondocajacons`(
 `IdFondoFONP` int(11) ,
 `FechaFONP` date ,
 `IdPcFONP` int(11) ,
 `IdLocalLOC` int(11) ,
 `NombreLOC` varchar(50) ,
 `Detalle` varchar(50) ,
 `ImporteFONP` double 
)*/;

/*Table structure for table `localescons` */

DROP TABLE IF EXISTS `localescons`;

/*!50001 DROP VIEW IF EXISTS `localescons` */;
/*!50001 DROP TABLE IF EXISTS `localescons` */;

/*!50001 CREATE TABLE  `localescons`(
 `IdLocalLOC` int(11) 
)*/;

/*Table structure for table `pedidostockcons` */

DROP TABLE IF EXISTS `pedidostockcons`;

/*!50001 DROP VIEW IF EXISTS `pedidostockcons` */;
/*!50001 DROP TABLE IF EXISTS `pedidostockcons` */;

/*!50001 CREATE TABLE  `pedidostockcons`(
 `RazonSocialPRO` varchar(50) ,
 `IdArticuloSTK` varchar(50) ,
 `DescripcionART` tinytext ,
 `Costo` decimal(19,2) unsigned ,
 `Publico` decimal(19,2) unsigned ,
 `IdGeneroART` char(3) ,
 `Stock` decimal(32,0) 
)*/;

/*Table structure for table `stockmovcons` */

DROP TABLE IF EXISTS `stockmovcons`;

/*!50001 DROP VIEW IF EXISTS `stockmovcons` */;
/*!50001 DROP TABLE IF EXISTS `stockmovcons` */;

/*!50001 CREATE TABLE  `stockmovcons`(
 `FechaMSTK` datetime ,
 `IdMovMSTK` int(11) ,
 `OrigenMSTK` int(11) ,
 `DestinoMSTK` int(11) ,
 `CompensaMSTK` tinyint(1) ,
 `ordenar` int(11) ,
 `IdMSTKD` int(11) ,
 `IdMovMSTKD` int(11) ,
 `IdArticuloMSTKD` varchar(50) ,
 `DescripcionART` tinytext ,
 `CantidadMSTKD` int(11) ,
 `CompensaMSTKD` tinyint(1) ,
 `OrigenMSTKD` tinyint(2) ,
 `DestinoMSTKD` tinyint(2) 
)*/;

/*Table structure for table `ventasarqueocons` */

DROP TABLE IF EXISTS `ventasarqueocons`;

/*!50001 DROP VIEW IF EXISTS `ventasarqueocons` */;
/*!50001 DROP TABLE IF EXISTS `ventasarqueocons` */;

/*!50001 CREATE TABLE  `ventasarqueocons`(
 `IdPCVEN` int(11) ,
 `FechaVEN` datetime ,
 `IdDVEN` int(11) ,
 `IdVentaDVEN` int(11) ,
 `IdArticuloDVEN` varchar(50) ,
 `DescripcionDVEN` varchar(50) ,
 `CantidadDVEN` int(11) ,
 `PrecioPublicoDVEN` decimal(19,2) ,
 `PrecioCostoDVEN` decimal(19,2) ,
 `PrecioMayorDVEN` decimal(19,2) ,
 `IdFormaPagoDVEN` int(11) ,
 `NroCuponDVEN` int(11) ,
 `NroFacturaDVEN` int(11) ,
 `IdEmpleadoDVEN` int(11) ,
 `LiquidadoDVEN` bit(1) ,
 `EsperaDVEN` bit(1) ,
 `DevolucionDVEN` smallint(1) ,
 `IdLocalDVEN` int(3) 
)*/;

/*Table structure for table `ventasdetallecons` */

DROP TABLE IF EXISTS `ventasdetallecons`;

/*!50001 DROP VIEW IF EXISTS `ventasdetallecons` */;
/*!50001 DROP TABLE IF EXISTS `ventasdetallecons` */;

/*!50001 CREATE TABLE  `ventasdetallecons`(
 `IdLocalLOC` int(11) ,
 `NombreLOC` varchar(50) ,
 `FechaVEN` datetime ,
 `IdDVEN` int(11) ,
 `IdArticuloDVEN` varchar(50) ,
 `DescripcionART` tinytext ,
 `CantidadDVEN` int(11) ,
 `PrecioPublicoDVEN` decimal(19,2) ,
 `IdFormaPagoFOR` int(11) ,
 `DescripcionFOR` varchar(50) 
)*/;

/*Table structure for table `ventasdetallepedidocons` */

DROP TABLE IF EXISTS `ventasdetallepedidocons`;

/*!50001 DROP VIEW IF EXISTS `ventasdetallepedidocons` */;
/*!50001 DROP TABLE IF EXISTS `ventasdetallepedidocons` */;

/*!50001 CREATE TABLE  `ventasdetallepedidocons`(
 `IdVentaDVEN` int(11) ,
 `IdArticuloDVEN` varchar(50) ,
 `CantidadDVEN` int(11) ,
 `IdGeneroART` char(3) 
)*/;

/*Table structure for table `ventasdiariascons` */

DROP TABLE IF EXISTS `ventasdiariascons`;

/*!50001 DROP VIEW IF EXISTS `ventasdiariascons` */;
/*!50001 DROP TABLE IF EXISTS `ventasdiariascons` */;

/*!50001 CREATE TABLE  `ventasdiariascons`(
 `Fecha` varchar(10) ,
 `NombreLOC` varchar(50) ,
 `DescripcionFOR` varchar(50) ,
 `TotalPublico` decimal(51,2) ,
 `TotalCosto` decimal(51,2) ,
 `Prendas` decimal(32,0) 
)*/;

/*Table structure for table `ventasdiariasdifhistoricas` */

DROP TABLE IF EXISTS `ventasdiariasdifhistoricas`;

/*!50001 DROP VIEW IF EXISTS `ventasdiariasdifhistoricas` */;
/*!50001 DROP TABLE IF EXISTS `ventasdiariasdifhistoricas` */;

/*!50001 CREATE TABLE  `ventasdiariasdifhistoricas`(
 `Fecha` varchar(10) ,
 `NombreLOC` varchar(50) ,
 `DescripcionFOR` varchar(50) ,
 `PublicoDiarias` decimal(51,2) ,
 `CostoDiarias` decimal(51,2) ,
 `PrendasDiarias` decimal(32,0) 
)*/;

/*Table structure for table `ventaspesoscons2` */

DROP TABLE IF EXISTS `ventaspesoscons2`;

/*!50001 DROP VIEW IF EXISTS `ventaspesoscons2` */;
/*!50001 DROP TABLE IF EXISTS `ventaspesoscons2` */;

/*!50001 CREATE TABLE  `ventaspesoscons2`(
 `IdLocalLOC` int(11) ,
 `NombreLOC` varchar(50) ,
 `FechaVEN` datetime ,
 `CantidadDVEN` int(11) ,
 `PrecioPublicoDVEN` decimal(19,2) ,
 `PrecioCostoDVEN` decimal(19,2) ,
 `IdFormaPagoDVEN` int(11) 
)*/;

/*Table structure for table `ventaspesosconscopia` */

DROP TABLE IF EXISTS `ventaspesosconscopia`;

/*!50001 DROP VIEW IF EXISTS `ventaspesosconscopia` */;
/*!50001 DROP TABLE IF EXISTS `ventaspesosconscopia` */;

/*!50001 CREATE TABLE  `ventaspesosconscopia`(
 `IdLocalLOC` int(11) ,
 `NombreLOC` varchar(50) ,
 `FechaVEN` datetime ,
 `IdFormaPagoFOR` int(11) ,
 `Prendas` int(11) ,
 `Costo` decimal(29,2) ,
 `Venta` decimal(29,2) 
)*/;

/*Table structure for table `web_articulosbyitemscons` */

DROP TABLE IF EXISTS `web_articulosbyitemscons`;

/*!50001 DROP VIEW IF EXISTS `web_articulosbyitemscons` */;
/*!50001 DROP TABLE IF EXISTS `web_articulosbyitemscons` */;

/*!50001 CREATE TABLE  `web_articulosbyitemscons`(
 `IdArticuloART` varchar(7) ,
 `IdItemART` smallint(3) ,
 `DescripcionWebART` tinytext ,
 `PrecioPublicoART` decimal(19,2) unsigned ,
 `ImagenART` varchar(20) ,
 `Cantidad` decimal(32,0) ,
 `IdLocalSTK` int(11) 
)*/;

/*Table structure for table `web_articuloscons` */

DROP TABLE IF EXISTS `web_articuloscons`;

/*!50001 DROP VIEW IF EXISTS `web_articuloscons` */;
/*!50001 DROP TABLE IF EXISTS `web_articuloscons` */;

/*!50001 CREATE TABLE  `web_articuloscons`(
 `IdItemITE` smallint(3) ,
 `IdArticuloART` char(10) ,
 `DescripcionWebART` tinytext ,
 `PrecioPublicoART` decimal(19,2) unsigned ,
 `ImagenART` varchar(20) ,
 `ImagenBackART` varchar(20) ,
 `ImagenColorART` varchar(20) ,
 `IdLocalLOC` int(11) 
)*/;

/*Table structure for table `web_articulosnuevoscons` */

DROP TABLE IF EXISTS `web_articulosnuevoscons`;

/*!50001 DROP VIEW IF EXISTS `web_articulosnuevoscons` */;
/*!50001 DROP TABLE IF EXISTS `web_articulosnuevoscons` */;

/*!50001 CREATE TABLE  `web_articulosnuevoscons`(
 `IdArticuloART` char(10) ,
 `DescripcionWebART` tinytext ,
 `PrecioPublicoART` decimal(19,2) unsigned ,
 `ImagenART` varchar(20) ,
 `FechaART` datetime ,
 `IdLocalLOC` int(11) 
)*/;

/*Table structure for table `web_lonuevocons` */

DROP TABLE IF EXISTS `web_lonuevocons`;

/*!50001 DROP VIEW IF EXISTS `web_lonuevocons` */;
/*!50001 DROP TABLE IF EXISTS `web_lonuevocons` */;

/*!50001 CREATE TABLE  `web_lonuevocons`(
 `IdArticuloART` varchar(7) ,
 `DescripcionWebART` tinytext ,
 `PrecioPublicoART` decimal(19,2) unsigned ,
 `ImagenART` varchar(20) ,
 `ActivoWebART` tinyint(1) ,
 `NuevoART` varchar(1) ,
 `Cantidad` decimal(32,0) ,
 `IdLocalSTK` int(11) 
)*/;

/*Table structure for table `web_stockcons` */

DROP TABLE IF EXISTS `web_stockcons`;

/*!50001 DROP VIEW IF EXISTS `web_stockcons` */;
/*!50001 DROP TABLE IF EXISTS `web_stockcons` */;

/*!50001 CREATE TABLE  `web_stockcons`(
 `ActivoWebITE` tinyint(1) ,
 `IdItemITE` smallint(3) ,
 `IdArticuloART` char(10) ,
 `DescripcionART` tinytext ,
 `DescripcionWebART` tinytext ,
 `PrecioPublicoART` decimal(19,2) unsigned ,
 `ImagenART` varchar(20) ,
 `ImagenBackART` varchar(20) ,
 `ImagenColorART` varchar(20) ,
 `CantidadSTK` int(11) ,
 `IdLocalSTK` int(11) ,
 `HexCOL` varchar(20) ,
 `DescripcionCOL` varchar(50) ,
 `TalleART` char(2) 
)*/;

/*View structure for view arqueocons */

/*!50001 DROP TABLE IF EXISTS `arqueocons` */;
/*!50001 DROP VIEW IF EXISTS `arqueocons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `arqueocons` AS select `ventas`.`IdVentaVEN` AS `IdVentaVEN`,`ventas`.`IdPCVEN` AS `IdPCVEN`,`ventas`.`FechaVEN` AS `FechaVEN`,`ventas`.`IdClienteVEN` AS `IdClienteVEN`,`ventasdetalle`.`IdDVEN` AS `IdDVEN`,`ventasdetalle`.`IdVentaDVEN` AS `IdVentaDVEN`,`ventasdetalle`.`IdArticuloDVEN` AS `IdArticuloDVEN`,`ventasdetalle`.`DescripcionDVEN` AS `DescripcionDVEN`,`ventasdetalle`.`CantidadDVEN` AS `CantidadDVEN`,`ventasdetalle`.`PrecioPublicoDVEN` AS `PrecioPublicoDVEN`,`ventasdetalle`.`PrecioCostoDVEN` AS `PrecioCostoDVEN`,`ventasdetalle`.`PrecioMayorDVEN` AS `PrecioMayorDVEN`,`ventasdetalle`.`IdFormaPagoDVEN` AS `IdFormaPagoDVEN`,`ventasdetalle`.`NroCuponDVEN` AS `NroCuponDVEN`,`ventasdetalle`.`NroFacturaDVEN` AS `NroFacturaDVEN`,`ventasdetalle`.`IdEmpleadoDVEN` AS `IdEmpleadoDVEN`,`ventasdetalle`.`LiquidadoDVEN` AS `LiquidadoDVEN`,`ventasdetalle`.`EsperaDVEN` AS `EsperaDVEN`,`ventasdetalle`.`DevolucionDVEN` AS `DevolucionDVEN`,`articulos`.`DescripcionART` AS `DescripcionART`,`formaspago`.`DescripcionFOR` AS `DescripcionFOR` from (((`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) join `articulos` on((`ventasdetalle`.`IdArticuloDVEN` = `articulos`.`IdArticuloART`))) join `formaspago` on((`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`))) */;

/*View structure for view arqueocons2 */

/*!50001 DROP TABLE IF EXISTS `arqueocons2` */;
/*!50001 DROP VIEW IF EXISTS `arqueocons2` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `arqueocons2` AS select `ventas`.`IdVentaVEN` AS `IdVentaVEN`,`ventas`.`FechaVEN` AS `FechaVEN`,`ventas`.`IdPCVEN` AS `IdPCVEN`,`ventas`.`IdClienteVEN` AS `IdClienteVEN`,`ventasdetalle`.`IdDVEN` AS `IdDVEN`,`ventasdetalle`.`IdVentaDVEN` AS `IdVentaDVEN`,`ventasdetalle`.`IdLocalDVEN` AS `IdLocalDVEN`,`ventasdetalle`.`IdArticuloDVEN` AS `IdArticuloDVEN`,`articulos`.`DescripcionART` AS `Descripcion`,`ventasdetalle`.`CantidadDVEN` AS `CantidadDVEN`,`ventasdetalle`.`PrecioPublicoDVEN` AS `PrecioPublicoDVEN`,`ventasdetalle`.`PrecioCostoDVEN` AS `PrecioCostoDVEN`,`ventasdetalle`.`PrecioMayorDVEN` AS `PrecioMayorDVEN`,`ventasdetalle`.`IdFormaPagoDVEN` AS `IdFormaPagoDVEN`,`ventasdetalle`.`NroCuponDVEN` AS `NroCuponDVEN`,`ventasdetalle`.`NroFacturaDVEN` AS `NroFacturaDVEN`,`ventasdetalle`.`IdEmpleadoDVEN` AS `IdEmpleadoDVEN`,`ventasdetalle`.`LiquidadoDVEN` AS `LiquidadoDVEN`,`ventasdetalle`.`EsperaDVEN` AS `EsperaDVEN`,`ventasdetalle`.`DevolucionDVEN` AS `DevolucionDVEN`,`formaspago`.`DescripcionFOR` AS `Forma pago`,(`ventasdetalle`.`CantidadDVEN` * `ventasdetalle`.`PrecioPublicoDVEN`) AS `Subtotal`,`ventasdetalle`.`OrdenarDVEN` AS `OrdenarDVEN` from (((`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) join `articulos` on((`ventasdetalle`.`IdArticuloDVEN` = `articulos`.`IdArticuloART`))) join `formaspago` on((`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`))) order by `ventas`.`FechaVEN` desc,`ventasdetalle`.`OrdenarDVEN` */;

/*View structure for view articuloscons */

/*!50001 DROP TABLE IF EXISTS `articuloscons` */;
/*!50001 DROP VIEW IF EXISTS `articuloscons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `articuloscons` AS select `articulos`.`IdArticuloART` AS `IdArticuloART`,`articulos`.`IdItemART` AS `IdItemART`,`articulos`.`IdGeneroART` AS `IdGeneroART`,`articulos`.`IdColorART` AS `IdColorART`,`articulos`.`TalleART` AS `TalleART`,`articulos`.`IdProveedorART` AS `IdProveedorART`,`articulos`.`DescripcionART` AS `DescripcionART`,`articulos`.`DescripcionWebART` AS `DescripcionWebART`,replace(cast(`articulos`.`PrecioCostoART` as char charset utf8),'.',',') AS `PrecioCostoART`,replace(cast(`articulos`.`PrecioPublicoART` as char charset utf8),'.',',') AS `PrecioPublicoART`,replace(cast(`articulos`.`PrecioMayorART` as char charset utf8),'.',',') AS `PrecioMayorART`,`articulos`.`FechaART` AS `FechaART`,`articulos`.`ImagenART` AS `ImagenART`,`articulos`.`ImagenBackART` AS `ImagenBackART`,`articulos`.`ImagenColorART` AS `ImagenColorART`,`articulos`.`ActivoWebART` AS `ActivoWebART`,`articulos`.`NuevoART` AS `NuevoART`,`proveedores`.`RazonSocialPRO` AS `RazonSocialPRO`,`articulos`.`IdAliculotaIvaART` AS `IdAliculotaIvaART` from (`articulos` join `proveedores` on((`articulos`.`IdProveedorART` = `proveedores`.`IdProveedorPRO`))) order by `articulos`.`DescripcionART` */;

/*View structure for view empleados_liquidacion_cons */

/*!50001 DROP TABLE IF EXISTS `empleados_liquidacion_cons` */;
/*!50001 DROP VIEW IF EXISTS `empleados_liquidacion_cons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `empleados_liquidacion_cons` AS select `empleadosmovimientos`.`FechaEMOV` AS `FechaEMOV`,`empleadosmovimientos`.`IdEmpleadoEMOV` AS `IdEmpleadoEMOV`,`empleados`.`ApellidoEMP` AS `ApellidoEMP`,`empleados`.`NombreEMP` AS `NombreEMP`,`empleados`.`SalarioEMP` AS `SalarioEMP`,`empleadosmovimientos`.`CantidadEMOV` AS `CantidadEMOV`,`empleadosmovimientos`.`DetalleEMOV` AS `DetalleEMOV`,`empleadosmovimientos`.`ImporteEMOV` AS `ImporteEMOV`,`empleadosmovtipos`.`RemuneracionETIP` AS `RemuneracionETIP`,`empleadosmovimientos`.`LiquidadoEMOV` AS `LiquidadoEMOV` from ((`empleadosmovimientos` join `empleados` on((`empleadosmovimientos`.`IdEmpleadoEMOV` = `empleados`.`IdEmpleadoEMP`))) join `empleadosmovtipos` on((`empleadosmovimientos`.`IdMovTipoEMOV` = `empleadosmovtipos`.`IdMovETIP`))) where (`empleadosmovimientos`.`LiquidadoEMOV` = 0) */;

/*View structure for view empleados_mov_cons */

/*!50001 DROP TABLE IF EXISTS `empleados_mov_cons` */;
/*!50001 DROP VIEW IF EXISTS `empleados_mov_cons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `empleados_mov_cons` AS select `empleadosmovimientos`.`IdMovEMOV` AS `IdMovEMOV`,`empleadosmovimientos`.`FechaEMOV` AS `FechaEMOV`,`empleadosmovimientos`.`IdEmpleadoEMOV` AS `IdEmpleadoEMOV`,`empleados`.`NombreEMP` AS `NombreEMP`,`empleados`.`ApellidoEMP` AS `ApellidoEMP`,`empleadosmovimientos`.`IdMovTipoEMOV` AS `IdMovTipoEMOV`,`empleadosmovimientos`.`CantidadEMOV` AS `CantidadEMOV`,`empleadosmovimientos`.`DetalleEMOV` AS `DetalleEMOV`,`empleadosmovimientos`.`ImporteEMOV` AS `ImporteEMOV`,`empleadosmovimientos`.`LiquidadoEMOV` AS `LiquidadoEMOV` from ((`empleadosmovimientos` join `empleados` on((`empleadosmovimientos`.`IdEmpleadoEMOV` = `empleados`.`IdEmpleadoEMP`))) join `empleadosmovtipos` on((`empleadosmovimientos`.`IdMovTipoEMOV` = `empleadosmovtipos`.`IdMovETIP`))) */;

/*View structure for view fondocajacons */

/*!50001 DROP TABLE IF EXISTS `fondocajacons` */;
/*!50001 DROP VIEW IF EXISTS `fondocajacons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `fondocajacons` AS select `fondocaja`.`IdFondoFONP` AS `IdFondoFONP`,`fondocaja`.`FechaFONP` AS `FechaFONP`,`fondocaja`.`IdPcFONP` AS `IdPcFONP`,`locales`.`IdLocalLOC` AS `IdLocalLOC`,`locales`.`NombreLOC` AS `NombreLOC`,`pc`.`Detalle` AS `Detalle`,`fondocaja`.`ImporteFONP` AS `ImporteFONP` from ((`pc` join `locales` on((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`))) join `fondocaja` on((`fondocaja`.`IdPcFONP` = `pc`.`IdPC`))) order by `fondocaja`.`FechaFONP` */;

/*View structure for view localescons */

/*!50001 DROP TABLE IF EXISTS `localescons` */;
/*!50001 DROP VIEW IF EXISTS `localescons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `localescons` AS select `locales`.`IdLocalLOC` AS `IdLocalLOC` from `locales` where ((`locales`.`IdLocalLOC` <> 1) and (`locales`.`IdLocalLOC` <> 2)) */;

/*View structure for view pedidostockcons */

/*!50001 DROP TABLE IF EXISTS `pedidostockcons` */;
/*!50001 DROP VIEW IF EXISTS `pedidostockcons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `pedidostockcons` AS select `proveedores`.`RazonSocialPRO` AS `RazonSocialPRO`,`stock`.`IdArticuloSTK` AS `IdArticuloSTK`,`articulos`.`DescripcionART` AS `DescripcionART`,`articulos`.`PrecioCostoART` AS `Costo`,`articulos`.`PrecioPublicoART` AS `Publico`,`articulos`.`IdGeneroART` AS `IdGeneroART`,sum(`stock`.`CantidadSTK`) AS `Stock` from ((`stock` left join `articulos` on((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`))) join `proveedores` on((`articulos`.`IdProveedorART` = `proveedores`.`IdProveedorPRO`))) group by `proveedores`.`RazonSocialPRO`,`stock`.`IdArticuloSTK`,`articulos`.`DescripcionART` */;

/*View structure for view stockmovcons */

/*!50001 DROP TABLE IF EXISTS `stockmovcons` */;
/*!50001 DROP VIEW IF EXISTS `stockmovcons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `stockmovcons` AS select `stockmov`.`FechaMSTK` AS `FechaMSTK`,`stockmov`.`IdMovMSTK` AS `IdMovMSTK`,`stockmov`.`OrigenMSTK` AS `OrigenMSTK`,`stockmov`.`DestinoMSTK` AS `DestinoMSTK`,`stockmov`.`CompensaMSTK` AS `CompensaMSTK`,`stockmovdetalle`.`ordenar` AS `ordenar`,`stockmovdetalle`.`IdMSTKD` AS `IdMSTKD`,`stockmovdetalle`.`IdMovMSTKD` AS `IdMovMSTKD`,`stockmovdetalle`.`IdArticuloMSTKD` AS `IdArticuloMSTKD`,`articulos`.`DescripcionART` AS `DescripcionART`,`stockmovdetalle`.`CantidadMSTKD` AS `CantidadMSTKD`,`stockmovdetalle`.`CompensaMSTKD` AS `CompensaMSTKD`,`stockmovdetalle`.`OrigenMSTKD` AS `OrigenMSTKD`,`stockmovdetalle`.`DestinoMSTKD` AS `DestinoMSTKD` from ((`stockmovdetalle` join `stockmov` on((`stockmovdetalle`.`IdMovMSTKD` = `stockmov`.`IdMovMSTK`))) join `articulos` on((`stockmovdetalle`.`IdArticuloMSTKD` = `articulos`.`IdArticuloART`))) order by `stockmov`.`FechaMSTK`,`stockmov`.`IdMovMSTK` */;

/*View structure for view ventasarqueocons */

/*!50001 DROP TABLE IF EXISTS `ventasarqueocons` */;
/*!50001 DROP VIEW IF EXISTS `ventasarqueocons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventasarqueocons` AS select `ventas`.`IdPCVEN` AS `IdPCVEN`,`ventas`.`FechaVEN` AS `FechaVEN`,`ventasdetalle`.`IdDVEN` AS `IdDVEN`,`ventasdetalle`.`IdVentaDVEN` AS `IdVentaDVEN`,`ventasdetalle`.`IdArticuloDVEN` AS `IdArticuloDVEN`,`ventasdetalle`.`DescripcionDVEN` AS `DescripcionDVEN`,`ventasdetalle`.`CantidadDVEN` AS `CantidadDVEN`,`ventasdetalle`.`PrecioPublicoDVEN` AS `PrecioPublicoDVEN`,`ventasdetalle`.`PrecioCostoDVEN` AS `PrecioCostoDVEN`,`ventasdetalle`.`PrecioMayorDVEN` AS `PrecioMayorDVEN`,`ventasdetalle`.`IdFormaPagoDVEN` AS `IdFormaPagoDVEN`,`ventasdetalle`.`NroCuponDVEN` AS `NroCuponDVEN`,`ventasdetalle`.`NroFacturaDVEN` AS `NroFacturaDVEN`,`ventasdetalle`.`IdEmpleadoDVEN` AS `IdEmpleadoDVEN`,`ventasdetalle`.`LiquidadoDVEN` AS `LiquidadoDVEN`,`ventasdetalle`.`EsperaDVEN` AS `EsperaDVEN`,`ventasdetalle`.`DevolucionDVEN` AS `DevolucionDVEN`,`ventasdetalle`.`IdLocalDVEN` AS `IdLocalDVEN` from (`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) */;

/*View structure for view ventasdetallecons */

/*!50001 DROP TABLE IF EXISTS `ventasdetallecons` */;
/*!50001 DROP VIEW IF EXISTS `ventasdetallecons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventasdetallecons` AS select `locales`.`IdLocalLOC` AS `IdLocalLOC`,`locales`.`NombreLOC` AS `NombreLOC`,`ventas`.`FechaVEN` AS `FechaVEN`,`ventasdetalle`.`IdDVEN` AS `IdDVEN`,`ventasdetalle`.`IdArticuloDVEN` AS `IdArticuloDVEN`,`articulos`.`DescripcionART` AS `DescripcionART`,`ventasdetalle`.`CantidadDVEN` AS `CantidadDVEN`,`ventasdetalle`.`PrecioPublicoDVEN` AS `PrecioPublicoDVEN`,`formaspago`.`IdFormaPagoFOR` AS `IdFormaPagoFOR`,`formaspago`.`DescripcionFOR` AS `DescripcionFOR` from (((((`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) join `pc` on((`ventas`.`IdPCVEN` = `pc`.`IdPC`))) join `locales` on((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`))) join `articulos` on((`ventasdetalle`.`IdArticuloDVEN` = `articulos`.`IdArticuloART`))) join `formaspago` on((`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`))) */;

/*View structure for view ventasdetallepedidocons */

/*!50001 DROP TABLE IF EXISTS `ventasdetallepedidocons` */;
/*!50001 DROP VIEW IF EXISTS `ventasdetallepedidocons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventasdetallepedidocons` AS select `ventasdetalle`.`IdVentaDVEN` AS `IdVentaDVEN`,`ventasdetalle`.`IdArticuloDVEN` AS `IdArticuloDVEN`,`ventasdetalle`.`CantidadDVEN` AS `CantidadDVEN`,`articulos`.`IdGeneroART` AS `IdGeneroART` from (`ventasdetalle` join `articulos` on((`ventasdetalle`.`IdArticuloDVEN` = `articulos`.`IdArticuloART`))) */;

/*View structure for view ventasdiariascons */

/*!50001 DROP TABLE IF EXISTS `ventasdiariascons` */;
/*!50001 DROP VIEW IF EXISTS `ventasdiariascons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventasdiariascons` AS select date_format(`ventas`.`FechaVEN`,'%Y-%m-%d') AS `Fecha`,`locales`.`NombreLOC` AS `NombreLOC`,`formaspago`.`DescripcionFOR` AS `DescripcionFOR`,sum((`ventasdetalle`.`CantidadDVEN` * `ventasdetalle`.`PrecioPublicoDVEN`)) AS `TotalPublico`,sum((`ventasdetalle`.`CantidadDVEN` * `ventasdetalle`.`PrecioCostoDVEN`)) AS `TotalCosto`,sum(`ventasdetalle`.`CantidadDVEN`) AS `Prendas` from ((((`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) join `pc` on((`ventas`.`IdPCVEN` = `pc`.`IdPC`))) join `locales` on((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`))) join `formaspago` on((`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`))) where ((`locales`.`NombreLOC` <> 'Entradas') and (`locales`.`NombreLOC` <> 'Salidas')) group by date_format(`ventas`.`FechaVEN`,'%Y-%m-%d'),`locales`.`NombreLOC`,`formaspago`.`DescripcionFOR` order by date_format(`ventas`.`FechaVEN`,'%Y-%m-%d') */;

/*View structure for view ventasdiariasdifhistoricas */

/*!50001 DROP TABLE IF EXISTS `ventasdiariasdifhistoricas` */;
/*!50001 DROP VIEW IF EXISTS `ventasdiariasdifhistoricas` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventasdiariasdifhistoricas` AS select `ventasdiariascons`.`Fecha` AS `Fecha`,`ventasdiariascons`.`NombreLOC` AS `NombreLOC`,`ventasdiariascons`.`DescripcionFOR` AS `DescripcionFOR`,`ventasdiariascons`.`TotalPublico` AS `PublicoDiarias`,`ventasdiariascons`.`TotalCosto` AS `CostoDiarias`,`ventasdiariascons`.`Prendas` AS `PrendasDiarias` from (`ventasdiariascons` join `ventash` on(((`ventasdiariascons`.`Fecha` = `ventash`.`Fecha`) and (`ventasdiariascons`.`NombreLOC` = `ventash`.`NombreLocal`) and (`ventasdiariascons`.`DescripcionFOR` = `ventash`.`FormaPago`)))) where (`ventasdiariascons`.`TotalPublico` <> `ventash`.`TotalPublico`) order by `ventasdiariascons`.`Fecha`,`ventasdiariascons`.`NombreLOC` */;

/*View structure for view ventaspesoscons2 */

/*!50001 DROP TABLE IF EXISTS `ventaspesoscons2` */;
/*!50001 DROP VIEW IF EXISTS `ventaspesoscons2` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventaspesoscons2` AS select `locales`.`IdLocalLOC` AS `IdLocalLOC`,`locales`.`NombreLOC` AS `NombreLOC`,`ventas`.`FechaVEN` AS `FechaVEN`,`ventasdetalle`.`CantidadDVEN` AS `CantidadDVEN`,`ventasdetalle`.`PrecioPublicoDVEN` AS `PrecioPublicoDVEN`,`ventasdetalle`.`PrecioCostoDVEN` AS `PrecioCostoDVEN`,`ventasdetalle`.`IdFormaPagoDVEN` AS `IdFormaPagoDVEN` from (((`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) join `pc` on((`ventas`.`IdPCVEN` = `pc`.`IdPC`))) join `locales` on((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`))) */;

/*View structure for view ventaspesosconscopia */

/*!50001 DROP TABLE IF EXISTS `ventaspesosconscopia` */;
/*!50001 DROP VIEW IF EXISTS `ventaspesosconscopia` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `ventaspesosconscopia` AS select `locales`.`IdLocalLOC` AS `IdLocalLOC`,`locales`.`NombreLOC` AS `NombreLOC`,`ventas`.`FechaVEN` AS `FechaVEN`,`formaspago`.`IdFormaPagoFOR` AS `IdFormaPagoFOR`,`ventasdetalle`.`CantidadDVEN` AS `Prendas`,(`ventasdetalle`.`PrecioCostoDVEN` * `ventasdetalle`.`CantidadDVEN`) AS `Costo`,(`ventasdetalle`.`PrecioPublicoDVEN` * `ventasdetalle`.`CantidadDVEN`) AS `Venta` from ((((`ventasdetalle` join `ventas` on((`ventasdetalle`.`IdVentaDVEN` = `ventas`.`IdVentaVEN`))) join `pc` on((`ventas`.`IdPCVEN` = `pc`.`IdPC`))) join `locales` on((`pc`.`IdLocalPC` = `locales`.`IdLocalLOC`))) join `formaspago` on((`ventasdetalle`.`IdFormaPagoDVEN` = `formaspago`.`IdFormaPagoFOR`))) */;

/*View structure for view web_articulosbyitemscons */

/*!50001 DROP TABLE IF EXISTS `web_articulosbyitemscons` */;
/*!50001 DROP VIEW IF EXISTS `web_articulosbyitemscons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `web_articulosbyitemscons` AS select left(`articulos`.`IdArticuloART`,7) AS `IdArticuloART`,`articulos`.`IdItemART` AS `IdItemART`,`articulos`.`DescripcionWebART` AS `DescripcionWebART`,`articulos`.`PrecioPublicoART` AS `PrecioPublicoART`,`articulos`.`ImagenART` AS `ImagenART`,sum(`stock`.`CantidadSTK`) AS `Cantidad`,`stock`.`IdLocalSTK` AS `IdLocalSTK` from ((`stock` join `articulos` on((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`))) join `locales` on((`stock`.`IdLocalSTK` = `locales`.`IdLocalLOC`))) where ((`articulos`.`ActivoWebART` = 1) and (`stock`.`CantidadSTK` > 0) and (`articulos`.`ImagenART` <> '')) group by left(`articulos`.`IdArticuloART`,7),`articulos`.`PrecioPublicoART`,`articulos`.`ImagenART`,`articulos`.`ActivoWebART`,`stock`.`IdLocalSTK` */;

/*View structure for view web_articuloscons */

/*!50001 DROP TABLE IF EXISTS `web_articuloscons` */;
/*!50001 DROP VIEW IF EXISTS `web_articuloscons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `web_articuloscons` AS select `articulositems`.`IdItemITE` AS `IdItemITE`,`articulos`.`IdArticuloART` AS `IdArticuloART`,`articulos`.`DescripcionWebART` AS `DescripcionWebART`,`articulos`.`PrecioPublicoART` AS `PrecioPublicoART`,`articulos`.`ImagenART` AS `ImagenART`,`articulos`.`ImagenBackART` AS `ImagenBackART`,`articulos`.`ImagenColorART` AS `ImagenColorART`,`locales`.`IdLocalLOC` AS `IdLocalLOC` from (((`articulos` join `articulositems` on((`articulos`.`IdItemART` = `articulositems`.`IdItemITE`))) join `stock` on((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`))) join `locales` on((`stock`.`IdLocalSTK` = `locales`.`IdLocalLOC`))) where ((`articulos`.`ImagenART` <> '') and (`articulos`.`ActivoWebART` = 1)) */;

/*View structure for view web_articulosnuevoscons */

/*!50001 DROP TABLE IF EXISTS `web_articulosnuevoscons` */;
/*!50001 DROP VIEW IF EXISTS `web_articulosnuevoscons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `web_articulosnuevoscons` AS select `articulos`.`IdArticuloART` AS `IdArticuloART`,`articulos`.`DescripcionWebART` AS `DescripcionWebART`,`articulos`.`PrecioPublicoART` AS `PrecioPublicoART`,`articulos`.`ImagenART` AS `ImagenART`,`articulos`.`FechaART` AS `FechaART`,`locales`.`IdLocalLOC` AS `IdLocalLOC` from ((`stock` join `articulos` on((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`))) join `locales` on((`stock`.`IdLocalSTK` = `locales`.`IdLocalLOC`))) where ((`articulos`.`ImagenART` <> '') and (`articulos`.`ActivoWebART` = 1)) */;

/*View structure for view web_lonuevocons */

/*!50001 DROP TABLE IF EXISTS `web_lonuevocons` */;
/*!50001 DROP VIEW IF EXISTS `web_lonuevocons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `web_lonuevocons` AS select left(`articulos`.`IdArticuloART`,7) AS `IdArticuloART`,`articulos`.`DescripcionWebART` AS `DescripcionWebART`,`articulos`.`PrecioPublicoART` AS `PrecioPublicoART`,`articulos`.`ImagenART` AS `ImagenART`,`articulos`.`ActivoWebART` AS `ActivoWebART`,`articulos`.`NuevoART` AS `NuevoART`,sum(`stock`.`CantidadSTK`) AS `Cantidad`,`stock`.`IdLocalSTK` AS `IdLocalSTK` from ((`stock` join `articulos` on((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`))) join `locales` on((`stock`.`IdLocalSTK` = `locales`.`IdLocalLOC`))) where ((`articulos`.`ActivoWebART` = 1) and (`stock`.`CantidadSTK` > 0) and (`articulos`.`NuevoART` = 1) and (`articulos`.`ImagenART` <> '')) group by left(`articulos`.`IdArticuloART`,7),`articulos`.`PrecioPublicoART`,`articulos`.`ImagenART`,`articulos`.`ActivoWebART`,`articulos`.`NuevoART`,`stock`.`IdLocalSTK` */;

/*View structure for view web_stockcons */

/*!50001 DROP TABLE IF EXISTS `web_stockcons` */;
/*!50001 DROP VIEW IF EXISTS `web_stockcons` */;

/*!50001 CREATE ALGORITHM=UNDEFINED DEFINER=`ncsoftwa_re`@`%` SQL SECURITY DEFINER VIEW `web_stockcons` AS select `articulositems`.`ActivoWebITE` AS `ActivoWebITE`,`articulositems`.`IdItemITE` AS `IdItemITE`,`articulos`.`IdArticuloART` AS `IdArticuloART`,`articulos`.`DescripcionART` AS `DescripcionART`,`articulos`.`DescripcionWebART` AS `DescripcionWebART`,`articulos`.`PrecioPublicoART` AS `PrecioPublicoART`,`articulos`.`ImagenART` AS `ImagenART`,`articulos`.`ImagenBackART` AS `ImagenBackART`,`articulos`.`ImagenColorART` AS `ImagenColorART`,`stock`.`CantidadSTK` AS `CantidadSTK`,`stock`.`IdLocalSTK` AS `IdLocalSTK`,`colores`.`HexCOL` AS `HexCOL`,`colores`.`DescripcionCOL` AS `DescripcionCOL`,`articulos`.`TalleART` AS `TalleART` from (((`articulos` join `articulositems` on((`articulos`.`IdItemART` = `articulositems`.`IdItemITE`))) left join `colores` on((`articulos`.`IdColorART` = `colores`.`IdColorCOL`))) join `stock` on((`stock`.`IdArticuloSTK` = `articulos`.`IdArticuloART`))) where ((`stock`.`CantidadSTK` > 0) and (`articulos`.`ImagenART` <> '')) */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
