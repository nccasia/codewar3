import React, { memo, useState, useEffect } from "react";
import { StyleSheet, Text, View } from "react-native";
import { Ionicons } from "@expo/vector-icons";
import firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface GeneralInfoProps {
  path: string;
  name?: string;
  type: string;
}

const GeneralInfo: React.FC<GeneralInfoProps> = memo(({ name, path, type }) => {
  const [data, setData] = useState<number>(0);
  const getData = () => {
    firebase
      .database()
      .ref(`sensor/${path}`)
      .on("value", snap => {
        setData(snap.val());
        console.log(data)
      });
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <View
      style={{
        flex: 1,
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      {type == "weather" ? 
        <Ionicons name={data > 500 ? "rainy-outline" : 'sunny'} size={30} color={data > 500 ? "#1877f2" : '#f5dd4b'} />
       : 
        <Text style={{ fontSize: 27, fontWeight: "600" }}>
          {data}{type}
        </Text>
      }
      <Text style={{ paddingTop: 5, fontSize: 15, fontWeight: "500" }}>
        {name ? name : type == "weather" && data > 500 ? "Raining" : "Sun"}
      </Text>
    </View>
  );
});

export default GeneralInfo;


