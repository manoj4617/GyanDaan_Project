import NavbarItem from './Components/Navbar.jsx';
import Container from './Components/Container.jsx';
import { BrowserRouter } from 'react-router-dom';
import './App.css';


function App() {
  return (
    <>
      <BrowserRouter>
        <NavbarItem />
        <Container />
      </BrowserRouter>
    </>
  );
}

export default App;
