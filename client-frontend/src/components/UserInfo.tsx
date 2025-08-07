const UserInfo = ({ username, avatarUrl }) => {
  return (
    <div className="flex items-center space-x-3">
      <img
        src={avatarUrl}
        alt={`${username}'s avatar`}
        className="w-8 h-8 rounded-full border border-gray-300"
      />
      <span className="text-sm font-medium text-gray-800">{username}</span>
    </div>
  );
};

export default UserInfo;