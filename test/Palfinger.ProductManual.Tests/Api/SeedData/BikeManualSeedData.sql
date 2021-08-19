--*************************************************************
--Product
--*************************************************************
INSERT INTO main.Product (Id,Name) VALUES (1,'Bike');

--*************************************************************
--Attributes
--*************************************************************
INSERT INTO main.Attribute (Id,Name,ProductId) VALUES (1,'Handlebar',1);


--*************************************************************
--Configurations
--*************************************************************
INSERT INTO main.Configuration (Id,Name,AttributeId) VALUES (1,'Long Handlebar',1);

