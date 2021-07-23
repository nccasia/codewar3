import React from 'react';
import './style.css'
import englishReward from '../assets/images/englishReward.png'
import ClickButton from "../components/clickButton";

export const EnglishReward = () => {
    return (
        <div className="container english-reward">
            <div className="big-title">
                <h1 className="text-white">Let’s find the winner! </h1>
            </div>
            <div className="img-home">
                <img src={englishReward} alt="english-reward"/>
            </div>
            <ClickButton color={'btn-3d btn-purple'} link={'/english-reward-instruction'} value={'Click Me'}/>
        </div>
    )

}