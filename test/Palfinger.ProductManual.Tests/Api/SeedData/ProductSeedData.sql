--*************************************************************
--Products
--*************************************************************
INSERT INTO main.Product (Name,Description,ImageUrl) VALUES ('Bike','Description','https://example.com/image.png');
INSERT INTO main.Product (Name,Description,ImageUrl) VALUES ('Car','Description','https://example.com/image.png');
INSERT INTO main.Product (Name,Description,ImageUrl) VALUES ('Skate','Description','https://example.com/image.png');

--*************************************************************
--Attributes
--*************************************************************
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Handlebar',1,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Tyre',1,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Gear',1,'Description','https://example.com/image.png');

INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Tyre',2,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Door',2,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('GasType',2,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Power',2,'Description','https://example.com/image.png');

INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Wheel',3,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Trucks',3,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Deck',3,'Description','https://example.com/image.png');
INSERT INTO main.Attribute (Name,ProductId,Description,ImageUrl) VALUES ('Grip tape',3,'Description','https://example.com/image.png');

--*************************************************************
--Configurations
--*************************************************************
--Bike
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Long',1,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Short',1,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Medium',1,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Soft',2,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Medium',2,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Hard',2,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Gear Standard',3,'Description','https://example.com/image.png');

--Car
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Soft',4,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Medium',4,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Hard',4,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Standard',5,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Five',5,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Standard',6,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Diesel',6,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('150hp',7,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('200hp',7,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('300hp',7,'Description','https://example.com/image.png');

--Skate
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('70mm',8,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('75mm',8,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Standard',9,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Precision',9,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Standard',10,'Description','https://example.com/image.png');

INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Regular',11,'Description','https://example.com/image.png');
INSERT INTO main.Configuration (Name,AttributeId,Description,ImageUrl) VALUES ('Abrasive',11,'Description','https://example.com/image.png');