CREATE TABLE `Authentication` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Firstname` varchar(60) NOT NULL,
  `Lastname` varchar(60) NOT NULL,
  `Email` varchar(60) NOT NULL,
  `Password` varchar(64) NOT NULL,
  `Created` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Modified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
