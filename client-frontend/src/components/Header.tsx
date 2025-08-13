import { Link } from 'react-router-dom';
import { AuthButton } from './AuthButton';


const Header = () => {
  return (
    <header className="bg-green-800 fixed w-full shadow-md z-50">
      <div className="container mx-auto px-4 py-4 flex items-center justify-between">
        <h1 className="text-xl font-bold text-white">Nutrition Tracker App</h1>
      
          <nav className="md:flex gap-6 text-white font-medium absolute md:static top-16 left-0 w-full md:w-auto bg-cf-dark-red md:bg-transparent px-4 py-4 md:py-0">
          <Link
            to="/"
            className="block md:inline hover:underline hover:underline-offset-4 p-4 md:p-1"
          >
            Home
          </Link>
          <Link
            to="dashboard"
            className="block md:inline hover:underline hover:underline-offset-4 p-4 md:p-1"
          >
            Dashboard
          </Link>
          <AuthButton/>
        </nav>
      </div>
    </header>
  );
};

export default Header;