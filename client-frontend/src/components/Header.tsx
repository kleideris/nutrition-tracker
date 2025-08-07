import UserInfo from './UserInfo';
import avatar from '../assets/default-avatar.svg';


const Header = () => {
  const user = {
    username: 'Login',
    avatarUrl: avatar
  };

  return (
    <header className="bg-white shadow-md py-4 px-6 flex justify-between items-center">
      <h1 className="text-xl font-bold text-green-600">🥗 Welcome to Nutrition Tracker App</h1>
      <UserInfo username={user.username} avatarUrl={user.avatarUrl} />
    </header>
  );
};

export default Header;