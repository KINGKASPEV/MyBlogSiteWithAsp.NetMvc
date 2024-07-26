# MyBlogSiteWithAsp.NetMvc


Blog Website
This project is a fully functional blog website built using ASP.NET MVC. It allows users to read, like, comment on, and share blog posts. The website is designed with a clean and responsive layout, ensuring a great experience across various devices.


Key Features
User Authentication: Secure login and registration system with ASP.NET Identity.
Content Management: Admin panel for managing blog posts, categories, and comments.
Rich Text Editor: Easily create and format posts using a built-in rich text editor.
Responsive Design: Optimized for both desktop and mobile devices.
SEO Friendly: Implemented SEO best practices to enhance visibility on search engines.
Tagging System: Categorize and tag posts for easy navigation and search.


Technologies Used
ASP.NET MVC: For the core web framework.
Entity Framework Core: For database management and data access.
SQL Server: As the primary database.
Bootstrap: For responsive front-end design.
JavaScript/jQuery: For dynamic interactions and functionality.
HTML/CSS: For the structure and styling of the website.


Getting Started
To run this project locally, follow these steps:

Clone the repository:

git clone https://github.com/KINGKASPEV/MyBlogSiteWithAsp.NetMvc.git
Navigate to the project directory:

cd Bloggie.Web


Restore the dependencies:


dotnet restore


Update the connection string in appsettings.json to match your local SQL Server setup.


Apply the latest migrations to create the database schema:


dotnet ef database update


Run the application:

dotnet run


Contributing


Contributions are welcome! If you have any suggestions or improvements, feel free to submit a pull request or open an issue.
