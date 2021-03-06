import { memo, useState, useEffect } from "react";
import * as React from 'react' 
import { Text, View, Switch } from "react-native";
import * as firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface ChangeModeProps {
    path: string
}

const ChangeMode: React.FC<ChangeModeProps> = memo(({
    path
}) => {
    const [loading, setLoading] = useState<boolean>(true);
    const [value, setValue] = useState<boolean>();
    useEffect(() => {
      getValue();
    }, [path])

    const getValue = async () => {
        getData()
        if (!value) {
            setLoading(false);
            return;
        }
    }
  const getData = () => {
    firebase
      .database()
      .ref(`device/${path}`)
      .on("value", (snap) => {
          if(snap.val().mode == "no-auto") {
            setValue(false);
            console.log(value)
          } else setValue(true);
      });
  };
  const toggleDevice = () => {
    firebase
      .database()
      .ref(`device/${path}`)
      .update({
        mode: value == true ? "no-auto" : "auto",
      });
  };
  useEffect(getData, [])
  return (
   <View style={{ alignItems: 'center', paddingTop: 20, flexDirection: 'row', justifyContent: 'space-between'}}>
      <Text style={{ fontSize: 20, fontWeight:'bold'}}>Turn On/Off</Text>
      <Switch
            // style={{ transform: [{ scaleX: 2 }, { scaleY: 2 }] }}
            trackColor={{ false: "#767577", true: "#81b0ff" }}
            thumbColor={false ? "#f5dd4b" : "#f4f3f4"}
            ios_backgroundColor="#3e3e3e"
            onValueChange={toggleDevice}
            value={value}
          />
   
    </View>
  );
});

export default ChangeMode;
