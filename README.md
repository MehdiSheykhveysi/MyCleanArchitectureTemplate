# MyCleanArchitectureTemplate

A wonderful implementation of clean architecture.
I Implement CRUD Functionality using amazing library [MediatR](https://github.com/jbogard/MediatR) and CQRS also use built in
Aspnet Core DI Container with Assembly scanning and decoration extensions for Microsoft.Extensions.DependencyInjection is named [Scrutor](https://github.com/khellang/Scrutor)

# Important features there are in this project as follows

  * Config Fricikng [AutoMapper Version 9](https://docs.automapper.org/en/stable/9.0-Upgrade-Guide.html) using Facade pattern
  * Auto Register Dbset By Reflection
  * Use SequenceHilo Pattern For Generate Keys has type(int,..)
  * Use Sequential Guid For Keys Has Type Guid
  * Generic Repository with Ef Core
  * Generic CacheRepository with proxy Pattern
  * Exception handling with writing custom middleware
 
 And other features I'll add them in the future.
