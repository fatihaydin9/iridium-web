# Iridium Web

## 1. What Is Iridium Web?
Iridium Web is a monolithic software infrastructure developed using advanced methodologies such as Domain Driven Design (DDD), Clean Architecture, Command Query Responsibility Segregation (CQRS), and Docker. It encompasses a wide range of sophisticated features including detailed logging, an advanced role management system, effective caching solutions, authentication, and authorization mechanisms.

### 1.1. Domain Driven Design (DDD)
Iridium Web leverages Domain Driven Design to ensure the development process aligns with the complex and evolving needs of modern businesses. This approach fosters a common language between developers and business stakeholders, enhancing communication and understanding. It simplifies maintenance by organizing the system into clear bounded contexts, each tailored to address specific business complexities. Additionally, DDD ensures that the architecture can easily adapt to new business strategies.

### 1.2. Command Query Responsibility Segregation (CQRS)
The CQRS pattern is integrated into Iridium Web to handle increasing application complexity. By separating data operations into commands (writes) and queries (reads), CQRS boosts performance and scalability. It also enhances security by isolating sensitive operations and allows for customization of technologies and database schemas best suited for each operation.

### 1.3. MediatR and the Mediator Pattern
MediatR, a well-known .NET library implementing the mediator pattern, is used to reduce direct dependencies among components and simplify their communication. By decoupling the application's components, MediatR allows them to operate independently, interacting through a central mediator. This reduces dependency, improves maintainability, and enhances scalability. It also supports the Single Responsibility Principle by managing separate logic through discrete handlers, each responsible for a specific command or query.

## 2. Features Overview

### 2.1. AuditLog & Standard Fields
Iridium Web integrates an Entity Change Tracker that meticulously records every change, capturing detailed snapshots of entities before and after modifications. This feature is crucial for ensuring compliance and aiding in debugging. The system also automatically manages standard fields such as `CreatedBy`, `CreatedDate`, `ModifiedBy`, and `ModifiedDate`, essential for maintaining data accountability and integrity.

### 2.2. Role System
At startup, Iridium Web synchronizes predefined roles from the codebase to the database using a code-first migration strategy. This minimizes manual work and enhances security by regulating access based on well-defined parent-child roles.

### 2.3. Authentication & Authorization
Iridium Web maintains security using JWT Bearer Tokens, securely signed with symmetric keys. This robust authentication is supported by a dynamic role-based access control system, ensuring all actions within the application strictly adhere to established security policies.

### 2.4. Fluent Validation System
To maintain data integrity and simplify the validation process, Iridium Web uses the FluentValidation library. This allows for clear and comprehensive validation rules, making the code easier to read and maintain.

### 2.5. AutoMapper Integration
AutoMapper is integrated into Iridium Web to simplify data mapping between objects, significantly reducing the need for manual coding. This is particularly helpful when dealing with complex data structures and Data Transfer Objects (DTOs), ensuring a clear separation between data layers and business logic.

## 3. Setup
After launching the application using Docker Compose, follow these steps to create a new database called 'Iridium' and set up the necessary tables and roles:

1. Navigate to the directory where the `Iridium.Infrastructure` project is located:
    ```bash
    cd ../{IridiumPath}/Iridium.Persistence
    ```

2. Create a new database migration named `InitialCreate`, which includes all necessary database schema changes based on your model:
    ```bash
    dotnet ef migrations add InitialCreate
    ```

3. Apply the changes using the `dotnet ef database update` command:
    ```bash
    dotnet ef database update
    ```

4. Restart the application to ensure all roles are properly imported and the system is ready for use.

With these steps, your Iridium application setup is complete!

I hope you find this project useful!

Best regards,  
Fatih
