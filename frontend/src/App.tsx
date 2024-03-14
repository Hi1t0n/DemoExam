import {BrowserRouter, Route, Routes} from "react-router-dom"
import MainPage from "./Components/MainPage.tsx";
import RegistrationForm from "./Components/RegistrationForm/RegistrationForm.tsx";
import LoginForm from "./Components/LoginForm/LoginForm.tsx";

function App() {

  return (
    <>
      <BrowserRouter>
          <Routes>
              <Route path={"/"} element={<MainPage/>}/>
              <Route path={"/registration"} element={<RegistrationForm/>}/>
              <Route path={"/login"} element={<LoginForm/>}/>
          </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
