import { useNavigation } from "@react-navigation/native";
import React, { useRef, useState } from "react";
import {
  StyleSheet,
  Text,
  View,
  TextInput,
  TouchableOpacity,
  TouchableWithoutFeedback,
  Alert,
  Platform,
  KeyboardAvoidingView,
  Keyboard,
} from "react-native";
import { MaterialIcons, Feather } from "@expo/vector-icons";
import { useAuth } from "../../store";
import { Spinner } from "native-base";
import { users } from "./users-data";
interface LoginProps {}

const Login: React.FC<LoginProps> = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  const navigation = useNavigation();
  const { setIsAuth, setUser } = useAuth();

  const passwordRef = useRef<TextInput | null>(null);

  const login = () => {
    setLoading(true);
    const user = users.find(
      (u) =>
        u.username == email.toLowerCase().trim() &&
        u.password == password.toLowerCase().trim()
    );
    if (user) {
      setIsAuth(true);
      setUser(user);
      return;
    }
    Alert.alert("Login", "Login failed");
    setLoading(false);
  };

  return (
    <KeyboardAvoidingView
      style={styles.container}
      behavior={Platform.OS === "ios" ? "padding" : "height"}
    >
      <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
        <View style={styles.wrap}>
          <Text style={styles.logo}>Fresh</Text>
          <Text style={styles.content}>Welcome to,</Text>
          <Text style={styles.contentBottom}>Planet desert</Text>
          <Text style={styles.slogan}>
            Lorem ipsum dolor, sit amet consectetur adipisicing elit. Dolorem,
            provident. We love helping you to save the earth
          </Text>
          <View style={styles.inputView}>
            <Feather
              name="user"
              style={{ paddingRight: 15 }}
              size={20}
              color="black"
            />
            <TextInput
              style={styles.inputText}
              placeholder="Username"
              onChangeText={(text) => setEmail(text)}
              autoCorrect={true}
              placeholderTextColor="#adb8c5"
              onSubmitEditing={() => {
                passwordRef.current.focus();
              }}
              returnKeyLabel="Next"
            />
          </View>
          <View style={styles.inputView}>
            <MaterialIcons
              style={{ paddingRight: 15 }}
              name="lock-outline"
              size={20}
              color="black"
            />
            <TextInput
              inlineImageLeft="search_icon"
              secureTextEntry
              style={styles.inputText}
              placeholder="Password"
              placeholderTextColor="#adb8c5"
              onChangeText={(text) => setPassword(text)}
              ref={passwordRef}
              onSubmitEditing={login}
              returnKeyLabel="Done"
            />
          </View>
          <TouchableOpacity
            style={[styles.loginBtn, { opacity: loading ? 0.5 : 1 }]}
            onPress={login}
            disabled={loading}
          >
            {loading && (
              <Spinner
                color="white"
                size={20}
                style={{
                  marginRight: 10,
                }}
              />
            )}
            <Text style={styles.loginText}>LOGIN</Text>
          </TouchableOpacity>
        </View>
      </TouchableWithoutFeedback>
    </KeyboardAvoidingView>
  );
};

export default Login;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "white",
    justifyContent: "center",
  },
  wrap: {
    padding: 25,
  },
  content: {
    paddingTop: 20,
    textAlign: "justify",
    fontSize: 35,
    fontWeight: "bold",
  },
  contentBottom: {
    color: "#1877f2",
    textAlign: "justify",
    fontSize: 35,
    fontWeight: "bold",
  },
  slogan: {
    color: "#adb8c5",
    paddingVertical: 15,
    textAlign: "justify",
  },
  logo: {
    textTransform: "uppercase",
    textAlign: "left",
    fontWeight: "bold",
    fontSize: 30,
  },
  inputView: {
    paddingTop: 20,
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    borderBottomWidth: 1.5,
    // display: 'flex',
    // flexDirection: 'row',
    // // backgroundColor: "#fff",
    // // borderRadius: 25,
    // height: 50,
    // paddingTop: 20,
    // marginBottom: 20,
    // justifyContent: "center",
  },
  inputText: {
    flex: 1,
    paddingTop: 10,
    paddingRight: 10,
    paddingBottom: 10,
    paddingLeft: 0,
    color: "black",
  },
  forgot: {
    color: "#003f5c",
    fontSize: 11,
  },
  loginBtn: {
    backgroundColor: "#1877f2",
    borderRadius: 5,
    height: 50,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 40,
    marginBottom: 10,
    flexDirection: "row",
  },
  loginText: {
    color: "white",
    fontWeight: "bold",
  },
  nav: {
    position: "absolute",
    left: 15,
    top: 45,
  },
});
