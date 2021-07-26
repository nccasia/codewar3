import { StackHeaderProps } from '@react-navigation/stack';
import { Text, View } from 'native-base';
import React from 'react';
import { Platform, SafeAreaView, StyleSheet, TouchableOpacity } from 'react-native';
import { EvilIcons } from '@expo/vector-icons'
import { DrawerActions } from '@react-navigation/native';
import { AntDesign } from '@expo/vector-icons'; 

const Header: React.FC<StackHeaderProps> = ({
    navigation,
    scene,
    previous
}) => {

    return (
        <SafeAreaView style={styles.container}>
            <TouchableOpacity
                onPress={() => previous ? navigation.goBack() : navigation.dispatch(DrawerActions.toggleDrawer())}
                style={{
                    marginLeft: 5
                }}
            >
               {
                   previous ? <AntDesign name="arrowleft" size={30} color="black" />:
                   <EvilIcons name="navicon" size={30} color="black" />
               } 
            </TouchableOpacity>
            <View style={styles.content}>
                <Text>{
                    !scene.descriptor.options.headerTitle ? scene.route.name : scene.descriptor.options.headerTitle
                }</Text>
            </View>
        </SafeAreaView>
    );
}

export default Header;

const styles = StyleSheet.create({
    container: {
        height: 85,
        paddingTop: Platform.OS === 'android' ? 25 : 10,
        backgroundColor: "#f5f6fa",
        flexDirection: 'row',
        alignItems: 'center',
        paddingHorizontal: 15
    },
    content: {
        flexGrow: 1,
        height: "100%",
        alignItems: 'center',
        justifyContent: "center",
        fontSize: 40
    }
})  