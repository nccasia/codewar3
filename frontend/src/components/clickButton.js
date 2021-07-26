import React from 'react';
import {useHistory} from "react-router-dom";

export default function ClickButton({ color, value, link, ...props }) {
    const history = useHistory()

    return (
        <a className={color} data-title={value} onClick={() => history.push(link)}>
        </a>
    );
}