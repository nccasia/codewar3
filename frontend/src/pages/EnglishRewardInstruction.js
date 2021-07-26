import React from 'react';
import './style.css'
import slotmachine from '../assets/images/slotmachine.gif'
import ClickButton from "../components/clickButton";

export const EnglishRewardInstruction = () => {
    return (
        <div className="english-reward-instruction">
            <div className="left-content">
                <p className="text-instruct"><b>IT ONLY TAKES ONE</b>
                    <br/>Press the button and wait for your lucky.
                </p>
                <ClickButton color={'btn-3d btn-green'} value="Let's go" link="/eng"/>
            </div>
            <div className="right-content">
                <img src={slotmachine} alt="english-reward"/>
            </div>
        </div>
    )

}

