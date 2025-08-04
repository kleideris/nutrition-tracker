# 🥗 Nutrition Tracker Project 🥗
![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![React](https://img.shields.io/badge/React-19-blue)
![Docker](https://img.shields.io/badge/Docker-ready-blue)
![License](https://img.shields.io/github/license/kleideris/nutrition-tracker)
![Issues](https://img.shields.io/github/issues/kleideris/nutrition-tracker)
![Stars](https://img.shields.io/github/stars/kleideris/nutrition-tracker?style=social)
![Forks](https://img.shields.io/github/forks/kleideris/nutrition-tracker?style=social)


### A full-stack web application that enables users to log meals, monitor nutritional intake, and visualize personal health trends. Built with React and ASP.NET Core Web API, this project demonstrates end-to-end development in a clean, scalable, and containerized architecture.

<br>

# 🚀 How To Run

### 🧩 Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Docker](https://www.docker.com/products/docker-desktop/) (for Docker method)

### 🔐 Setup Environment Variables

Create a `.env` file in both `client-frontend` and `server-backend` directories using the provided `.env.example` as a template.


## ⚙️ Method 1: Run Locally with Concurrently

1. **Clone the repository**  
   ```bash
   git clone https://github.com/kleideris/nutrition-tracker.git
   ```

2. **Restore backend packages:**  
   ```bash
   cd server-backend/NutritionTracker.Api
   dotnet restore
   ```

3. **Install frontend dependencies:**  
   ```bash
   cd ../../client-frontend
   npm install
   ```

4. **Start both frontend and backend concurrently**
   ```bash
   npm run start
   ```

## 🐳 Method 2: Run with Docker:

Make sure Docker Desktop is installed on your machine and is running then follow these steps.

1. **- Clone and navigate:**  
   ```bash
   git clone https://github.com/kleideris/nutrition-tracker.git
   cd nutrition-tracker
   ```

2. **Build and run the application**  
   ```bash
   docker-compose up --build
   ```

### 🛠 Troubleshooting

- **Port conflicts**: Make sure ports `3000` (frontend) and `5000` (backend) are free.


<br>

# 📊 Core Features (MVP)

- Meal logging (food name, macros, calories)
- Nutrition data fetch from external APIs
- Weekly summary visualization
- Entity Framework–based relational DB
- Swagger auto-doc for backend routes
- Logging with Serilog (file or console-based)  
  
<br>

# 🔧 Tech Stack

| Layer       | Tech                                      |
|:------------|:------------------------------------------|
| Frontend    | React (with Vite), Axios, React Router DOM|
| Backend     | C#, ASP.NET Core Web API (.NET 8)         |
| Database    | SQL Server + Entity Framework Core        |
| Deployment  | Docker + `docker-compose`                 |
| Tooling     | Visual Studio, VS Code, SSMS              |
  
<br>

# 🔐 Authentication

- User login & registration with bcrypt password hashing
- JWT-based token system for protected routes