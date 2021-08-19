
--*************************************************************
--Product
--*************************************************************
INSERT INTO main.Product (Id,Name) VALUES (1,'Bike');

--*************************************************************
--Attributes
--*************************************************************
INSERT INTO main.Attribute (Id,Name,ProductId) VALUES (1,'Handlebar',1);
INSERT INTO main.Attribute (Id,Name,ProductId) VALUES (2,'Tyre',1);
INSERT INTO main.Attribute (Id,Name,ProductId) VALUES (3,'Gear',1);


--*************************************************************
--Configurations
--*************************************************************
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (1,'Long Handlebar',1);
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (2,'Short Handlebar',1);
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (3,'Medium Handlebar',1);
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (4,'Soft Tyre',2);
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (5,'Medium Tyre',2);
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (6,'Hard Tyre',2);
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (7,'Gear Standard',3);

