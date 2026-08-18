# Hotel Room Booking API

A RESTful API built with ASP.NET Core for managing hotel room bookings. This project was developed to demonstrate clean architecture, separation of concerns, and robust API design.

## How to Run the Application

1. **Clone the repository:**
   ```bash
   git clone "https://github.com/MikageReo/HotelRoomBooking.git"
   ```
2. **Open the project in Visual Studio**

3. **Restore NuGet packages:**
   - Right-click on the solution in Solution Explorer and select "Restore NuGet Packages".
	- Alternatively, you can run the following command in the terminal:
   ```bash
   dotnet restore
   ```

4. **Run the application:**
   - Press `F5` or click on the "Run" button in Visual Studio to start the application.

5. **Test via Swagger UI:**
   - Once the application is running, navigate to `https://localhost:<port>/swagger` in your web browser to access the Swagger UI for testing the API endpoints.

**Testing (Test-Driven Development)**
This project was built utilizing a Test-Driven Development (TDD) approach. Tests were written prior to implementing the core business logic to ensure that all requirements were met by design and to maintain high code reliability.

The unit tests are built using xUnit alongside Moq for mocking dependencies. The test suite provides robust coverage across the application layers, specifically targeting:

- **Controllers:** BookingsControllerTest and RoomsControllerTest to verify correct HTTP status codes (201 Created, 400 Bad Request, 200 OK) and proper response formatting.
- **Services:** BookingServiceTest and RoomServiceTest to validate complex business logic, correct database model translation, and DTO mapping.

**Explanation of Design Decisions**
- **Repository and Service Patterns**: I separated the data access logic (Repositories) from the business logic (Services). This makes the application highly testable and ensures the Controllers remain lightweight, only handling HTTP requests and responses.

- **Dependency Injection**: Used extensively to inject repositories into services, and services into controllers. This promotes loose coupling and allows for easy unit testing using Moq.

- **BookingResult Wrapper**: Instead of returning raw data or throwing exceptions for business logic errors (like booking an unavailable room), I implemented a BookingResult class. This securely passes success states, messages, and data back to the controller to formulate the correct HTTP response (e.g., 201 Created vs. 400 Bad Request).

**Suggested Features Implemented**
**1. Data Transfer Objects (DTOs) & Data Validation**
- Implementation: Replaced raw database entity models (Booking, Room) with specific DTOs (CreateBookingRequestDto, BookingResponseDto, RoomResponseDto). I also implemented IValidatableObject and built-in Data Annotations on the request DTO to ensure data integrity (e.g., ensuring CheckOutDate is strictly after CheckInDate).

Reason:
- Prevents "over-posting" vulnerabilities where a user could manipulate internal fields (like an Id).
- Solves Entity Framework JSON serialization cycle errors (A possible object cycle was detected).
- ModelState automatically intercepts bad requests before they hit the controller, saving server resources.

**2. Database Seeder**
- Implementation: Added a DatabaseSeeder that runs on application startup to automatically populate the in-memory database with initial Room records.

**3. FluentValidation**
- Instead of pulling in a third-party library for complex validation rules, I utilized the built-in IValidatableObject interface.

