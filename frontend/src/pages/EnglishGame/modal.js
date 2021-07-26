import styled from 'styled-components';
import React from 'react'
import LogoWin from '../../assets/images/wingame.svg'
import OutsideClickHandler from 'react-outside-click-handler';

export const ModalEnglishGame = ({ data, handleClose }) => {
  console.log(data);
  return (
    <OutsideClickHandler
      onOutsideClick={() => {
        handleClose();
      }}
    >
      <Wrapper>
        <Logo src={LogoWin}></Logo>
        <Name>{data}</Name>
      </Wrapper>
    </OutsideClickHandler>
  )
};

const Wrapper = styled.div`
  cursor: pointer;
`;

const Logo = styled.img`
  width: 600px;
  margin-top: -100px;
  height: auto;
`
const Name = styled.div`
  margin-top: -150px;
  font-size: 28px;
  font-weight: bold;
  text-transform: uppercase;
  color: white;
`