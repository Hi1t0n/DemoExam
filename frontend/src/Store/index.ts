import  {configureStore} from '@reduxjs/toolkit';
import loginReducer from "./LoginSlice.ts";

export default configureStore({
    reducer: {
        login : loginReducer,
    },
});