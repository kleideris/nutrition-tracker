import { Routes, Route, BrowserRouter } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import { AuthProvider } from './context/AuthProvider';
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './pages/HomePage';
import DashboardPage from './pages/DashboardPage';
import { Toaster } from 'sonner';
import NotFoundPage from './pages/NotFoundPage';
import { PublicRoute } from './components/PublicRoute';
import RegisterPage from './pages/RegisterPage';
import FoodItemsPage from './pages/FoodItemsPage';
import UserProfilePage from './pages/UserProfilePage';
import MyMealsPage from './pages/MyMealsPage';
import DashboardWelcome from './components/DashboardWelcome';
import LogMealPage from './pages/LogMealPage';
import UsersPage from './pages/UsersPage';


const App = () => {
  return (
    <>
      <AuthProvider>
        <BrowserRouter>
            <Routes>
              <Route element={<Layout />}>
                <Route index element={<HomePage/>} />
                <Route path="/login" element={<PublicRoute><LoginPage /></PublicRoute>} />
                <Route path="/register" element={<RegisterPage />} />


                <Route path="/dashboard" element={<ProtectedRoute />}>
                  <Route element={<DashboardPage />}>
                  <Route index element={<DashboardWelcome />} /> {/* 👈 Add this */}
                    <Route path="log-meal" element={<LogMealPage />} />
                    <Route path="my-meals" element={<MyMealsPage />} />
                    <Route path="food-items" element={<FoodItemsPage />} />
                    <Route path="profile" element={<UserProfilePage />} />
                    <Route path="users" element={<UsersPage />} />
                  </Route>
                </Route>

                <Route path="*" element={<NotFoundPage />} />
              </Route>
            </Routes>
          <Toaster richColors />
        </BrowserRouter>
      </AuthProvider>
    </>  
  );
}

export default App;
