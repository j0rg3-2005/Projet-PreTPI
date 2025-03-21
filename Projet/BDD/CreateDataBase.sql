DROP DATABASE IF EXISTS bibliotech;
CREATE DATABASE bibliotech;

USE bibliotech;

CREATE TABLE utilisateurs (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    telephone VARCHAR(20) NOT NULL,
    adresse_postale VARCHAR(255),
    adresse_email VARCHAR(100)
);

CREATE TABLE livres (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    titre VARCHAR(255) NOT NULL,
    genre VARCHAR(255) NOT NULL,
    auteur VARCHAR(255) NOT NULL,
    date_publication DATE NOT NULL,
    etat BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE emprunts (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    id_utilisateur INT,
    id_livre INT,
    date_debut DATE NOT NULL,
    date_fin DATE,
    etat VARCHAR(50) NOT NULL,
    FOREIGN KEY (id_utilisateur) REFERENCES utilisateurs(ID),
    FOREIGN KEY (id_livre) REFERENCES livres(ID)
);