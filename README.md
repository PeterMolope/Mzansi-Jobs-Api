# Mzansi Jobs API

A secure, modular REST API for connecting candidates, employers, companies, and job opportunities across South Africa.

Mzansi Jobs API is built with **.NET 10**, **ASP.NET Core**, **Entity Framework Core**, **PostgreSQL**, **MediatR**, **JWT authentication**, and **Swagger UI**. The project demonstrates clean separation of concerns, domain-driven organization, role-based authorization, and production-oriented API practices.

## Why This Project Matters

Mzansi Jobs API models a complete recruitment workflow:

- Candidates create profiles and apply for jobs.
- Employers create companies and publish job opportunities.
- Companies are associated with employers and job postings.
- Applications connect candidates to jobs.
- Employers and administrators manage recruitment activity.
- JWT authentication protects role-specific operations.
- Swagger UI provides an interactive API testing experience.

The project is structured to remain maintainable as the platform grows.

---

## Key Features

### Authentication and Authorization

- User registration for candidates and employers.
- JWT access-token authentication.
- Refresh-token persistence.
- BCrypt password hashing.
- Role-based authorization:
  - `Candidate`
  - `Employer`
  - `Admin`
- Protected endpoints using ASP.NET Core authorization policies.

### Candidate Management

- Candidate profile creation and updates.
- Candidate contact details and professional summaries.
- Resume URL support.
- Candidate job applications.
- Candidate application history.

### Employer and Company Management

- Employer account registration.
- Company creation and updates.
- Company industry and province classification.
- Company-to-employer relationships.
- Company-to-job relationships.

### Job Management

- Employer job posting.
- Job search and pagination.
- Province and employment-type filtering.
- Active job filtering.
- Salary and expiration-date support.
- Public job discovery endpoints.

### Application Management

- Candidates apply for jobs.
- Duplicate applications are prevented.
- Employers view applications for their jobs.
- Employers update application status.
- Candidate and employer application views.

### API and Infrastructure

- PostgreSQL persistence through EF Core.
- Repository and Unit of Work patterns.
- MediatR command/query handling.
- Global exception middleware.
- RFC 7807 Problem Details responses.
- Swagger UI with JWT Bearer support.
- CORS configuration.
- Enum serialization using readable string values.

---

## Technology Stack

| Category | Technology |
|---|---|
| Runtime | .NET 10 |
| API | ASP.NET Core Web API |
| Language | C# |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Database Provider | Npgsql |
| Application Pattern | CQRS-style MediatR handlers |
| Authentication | JWT Bearer |
| Password Hashing | BCrypt.Net-Next |
| API Documentation | Swagger / OpenAPI |
| Testing | xUnit |
| IDE | Visual Studio |

---

## Architecture

The solution is separated into focused projects:
