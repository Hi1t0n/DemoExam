import NavigationButton from "../NavigationButton/NavigationButton.tsx";
import "./Header.css"
import {useNavigate} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {RootState, setLoginState} from "../../Store/LoginSlice.ts";

function Header(){
    const navigate = useNavigate()
    const isLogin = useSelector((state: RootState) => state.login.isLogin);
    const dispatch = useDispatch();
    return(
        <header className={"header"}>
            <nav className={"navigationMenu"}>
                <h1 className={"nameLogo"}>Нарушениям.Нет</h1>
                <div className={"navigationButtonContainer"}>
                    {isLogin === false ? (
                        <>
                            <NavigationButton children={"Регистрация"}
                                              onClick={() => navigate('/registration', {replace: false})}/>
                            <NavigationButton children={"Вход"} onClick={() => navigate('/login', {replace: false})}/>
                        </>
                    ) : (
                        <>
                            <NavigationButton children={"Заявления"}
                                              onClick={() => navigate('/', {replace: false})}/>
                            <NavigationButton children={"Выход"} onClick={() => dispatch(setLoginState(false), {replace: false})}/>
                        </>
                    )}
                </div>
            </nav>
        </header>

    );
}

export default Header;