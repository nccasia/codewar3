import React, { useState } from 'react';
import styled, { keyframes } from 'styled-components'
import box from '../assets/images/box.svg'
import { Modal } from '../components/modelOpen';
import './style.css'

export const LuckyDraw = () => {
  const [open, setOpen] = useState(false);
  const openModal = () => {
    setOpen(true);
  }
  return (
    <Wrapper>
      <DivModal open={open}>
        <Modal />
      </DivModal>
      <WrapperLucky>
        <Div>
          <LuckyBox onClick={() => openModal()} src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
          <LuckyBox src={box}></LuckyBox>
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
    </Wrapper>
  )
};

const Wrapper = styled.div`
  height: 100vh;
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
    height: 100vh;
    width: 30% !important;
    align-items: center;
    position: absolute;
    justify-content: center;
    flex-direction: row;
    align-content: center;
    flex-wrap: wrap;
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
  width: 100px;
  height: 100px;
  margin: 10px;
  z-index: 999;
  cursor: pointer;
  &:hover {
    animation: ${shake} 1s ease-out;
    animation-iteration-count: infinite;
  }
`

const DivModal = styled.div`
height: 100vh;
top: 0;
width: 100vw;
  display: ${props => props.open ? 'flex' : 'none'};
  transition: display 0.5s;
  position: absolute;
    align-items: center;
    justify-content: center;
    z-index: 9999;
    box-shadow: inset 0 0 0 2000px #000000ab;
`;