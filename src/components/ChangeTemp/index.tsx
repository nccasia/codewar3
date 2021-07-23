import React, { memo, useState, useEffect } from "react";
import { Text, View } from "react-native";
import CircleSlider from "react-native-circle-slider";
import DateTimePicker from '@react-native-community/datetimepicker';
import Slider from '@react-native-community/slider';
import * as firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface ChangeTempProps {
    type: string
    path: string
}

const ChangeTemp: React.FC<ChangeTempProps> = memo(({
    type,
    path
}) => {
  console.log('aa',path)
  const [value, setValue] = useState(0);
  const getData = () => {
    firebase
      .database()
      .ref(`device/${path}`)
      .on("value", (snap) => {
        setValue(snap.val());
        console.log(snap.val())
      });
  };
  const updateData = (value: number) => {
    firebase
      .database()
      .ref(`device/${path}`).set(value)
  };
  useEffect(getData, [path])
  return (
    <View style={{ alignItems: 'center', paddingTop: 20}}>
      <Text style={{ fontSize: 38, fontWeight:'bold'}}>{Math.round(value)}{type}</Text>
      <Slider
      value={value}
    style={{width: '100%', height: 40, marginTop: 20}}
    onValueChange={value => {
      updateData(Math.round(value))
    }}
    minimumValue={0}
    maximumValue={100}
    minimumTrackTintColor="#1877f2"
    maximumTrackTintColor="#D9E5FC"
  />
    </View>
  );
});

export default ChangeTemp;
