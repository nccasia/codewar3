import * as React from 'react';
import styled from 'styled-components';
import Slider from 'react-slick';
import '../../assets/css/slick.css'
import { Header } from '../../components/header';
import '../style.css'
import { ModalEnglishGame } from './modal'
import {useEffect} from "react";

let listGame = [
    {
        index: 1,
        name: 'Nhan'
    },
    {
        index: 2,
        name: 'Oanh'
    },
    {
        index: 3,
        name: 'Manh'
    },
    {
        index: 4,
        name: 'Tuan'
    },
    {
        index: 5,
        name: 'Thien'
    }
]

export const EnglishGame = () => {
    const [current, setCurrent] = React.useState();
    const [open, setOpen] = React.useState(false);
    const [data, setData] = React.useState();
    const [settings, setSetting] = React.useState({
        dots: false,
        infinite: true,
        slidesToShow: 3,
        centerMode: true,
        arrows: false,
        centerPadding: "40px",
        autoplay: false,
        speed: 10,
        autoplaySpeed: 0,
        beforeChange: (current, next) => setCurrent(next),
        afterChange: current => setCurrent(current)
    })

    let slider;

    const openModal = (stt) => {
        setOpen(stt);
    }

    const ClickPlay = () => {
        slider.slickPlay()
    }
    const ClickPause = () => {
        slider.slickPause();
        setData(current)
        setTimeout(() => {
            setOpen(true);
        }, 1100);
    }

useEffect(() => {
        let fetchUrl = 'http://127.0.0.1:8000/api/employees'
         fetch(fetchUrl)
        .then(result => result.json())
        .then(curentData => setCurrent(
            (curentData.data)
        ))
    }, []);
    return (
        <Div>
            <Header />
            <DivModal open={open}>
                <Close onClick={() => openModal(false)}>X</Close>
                <ModalEnglishGame data={data} handleClose={() => openModal(false)} />
            </DivModal>
            <WrapperPeople>
                <Imagepeople src="https://kenh14cdn.com/thumb_w/660/2020/4/9/photo-1-15864365280411955650737.jpg" />
                <Imagepeople src="https://i.pinimg.com/originals/7e/da/a4/7edaa44235f5c717d6d7c65c14727ee4.png" />
                <Imagepeople src="https://vcdn-giaitri.vnecdn.net/2021/02/24/j-4353-1614154422.jpg" />
                <Imagepeople src="https://static2.yan.vn/YanNews/2167221/201908/iu-la-ai-tinh-yeu-su-nghiep-bai-hat-cua-iu-4bb930f2.jpg" />
                <Imagepeople src="https://kenh14cdn.com/thumb_w/660/203336854389633024/2021/7/9/photo-1-16257989599561090737937.jpeg" />
            </WrapperPeople>
            <WrapperSlider>
                <Title style={{ marginBottom: '30px', }}>Let't find a lucky member!</Title>
                <Slider ref={c => (slider = c)} {...settings}>
                    <Image src="https://kenh14cdn.com/thumb_w/660/203336854389633024/2021/7/9/photo-1-16257989599561090737937.jpeg" />
                    <Image src="https://kenh14cdn.com/thumb_w/660/2020/4/9/photo-1-15864365280411955650737.jpg" />
                    <Image src="https://i.pinimg.com/originals/7e/da/a4/7edaa44235f5c717d6d7c65c14727ee4.png" />
                    <Image src="https://vcdn-giaitri.vnecdn.net/2021/02/24/j-4353-1614154422.jpg" />
                    <Image src="https://static2.yan.vn/YanNews/2167221/201908/iu-la-ai-tinh-yeu-su-nghiep-bai-hat-cua-iu-4bb930f2.jpg" />
                </Slider>
                <WrapperButton>
                    <Button onClick={ClickPlay}>play</Button>
                    <Button onClick={ClickPause}>stop</Button>
                </WrapperButton>
            </WrapperSlider>
            <div className="area" >
                <ul className="circles">
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div >
        </Div >
    )
}



const Div = styled.div`
    height: 100vh;
`;

const Title = styled.div`
  font-size: 28px;
  font-weight: bold;
  color: white
`;


const Card = styled.div`
    height: 100px;
    width: 200px;
    background-color: ${props => props.numIndex === 2 ? 'blue' : 'white'};
    color: red;
`;

const WrapperSlider = styled.div`
    position: absolute;
    width: 100vw;
    display: flex;
    align-items: center;
    height: 100vh;
    flex-direction: column;
    justify-content: center;
    z-index: 999;
`

const Image = styled.img`
    height: 200px;
    border: 1px solid white;
`;

const Button = styled.div`
    width: 100px;
    height: 40px;
    font-size: 16px;
    font-weight: bold;
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 10px;
    background-color: white;
    color: red;
    margin-top: 30px;
    cursor: pointer;
    z-index: 999;
    margin: 30px 10px;
    &:hover{
        transform: scale(1.1);
        transition: all 0.2s;
    }
`;

const WrapperPeople = styled.div`
display: flex;
    position: absolute;
    flex-direction: column;
    top: 25%;
    left: 70px;
`

const Imagepeople = styled.img`
    width: 70px;
    height: 70px;
    margin: 10px 0;
    border-radius: 10px;
`

const WrapperButton = styled.div`
    display: flex;
`

const DivModal = styled.div`
    height: 100vh;
    top: 0;
    cursor: pointer;
    width: 100vw;
    opacity: 1;
    display: ${props => props.open ? 'flex' : 'none'};
    transition: display 0.5s;
    position: absolute;
    align-items: center;
    justify-content: center;
    z-index: 9999;
    box-shadow: inset 0 0 0 2000px #000000ab;
    transition: opacity 0.8s;
    transition-delay: 1.5s;
`;

const Close = styled.div`
    width: 30px;
    height: 30px;
    font-weight: bold;
    font-size: 24px;
    cursor: pointer;
    background-color: whitesmoke;
    color: blue;
    top: 40px;
    right: 60px;
    position: absolute;
`