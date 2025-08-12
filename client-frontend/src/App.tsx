import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
// import RegisterPage from './pages/RegisterPage';
// import LogMealPage from './pages/LogMealPage';
import { AuthProvider } from './context/AuthProvider';
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';
import HomePage from './pages/HomePage';


const App = () => {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route element={<Layout />}>
            <Route index element={<HomePage/>} />
            <Route path="/login" element={<LoginPage />} />

            <Route path="/dashboard" element={<ProtectedRoute />} />
            {/* <Route path="/register" element={<RegisterPage />} /> */}
            {/* <Route path="/log" element={<LogMealPage />} /> */}
          </Route>
          
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
