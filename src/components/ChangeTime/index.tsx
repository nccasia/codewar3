import React, { memo, useState, useEffect } from "react";
import { Text, View, StyleSheet, Platform, FlatList } from "react-native";
import firebase from "firebase";
import ListItem from './ListItem'
import { firebaseConfig } from "../../../firebase";
if (!firebase.apps.length) {
  firebase.initializeApp(firebaseConfig);
} else {
  firebase.app();
}

interface ChangeTimeProps {}



const ChangeTime: React.FC<ChangeTimeProps> = memo(({}) => {
  const hourRender = []
  for (let h = 1; h < 25; h++) { 
   hourRender.push({value: h, label: h})
  }
  const minutesRender = []
  for (let m = 1; m < 61; m++) { 
    minutesRender.push({value: m, label: m})
   }

  return (
    <View style={{ flexDirection: 'row', justifyContent: 'center', alignItems: 'center'}}>
    <View>
    <View style={ styles.list } >
			<FlatList
			showsVerticalScrollIndicator={false}
				onMomentumScrollEnd={ ( event ) => {
					let index = Math.round( event.nativeEvent.contentOffset.y / 80 );
					// onChange( { index, item: items[ index ] } );
          console.log(index)
				} }
				initialScrollIndex={ 2 }
			
				data={ hourRender.map( item => ( {
					key: item.value.toString(),
					...item
				} ) ) }
				renderItem={ item => (
					<ListItem
						label={ item.item.label }
						 />
				 ) }
         getItemLayout={ ( _, index ) => ( { length: 80, offset: index * 80, index } ) }
         snapToInterval={ 80 }
			/>
		</View>
    </View>
    <Text style={{ fontSize: 80, fontWeight: 'bold', paddingHorizontal: 20}}>:</Text>
    <View style={ styles.list } >
			<FlatList
			showsVerticalScrollIndicator={false}
				onMomentumScrollEnd={ ( event ) => {
					let index = Math.round( event.nativeEvent.contentOffset.y / 80 );
					// onChange( { index, item: items[ index ] } );
          console.log(index)
				} }
				// initialScrollIndex={ 2 }
			
				data={ hourRender.map( item => ( {
					key: item.value.toString(),
					...item
				} ) ) }
				renderItem={ item => (
					<ListItem
						label={ item.item.label }
						 />
				 ) }
				getItemLayout={ ( _, index ) => ( { length: 80, offset: index * 80, index } ) }
				snapToInterval={ 80 }
			/>
		</View>
    </View>
  );
});
const styles = StyleSheet.create({
  list: {
    height: 80,
    width: 100,
  },
  listItem: {
   
    zIndex: 2,
    color: 'white'
  },
});

export default ChangeTime;
