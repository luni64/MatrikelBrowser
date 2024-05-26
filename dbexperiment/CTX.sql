CREATE TABLE "Persons" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Persons" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL
);


CREATE TABLE "Families" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Families" PRIMARY KEY AUTOINCREMENT,
    "FatherID" INTEGER NULL,
    "MotherID" INTEGER NULL,
    CONSTRAINT "FK_Families_Persons_FatherID" FOREIGN KEY ("FatherID") REFERENCES "Persons" ("Id"),
    CONSTRAINT "FK_Families_Persons_MotherID" FOREIGN KEY ("MotherID") REFERENCES "Persons" ("Id")
);


CREATE TABLE "ChildTable" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ChildTable" PRIMARY KEY AUTOINCREMENT,
    "PersonId" INTEGER NOT NULL,
    "FamilyId" INTEGER NOT NULL,
    "data" TEXT NULL,
    CONSTRAINT "FK_ChildTable_Families_FamilyId" FOREIGN KEY ("FamilyId") REFERENCES "Families" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ChildTable_Persons_PersonId" FOREIGN KEY ("PersonId") REFERENCES "Persons" ("Id") ON DELETE CASCADE
);


CREATE INDEX "IX_ChildTable_FamilyId" ON "ChildTable" ("FamilyId");


CREATE INDEX "IX_ChildTable_PersonId" ON "ChildTable" ("PersonId");


CREATE INDEX "IX_Families_FatherID" ON "Families" ("FatherID");


CREATE INDEX "IX_Families_MotherID" ON "Families" ("MotherID");