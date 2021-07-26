import React, { memo, useState } from "react";
import { StyleSheet, Text, View, Switch, TouchableOpacity } from "react-native";
import { MaterialCommunityIcons } from "@expo/vector-icons";
import firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
import { useEffect } from "react";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface SettingListProps {
  name: string;
  screenActive: string;
  icon: any;
  click: Function
}

const SettingList: React.FC<SettingListProps> = memo(({ name, icon, screenActive, click }) => {
  const clickScreen = () => {
      click(name)
  };
  return (
    <TouchableOpacity style={styles.container} onPress={clickScreen}>
      <View
        style={[
          styles.iconWrap,
          screenActive == name
            ? styles.activeIconWrap
            : styles.unActiveIconWrap,
        ]}
      >
        <MaterialCommunityIcons
          name={icon}
          size={24}
          color={screenActive == name ? "white" : "#1877f2"}
        />
      </View>
      <Text style={styles.name}>{name}</Text>
    </TouchableOpacity>
  );
});

export default SettingList;

const styles = StyleSheet.create({
    container: {
        justifyContent: 'center',
        alignItems: 'center'
    },
    iconWrap: {
        width: 50,
        alignItems: 'center',
        justifyContent: 'center',
        height: 50,
        borderRadius: 50/2,
        
    },
    activeIconWrap: {
        backgroundColor: '#1877f2',
    },
    unActiveIconWrap: {
        borderColor: '#adb8c5',
        borderWidth: 1
    },
    name: {
        textTransform: 'capitalize',
        paddingTop: 5
    }
});
