import React from 'react';
import {
    DrawerContentComponentProps,
    DrawerContentOptions,
    DrawerContentScrollView,
    DrawerItem,
    DrawerItemList,

} from '@react-navigation/drawer';
import { Linking } from 'react-native';
import { Text, View } from 'native-base';
import { useAuth } from '../store';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { DrawerActions } from '@react-navigation/native';

interface DrawProps {
    logout?: Function
}

const CustomDrawerContent: React.FC<DrawerContentComponentProps<DrawerContentOptions> & DrawProps> = (props) => {

    return (
        <View style={{
            height: "100%"
        }}>
            <View
                style={{
                    height: 200,
                    backgroundColor: "#f5f6fa",
                    justifyContent: "center",
                    alignItems: "center"
                }}
            >
                <Text style={{
                    fontWeight: "bold",
                    fontSize: 50,
                    color: "#fb5b5a",
                }}>FRESH</Text>
            </View>
            <DrawerContentScrollView {...props} style={{
            }}>
                <DrawerItemList {...props} />
                {
                    props.logout &&
                    <DrawerItem
                        label="Logout"
                        onPress={() => {
                            props.logout && props.logout();
                            props.navigation.dispatch(DrawerActions.closeDrawer());
                        }}
                    />
                }
                <DrawerItem
                    label="Help"
                    onPress={() => Linking.openURL('https://mywebsite.com/help')}
                />
            </DrawerContentScrollView>
        </View>
    );
}

export default CustomDrawerContent;