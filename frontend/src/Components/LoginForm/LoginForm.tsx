import Input from "../Input/Input.tsx";
import Button from "../Button/Button.tsx";
import React, {useState} from "react";
import "./LoginForm.css"
import {useSelector} from 'react-redux';
import {useDispatch} from 'react-redux';
import {setLoginState} from "../../Store/LoginSlice.ts";
import axios, {AxiosError} from "axios";
import {useNavigate} from "react-router-dom";

function LoginForm(){
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleLoginChange = (e : React.ChangeEvent<HTMLInputElement>)=> {
        setLogin(e.target.value);
    }

    const handlePasswordChange = (e : React.ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
    }

    const handleErrorMessage = (e : string) => {
        setErrorMessage(e);
    }

    const handleLogin = async ()=>{
        const userLoginRequest = {
            Login : login,
            Password : password
        };

        try {
            const response = await axios.post('https://localhost:7240/api/auth/login', userLoginRequest);
            if(response.status === 200){
                dispatch(setLoginState(true));
                navigate('/', {replace: true});
            }
        } catch (error) {
            const axiosError = error as AxiosError;
            if (axiosError.response) {
                const errorMessage: string = axiosError.response.data.error;
                handleErrorMessage(errorMessage);
            }
        }
    }

    return (
        <form className={"loginForm"}>
            <div className={"container"}>
                <p>
                    <label htmlFor={"loginInput"}>Логин: </label>
                    <Input id={"loginInput"} type={"text"} placeholder={"Логин"} value={login} pattern={"[A-Za-z0-9_-"}
                           required={true}
                           minLength={6}
                           onChange={handleLoginChange}/>
                </p>
                <p>
                    <label htmlFor={"passwordInput"}>Пароль: </label>
                    <Input id={"passwordInput"} type={"password"} placeholder={"Пароль"}
                           value={password}
                           required={true} minLength={6}
                           onChange={handlePasswordChange}/>
                </p>
                <p>
                    <p className={"errorMessage"}>{errorMessage}</p>
                    <Button type={"button"} onClick={handleLogin} id={"registrationButton"}
                            children={"Войти"}/>
                </p>
            </div>
        </form>
    )
}

export default LoginForm;