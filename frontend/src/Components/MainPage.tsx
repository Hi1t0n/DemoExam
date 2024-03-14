import Header from "./Header/Header.tsx";
import {useSelector} from "react-redux";
import {RootState} from "../Store/LoginSlice.ts";
import {useNavigate} from "react-router-dom";

function MainPage(){
    const isLogin = useSelector((state: RootState)=> state.login.isLogin);
    const navigate = useNavigate();
    return(
        <>
        <Header></Header>
        <body>
            <button onClick={()=> isLogin ? navigate('/', {replace: false}) : navigate('/login',{replace: false})}>Подать заявление</button>
        </body>
        </>
    );
}

export default MainPage;