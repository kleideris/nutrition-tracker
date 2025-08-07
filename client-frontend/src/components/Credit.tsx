type props = {
  title: string
}

const Credit = ({title}: props) => {
  return(
    <>
      <h1 className="bg-white py-4 px-6 flex">{title}</h1>
    </>
  )
}

export default Credit