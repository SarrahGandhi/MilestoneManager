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
- **Role-Based Access**: Limited access for non-primary admins.

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

## Installation
### Prerequisites
- .NET 8.0 SDK
- Microsoft SQL Server
- Visual Studio (or VS Code with C# extension)
- XAMPP (for MySQL testing on macOS)

### Setup Steps
1. **Clone the Repository**
   ```sh
   git clone https://github.com/yourusername/wedding-planner.git
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
4. **Access the Web App**
   Open `http://localhost:5000` in your browser.

## API Endpoints
| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/events` | GET | Get all events |
| `/api/events/{id}` | GET | Get a specific event |
| `/api/tasks` | GET | Get all tasks |
| `/api/guests` | GET | Get all guests |

## Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature-name`)
3. Commit your changes (`git commit -m "Added new feature"`)
4. Push to the branch (`git push origin feature-name`)
5. Open a Pull Request

## License
This project is licensed under the **MIT License**.

## Contact
- **Developer**: Sarrah Gandhi
- **Portfolio**: [www.sarrahgandhi.com](https://www.sarrahgandhi.com)
- **Email**: thesarrahgandhi@gmail.com

