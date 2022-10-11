CREATE TABLE `user` (
	`PK_UserId` INT NOT NULL Auto_INCREMENT,
	`Firstname` VARCHAR(50) NOT NULL DEFAULT '',
	`LastName` VARCHAR(50) NOT NULL DEFAULT '',
	`Username` VARCHAR(50) NOT NULL DEFAULT '',
	PRIMARY KEY (`PK_UserId`)
);

CREATE TABLE `exchangeRatesHistory` (
	`PK_Id` INT NOT NULL Auto_INCREMENT,
	`FK_UserID` INT NOT NULL,
	`BaseCurrency` CHAR(3) NOT NULL DEFAULT '',
	`BaseAmount` DECIMAL(10,0) NOT NULL DEFAULT 0,
	`ToCurrency` CHAR(3) NOT NULL DEFAULT '0',
	`ToAmount` DECIMAL(10,0) NOT NULL DEFAULT 0,
	`ExchangeRate` DECIMAL(10,0) NOT NULL DEFAULT 0,
	`DateInserted` DATETIME NOT NULL DEFAULT 0,
	PRIMARY KEY (`PK_Id`),
	CONSTRAINT `FK__user` FOREIGN KEY (`FK_UserID`) REFERENCES `user` (`PK_UserId`)
)
COLLATE='utf8mb4_0900_ai_ci'
;


INSERT INTO `testr`.`user` (`Firstname`, `LastName`, `Username`) VALUES ('Joe', 'Borg', 'joe.borg');



