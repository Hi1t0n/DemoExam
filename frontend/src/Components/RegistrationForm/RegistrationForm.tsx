import Input from "../Input/Input.tsx";
import React, {useState} from "react";
import axios, {AxiosError} from "axios";
import {useNavigate} from "react-router-dom";
import "./RegistrationForm.css"
import Button from "../Button/Button.tsx";


function RegistrationForm() {
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [patronymic, setPatronymic] = useState("");
    const [email, setEmail] = useState("")
    const [phonenumber, setPhonenumber] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const navigate = useNavigate();

    const handleLoginChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setLogin(e.target.value)
    }
    const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPassword(e.target.value);
    }

    const handleFirstnameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFirstname(e.target.value);
    }

    const handleLastnameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setLastname(e.target.value);
    }

    const handlePatronymic = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPatronymic(e.target.value);
    }

    const handleEmail = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(e.target.value);
    }

    const handlePhonenumber = (e: React.ChangeEvent<HTMLInputElement>) => {
        setPhonenumber(e.target.value);
    }

    const handleErrorMessage = (e : string) => {
        setErrorMessage(e)
    }
    /* Регулярные выражения для проверок */
    const EMAIL_REGEX = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+.+.[a-zA-Z]{2,4}$/i;
    const LOGIN_REGEX = /^[a-zA-Z0-9_-]{6,}$/;
    const PASSWORD_REGEX = /^[a-zA-Z0-9@#$&*]{6,}$/;
    const PHONENUMBER_REGEX = /^[0-9]{11}$/;
    const FULLNAME_REGEX = /^[a-zA-Zа-яА-Я]{1,}$/;

    const handleRegistaration = async () => {

        if (!LOGIN_REGEX.test(login)) {
            handleErrorMessage('Логин должен содержать только латинские символы, цифры, символы _ и - и быть не менее 6 символов');
            return;
        }
        if(password.length < 6){
            handleErrorMessage('Длина пароля должна быть не менее 6 символов')
            return;
        }
        if (!PASSWORD_REGEX.test(password)){
            handleErrorMessage('Пароль должен состоять только из латинских символом, цифр и спец сиволов');
            return;
        }

        if (!FULLNAME_REGEX.test(firstname)){
            handleErrorMessage("Имя должно содержать только русские или английские символы");
            return;
        }
        if (!FULLNAME_REGEX.test(lastname)){
            handleErrorMessage("Фамилия должна содержать только русские или английские символы");
            return;
        }
        if (!FULLNAME_REGEX.test(patronymic) && patronymic.length > 0){
            handleErrorMessage("Отчество должно содержать только русские или английские символы");
            return;
        }

        if(!EMAIL_REGEX.test(email)){
            handleErrorMessage("Введена не верная почта")
            return;
        }
        if(!PHONENUMBER_REGEX.test(phonenumber)){
            handleErrorMessage("Введен не верный номер")
            return;
        }


        const userData = {
            Login: login,
            Password: password,
            Firstname: firstname,
            Lastname: lastname,
            Patronymic: patronymic,
            Email: email,
            PhoneNumber: phonenumber
        };
        try {
            const response = await axios.post('https://localhost:7240/api/auth/register', userData);
            if (response.status === 200) {
                navigate('/', {replace: true});
            }
        } catch (error) {
            const axiosError = error as AxiosError;
            if(axiosError.response){
                const errorMessage : string = axiosError.response.data.error;
                handleErrorMessage(errorMessage);

            }
        }
    }

    return (
        <form className={"registrationForm"}>
            <div className={"container"}>
                <p>
                    <label htmlFor={"loginInput"}>Логин: </label>
                    <Input id={"loginInput"} type={"text"} placeholder={"Логин"} value={login} pattern={"[A-Za-z0-9_-"} required={true}
                           minLength={6}
                           onChange={handleLoginChange}/>
                </p>
                <p>
                    <label htmlFor={"passwordInput"}>Пароль: </label>
                    <Input id={"passwordInput"} type={"password"} pattern={"a-zA-Z0-9@#$&*"}  placeholder={"Пароль"} value={password}
                           required={true} minLength={6}
                           onChange={handlePasswordChange}/>
                </p>
                <p>
                    <label htmlFor={"firstnameInput"}>Имя: </label>
                    <Input id={"firstnameInput"} type={"text"} placeholder={"Имя"} value={firstname} required={true}
                           onChange={handleFirstnameChange}/>
                </p>
                <p>
                    <label htmlFor={"lastnameInput"}>Фамилия: </label>
                    <Input id={"lastnameInput"} type={"text"} placeholder={"Фамилия"} value={lastname} required={true}
                           onChange={handleLastnameChange}/>
                </p>
                <p>
                    <label htmlFor={"patronymicInput"}>Отчество: </label>
                    <Input id={"patronymicInput"} type={"text"} placeholder={"Отчество"} value={patronymic}
                           required={false}
                           onChange={handlePatronymic}/>
                </p>
                <p>
                    <label htmlFor={"emailInput"}>Эл.почта: </label>
                    <Input id={"emailInput"} type={"email"} placeholder={"user@example.ru"} value={email}
                           required={true}
                           onChange={handleEmail}/>
                </p>
                <p>
                    <label htmlFor={"phonenumberInput"}>Номер телефона: </label>
                    <Input id={"phonenumberInput"} type={"tel"} placeholder={"89999999999"} minLength={11}
                           maxLength={11} value={phonenumber} pattern="\d{11}" required={false}
                           onChange={handlePhonenumber}/>
                </p>
                <p>
                    <p className={"errorMessage"}>{errorMessage}</p>
                   <Button type={"button"} onClick={handleRegistaration} id={"registrationButton"} children={"Зарегистрироваться"}/>
                </p>
            </div>
        </form>
    );
}

export default RegistrationForm;