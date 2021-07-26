import styled from 'styled-components';
import React from 'react'
import OutsideClickHandler from 'react-outside-click-handler';
import './modal.css'

export const Modal = ({ gift, handleClose }) => {

  return (
    <OutsideClickHandler
      onOutsideClick={() => {
        handleClose();
      }}
    >
      <Wrapper>
        <div class="container">
          <div class="row">
            <div class="col-12">
            </div>
            <div class="col-12 mt-5 d-flex justify-content-center">
              <div class="box">
                <div class="box-body">
                  <div class="img"><strong>Chúc mừng bạn đã nhận được</strong> <br /> {gift}</div>
                  <div class="box-lid">

                    <div class="box-bowtie"></div>

                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </Wrapper>
    </OutsideClickHandler>
  )
};

const Wrapper = styled.div`
  cursor: pointer;
`;