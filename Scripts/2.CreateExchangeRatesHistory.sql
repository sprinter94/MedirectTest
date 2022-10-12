-- --------------------------------------------------------
-- Host:                         192.168.23.50
-- Server version:               8.0.23-commercial - MySQL Enterprise Server - Commercial
-- Server OS:                    Win64
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for testr
USE `testr`;

-- Dumping structure for table testr.exchangerateshistory
DROP TABLE IF EXISTS `exchangerateshistory`;
CREATE TABLE IF NOT EXISTS `exchangerateshistory` (
  `PK_Id` int NOT NULL AUTO_INCREMENT,
  `FK_UserID` int NOT NULL,
  `BaseCurrency` char(3) NOT NULL DEFAULT '',
  `BaseAmount` decimal(10,2) NOT NULL DEFAULT '0.00',
  `ToCurrency` char(3) NOT NULL DEFAULT '0',
  `ToAmount` decimal(10,2) NOT NULL DEFAULT '0.00',
  `ExchangeRate` decimal(10,2) NOT NULL DEFAULT '0.00',
  `DateInserted` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`PK_Id`),
  KEY `FK__user` (`FK_UserID`),
  CONSTRAINT `FK__user` FOREIGN KEY (`FK_UserID`) REFERENCES `user` (`PK_UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Data exporting was unselected.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
