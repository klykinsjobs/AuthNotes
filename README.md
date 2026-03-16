# AuthNotes

AuthNotes is a .NET 10 WinForms application built to demonstrate secure authentication, role‑based authorization, and reliable CRUD operations within a clean, layered architecture. The solution separates concerns across Domain, Application, Infrastructure, and UI layers, using Entity Framework Core with SQLite, dependency injection, and repository abstractions to create a maintainable and testable codebase. The application initializes with seeded users, enforces unique constraints, and provides a straightforward interface for managing notes and users.

The project includes a comprehensive automated test suite with more than sixty unit and integration tests validating authentication logic, repository behavior, domain rules, and end‑to‑end service interactions. Some of these tests use in‑memory SQLite to verify real database operations and ensure correctness across layers. AuthNotes serves as a compact but production‑style example of secure application design, clean architecture, asynchronous programming, and test‑driven engineering within the .NET ecosystem.

## Prerequisites
- .NET 10 SDK
- Windows 10/11

## Getting started
- Clone the repository
- Press F5 in Visual Studio to build and start the application
- Use one of the seeded accounts:
   - **guest / 123**  
   - **admin / password**

### Tests
- Click View and then click Test Explorer
- Click Run All Tests In View

## License
AuthNotes is licensed under the MIT license. See the LICENSE file for details.

[![Build and Test](https://github.com/klykinsjobs/AuthNotes/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/klykinsjobs/AuthNotes/actions/workflows/build-and-test.yml)
