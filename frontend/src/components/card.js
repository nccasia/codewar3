import React from 'react';
import styled from 'styled-components'
import Grid from '@material-ui/core/Grid';
import { useHistory } from 'react-router-dom';

export const Card = ({ width, height, data, to, label, ...props }) => {
  const history = useHistory()

  return (
    <CardContainer onClick={() => history.push(to)}>
      <Content>
        {label}
      </Content>
    </CardContainer>
  )
}



const CardContainer = styled(Grid)`
  width: 250px;
  height: 150px;
  border-radius: 10px;
  background-color: #c9c9d3;
  transition: all .4s ease-in-out;
  cursor: pointer;
  &:hover {
    transform: translateZ(-5px) scale(1.1) translateZ(0);
    box-shadow: 0 24px 36px rgba(0,0,0,0.11),
    0 24px 46px var(--box-shadow-color);
  }
`;

const Content = styled.span`
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  color: white;
  text-transform: uppercase;
  font-size: 20px;
`;

const Image = styled.img`

`;

