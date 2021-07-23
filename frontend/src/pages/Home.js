import React, { useEffect, useState } from 'react';
import styled from 'styled-components'
import Grid from '@material-ui/core/Grid';
import { Card } from '../components/card';
import CardNew from '../components/cardNewfeed';
import Flickity from 'react-flickity-component';
import './style.css'
import './flickity.css'
import { Header } from '../components/header';

export const Home = () => {
  let flkty;
  let [carouselIndex, setCarouselIndex] = useState(2);

  const handleChange = index => {
    setCarouselIndex(index); // Not working
  };

  useEffect(() => {
    if (flkty) {
      console.log("this ran");
      flkty.on("settle", () => {
        console.log(`current index is ${flkty.selectedIndex}`);
      });

      flkty.on("change", index => {
        handleChange(index);
      });
    }
  }, [carouselIndex]);

  return (
    <Div>
      <Wrapper>
        <Box direction="row" container>
          <Card to='/lucky' label="Opentalk" />
          <Card to='/eng' label="English Game" />
          <Card to='/lib' label="Library" />
        </Box>
        <NewFeed>
          <Flickity flickityRef={c => (flkty = c)} options={{ initialIndex: carouselIndex }}>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
            <div>
              <CardNew />
            </div>
          </Flickity>
        </NewFeed>
      </Wrapper>
      <div className="area" >
        <ul className="circles">
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
          <li></li>
        </ul>
      </div >
    </Div>
  )
}

const Div = styled.div`
height: 100vh;
`

const Wrapper = styled.div`
      position: absolute;
      z-index: 999;
      width: 100%;
      `

const Box = styled(Grid)`
      height: 30%;
      padding: 20px 200px;
      align-items: center;
      display: flex;
      flex-direction: row;
      justify-content: space-around;
      `;

const NewFeed = styled.div`
      height: 65%;
      margin-top: 100px;
      padding: 0 30px;
      /* display: flex; */
      /* overflow-x: scroll; */
      `
