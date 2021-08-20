--*************************************************************
--Products
--*************************************************************
INSERT INTO main.Product (Name) VALUES ('Bike');
INSERT INTO main.Product (Name) VALUES ('Car');
INSERT INTO main.Product (Name) VALUES ('Skate');

--*************************************************************
--Attributes
--*************************************************************
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Handlebar',1);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Tyre',1);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Gear',1);

INSERT INTO main.Attribute (Name,ProductId) VALUES ('Tyre',2);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Door',2);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('GasType',2);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Power',2);

INSERT INTO main.Attribute (Name,ProductId) VALUES ('Wheel',3);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Trucks',3);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Deck',3);
INSERT INTO main.Attribute (Name,ProductId) VALUES ('Grip tape',3);

--*************************************************************
--Configurations
--*************************************************************
--Bike
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Long',1);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Short',1);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Medium',1);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Soft',2);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Medium',2);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Hard',2);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Gear Standard',3);

--Car
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Soft',4);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Medium',4);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Hard',4);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Standard',5);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Five',5);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Standard',6);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Diesel',6);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('150hp',7);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('200hp',7);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('300hp',7);

--Skate
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('70mm',8);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('75mm',8);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Standard',9);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Precision',9);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Standard',10);

INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Regular',11);
INSERT INTO main.Configuration (Name,AttributeId) VALUES ('Abrasive',11);