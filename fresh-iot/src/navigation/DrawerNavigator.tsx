import React from 'react';
import { createDrawerNavigator } from "@react-navigation/drawer";
import BottomTabNavigator from './BottomTabNavigator';
import Login from "../screens/login";
import CustomDrawerContent from './CustomDrawerContent';
import { useAuth } from '../store';
import AsyncStorage from '@react-native-async-storage/async-storage';

interface DrawNavigateProps {

}

const MainDrawerNavigator = createDrawerNavigator();

const DrawNavigate: React.FC<DrawNavigateProps> = () => {

    const { isAuth, setUser, setIsAuth, user } = useAuth();

    const logout = () => {
        setIsAuth(false);
        setUser(null);
        AsyncStorage.clear();

    }

    return (
        <MainDrawerNavigator.Navigator
            statusBarAnimation="fade"
            screenOptions={{
                swipeEnabled: true
            }}
            drawerContent={props => <CustomDrawerContent {...props} logout={isAuth ? logout : null} />}
            drawerStyle={{
                // backgroundColor: "red"
            }}
            initialRouteName={isAuth ? "Login" : "Home"}
        >
            {
                isAuth &&
                <MainDrawerNavigator.Screen
                    name="Home"
                    component={BottomTabNavigator}
                />
            }
           
            {
                !isAuth && <MainDrawerNavigator.Screen
                    name="Login"
                    component={Login}
                />
            }
        </MainDrawerNavigator.Navigator>
    );
}

export default DrawNavigate;