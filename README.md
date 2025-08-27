# Nutrition Tracker Project 🥗
![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![React](https://img.shields.io/badge/React-19-blue)
![Docker](https://img.shields.io/badge/Docker-ready-blue)
![License](https://img.shields.io/github/license/kleideris/nutrition-tracker)
![Issues](https://img.shields.io/github/issues/kleideris/nutrition-tracker)
![Stars](https://img.shields.io/github/stars/kleideris/nutrition-tracker?style=social)
![Forks](https://img.shields.io/github/forks/kleideris/nutrition-tracker?style=social)


### A full-stack web application for logging meals, tracking nutritional intake, and managing a personalized food item database. Built with React and ASP.NET Core Web API, the app features secure JWT-based authentication and is fully containerized with Docker for streamlined deployment.

<br>

## Table of Contents
- [Core Features](#core-features-mvp)
- [Prerequisites](#prerequisites)
- [How To Run](#how-to-run)
  - [Run with Docker](#run-with-docker)
  - [Alternative Running Method (Without Docker)](#alternative-running-method-without-docker)
- [Environment Variables](#environment-variables)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Authentication Flow](#authentication-flow)
- [Tech Stack Used](#tech-stack-used)
- [Developer Notes](#developer-notes)
- [Troubleshooting](#troubleshooting)
- [License](#license)

<br>

## Core Features (MVP)
- Meal logging with macro breakdown
- Food item search & management
- Nutrition aggregation logic
- Entity Framework–based relational DB
- Swagger auto-generated docs
- JWT-based authentication
- Serilog logging
- admin and fooditem seeding at initial startup  (Security Note: these are only for development and shouldnt be used in a production environment!)


<br>

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Docker](https://www.docker.com/products/docker-desktop/) (for Docker method)

<br>

## How To Run

### Run with Docker:

Make sure Docker Desktop is installed on your machine and is running then follow these steps.

1. **- Clone and navigate:**  
   ```bash
   git clone https://github.com/kleideris/nutrition-tracker.git
   cd nutrition-tracker
   ```

2. create a `.env` in the root and populate it with the variables needed for the frontend and backend (follow the `.env.example` template)
   ```dotenv
   SA_PASSWORD=<your db sa password>
   DB_CONNECTION=<your db connection string>
   JWT_SECRET=<your JWT secret> 

   VITE_PORT=3000
   VITE_API_URL=http://localhost:5000/api
   APP_UID=1000
   ASPNETCORE_URLS=http://0.0.0.0:8080
   ```

3. **Build and run the application (the first run can take up to 10 minutes to setup docker)**
   ```bash
   docker-compose up --build
   ```

All done, the frontend runs at http://localhost:3000 and the backend API is accessed at http://localhost:5000/api

At startup, the db is automatically created if it doesn't exist, an admin is seeded if no admin users exist to give access to admin features and some test food items are automatically seeded if none exist.

#### Admin credential:
- **Username: admin**
- **Password: Admin.94**

<br>

### Alternative Running Method (Without Docker)
If you prefer or need to run the project manually, here’s how:

1. Generate Frontend & Backend .env Files
   You can auto-generate them from your root .env by running:

   ```bash
   cd _docker
   ./generate-envs.sh
   ```
   Make sure the script is executable:

   ```bash
   chmod +x _docker/generate-envs.sh
   ```

   Alternatively, create them manually from .env.example.

2. Start the Frontend
   ```bash
   cd client-frontend
   npm install
   npm run dev
   ```

3. Start the Backend
   ```bash
   cd server-backend/NutritionTracker.Api
   dotnet run
   ```

4. Create the SQL Server Database
You can manually create the NutritionTrackerDB or run the provided SQL script:
   ```bash
   sqlcmd -S <your_server> -U sa -P <your_password> -i _docker/init.sql
   ```

5. Inside client-frontend/package.json, you’ll find helpful scripts that let you deploy the app from the frontend after initial setup:
- npm run start
- npm run start:frontend
- npm run start:backend

<br>

### Environment Variables

#### Backend
- SA_PASSWORD - The password used to authenticate with the SQL Server database
- DB_CONNECTION - Full connection string for accessing the database (e.g., Server, Database, User ID, Password)
- JWT_SECRET - Secret key used to sign and verify JWT tokens for secure API authentication

#### Frontend
- VITE_PORT - Port number for running the Vite development server (default: 3000)
- VITE_API_URL - URL of the backend API that the frontend will communicate with (default: http://localhost:5000/api)

#### Docker
- APP_UID - User ID used inside the container to avoid permission issues (default: 1000)
- ASPNETCORE_URLS - URL binding for ASP.NET Core app inside the container (default: http://0.0.0.0:8080)

<br>>

## API Endpoints

### Authentication
- `POST /api/auth/login/access-token` - Authenticates user credentials and returns a JWT token

### Users
- `GET /api/users` - Retrieves a paginated list of users that match the specified filter criteria  (Admin only)
- `GET /api/users/{id}` - Retrieves a user by their id (only admin can get any user)
- `GET /api/users/username?username=..` - Retrieves a user by their username (only admin can get any user)
- `GET /api/users/admin-count` - Retrieves the total number of Admins (Admin Only)
- `POST /api/users` - Registers a new user
- `PATCH /api/users/{id}` - Updates a user (only Admin can update other users)
- `PATCH /api/users/{id}/role` - Updates a user's role (Admin Only)
- `DELETE /api/users/{id}` - Deletes a use (Admin only)

### Meals
- `GET /api/meals/{id}` - Retrieves a specific meal by its Id
- `GET /api/meals/user-id/{userId}` - Retrieves all meals logged by a specific user
- `POST /api/meals/meal-type/{mealType}` - Logs a new meal
- `PUT /api/meals/{mealId}` - Updates the details of an existing meal
- `DELETE /api/meals/{mealId}` - Deletes a specific meal

### FoodItems
- `GET /api/food-items/search?query=..` - Searches for food items by name
- `POST /api/food-items` - Adds a new food item (Admin only)
- `DELETE /api/food-items/{id}` - Deletes a food item by its Id (Admin only)

<br>

## Authentication
- User login & registration with bcrypt password hashing
- JWT-based token system for protected routes

<br>

## Authentication Flow
- Register: POST /api/users
- Login: POST /api/auth/login/access-token
- Protected Routes: Require Authorization: Bearer <token> header

<br>

## Tech Stack Used
- **Frontend** - React (with Vite), React Router DOM  
- **Backend** - C#, ASP.NET Core Web API (.NET 8)  
- **Database** - SQL Server, Entity Framework Core  
- **Deployment** - Docker + `docker-compose`  
- **Tooling** - Studio, VS Code, SSMS  

<br>

## Developer Notes
- All controllers inherit from BaseController, which provides access to the authenticated ApplicationUser via claims.
- Nutrition aggregation is handled manually via NutritionService.CalculateNutrition(...).
- Role-based authorization is enforced using [Authorize(Roles = "Admin")].
- An Admin and some food data are seeded in the database when first running the project.
- Only an Admin has access to FoodItem creation, deletion and is able to change the roles of other users to Admin.
- There are many scripts that help with tedious actions such as:
   - inside _docker (mostly used by docker but can be used manually too)
      - generate-env.sh - generate frontend and backend .envs (used for individual manual startup of frontend or backend) from root .env
      - entrypoint.sh - ensures the db is up then runs init.sql
      - init.sql - creates the db in it doesnt exist.
   - inside root:
      - start.sh - runs generate-envs.sh then runs docker-compose
   - inside package.json are scripts that can be used to run the project from the frontend folder once build using npm commands:
      - npm run start - deploys the frontend and backend concurrently
      - npm run start:frontend - deploys only the frontend
      - npm run start:backend - deploys only the backend

<br>

## Troubleshooting

Here are common issues and how to resolve them:

- **Port Conflicts**  
  Ensure ports `3000` (frontend) and `5000` (backend) are not in use by other applications.

- **Missing `.env` Files**  
  Confirm that a `.env` file exists in the root directory. This file is essential for both Docker and manual setups.

- **JWT Secret Issues**  
  If your JWT tokens aren’t working, regenerate a secure secret using:
  ```bash
  python -c "import secrets; print(secrets.token_urlsafe(64))"
  ```
  
<br>

## License
MIT License. See the LICENSE file for details.