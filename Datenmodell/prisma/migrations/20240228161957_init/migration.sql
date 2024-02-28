-- CreateTable
CREATE TABLE "User" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "vorname" TEXT NOT NULL,
    "nachname" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "telefon" TEXT NOT NULL
);

-- CreateTable
CREATE TABLE "Rabatt" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "startDatum" DATETIME NOT NULL,
    "endDatum" DATETIME NOT NULL,
    "prozent" INTEGER NOT NULL
);

-- CreateTable
CREATE TABLE "Produkt" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "beschreibung" TEXT NOT NULL,
    "dauermin" INTEGER NOT NULL,
    "preis" REAL NOT NULL
);

-- CreateTable
CREATE TABLE "Raum" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL
);

-- CreateTable
CREATE TABLE "Termin" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "start" DATETIME NOT NULL,
    "ende" DATETIME NOT NULL,
    "userId" INTEGER,
    "raumId" INTEGER,
    "terminartId" INTEGER NOT NULL,
    CONSTRAINT "Termin_terminartId_fkey" FOREIGN KEY ("terminartId") REFERENCES "Terminart" ("id") ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT "Termin_userId_fkey" FOREIGN KEY ("userId") REFERENCES "User" ("id") ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT "Termin_raumId_fkey" FOREIGN KEY ("raumId") REFERENCES "Raum" ("id") ON DELETE SET NULL ON UPDATE CASCADE
);

-- CreateTable
CREATE TABLE "Terminart" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "art" TEXT NOT NULL
);

-- CreateTable
CREATE TABLE "Buchung" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "mitarbeiterId" INTEGER,
    "kundeId" INTEGER NOT NULL,
    "terminId" INTEGER NOT NULL,
    "produktId" INTEGER NOT NULL,
    "rabattId" INTEGER,
    "raumId" INTEGER,
    CONSTRAINT "Buchung_terminId_fkey" FOREIGN KEY ("terminId") REFERENCES "Termin" ("id") ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT "Buchung_mitarbeiterId_fkey" FOREIGN KEY ("mitarbeiterId") REFERENCES "User" ("id") ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT "Buchung_kundeId_fkey" FOREIGN KEY ("kundeId") REFERENCES "User" ("id") ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT "Buchung_produktId_fkey" FOREIGN KEY ("produktId") REFERENCES "Produkt" ("id") ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT "Buchung_rabattId_fkey" FOREIGN KEY ("rabattId") REFERENCES "Rabatt" ("id") ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT "Buchung_raumId_fkey" FOREIGN KEY ("raumId") REFERENCES "Raum" ("id") ON DELETE SET NULL ON UPDATE CASCADE
);

-- CreateTable
CREATE TABLE "Role" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "userId" INTEGER,
    CONSTRAINT "Role_userId_fkey" FOREIGN KEY ("userId") REFERENCES "User" ("id") ON DELETE SET NULL ON UPDATE CASCADE
);

-- CreateIndex
CREATE UNIQUE INDEX "User_email_key" ON "User"("email");

-- CreateIndex
CREATE UNIQUE INDEX "Terminart_art_key" ON "Terminart"("art");

-- CreateIndex
CREATE UNIQUE INDEX "Role_name_key" ON "Role"("name");
