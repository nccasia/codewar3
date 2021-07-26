import AsyncStorage from '@react-native-async-storage/async-storage';
import { Spinner } from 'native-base';
import React, { useEffect, useState } from 'react';
import { SafeAreaView, StatusBar } from 'react-native';
import useColorScheme from '../../hooks/useColorScheme';
import Navigation from '../../navigation';
import { useAuth } from '../../store';

interface AppProps {

}

const App: React.FC<AppProps> = () => {
    const [loading, setLoading] = useState<boolean>(true);
    const colorScheme = useColorScheme();
    const { setIsAuth, setUser } = useAuth()

    useEffect(() => {
        getUserLogin();
    }, [])

    const getUserLogin = async () => {
        const auth = JSON.parse(await AsyncStorage.getItem("auth"));
        if (!auth) {
            setLoading(false);
            return;
        }
        setIsAuth(true);
        setUser(auth);
        setLoading(false);
    }

    return (
        <SafeAreaView style={{
            flex: 1
        }}>
            {
                loading ? <Spinner /> :
                    <Navigation colorScheme={colorScheme} />
            }
        </SafeAreaView>
    );
}

export default App;