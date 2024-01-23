CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(255) COMMENT 'User CoverImg',
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(1000) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT DEFAULT 0,
        kept INT DEFAULT 0,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE -- FOREIGN KEY (kept) REFERENCES vaults (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(1000) NOT NULL,
        img VARCHAR(500) NOT NULL,
        isPrivate BOOLEAN DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

SELECT * FROM accounts WHERE id = "650b2a54c31ea10c7b4b13bf";