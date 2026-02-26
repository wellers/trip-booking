# Trip Booking API

A production-style .NET 8 REST API demonstrating secure authentication, layered architecture, and clean separation of concerns for a trip booking domain.

This project showcases modern backend engineering practices including JWT authentication, Entity Framework Core, dependency injection, and structured solution design.

## Design Principles

* Clear separation between API, domain logic, and data access
* Dependency Injection throughout
* DTO-based API contracts (no direct entity exposure)
* Centralised authentication and authorization
* Testable service layer

## Authentication

The API uses JWT Bearer Authentication to secure endpoints.

### Authentication Flow
1. User submits credentials to /auth/login
2. API validates credentials
3. JWT token is generated and returned
4. Client includes token in subsequent requests:

`Authorization: Bearer {your_token}`

Protected endpoints require a valid token.