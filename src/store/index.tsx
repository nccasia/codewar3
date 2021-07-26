import { createContext, FunctionComponent, useContext, useState } from "react";
import * as React from 'react'

export type User = {
    username: string,
    fullname: string,
    email?: string,
    image?: string
}

interface AuthProps {
    isAuth: boolean,
    setIsAuth: (isAuth: boolean) => void,
    user?: User,
    setUser: (user?: User) => void
}

const AuthContext = createContext<AuthProps>({
    isAuth: false,
    setIsAuth: (isAuth: boolean) => {},
    user: null,
    setUser: (user?: User) => {}
});

const AuthProvider: FunctionComponent = ({
    children
}) => {

    const [isAuth, setIsAuth] = useState<boolean>(false);
    const [user, setUser] = useState<User | null>(null);

    return (
        <AuthContext.Provider
            value={{
                isAuth, user,
                setIsAuth, setUser
            }}
        >
            {
                children
            }
        </AuthContext.Provider>
    )
}

export const useAuth = () => useContext(AuthContext);

export {
    AuthProvider
}