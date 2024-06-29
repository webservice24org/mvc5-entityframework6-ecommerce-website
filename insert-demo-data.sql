USE MohiuddinEcommerceDB
GO

BEGIN TRY
    INSERT INTO Brands (BrandName)
    VALUES ('ACI'),
           ('Basundhara'),
           ('MWT'),
		   ('Dan Foods'),
		   ('Kazi');

    PRINT 'Brands inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Units (UnitName)
    VALUES 
	('Piece'),
	('KG'),
	('Kilogram');
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Unit inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting unit: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO ProductCategories (CategoryName, CategoryImage)
    VALUES 
	('Vegetables', '~/Images/product-category/Vegetables.png'),
	('Fish', '~/Images/product-category/Fish.jpg'),
	('Meat', '~/Images/product-category/Meat.png'),
	('Fruits', '~/Images/product-category/Fruits.png');

    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Product category inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting product category: ' + ERROR_MESSAGE();
END CATCH


BEGIN TRY
    INSERT INTO ProductSubCategories (SubCategoryName, SubCategoryImg, ProductCategoryId)
    VALUES
	('Onion', '~/Images/product-subcategory/Onion.png', 1),
	('Tomato', '~/Images/product-subcategory/Tomato.png', 1),
	('Potato', '~/Images/product-subcategory/Potato.png', 1),
	('Hilsha', '~/Images/product-subcategory/Hilsha.png', 2),
	('Chingri', '~/Images/product-subcategory/Chingri.png', 2),
	('Pangash', '~/Images/product-subcategory/Pangash.png', 2),
	('Bengal Meat', '~/Images/product-subcategory/Bengal_Meat.png', 3),
	('Boiler Chicker', '~/Images/product-subcategory/Bioler_chicken.png', 3),
	('Beef Stomach', '~/Images/product-subcategory/Meat_Stomach.png', 3),
	('Malta', '~/Images/product-subcategory/Malta.png', 4),
	('Guava', '~/Images/product-subcategory/Guava.png', 4),
	('Apple', '~/Images/product-subcategory/Apple.png', 4);
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Product subcategory inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting product subcategory: ' + ERROR_MESSAGE();
END CATCH


BEGIN TRY
    INSERT INTO ProductSuppliers (SupplierName, ContactEmail, ContactPhone, Address)
    VALUES ('ABC Suppliers', 'abc@example.com', '123-456-7890', '123 Main Street'),
	('Jamuna Suppliers', 'jamuna@example.com', '123-456-7890', '123 Farmgate, Dhaka'),
	('Keyamy Suppliers', 'kem@example.com', '123-456-7890', '225 Agargoun, Dhaka');

    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Product supplier inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting product supplier: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Discounts (Name, Percentage)
    VALUES 
	('Eid', 25.0),
	('Year End Sale', 15.0),
	('Ramadan Offer', 20.0);
    IF @@ROWCOUNT > 0
    BEGIN
        PRINT 'Discount inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'No rows inserted.';
    END
END TRY
BEGIN CATCH
    PRINT 'Error inserting discount: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Products(ProductName, Price, Description, ImageUrl, IsActive, ProductCategoryId, ProductSubCategoryId, UnitId, BrandId, ProductSupplierId, DiscountId, Quantity)
    VALUES 
    ('Onion Red', 30.00, 'Fresh red onions.', '~/Images/products/onion-red.jpg', 1, 1, 1, 2, 1, 1, 1, 100),
    ('Onion White', 35.00, 'Fresh white onions.', '~/Images/products/onion-white.jpg', 1, 1, 1, 2, 2, 2, 1, 200),
    ('Tomato Local', 50.00, 'Locally sourced tomatoes.', '~/Images/products/tomato-local.jpg', 1, 1, 2, 2, 3, 3, 1, 150),
    ('Tomato Imported', 60.00, 'Imported tomatoes.', '~/Images/products/tomato-imported.jpg', 1, 1, 2, 2, 4, 1, 2, 120),
    ('Potato Fresh', 25.00, 'Fresh potatoes.', '~/Images/products/potato-fresh.jpg', 1, 1, 3, 2, 5, 2, 1, 300);
    PRINT 'Vegetables products inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error inserting vegetables products: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Products(ProductName, Price, Description, ImageUrl, IsActive, ProductCategoryId, ProductSubCategoryId, UnitId, BrandId, ProductSupplierId, DiscountId, Quantity)
    VALUES 
    ('Hilsha Large', 500.00, 'Large size Hilsha fish.', '~/Images/products/hilsha-large.jpg', 1, 2, 4, 2, 1, 1, 2, 50),
    ('Hilsha Medium', 400.00, 'Medium size Hilsha fish.', '~/Images/products/hilsha-medium.jpg', 1, 2, 4, 2, 2, 2, 2, 60),
    ('Chingri Fresh', 300.00, 'Fresh Chingri.', '~/Images/products/chingri-fresh.jpg', 1, 2, 5, 2, 3, 3, 2, 70),
    ('Chingri Frozen', 350.00, 'Frozen Chingri.', '~/Images/products/chingri-frozen.jpg', 1, 2, 5, 2, 4, 1, 2, 80),
    ('Pangash Large', 150.00, 'Large Pangash fish.', '~/Images/products/pangash-large.jpg', 1, 2, 6, 2, 5, 2, 2, 90);
    PRINT 'Fish products inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error inserting fish products: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Products(ProductName, Price, Description, ImageUrl, IsActive, ProductCategoryId, ProductSubCategoryId, UnitId, BrandId, ProductSupplierId, DiscountId, Quantity)
    VALUES 
    ('Bengal Meat Beef', 800.00, 'Fresh Bengal Meat beef.', '~/Images/products/bengal-meat-beef.jpg', 1, 3, 7, 2, 1, 1, 3, 40),
    ('Bengal Meat Mutton', 1000.00, 'Fresh Bengal Meat mutton.', '~/Images/products/bengal-meat-mutton.jpg', 1, 3, 7, 2, 2, 2, 3, 50),
    ('Boiler Chicken', 150.00, 'Fresh boiler chicken.', '~/Images/products/boiler-chicken.jpg', 1, 3, 8, 2, 3, 3, 3, 60),
    ('Beef Stomach Clean', 500.00, 'Clean beef stomach.', '~/Images/products/beef-stomach-clean.jpg', 1, 3, 9, 2, 4, 1, 3, 70),
    ('Beef Stomach Raw', 450.00, 'Raw beef stomach.', '~/Images/products/beef-stomach-raw.jpg', 1, 3, 9, 2, 5, 2, 3, 80);
    PRINT 'Meat products inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error inserting meat products: ' + ERROR_MESSAGE();
END CATCH

BEGIN TRY
    INSERT INTO Products(ProductName, Price, Description, ImageUrl, IsActive, ProductCategoryId, ProductSubCategoryId, UnitId, BrandId, ProductSupplierId, DiscountId, Quantity)
    VALUES 
    ('Malta Fresh', 100.00, 'Fresh Malta.', '~/Images/products/malta-fresh.jpg', 1, 4, 10, 2, 1, 1, 1, 90),
    ('Guava Fresh', 60.00, 'Fresh Guava.', '~/Images/products/guava-fresh.jpg', 1, 4, 11, 2, 2, 2, 1, 100),
    ('Apple Red', 150.00, 'Fresh Red Apples.', '~/Images/products/apple-red.jpg', 1, 4, 12, 2, 3, 3, 1, 110),
    ('Apple Green', 140.00, 'Fresh Green Apples.', '~/Images/products/apple-green.jpg', 1, 4, 12, 2, 4, 1, 2, 120),
    ('Malta Sweet', 110.00, 'Sweet Malta.', '~/Images/products/malta-sweet.jpg', 1, 4, 10, 2, 5, 2, 1, 130);
    PRINT 'Fruits products inserted successfully.';
END TRY
BEGIN CATCH
    PRINT 'Error inserting fruits products: ' + ERROR_MESSAGE();
END CATCH


select * from Brands

select * from ProductCategories

select * from ProductSubCategories

select * from Units

select * from ProductSuppliers

select * from Products
