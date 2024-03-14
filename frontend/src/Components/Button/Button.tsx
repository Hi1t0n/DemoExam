import "./Button.css"
interface Props {
    type: "button" | "submit" | "reset" | undefined;
    children : string;
    onClick : () => void;
    id : string;
}

function Button({type, children, id, onClick} : Props) {
    return (
        <button className={"button"} type={type} onClick={onClick} id={id}>{children}</button>
    );
}

export default Button;