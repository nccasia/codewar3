import React, { memo, useState } from "react";
import { StyleSheet, Text, View, Switch } from "react-native";
import { MaterialCommunityIcons } from "@expo/vector-icons";
import firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
import { useEffect } from "react";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface ShortCutItemProps {
  name: string;
  icon: any;
}

const ShortCutItem: React.FC<ShortCutItemProps> = memo(({ name, icon }) => {
  const [isAuto, setIsAuto] = useState<boolean>(false);
  const [isRunning, setIsRunning] = useState<boolean>(true);
  const getDeviceMode = () => {
    firebase
      .database()
      .ref(`device/${name}`)
      .on("value", (snap) => {
        if (snap.val().mode == "no-auto") {
          setIsAuto(false);
        } else setIsAuto(true);
      });
  };
  const getDeviceAction = () => {
    firebase
      .database()
      .ref(`device/${name}/action`)
      .on("value", (snap) => {
        if (snap.val() == "off") {
          setIsRunning(false);
        } else setIsRunning(true);
      });
  };
  const toggleDevice = () => {
    firebase
      .database()
      .ref(`device/${name}`)
      .update({
        action: isRunning ? "off" : "on",
      });
  };
  useEffect(() => {
    getDeviceMode();
    getDeviceAction();
  }, []);

  return (
    <View style={styles.shortCutItemCard}>
      <View style={styles.shortCutContent}>
        <View
          style={{
            flexDirection: "row",
            justifyContent: "space-between",
          }}
        >
          <Text
            style={{
              textTransform: "capitalize",
              color: isRunning ? "#1877f2" : "#adb8c5",
            }}
          >
            {name}
          </Text>
          <MaterialCommunityIcons
            name={icon}
            size={24}
            color={isRunning ? "#1877f2" : "#adb8c5"}
          />
        </View>
        <View
          style={{
            flexDirection: "row",
            paddingTop: 15,
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <Text
            style={{
              fontSize: 17,
              textTransform: "uppercase",
              fontWeight: "bold",
            }}
          >
            {isRunning ? "On" : "Off"}
          </Text>
          <Switch
            disabled={isAuto}
            style={{ transform: [{ scaleX: 0.8 }, { scaleY: 0.8 }] }}
            trackColor={{ false: "#767577", true: "#81b0ff" }}
            thumbColor={isAuto ? "#f5dd4b" : "#f4f3f4"}
            ios_backgroundColor="#3e3e3e"
            onValueChange={toggleDevice}
            value={isRunning}
          />
        </View>
      </View>
    </View>
  );
});

export default ShortCutItem;

const styles = StyleSheet.create({
  shortCutItemCard: {
    paddingHorizontal: 10,
    paddingTop: 10,
    paddingBottom: 5,
    width: "50%",
  },
  shortCutContent: {
    borderRadius: 5,
    backgroundColor: "white",
    padding: 10,
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.23,
    shadowRadius: 2.62,

    elevation: 4,
  },

  onAuto: {
    backgroundColor: "#1877f2",
    shadowColor: "#1877f2",
  },
  offAuto: {
    backgroundColor: "white",
    shadowColor: "#000",
  },
});
