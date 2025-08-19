import { Outlet } from "react-router";
import Footer from "./Footer";
import Header from "./Header.tsx";
import BackgroundLayout from "./BackgroundLayout.tsx";

const Layout = () => {
  return (
    <>
      <Header />
      <div className="container mx-auto min-h-[95vh] pt-18 pb-1">
        <BackgroundLayout>
          <Outlet />
        </BackgroundLayout>
      </div>
      <Footer />
    </>
  );
};

export default Layout;
