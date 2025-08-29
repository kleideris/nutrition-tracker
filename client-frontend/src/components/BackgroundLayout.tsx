import { useLocation } from "react-router-dom";
import bgImage from "../assets/nutrition-app-image.jpg";

const BackgroundLayout = ({ children }: { children: React.ReactNode }) => {
  const location = useLocation();
  const isHomePage = location.pathname === "/";

  return (
    <div className="relative w-full min-h-screen overflow-hidden">
      {/* Background Image Layer */}
      <div
        className="absolute inset-0 bg-cover bg-center bg-no-repeat z-0"
        style={{ 
          backgroundImage: `url(${bgImage})`,
          backgroundAttachment: "fixed"  // Keeps image fixed
        }}
      />

      {/* Blur Overlay Layer */}
      {!isHomePage && (
        <div className="absolute inset-0 backdrop-blur-xl z-10 transition-all duration-700 ease-in-out animate-[fadeIn_1.5s_ease-in-out_forwards]" />
      )}

      {/* Content Layer */}
      <div className="relative z-20">
        {children}
      </div>
    </div>
  );
};

export default BackgroundLayout;