import { Routes, Route, BrowserRouter } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
// import RegisterPage from './pages/RegisterPage';
// import LogMealPage from './pages/LogMealPage';
import { AuthProvider } from './context/AuthProvider';
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './pages/HomePage';
import DashboardPage from './pages/DashboardPage';
import { Toaster } from 'sonner';
import NotFoundPage from './pages/NotFoundPage';
import { PublicRoute } from './components/PublicRoute';


const App = () => {
  return (
    <>
      <AuthProvider>
        <BrowserRouter>
            <Routes>
              <Route element={<Layout />}>
                <Route index element={<HomePage/>} />
                <Route path="/login" element={<PublicRoute><LoginPage /></PublicRoute>} />

                <Route path="/dashboard" element={<ProtectedRoute />}>
                  <Route index element={<DashboardPage />} />
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
