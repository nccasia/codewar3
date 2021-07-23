import { useState, useEffect } from "react";
import * as React from "react";
import {
  StyleSheet,
  View,
  Text,
  TouchableOpacity,
  SafeAreaView,
  FlatList,
} from "react-native";
import { ShortCutItem, GeneralInfo, AutoModeItem } from "../../components";
import UserAvatar from "@muhzi/react-native-user-avatar";
import {
  EvilIcons,
  Ionicons,
  AntDesign,
  FontAwesome,
  Entypo,
  MaterialCommunityIcons,
} from "@expo/vector-icons";
import { DrawerActions, useNavigation } from "@react-navigation/native";
import { useAuth } from "../../store";
import { Spinner } from "native-base";
import {} from "react";
import * as firebase from "firebase";

import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app(); // if already initialized, use that one
}

const App = () => {
  const [loading, setLoading] = useState<boolean>(true);
  const { user } = useAuth();
  const generalDataInfo = [
    {
      path: "rain/value",
      type: "weather",
      id: 1,
    },
    {
      path: "dht11/temperature",
      name: "Outside Temp",
      type: "°C",
      id: 2,
    },
    {
      path: "mh/value",
      name: "Humi",
      type: "%",
      id: 3,
    },
  ];
  const renderGeneralInfo = () => {
    return generalDataInfo.map((item) => {
      return (
        <GeneralInfo
          key={item.id}
          path={item.path}
          name={item.name}
          type={item.type}
        />
      );
    });
  };

  useEffect(() => {
    getData();
  }, []);
  const getData = async () => {
    setLoading(false);
  };
  const shortCutData = [
    {
      name: "light",
      id: 1,
      icon: "lightbulb-on-outline",
    },
    {
      name: "pump",
      icon: "water",
      id: 2,
    },
    {
      name: "servo",
      icon: "home-roof",
      id: 3,
    },
  ];

  const data = [
    {
      name: "Light",
      id: 1,
      path: "light",
      icon: "lightbulb-on-outline",
    },
    {
      name: "Water",
      id: 2,
      path: "pump",
      icon: "water",
    },
    {
      name: "Roof",
      path: "servo",
      id: 3,
      icon: "home-roof",
    },
  ];

  return (
    <SafeAreaView style={styles.screen}>
      {loading ? (
        <Spinner />
      ) : (
        <View style={styles.container}>
          <View style={styles.greetSection}>
            <UserAvatar
              userName={user.fullname}
              size={50}
              backgroundColor="#0be881"
              src={
                user && user.image
                  ? user.image
                  : "https://cdn4.vectorstock.com/i/1000x1000/86/88/woman-female-avatar-character-vector-11708688.jpg"
              }
            />
            <View style={styles.userAvatar}>
              <Text style={{ fontWeight: "500", fontSize: 17 }}>
                Hello!
                <Text style={{ fontWeight: "600", color: "#1877f2" }}>
                  {" "}
                  {user.fullname}
                </Text>
              </Text>
              <Text style={{ paddingTop: 5, color: "#adb8c5" }}>
                Lorem ipsum dolor sit amet.
              </Text>
            </View>
          </View>
          <View style={{ paddingTop: 20 }}>
            <Text
              style={{
                fontWeight: "600",
                fontSize: 17,
                paddingBottom: 15,
                paddingHorizontal: 15,
              }}
            >
              General Information
            </Text>
            <View style={{ flexDirection: "row" }}>{renderGeneralInfo()}</View>
          </View>
          <View>
            <Text
              style={{
                fontWeight: "600",
                fontSize: 17,
                paddingTop: 15,
                paddingHorizontal: 15,
              }}
            >
              Short cuts
            </Text>
            <View style={{ paddingHorizontal: 5 }}>
              <FlatList
                numColumns={2}
                scrollEnabled={false}
                keyExtractor={(item: any) => item.id}
                data={shortCutData}
                columnWrapperStyle={{
                  justifyContent: "space-between",
                }}
                renderItem={(itemData) => (
                  <ShortCutItem
                    name={itemData.item.name}
                    icon={itemData.item.icon}
                  />
                )}
              />
            </View>
          </View>
          <View style={{ marginBottom: 100 }}>
            <Text
              style={{
                fontWeight: "600",
                fontSize: 17,
                paddingTop: 15,
                paddingHorizontal: 15,
              }}
            >
              Auto mode
            </Text>
            <FlatList
              horizontal
              data={data}
              keyExtractor={(item: any) => item.id}
              renderItem={(itemData) => (
                <AutoModeItem
                  name={itemData.item.name}
                  path={itemData.item.path}
                  icon={itemData.item.icon}
                />
              )}
            />
          </View>
        </View>
      )}
    </SafeAreaView>
  );
};
const styles = StyleSheet.create({
  screen: {
    backgroundColor: "white",
  },
  container: {},
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

export default App;
