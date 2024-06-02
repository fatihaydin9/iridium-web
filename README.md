## 1. What Is Iridium Web?
Iridium Web is a monolithic software infrastructure that I've developed using cutting-edge approaches like Domain Driven Design, Clean Architecture, CQRS, and Docker. I designed it to include a wide range of sophisticated functionalities such as detailed logging, an advanced role management system, effective caching solutions, authentication and authorization mechanisms.

### 1.1. DDD (Domain Driven Design)
I implemented Domain Driven Design in Iridium Web to make sure the development process closely matches the complex and changing needs of modern businesses. This approach helps create a common language between me, other developers and business stakeholders, greatly improving our communication and understanding. It simplifies maintenance by organizing the system into clear bounded contexts each designed to tackle specific business complexities. Plus, DDD ensures that the architecture can easily adapt to new business strategies.

### 1.2. CQRS (Command Query Responsibility Segregation) Pattern
In Iridium Web I've integrated the CQRS pattern, which becomes more and more crucial as the complexity of the application increases. This pattern divides data operations into distinct commands (writes) and queries (reads) boosting performance and scalability. It also enhances security by isolating sensitive operations and allows me to customize technologies and database schemas that are best suited for each side's needs.

### 1.3. MediatR and the Mediator Pattern
I chose MediatR is a well-known .NET library that implements the mediator pattern, to reduce the direct dependencies among components and simplifying their communication. By decoupling the application’s components, MediatR lets them operate more independently interacting through a central mediator. This reduces dependency and improves both maintainability and scalability. It also supports the Single Responsibility Principle by managing separate logic through discrete handlers, each responsible for a specific command or query.

## 2. Features Overview

#### 2.1. AuditLog & Standard Fields
In Iridium Web, I integrated an Entity Change Tracker that meticulously records every change, capturing detailed snapshots of entities before and after any modifications. This feature is crucial for ensuring compliance and aiding debugging. The system also automatically manages standard fields such as `CreatedBy`, `CreatedDate`, `ModifiedBy`, and `ModifiedDate`, essential for maintaining data accountability and integrity.

#### 2.2. Role System
At startup, I make sure Iridium Web synchronizes predefined roles from the codebase to the database using a code-first migration strategy. This minimizes manual work and boosts security by regulating access based on well-defined parent-child roles.

#### 2.3. Authentication & Authorization
I maintain security within Iridium Web using JWT Bearer Tokens, which are securely signed with symmetric keys. This strong authentication is supported by a dynamic role-based access control system, ensuring all actions within the application strictly adhere to established security policies.

#### 2.4. Fluent Validation System
To keep data integrity and simplify the validation process, I use the FluentValidation library in Iridium Web. This allows me to set up clear and comprehensive validation rules, making the code easier to read and maintain.

#### 2.5. AutoMapper Integration
I've also integrated AutoMapper into Iridium Web to simplify the mapping of data between objects, greatly reducing the need for manual coding. This is especially helpful when dealing with complex data structures and Data Transfer Objects (DTOs), ensuring a clear separation between data layers and business logic.

## 3. Setup
After launching the application using Docker Compose, follow these steps to create a new database called 'Iridium' and set up the necessary tables and roles:
Change to the directory where the `Iridium.Infrastructure` project is located. Use the following command:

```bash
cd ../{IridiumPath}/Iridium.Persistence
```
Now, create a new database migration named InitialCreate. This migration will include all the necessary database schema changes based on your model. Run the following command:
```bash
dotnet ef migrations add InitialCreate
```
Apply changes using dotnet ef database update command:
```bash
dotnet ef database update
```

Now, restart the application to ensure that all roles are properly imported and the system is ready to be used.

With these steps, your Iridium application setup is complete!

I hope you like it! <br>
All the best, <br>
Fatih
