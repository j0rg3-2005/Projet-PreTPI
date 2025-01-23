-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: bibliotech
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `utilisateurs`
--

DROP TABLE IF EXISTS `utilisateurs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utilisateurs` (
  `id_utilisateur` int NOT NULL AUTO_INCREMENT,
  `nom` varchar(255) NOT NULL,
  `prenom` varchar(255) NOT NULL,
  `telephone` varchar(15) DEFAULT NULL,
  `adresse_postale` text,
  `adresse_email` varchar(255) NOT NULL,
  PRIMARY KEY (`id_utilisateur`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utilisateurs`
--

LOCK TABLES `utilisateurs` WRITE;
/*!40000 ALTER TABLE `utilisateurs` DISABLE KEYS */;
INSERT INTO `utilisateurs` VALUES (1,'Dupont','Jean','0123456789','10 Rue de Paris, 75001 Paris','jean.dupont@email.com'),(2,'Martin','Claire','0987654321','5 Avenue des Champs-Élysées, 75008 Paris','claire.martin@email.com'),(3,'Lemoine','Luc','0147258369','34 Boulevard Saint-Germain, 75005 Paris','luc.lemoine@email.com'),(4,'Dupont','Jean','0123456789','10 Rue de Paris, 75001 Paris','jean.dupont@email.com'),(5,'Martin','Claire','0987654321','5 Avenue des Champs-Élysées, 75008 Paris','claire.martin@email.com'),(6,'Lemoine','Luc','0147258369','34 Boulevard Saint-Germain, 75005 Paris','luc.lemoine@email.com'),(7,'Dupont','Jean','0123456789','10 Rue de Paris, 75001 Paris','jean.dupont@email.com'),(8,'Martin','Claire','0987654321','5 Avenue des Champs-Élysées, 75008 Paris','claire.martin@email.com'),(9,'Lemoine','Luc','0147258369','34 Boulevard Saint-Germain, 75005 Paris','luc.lemoine@email.com'),(10,'Dupont','Jean','0123456789','10 Rue de Paris, 75001 Paris','jean.dupont@email.com'),(11,'Martin','Claire','0987654321','5 Avenue des Champs-Élysées, 75008 Paris','claire.martin@email.com'),(12,'Lemoine','Luc','0147258369','34 Boulevard Saint-Germain, 75005 Paris','luc.lemoine@email.com'),(13,'Dupont','Jean','0123456789','10 Rue de Paris, 75001 Paris','jean.dupont@email.com'),(14,'Martin','Claire','0987654321','5 Avenue des Champs-Élysées, 75008 Paris','claire.martin@email.com'),(15,'Lemoine','Luc','0147258369','34 Boulevard Saint-Germain, 75005 Paris','luc.lemoine@email.com');
/*!40000 ALTER TABLE `utilisateurs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-23 17:07:47
