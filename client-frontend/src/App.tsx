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
import LogMealForm from './components/LogMealForm';
import MealList from './components/MealList';
import UserProfile from './components/UserProfile';
import RegisterPage from './pages/RegisterPage';
import FoodItemsPage from './pages/FoodItemsPage';


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
                  <Route index element={<DashboardPage />} />
                    <Route path="log-meal" element={<LogMealForm />} />
                    <Route path="my-meals" element={<MealList />} />
                    <Route path="profile" element={<UserProfile />} />
                    <Route path="food-items" element={<FoodItemsPage />} />
                  <Route/>

                </Route>
                {/* <Route path="/register" element={<RegisterPage />} /> */}
                {/* <Route path="/log" element={<LogMealPage />} /> */}

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
