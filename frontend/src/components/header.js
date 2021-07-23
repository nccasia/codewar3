import styled from 'styled-components';
import React from 'react'
import { Link } from 'react-router-dom';
import logo from '../assets/images/logo.png'

export const Header = () => {

  return (
    <WrapperHeader>
      <WrapperLogo href='/'>
        <Logo src={logo}></Logo>
      </WrapperLogo>
      <WrapperContent>
        <Ul>
          <Li>
            <Linkk to='/opentalk'>opentalk</Linkk>
          </Li>
          <Li>
            <Linkk to='/'>lucky draw</Linkk>
          </Li>
          <Li>
            <Linkk to="/lib">english</Linkk>
          </Li>
        </Ul>
      </WrapperContent>
    </WrapperHeader>
  )
};

const WrapperHeader = styled.div`
  height: 100px;
  top: 0;
  position: fixed;
  display: flex;
  width: 100vw;
  background-color: white;
  z-index: 999;
  flex-direction: row;
  justify-content: space-between;
  z-index: 9999;
`

const WrapperLogo = styled.a`
  cursor: pointer;
  height: 100%;
  margin-left: 50px;
`;

const Logo = styled.img`
height: 100%;
`

const WrapperContent = styled.div`

`

const Ul = styled.ul`
      display: flex;
    list-style: none;
    height: 100%;
    align-items: center;
    margin-right: 50px;
`

const Li = styled.li`
  padding: 0 30px;
  cursor: pointer;
  height: 100%;
  display: flex;
  align-items: center;
  font-size: 18px;
  font-weight: 600;
`

const Linkk = styled(Link)`
  text-decoration: none;
`