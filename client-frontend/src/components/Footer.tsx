const Footer = () => {
  const currentYear: number = new Date().getFullYear();

  return(
    <footer className="bg-neutral-800 text-white">
      <div className="container mx-auto py-8 text-center">
        <h1>&copy; {currentYear} Nutrition Tracker. Open source project</h1>
      </div>
    </footer>
  )
}

export default Footer