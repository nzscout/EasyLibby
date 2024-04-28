# EasyLibby

##### Library Web API

## How to run

#### Running SQL Server in Docker

Run SQL Server inside a Docker container using the following command:

```powershell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=ProudClassMaximum68" -p 1445:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

For the local SQL Server, update ***DefaultConnection*** in **<u>appsettings.json</u>**.

Database migrations and data seeding are applied at project startup.

#### Optional: Running Seq Server in Docker

```powershell
docker run --name seq -d --restart=unless-stopped -e ACCEPT_EULA=Y -v seq:/data -p 81:80 -p 5345:5341 datalust/seq:latest
```

Access the Seq web interface: <http://localhost:81/>

## Design Decisions and Rationale

1. **Minimal APIs**:
  Minimal API is now a recommended approach for implementing Web APIs, due to their superior performance and reduced resource consumption compared to MVC-based APIs.
  
2. **Repository Pattern**:
  The project implements a custom repository pattern. Although EF Core already implements the repository and Unit of Work patterns, this additional complexity is optional and can be removed if there are no plans to replace EF Core with another data access framework.
  
3. **User Settings**:
  Currently, most settings are stored in `appsettings.json`. For production environments, they will be moved to the database.
  
4. **Using Auto Mappers**:
  The Mapperly library is utilized for mapping entity objects to DTOs. Generally, auto mappers like AutoMapper are discouraged due to reflection overheads and performance penalties. However, Mapperly is distinct as it is a source generator that operates without reflection at runtime, minimising its impact on performance.
  
5. **Security**:
  The API currently lacks protection measures. For production, a robust mechanism for user identification and resource authorisation should be implemented based on JWT-Based Authentication. Compatible OpenID providers include Auth0, Azure AD, or IdentityServer. Also, user registration and management should be added.
  
6. **Logging**:
  Serilog is used for structured logging, with configurations for Console and Seq sinks. The log level is set to verbose, with high overhead and should be adjusted for the production environment.
  
7. **Testing**:
  Due to the minimal business logic, unit tests are not required. Instead, functional tests were implemented for BooksEndpoints to ensure comprehensive test coverage.
  
8. **Validation**:
  Currently, only minimal validations are implemented.
  

### Actors

#### User (public)

- Does not require authorisation.
- Can browse and search books.

#### Member

- Requires authorisation.
- Can browse and search books.
- Can borrow available books.
- Can return previously borrowed books.
- Can renew already borrowed books.

#### Librarian

- Requires authorisation.
- Can add, delete, and update members.
- Can add, delete, and update books.
- Can add, delete, and update authors.
