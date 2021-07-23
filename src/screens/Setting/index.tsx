import { useState } from "react";
import * as React from 'react'
import {
  StyleSheet,
  View,
  SafeAreaView,
} from "react-native";
import { ChangeMode, ChangeTemp, ChangeTime, SettingList } from "../../components";
import * as firebase from "firebase";

import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app(); // if already initialized, use that one
}

const Setting = () => {
    const [screenActive, setScreenActive] = useState('temp.')
    const list = [
        {
            name: 'temp.',
            icon: 'oil-temperature',
            id: 1
        },
        {
            name: 'humidity',
            icon: 'waves',
            id: 2
        },
        {
            name: 'time',
            icon: 'timer-sand-empty',
            id: 3
        },
        {
            name: 'roof',
            icon: 'home-roof',
            id: 4
        },
    ]
    const setClick = (screen: any) => {
      setScreenActive(screen);
    };
    const renderSettingList = () => {
        return list.map(item => {
            return (
                <SettingList 
                key={item.id}
                click={setClick}
                name={item.name} icon={item.icon} screenActive={screenActive}/>
            )
        })
    }
  return (
    <SafeAreaView style={styles.screen}>
      <View style={styles.container}>
        <View style={styles.listWrap}>{renderSettingList()}</View>
        {(screenActive == "temp." || screenActive == "humidity") ? (
          <ChangeTemp
            type={screenActive == "temp." ? "°C" : "%"}
            path={screenActive == "temp." ? "light/setTem" : "pump/mh"}
          />
        ) : (
          <ChangeTime />
        )}

        <ChangeMode path="light" />
      </View>
    </SafeAreaView>
  );
};
const styles = StyleSheet.create({
  screen: {
     backgroundColor: 'white',
     flex: 1,
     
    
  },
  container: {
    justifyContent: 'space-between',
    flex: 1,
    padding: 25
  },
  listWrap: {
      flexDirection: 'row',
      justifyContent: 'space-between'
  }
});

export default Setting;
