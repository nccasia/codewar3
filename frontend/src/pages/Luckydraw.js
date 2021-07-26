import React, { useState } from 'react';
import styled, { keyframes } from 'styled-components'
import box from '../assets/images/box.svg'
import { Header } from '../components/header';
import { Modal } from '../components/modelOpen';
import './style.css'

const listGift = [
  '1 ly trà sửa 35k',
  '1 chuyến du lịch cầu Rồng 100k',
  '1 ly nước mía 10k full topping',
  'còn cái nịt',
  '200k luôn nè',
  '250k mời trà sữa cả công ty',
  '80k hiuhiu'
]

export const LuckyDraw = () => {
  const [open, setOpen] = useState(false);
  const [gift, setGift] = useState('');

  const RandomGift = () => {
    const g = listGift[Math.floor(Math.random() * listGift.length)];
    setGift(g);
  }

  const openModal = (stt) => {
    setOpen(stt);
  }
  return (
    <Wrapper>
      <Header />
      <DivModal open={open}>
        <Close onClick={() => openModal(false)}>X</Close>
        <Modal gift={gift} handleClose={() => openModal(false)} />
      </DivModal>
      <WrapperLucky>
        <Div>
          <div style={{ height: '90px', width: '100vw' }}></div>
          <Title>LET'S PARTY!</Title>
          <Child>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
          </Child>
          <Child>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
            <LuckyBox onClick={() => { openModal(true); RandomGift() }} src={box}></LuckyBox>
          </Child>
          <Title>Pick your favorite!</Title>
        </Div>
      </WrapperLucky>
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
    </Wrapper >
  )
};

const Wrapper = styled.div`
  height: 100vh;
`

const Close = styled.div`
  width: 30px;
  height: 30px;
  font-weight: bold;
  font-size: 24px;
  cursor: pointer;
  background-color: whitesmoke;
  color: blue;
  top: 40px;
    right: 60px;
    position: absolute;
`

const WrapperLucky = styled.div`
      width: 50%;
    height: 0;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    width: 100vw;
`
const Div = styled.div`
  display: flex;
    height: 100%;
    width: 100% !important;
    align-items: center;
    position: absolute;
    flex-direction: column;
    justify-content: center;
    bottom: 0;
    z-index: 999;
    /* top: 50px; */
    /* overflow: hidden; */
`

const Child = styled.div`

`
const shake = keyframes`
  0% { transform: translate(1px, 1px) rotate(0deg) };
  10% { transform: translate(-1px, -2px) rotate(-1deg)};
  20% { transform: translate(-3px, 0px) rotate(1deg) };
  30% { transform: translate(3px, 2px) rotate(0deg);};
  40% { transform: translate(1px, -1px) rotate(1deg)};
  50% { transform: translate(-1px, 2px) rotate(-1deg)};
  60% { transform: translate(-3px, 1px) rotate(0deg) };
  70% { transform: translate(3px, 1px) rotate(-1deg) };
  80% { transform: translate(-1px, -1px) rotate(1deg)};
  90% { transform: translate(1px, 2px) rotate(0deg) };
  100% { transform: translate(1px, -2px) rotate(-1deg) };
`

const LuckyBox = styled.img`
  width: 15%;
  height: auto;
  margin: 10px;
  z-index: 999;
  cursor: pointer;
  &:hover {
    animation: ${shake} 1s ease-out;
    animation-iteration-count: infinite;
  }
`

const Title = styled.div`
  font-size: 28px;
  font-weight: bold;
  color: white
`;

const DivModal = styled.div`
height: 100vh;
top: 0;
cursor: pointer;
width: 100vw;
  display: ${props => props.open ? 'flex' : 'none'};
  transition: display 0.5s;
  position: absolute;
    align-items: center;
    justify-content: center;
    z-index: 9999;
    box-shadow: inset 0 0 0 2000px #000000ab;
`;