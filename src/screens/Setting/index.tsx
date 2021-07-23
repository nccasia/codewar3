import { useState } from "react";
import * as React from 'react'
import {
  StyleSheet,
  View,
  SafeAreaView,
  Text
} from "react-native";
import { ChangeMode, ChangeTemp, ChangeTime, NoteText, SettingList } from "../../components";
import * as firebase from "firebase";

import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app(); // if already initialized, use that one
}
interface screenDataProps{
    name: string;
    path: string;
    type: string;
    icon: any;
    device: string;
    secondPath?: string
}

const Setting = () => {

    const [screenActive, setScreenActive] = useState('temp.')
    const [hour, setHour] = useState<number>()
    const [min, setMin] = useState<number>()
    const getHour = () => {
      firebase
        .database()
        .ref(`device/pump`)
        .on("value", (snap) => {
          setHour(snap.val().hour);
        });
    };
    const getMin = () => {
      firebase
        .database()
        .ref(`device/pump`)
        .on("value", (snap) => {
          setMin(snap.val().min);
        });
    };
    React.useEffect(() => {
      getHour();
      getMin()
    }, [])
    const [screenData, setScreenData] = useState<screenDataProps>({
      name: "Light",
      path: "light/setTem",
      device: "light",
      type: "°C",
      icon: "oil-temperature",
    }); 
    const setScreenActiveData = (screen: string) => {
      switch (screen) {
        case "temp.":
          setScreenData({
            name: "Light",
            path: "light/setTem",
            device: "light",
            type: "°C",
            icon: "oil-temperature",
          });
          break;
        case "humidity":
          console.log(screen);
          setScreenData({
            name: "Pump",
            path: "pump/mh",
            type: "%",
            device: "pump",
            icon: "waves",
          });
          break;
        case "roof":
          setScreenData({
            name: "Roof",
            path: "servo/mh",
            type: "%",
            device: "servo",
            icon: "home-roof",
          });
          break;
        case "time":
          setScreenData({
            name: "Pump",
            device: "pump",
            path: "pump/hour",
            type: "",
            secondPath: "pump/minute",
            icon: "timer-sand-empty",
          });
          break;
      }
    }
    
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
      setScreenActiveData(screen)
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
        {screenActive == "time" ? (
           min !=undefined ? <ChangeTime min={min} hour={hour}/> : null
         
        ) : (
          <ChangeTemp
        
            type={screenData.type}
            path={screenData.path}
          />
        )}
        <NoteText data={screenData} />
        <ChangeMode path={screenData.device} />
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
