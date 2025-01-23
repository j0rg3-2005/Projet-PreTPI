-- Créer la base de données BiblioTech si elle n'existe pas déjà
CREATE DATABASE IF NOT EXISTS BiblioTech;

-- Sélectionner la base de données à utiliser
USE BiblioTech;

-- Création de la table des utilisateurs si elle n'existe pas déjà
CREATE TABLE IF NOT EXISTS utilisateurs (
    id_utilisateur INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    prenom VARCHAR(255) NOT NULL,
    telephone VARCHAR(15),
    adresse_postale TEXT,
    adresse_email VARCHAR(255) NOT NULL
);

-- Création de la table des livres si elle n'existe pas déjà
CREATE TABLE IF NOT EXISTS livres (
    id_livre INT AUTO_INCREMENT PRIMARY KEY,
    titre VARCHAR(255) NOT NULL,
    genre VARCHAR(255),
    auteur VARCHAR(255),
    date_publication DATE
);

-- Création de la table des emprunts si elle n'existe pas déjà
CREATE TABLE IF NOT EXISTS emprunts (
    id_emprunt INT AUTO_INCREMENT PRIMARY KEY,
    id_utilisateur INT,
    id_livre INT,
    date_debut DATE NOT NULL,
    date_fin DATE,
    etat VARCHAR(50) NOT NULL,
    FOREIGN KEY (id_utilisateur) REFERENCES utilisateurs(id_utilisateur),
    FOREIGN KEY (id_livre) REFERENCES livres(id_livre)
);

-- Insertion de données dans la table des utilisateurs
INSERT INTO utilisateurs (nom, prenom, telephone, adresse_postale, adresse_email)
VALUES
('Dupont', 'Jean', '0123456789', '10 Rue de Paris, 75001 Paris', 'jean.dupont@email.com'),
('Martin', 'Claire', '0987654321', '5 Avenue des Champs-Élysées, 75008 Paris', 'claire.martin@email.com'),
('Lemoine', 'Luc', '0147258369', '34 Boulevard Saint-Germain, 75005 Paris', 'luc.lemoine@email.com');

-- Insertion de données dans la table des livres
INSERT INTO livres (titre, genre, auteur, date_publication)
VALUES
('Les Misérables', 'Roman', 'Victor Hugo', '1862-01-01'),
('1984', 'Science-fiction', 'George Orwell', '1949-06-08'),
('Le Petit Prince', 'Conte', 'Antoine de Saint-Exupéry', '1943-04-06');

-- Insertion de données dans la table des emprunts
INSERT INTO emprunts (id_utilisateur, id_livre, date_debut, date_fin, etat)
VALUES
(1, 1, '2025-01-23', '2025-02-23', 'En cours d’utilisation'),
(2, 2, '2025-01-15', '2025-02-15', 'Rendu'),
(3, 3, '2025-01-10', '2025-02-10', 'En cours d’utilisation');