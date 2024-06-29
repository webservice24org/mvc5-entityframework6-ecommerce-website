<h2>Overview</h2>
This is an ASP.NET MVC 5 E-commerce website designed to provide a comprehensive shopping experience for users and a robust management system for administrators. The project includes features for managing products, categories, brands, suppliers, discounts, and orders. Users can browse products, place orders, and manage their profiles.

## Video Demonstration

Check out our [video demonstration](https://www.youtube.com/watch?v=8Q7e4RHjsoE) on YouTube.

[![Watch the video](https://img.youtube.com/vi/8Q7e4RHjsoE/0.jpg)](https://www.youtube.com/watch?v=8Q7e4RHjsoE)


<h2>Features</h2>
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
  <li><h2>Update Database:</h2>
    <ul>
      <li>Open Tools -> NuGet Package Manager -> Package Manager Console.</li>
      <li><strong>Run the command:</strong> update-database.</li>
    </ul>
  </li>
  
</ul>

<h2>Seed Demo Data:</h2>
<ul>
  <li>Open insert-demo-data.sql file (located in the project folder) in SQL Server Management Studio.</li>
  <li>Press Ctrl+A to select all, then click Execute or press F5.</li>
</ul>
<h2>Run the Project:</h2>
<u>
  <li>Move to Visual Studio and open HomeController.</li>
  <li>Run the project.</li>
</u>

<h2>Admin Login:</h2>
Username: admin@example.com
Password: Admin@123

<h2>Customer Login:</h2>
Username: customer@example.com
Password: Customer@123

Now the E-commerce project will work fine!

<h1>Alternative Options</h1>
<strong>If update-database does not work, follow these steps:</strong>

Delete the Migrations Folder.
Enable Migrations:
Run the command: Enable-Migrations.
Create Initial Migration:
Run the command: Add-Migration mohiuddin-ecommerce.
Seed User Data:

Add the following code to the configuration file to seed admin and customer users:
<br />

var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

if (!roleManager.RoleExists("Admin"))
{
    roleManager.Create(new IdentityRole("Admin"));
}

if (!roleManager.RoleExists("Customer"))
{
    roleManager.Create(new IdentityRole("Customer"));
}

if (userManager.FindByName("admin@example.com") == null)
{
    var adminUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
    var result = userManager.Create(adminUser, "Admin@123");

    if (result.Succeeded)
    {
        userManager.AddToRole(adminUser.Id, "Admin");
    }
}

if (userManager.FindByName("customer@example.com") == null)
{
    var customerUser = new ApplicationUser { UserName = "customer@example.com", Email = "customer@example.com" };
    var result = userManager.Create(customerUser, "Customer@123");

    if (result.Succeeded)
    {
        userManager.AddToRole(customerUser.Id, "Customer");
    }
}


Run Update-Database:

Run the command: Update-Database.
If Needed:

Check the database catalog name in Web.config.
Once completed, run the project from the HomeController.
