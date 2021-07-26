import React, { memo, useState, useEffect } from "react";
import { StyleSheet, Text, View, TouchableOpacity } from "react-native";
import { MaterialCommunityIcons } from "@expo/vector-icons";
import firebase from "firebase";
import { firebaseConfig } from "../../../firebase";
import { useNavigation } from "@react-navigation/native";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface AutoModeItemProps {
  name: string;
  icon: any;
  path: string;
}

const AutoModeItem: React.FC<AutoModeItemProps> = memo(
  ({ name, icon, path }) => {
    const navigation = useNavigation();
    const [mode, setMode] = useState<boolean>(false);
    const getMode = () => {
      firebase
        .database()
        .ref(`device/${path}`)
        .on("value", (snap) => {
            if(snap.val().mode == 'no-auto') {
                setMode(false)
            } else setMode(true)
        });
    };

    useEffect(() => {
    getMode();
    }, []);

    return (
      <View style={styles.setupSection}>
        <TouchableOpacity
          style={[
            styles.setupContainer,
            mode ? styles.onAuto : styles.offAuto,
          ]}
          onPress={() => {
            navigation.navigate("Setting");
          }}
        >
          <Text style={{ color: mode ? "white" : "black", fontSize: 18, fontWeight: 'bold' }}>
            {name}
          </Text>
          <MaterialCommunityIcons
            name={icon}
            size={35}
            color={mode ? "white" : "#1877f2"}
          />
          <View
            style={{
              flexDirection: "row",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <Text style={{ color: mode ? "white" : "black", fontSize: 18, fontWeight: 'bold' }}>{mode ? 'ON AUTO' : 'OFF-AUTO'}</Text>
          </View>
        </TouchableOpacity>
      </View>
    );
  }
);
const styles = StyleSheet.create({
  screen: {
    backgroundColor: "white",
  },
  container: {
    paddingTop:10,
  },
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
  setupSection: {
    paddingLeft: 15,
    paddingVertical: 15,
  },
  onAuto: {
    backgroundColor: "#1877f2",
    shadowColor: "#1877f2",
  },
  offAuto: {
    backgroundColor: "white",
    shadowColor: "#000",
  },
  setupContainer: {
    height: 130,
    alignItems: "center",
    justifyContent: "space-between",
    padding: 10,
    borderRadius: 5,
    width: 130,
    shadowOffset: {
      width: 0,
      height: 2,
    },

    shadowOpacity: 0.23,
    shadowRadius: 2.62,

    elevation: 4,
  },
  homeText: {
    textAlign: "center",
    paddingTop: 5,
    fontSize: 16,
    fontWeight: "700",
  },
  greetSection: {
    paddingTop: 20,
    flexDirection: "row",
    alignItems: "center",
  },
  userAvatar: {
    paddingLeft: 15,
  },
});

export default AutoModeItem;
