import React from 'react';
import useCachedResources from './src/hooks/useCachedResources';
import useColorScheme from './src/hooks/useColorScheme';
import { AuthProvider } from './src/store';
import AppRouter from "./src/screens/app";
import Navigation from './src/navigation';
import { StatusBar } from 'react-native';

export default function App() {
  const isLoadingComplete = useCachedResources();
  const colorScheme = useColorScheme();

  if (!isLoadingComplete) {
    return null;
  } else {
    return (
      <AuthProvider>
        <AppRouter />
        {/* <Navigation colorScheme={colorScheme} /> */}
        {/* <StatusBar /> */}

      </AuthProvider>
    );
  }
}
