# Plant Care Scheduler API

## Overview
The Plant Care Scheduler API is designed to help users manage plant care schedules by tracking watering dates, calculating the next watering date, and determining the current status of plants. The API follows the principles of **Clean Architecture**, ensuring modularity, maintainability, and scalability.

---

## Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Azure Cosmos DB](https://azure.microsoft.com/en-us/products/cosmos-db)
- A modern IDE like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Steps to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/HC809/PlantCareScheduler.API.git
   cd PlantCareScheduler.API

# Why Cosmos DB?
The job description emphasized the importance of expertise in Azure and document-based databases. Cosmos DB was chosen as the ideal solution for this project due to its seamless integration with Azure services, scalability, and support for document-based data storage.


# Technical Overview

## Persistence
* Azure Cosmos DB is used for storing plant and watering data.
Containers:
* Plants: Stores plant data (e.g., name, type, watering frequency).
* Watering: Tracks individual watering events.

## Clean Architecture
* Domain: Core business logic and entities.
* Application: CQRS pattern for commands and queries.
* Infrastructure: Database access and integrations.
* API: Entry point for HTTP requests.

##Entities and Patterns
* Entities are implemented with the Static Factory Pattern and Factory Methods to ensure valid object creation.

## Entity Framework
* Cosmos DB integration via ApplicationDbContext.
* IEntityTypeConfiguration used for container mappings.

## Repositories and Unit of Work
Data access logic is centralized using the Repository Pattern.
* UnitOfWork ensures atomicity in database operations.

## Dependency Injection
* All services and repositories are registered via DIContainer.

## CQRS with MediatR
* Commands and queries are handled using the MediatR library for decoupled request-response handling.

## Error and Result Patterns
* Result<T> encapsulates success or failure in operations.
* Errors are handled via an ExceptionHandlingMiddleware.

## Validation Behavior
* Request validation is implemented with FluentValidation.

## Exception Handling
* Middleware ensures consistent error handling for all API operations.

## Business Logic
### Next Watering Date Calculation
* Formula: nextWateringDate = lastWateredDate + wateringFrequencyDays

### Plant Status Calculation
* OK: Today's date < nextWateringDate.
* Due Soon: nextWateringDate is within the next 2 days.
* Overdue: Today's date > nextWateringDate.

# Limitations
## Unit Tests: Not implemented due to time constraints. For testing, I typically use:
* xUnit: For unit testing.
* FluentAssertions: For expressive assertions.
* NetArchTest.Rules: For validating architectural boundaries.

##Future Improvements
* Add unit and integration tests for commands, queries, and controllers.
* Implement advanced filtering and search capabilities.
* Add a notification system to alert users of overdue watering tasks.
