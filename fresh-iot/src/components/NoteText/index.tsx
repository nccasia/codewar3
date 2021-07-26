import { memo, useState} from "react";
import * as React from 'react'
import {MaterialCommunityIcons} from '@expo/vector-icons'
import { Text, View, StyleSheet } from "react-native";
import * as firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface NoteTextProps {
    data: {
        name: string
        icon: any
        type?: string
        path: string
        secondPath?: string
    }
    
  
}

const NoteText: React.FC<NoteTextProps> = memo(({
    data,
}) => {
console.log('a',data.secondPath)
  const [value, setValue] = useState<number>();
  const [min, setMin] = useState<number>()
  const getMinute = () => {
    firebase
      .database()
      .ref(`device/${data.secondPath}`)
      .on("value", (snap) => {
        setMin(snap.val());
      });
  };
  const getData = () => {
    firebase
      .database()
      .ref(`device/${data.path}`)
      .on("value", (snap) => {
        setValue(snap.val());
      });
  };
  React.useEffect(getData, [data.path])
  React.useEffect(getMinute , [
    data.secondPath
  ])
  
  return (
    <View style={styles.container}>
        <MaterialCommunityIcons name={data.icon} size={24} color="#1877f2" />
        <Text style={styles.text}>The {data.name} will automatically run at {value}{min ? `:${min}` : null} {data.type}</Text>
    </View>
  );
});
const styles = StyleSheet.create({
  container: {
    backgroundColor: "#D9E5FC",
    padding: 15,
    borderRadius: 25,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
  },
  text: {
    color: "#1877f2",
    fontSize: 18,
    fontWeight: "500",
    paddingLeft: 10,
  },
});


export default NoteText;
