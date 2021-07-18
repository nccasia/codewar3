import React from 'react';
import styled from 'styled-components'

export const Card = ({ width, height, data, ...props }) => {
  return (
    <CardContainer>

    </CardContainer>
  )
}

const Box = styled.div`
  
`;

const CardContainer = styled.div`
  width: 200px;
  height: 200px;
  border-radius: 1px solid;
  &:hover {
    width: 250px;
    height: 250px;
    transition: 0.4s;
  }
`;

const Image = styled.img`

`;

