import React from 'react';
import styled from 'styled-components'
import tip from '../assets/images/tip.jpg'
import history from '../assets/images/history.png'
import sample from '../assets/images/sample_question.png'
import Grid from '@material-ui/core/Grid';
import { Header } from '../components/header';

export const Library = () => {
  return (
    <>
      <Header />
      <Wrapper container spacing={0}>
        <div style={{ height: '100px', width: '100vw' }}></div>
        <ItemWrapper md={5} item={true}>
          <Item src={history}></Item>
        </ItemWrapper>
        <ItemWrapper md={7} item={true}>
          <Item src={tip}></Item>
        </ItemWrapper>
        <ItemWrapper md={12} item={true}>
          <Item src={sample}></Item>
        </ItemWrapper>
      </Wrapper>
    </>
  )
};

const Wrapper = styled(Grid)`
  height: 100vh;
  &:hover div{
    opacity: 0.7;
    transition: 0.5s;
  }
`

const ItemWrapper = styled(Grid)`
  cursor: pointer;
  &:hover {
    opacity: 1 !important;
  }
`
const Item = styled.img`
  width: 100%;
`