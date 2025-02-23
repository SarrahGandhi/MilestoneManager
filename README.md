# Wedding Planning Manager

## Overview

The **Wedding Planning Manager** is a web application built using **.NET 8.0 (MVC with Individual Accounts)**. It helps wedding organizers manage events, tasks, and guest lists efficiently. The system allows **admins from both the bride's and groom's side** to collaborate and oversee the wedding planning process.

## Features

- **Authentication & Authorization**: Secure login system with Individual Accounts.
- **Admin Management**: Separate admins for bride's and groom's sides.
- **Event Management**: Create and manage wedding events with due dates.
- **Task Management**: Assign tasks to admins with due dates.
- **Guest List Management**: Track guest invitations and RSVP status.
- **Event-Guest Relationship**: Connect guests to specific events.
-

## Technologies Used

- **Framework**: .NET 8.0 (MVC)
- **Database**: Microsoft SQL Server (Entity Framework Core)
- **Frontend**: Razor Views, HTML, CSS, JavaScript
- **Authentication**: Identity Framework (Individual Accounts)
- **Hosting**: XAMPP (For local MySQL testing)

## Database Schema

### Tables:

1. **Admins**: Stores admin details for both bride's and groom's side.
2. **Events**: Contains wedding events and due dates.
3. **Tasks**: Lists tasks assigned to admins.
4. **Guests**: Stores guest details and invitation status.
5. **EventGuest**: Many-to-many relationship tracking which guests are attending which events.



### Prerequisites

- .NET 8.0 SDK
- Microsoft SQL Server
- Visual Studio (or VS Code with C# extension)
- XAMPP (for MySQL testing on macOS)

### Setup Steps

1. **Clone the Repository**
   ```sh
   git clone https://github.com/SarrahGandhi/MilestoneManager.git
   cd wedding-planner
   ```
2. **Set Up Database**
   ```sh
   dotnet ef database update
   ```
3. **Run the Application**
   ```sh
   dotnet run
   ```

## API Endpoints

| Endpoint           | Method | Description          |
| ------------------ | ------ | -------------------- |
| `/api/events`      | GET    | Get all events       |
| `/api/events/{id}` | GET    | Get a specific event |
| `/api/tasks`       | GET    | Get all tasks        |
| `/api/guests`      | GET    | Get all guests       |


## Contact

- **Developer**: Sarrah Gandhi
- **Portfolio**: [www.sarrahgandhi.com](https://www.sarrahgandhi.com)
- **Email**: thesarrahgandhi@gmail.com
