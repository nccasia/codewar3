import styled from 'styled-components';
import React from 'react'
import './modal.css'

export const Modal = ({ gift }) => {
  // alert(gift)
  return (
    <Wrapper>
      <div class="container">
        <div class="row">
          <div class="col-12">
            {/* <h3 class="text-center text-light my-5"><strong>Hover the box</strong></h3> */}
          </div>
          <div class="col-12 mt-5 d-flex justify-content-center">
            <div class="box">
              <div class="box-body">
                <div class="img">{gift}</div>
                <div class="box-lid">

                  <div class="box-bowtie"></div>

                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </Wrapper>
  )
};

const Wrapper = styled.div`

      `