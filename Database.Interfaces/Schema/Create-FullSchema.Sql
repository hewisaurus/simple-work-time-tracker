CREATE DATABASE `SimpleWorkTimeTracker` /*!40100 DEFAULT CHARACTER SET latin1 */;

CREATE TABLE `PersonStatus` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `AppearanceClass` varchar(45) NOT NULL,
  `IsDefault` tinyint(1) NOT NULL DEFAULT '0',
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UQ_PersonStatus_Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Person` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(60) NOT NULL,
  `Lastname` varchar(60) NOT NULL,
  `Email` varchar(60) NOT NULL,
  `Password` varchar(64) NOT NULL,
  `PersonStatusId` int(11) NOT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UQ_Person_Email` (`Email`),
  KEY `FK_Person_PersonStatus_idx` (`PersonStatusId`),
  CONSTRAINT `FK_Person_PersonStatus` FOREIGN KEY (`PersonStatusId`) REFERENCES `PersonStatus` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;