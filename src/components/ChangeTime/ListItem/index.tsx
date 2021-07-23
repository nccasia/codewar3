import React, { memo, useState, useEffect } from "react";
import { Text, View, StyleSheet, Platform } from "react-native";
import CircleSlider from "react-native-circle-slider";
import DateTimePicker from "@react-native-community/datetimepicker";
import Slider from "@react-native-community/slider";

interface ListItemProps {
    label: any
  
}



const ListItem: React.FC<ListItemProps> = memo(({
    label,
   
}) => {


  return (
   <View style={{ height: 80, alignItems: 'center', justifyContent: 'center'}}>
    <Text style={styles.text}>{ label }</Text>
 </View>
  );
});
const styles = StyleSheet.create({
    text: {
        fontSize: 80,
        textAlign: 'center',
        fontWeight: 'bold'
    }
})


export default ListItem;
