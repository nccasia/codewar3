import React from 'react';
import styled from 'styled-components'
import tip from '../assets/images/tip.jpg'
import history from '../assets/images/history.png'
import sample from '../assets/images/sample_question.png'
import Grid from '@material-ui/core/Grid';
import { Header } from '../components/header';

let listQ = [
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
  "What's ur favorite food?",
]

export const Library = () => {
  return (
    <>
      <Header />
      <Wrapper>
        {/* <div style={{ height: '100px', width: '100vw' }}></div>
        <ItemWrapper md={5} item={true}>
          <Item src={history}></Item>
        </ItemWrapper>
        <ItemWrapper md={7} item={true}>
          <Item src={tip}></Item>
        </ItemWrapper>
        <ItemWrapper md={12} item={true}>
          <Item src={sample}></Item>
        </ItemWrapper> */}
        <Title>List Questions</Title>
        <WrapperQ>
          {
            listQ.map(item => (
              <Question>{item}</Question>
            ))
          }
        </WrapperQ>
      </Wrapper>
    </>
  )
};

const Wrapper = styled(Grid)`
  min-height: calc(100vh - 100px);
  width: 100vw;
  position: absolute;
  top: 100px;
  background-color: white;
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

const Title = styled.div`
  color: black;
  margin: 20px 0 40px 0;
  font-size: 32px;
  font-weight: 700;
  
`;

const WrapperQ = styled.div`
  display: flex;
  flex-direction: column;
  height: 350px;
  overflow-y: auto;
  width: 70%;
  margin: 0 auto;

  &::-webkit-scrollbar-track
  {
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
    border-radius: 10px;
    background-color: #F5F5F5;
  }

  &::-webkit-scrollbar
  {
    width: 7px;
    background-color: #F5F5F5;
  }

  &::-webkit-scrollbar-thumb
  {
    border-radius: 10px;
    -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
    background-color: #5c5c5c;
  }
`;

const Question = styled.div`
  height: 30px;
  border-left: 3px solid #21C0C0;
  display: flex;
  justify-content: flex-start;
  padding-left: 15px;
  margin-bottom: 20px;
  align-items: center;
`;