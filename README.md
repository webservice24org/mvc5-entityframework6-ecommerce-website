<h2>Overview</h2>
This is an ASP.NET MVC 5 E-commerce website designed to provide a comprehensive shopping experience for users and a robust management system for administrators. The project includes features for managing products, categories, brands, suppliers, discounts, and orders. Users can browse products, place orders, and manage their profiles.

<h2>Features</h2>h2>
<h3>Admin Panel</h3>
<strong>Admins </strong> have a wide range of features to manage the e-commerce platform effectively:

<strong>Brand Management:</strong> Edit, update, and delete brands.
<strong>Product Category Management:</strong> Edit, update, and delete product categories.
<strong>Product Sub Category Management:</strong> Edit, update, and delete product subcategories.
<strong>Unit Management:</strong> Edit, update, and delete units.
<strong>Supplier Management:</strong> Edit, update, and delete suppliers.
<strong>Discount Management:</strong> Edit, update, and delete discounts.
<strong>Product Management:</strong> Edit, update, and delete products.
<strong>Order Management:</strong> View and update orders.
<strong>Customer Management:</strong> Manage customer accounts.

<h2>Frontend</h2>
The frontend provides users with a seamless shopping experience:

<strong>Product Ordering:</strong> Users can browse and order products.
<strong>Product Details:</strong> View detailed information about products.
<strong>User Profile Management:</strong> Users can manage their profiles.
<strong>Order Tracking:</strong> Users can see their order status in their profile panel.
<strong>Authentication:</strong> Users can register and log in from the front page.

<h2>How to Run this Project</h2>
Follow these steps to set up and run the project on your local machine:
<ul>
  <li>Extract the ZIP folder.</li>
  <li>Delete all files and folders inside the BIN folder.</li>
  <li>Delete all files and folders inside the BIN folder.</li>
  <li>Open the project in Visual Studio.</li>
  <li>
    Configure the Database:
    <ul>
      <li>Open Web.config.</li>
      <li>Open SQL Server Management Studio and copy the server name.</li>
      <li>Change the data source in Web.config to your SQL Server instance (only change the data source, leave other settings as they are).</li>
    </ul>
  </li>
  <li><h2>Update Database:</h2></li>
  
</ul>









Open Tools -> NuGet Package Manager -> Package Manager Console.
Run the command: update-database.
Seed Demo Data:

Open insert-demo-data.sql file (located in the project folder) in SQL Server Management Studio.
Press Ctrl+A to select all, then click Execute or press F5.
Run the Project:

Move to Visual Studio and open HomeController.
Run the project.
Admin Login:

Username: admin@example.com
Password: Admin@123
Customer Login:

Username: customer@example.com
Password: Customer@123
Now the E-commerce project will work fine!
