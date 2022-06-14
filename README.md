# Learning ASP.NET Core 2.0
This is the code repository for [Learning ASP.NET Core 2.0](https://www.packtpub.com/application-development/learning-aspnet-core-20?utm_source=github&utm_medium=repository&utm_campaign=9781788476638), published by [Packt](https://www.packtpub.com/?utm_source=github). It contains all the supporting project files necessary to work through the book from start to finish.
## About the Book
Being able to develop web applications that are highly efficient while easy to maintain has become imperative in many businesses. ASP.NET Core 2.0 is an open source framework from Microsoft, which makes it easy to build cross-platform web applications that are modern and dynamic. This book will take you through all of the essential concepts in ASP.NET Core 2.0, so you can learn how to build powerful web applications.


## Instructions and Navigation
All of the code is organized into folders. Each folder starts with a number followed by the application name. For example, Chapter02.

All the code files are placed in their respective code folders. Chapters 1, 2, and 3 do not have any code files.

The code will look like the following:
```
[HttpGet] 
    public IActionResult EmailConfirmation (string email) 
    { 
      ViewBag.Email = email; 
      return View(); 
    } 
```

You will either need Visual Studio 2017 Community Edition or Visual Studio Code, which are both free of charge for testing and learning purposes, to be able to follow the code examples found within this book. You could also use any other text editor of your choice and then use the dotnet command-line tool, but it would be advised to use one of the development environments mentioned earlier for better productivity.

Later in the book, we will work with databases, so you will also need a version of SQL Server (any version in any edition will work). We advise using SQL Server 2016 Express Edition, which is also free of charge for testing purposes.

There might be other tools or frameworks that will be introduced during the following chapters. We will explain how to retrieve them when they are used.

If you need to develop for Linux, then Visual Studio Code and SQL Server 2016 are your primary choices, since they are the only ones running on Linux.

Additionally, you will need an Azure Subscription and Amazon Web Services Subscription for some of the examples shown within the book. There are multiple chapters dedicated to show you how to take advantage of the cloud.

## Related Products
* [Learning ASP.NET Core MVC Programming](https://www.packtpub.com/application-development/learning-aspnet-core-mvc-programming?utm_source=github&utm_medium=repository&utm_campaign=9781786463838)

* [Mastering ASP.NET Core 2.0](https://www.packtpub.com/application-development/mastering-aspnet-core?utm_source=github&utm_medium=repository&utm_campaign=9781787283688)

* [C# 7.1 and .NET Core 2.0 – Modern Cross-Platform Development - Third Edition](https://www.packtpub.com/application-development/c-71-and-net-core-20-–-modern-cross-platform-development-third-edition?utm_source=github&utm_medium=repository&utm_campaign=9781788398077)

### Suggestions and Feedback
[Click here](https://docs.google.com/forms/d/e/1FAIpQLSe5qwunkGf6PUvzPirPDtuy1Du5Rlzew23UBp2S-P3wB-GcwQ/viewform) if you have any feedback or suggestions.
