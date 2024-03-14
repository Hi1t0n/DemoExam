import React from "react";

interface Props{
    id : string
    type : string;
    placeholder : string;
    value : string;
    required : boolean;
    minLength? : number
    maxLength? : number
    pattern? : string;
    onChange : (e: React.ChangeEvent<HTMLInputElement>) => void;
}


function Input({id, type, placeholder, value,required, minLength, maxLength,pattern, onChange} : Props){
    return(
        <input id={id} type={type}  placeholder={placeholder} value={value} pattern={pattern} required={required} minLength={minLength} maxLength={maxLength} onChange={onChange}/>
    );
}

export default Input;