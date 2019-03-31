# davidedolce.com

This is the open source project of my personal website. It is not avaiable at the moment but I am planning to realease it in the future to the web.

The website is built using the following technologies:

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [Razor](https://docs.microsoft.com/en-us/aspnet/web-pages/overview/getting-started/introducing-razor-syntax-c) as view engine to create dynamic web pages.
* [Asp.Net Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-2.2)
* [Visible Recaptcha](https://www.google.com/recaptcha/intro/v3.html) for contact validation
* [SendGrid](https://sendgrid.com/) for delivery service email

## Installation

* Install [.Net Core 2.1 or later](https://dotnet.microsoft.com/download)
* Install the version 2.1.0 of [Microsoft.AspNetCore.All package](https://www.nuget.org/packages/Microsoft.AspNetCore.All)
* Install the last version of [SendGrid package](nuget.org/packages/Sendgrid)

### Install NuGet packages with CLI

```
dotnet add package Microsoft.AspNetCore.All --version 2.1.0

dotnet add package Sendgrid --version 9.10.0
```

### Install NuGet packages with VisualStudio

Open the project by clicking on the davidedolce.com.sln file, right click on the solution and click on **Manage NuGet Packages for Solution** and search the two packages that I mentioned above.

## Configuration

In order to run the web site you need to have an API key from [SendGrid](https://sendgrid.com/) and a client and a secret value from [Visible reCAPTCHA](https://www.google.com/recaptcha/intro/v3.html).

These values needs to be stored in the **appsettings.json** file.

If you want to add a new post for the blog section, you need to follow these steps:

1. Create a new file with cshtml extension in __/Views/Blog__, for example **MyNewAwesomeBlog.cshtml**
2. Add a new class of **IActionResult** type in __/Controllers/Blog.Controller.cs__, set the attribute routing to get and set the string in this way **my-new-awesome-blog**
3. Add a new **Article** in the **Articles** class which is in __/Models/FakeArticleRepository.cs__, for example:
```csharp
new Article {ArticleID = "5", Title = "My new awesome blog", SubTitle = "This is a subtitle, change it later", PubDate = "March 9, 2019"}
```

### Note

The title value in the **Articles** list, must contain white spaces because when the link will be generated to the web page it will change the white spaces to **-**, in this way your article link will be directly set to the **HttpGet** attribute that you defined in the **BlogController.cs**.

## Run

With **CLI** navigate to the path where your project is located, enter into __davidedolce.com__ directory and run the following command:

```
dotnet run
```

Check the CLI output to get the correct port for your localhost.

With **VisualStudio** you just need to open **davidedolce.com.sln** file and press on your keyboard **CTRL + F5** to start the website without debugging.

If you want to deploy the website in a production environment you need to use **wwwroot** as web directory.
