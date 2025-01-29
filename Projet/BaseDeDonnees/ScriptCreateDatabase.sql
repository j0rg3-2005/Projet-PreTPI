-- Créer la base de données BiblioTech si elle n'existe pas déjà
DROP DATABASE IF EXISTS bibliotech;
CREATE DATABASE bibliotech;

-- Sélectionner la base de données à utiliser
USE bibliotech;

-- Re-création de la table des utilisateurs si elle existe déjà
CREATE TABLE utilisateurs (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    telephone VARCHAR(20) NOT NULL,
    adresse_postale VARCHAR(255),
    adresse_email VARCHAR(100)
);

-- Re-création de la table des livres si elle existe déjà
CREATE TABLE livres (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    titre VARCHAR(255) NOT NULL,
    genre VARCHAR(255) NOT NULL,
    auteur VARCHAR(255) NOT NULL,
    date_publication DATE NOT NULL
);

-- Re-création de la table des emprunts si elle existe déjà
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

-- Insertion des utilisateurs
INSERT INTO utilisateurs (nom, prenom, telephone, adresse_postale, adresse_email)
VALUES
('Dupont', 'Jean', '0123456789', '10 Rue de Paris, 75001 Paris', 'jean.dupont@email.com'),
('Martin', 'Claire', '0987654321', '5 Avenue des Champs-Élysées, 75008 Paris', 'claire.martin@email.com'),
('Lemoine', 'Luc', '0147258369', '34 Boulevard Saint-Germain, 75005 Paris', 'luc.lemoine@email.com'),
('Bernard', 'Pierre', '0112233445', '15 Rue de la République, 69002 Lyon', 'pierre.bernard@email.com'),
('Moreau', 'Sophie', '0687788990', '27 Place Bellecour, 69001 Lyon', 'sophie.moreau@email.com'),
('Lefevre', 'Julien', '0162233445', '8 Rue de Rivoli, 75001 Paris', 'julien.lefevre@email.com'),
('Robert', 'Aline', '0156223344', '43 Avenue Victor Hugo, 75016 Paris', 'aline.robert@email.com'),
('Pires', 'Carlos', '0677889901', '12 Rue de la Paix, 75002 Paris', 'carlos.pires@email.com'),
('Dufresne', 'Martine', '0147852369', '21 Rue de la Gare, 75012 Paris', 'martine.dufresne@email.com'),
('Dubois', 'Eric', '0685237461', '57 Boulevard Montparnasse, 75014 Paris', 'eric.dubois@email.com'),
('Boucher', 'Marc', '0754638291', '28 Rue de l’Opéra, 75009 Paris', 'marc.boucher@email.com'),
('Lemoine', 'Mélanie', '0246127389', '32 Avenue des Ternes, 75017 Paris', 'melanie.lemoine@email.com'),
('Gerard', 'Nicolas', '0956421378', '18 Rue de la Libération, 67000 Strasbourg', 'nicolas.gerard@email.com'),
('Leclerc', 'Isabelle', '0612345678', '9 Place de l’Étoile, 75008 Paris', 'isabelle.leclerc@email.com'),
('Martin', 'Lucie', '0145789456', '55 Rue du Faubourg Saint-Antoine, 75011 Paris', 'lucie.martin@email.com'),
('Baron', 'Michel', '0234567890', '47 Boulevard de la Villette, 75019 Paris', 'michel.baron@email.com'),
('Chevalier', 'Élise', '0178901234', '8 Rue de la Pomme, 31000 Toulouse', 'elise.chevalier@email.com'),
('Fournier', 'Catherine', '0612340987', '14 Place du Capitole, 31000 Toulouse', 'catherine.fournier@email.com'),
('Lemoine', 'Valérie', '0157684321', '22 Boulevard Jean-Jaurès, 69007 Lyon', 'valerie.lemoine@email.com'),
('Lopez', 'Antonio', '0645231578', '30 Rue des Pyrénées, 75020 Paris', 'antonio.lopez@email.com'),
('Roux', 'Géraldine', '0745962364', '25 Rue de la Villette, 75019 Paris', 'geraldine.roux@email.com'),
('Meyer', 'Jean-Pierre', '0254789512', '50 Rue Saint-Antoine, 75011 Paris', 'jean-pierre.meyer@email.com'),
('Hernandez', 'Santiago', '0337892456', '17 Boulevard Voltaire, 75011 Paris', 'santiago.hernandez@email.com'),
('Blanc', 'Alice', '0654321987', '40 Rue de la République, 69003 Lyon', 'alice.blanc@email.com'),
('Perrot', 'Juliette', '0798452376', '29 Rue du Faubourg Saint-Antoine, 75011 Paris', 'juliette.perrot@email.com'),
('Perrin', 'Claude', '0912378465', '23 Avenue des Champs-Élysées, 75008 Paris', 'claude.perrin@email.com'),
('Lemoine', 'Denis', '0623457869', '3 Rue de la Source, 76000 Rouen', 'denis.lemoine@email.com'),
('Dumas', 'André', '0178645301', '12 Rue de la Liberté, 33000 Bordeaux', 'andre.dumas@email.com'),
('Gosse', 'Isabelle', '0654321345', '34 Rue de la Lune, 69002 Lyon', 'isabelle.gosse@email.com'),
('Miller', 'Nathan', '0987531246', '18 Rue du Four, 75006 Paris', 'nathan.miller@email.com'),
('Bois', 'Christophe', '0145126369', '50 Boulevard de Sébastopol, 75003 Paris', 'christophe.bois@email.com'),
('Dupuis', 'Emilie', '0145678901', '7 Place des Vosges, 75003 Paris', 'emilie.dupuis@email.com'),
('Vidal', 'Michel', '0685349760', '25 Boulevard de la Madeleine, 75001 Paris', 'michel.vidal@email.com'),
('Thomas', 'Mélissa', '0165332210', '10 Rue de l’Église, 69001 Lyon', 'melissa.thomas@email.com'),
('Chavez', 'Luis', '0146238475', '33 Rue de l’Arcade, 75008 Paris', 'luis.chavez@email.com'),
('Dufresne', 'Sylvie', '0681274935', '4 Rue de l’Église, 75012 Paris', 'sylvie.dufresne@email.com'),
('Renaud', 'Bernadette', '0987554321', '51 Rue de la Paix, 75002 Paris', 'bernadette.renaud@email.com'),
('Durand', 'Benoît', '0761234567', '22 Rue de la Trémoille, 75008 Paris', 'benoit.durand@email.com'),
('Saïd', 'Karim', '0736593842', '12 Rue du Montparnasse, 75014 Paris', 'karim.said@email.com'),
('Guillaume', 'Sophie', '0759326187', '19 Avenue de la République, 75011 Paris', 'sophie.guillaume@email.com'),
('Hebert', 'Paul', '0113587924', '29 Rue de la Marne, 34000 Montpellier', 'paul.hebert@email.com'),
('Lemoine', 'Mathieu', '0165890475', '55 Boulevard de l’Indépendance, 59000 Lille', 'mathieu.lemoine@email.com'),
('Giraud', 'François', '0682938475', '10 Rue de la République, 31000 Toulouse', 'francois.giraud@email.com'),
('Nicolas', 'Chantal', '0145783642', '20 Avenue Montaigne, 75008 Paris', 'chantal.nicolas@email.com'),
('Lemoine', 'Henri', '0336745923', '8 Rue des Martyrs, 69009 Lyon', 'henri.lemoine@email.com'),
('Benoit', 'Julie', '0712345980', '31 Boulevard des Belges, 34000 Montpellier', 'julie.benoit@email.com'),
('Dupuis', 'Luc', '0145638721', '55 Boulevard des Italiens, 75009 Paris', 'luc.dupuis@email.com'),
('Vasseur', 'Cédric', '0142356879', '48 Avenue de la République, 75011 Paris', 'cedric.vasseur@email.com'),
('Tanguy', 'Alain', '0192837465', '24 Rue de la Bastille, 75011 Paris', 'alain.tanguy@email.com'),
('Bouchard', 'Emilie', '0654892043', '42 Rue de la République, 69001 Lyon', 'emilie.bouchard@email.com'),
('Gagnon', 'Sylvain', '0143546219', '17 Boulevard Voltaire, 75011 Paris', 'sylvain.gagnon@email.com'),
('Aubry', 'Valérie', '0146547381', '8 Rue de la Croix, 31000 Toulouse', 'valerie.aubry@email.com'),
('Germain', 'Vincent', '0789324765', '30 Rue de la Glacière, 75013 Paris', 'vincent.germain@email.com'),
('Maillard', 'Christine', '0142765345', '10 Boulevard de Strasbourg, 67000 Strasbourg', 'christine.maillard@email.com');