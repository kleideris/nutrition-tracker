import { Outlet } from "react-router";
import Footer from "./Footer";
import Header from "./Header.tsx";

const Layout = () => {
  return (
    <>
      <Header />
      <div className="container mx-auto min-h-[95vh] pt-24">
        <Outlet />
      </div>
      <Footer />
    </>
  );
};

export default Layout;
