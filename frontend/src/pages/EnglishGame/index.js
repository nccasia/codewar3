import * as React from 'react';
import styled from 'styled-components';
import Slider from 'react-slick';
import '../../assets/css/slick.css'
import { Header } from '../../components/header';
import '../style.css'

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
    const [settings, setSetting] = React.useState({
        dots: false,
        infinite: true,
        slidesToShow: 3,
        centerMode: true,
        arrows: false,
        centerPadding: "40px",
        autoplay: true,
        speed: 10,
        autoplaySpeed: 0,
    })

    let slider;



    const ClickPlay = () => {
        let time = Math.floor(Math.random() * (6000 - 4000) + 4000);
        let sp = settings.speed;
        setInterval(() => {
            console.log(time, sp);
            if (time < 1000) {
                slider?.slickPause();
                return
            }
            time = time * 0.8;
            sp = sp + 100;
            setSetting({ ...settings, speed: sp })
        }, time * 0.8);
    }


    return (
        <Div>
            <Header />
            <WrapperSlider>
                <h2 style={{ marginBottom: '30px' }}>Let't find a lucky member!</h2>
                <Slider ref={c => (slider = c)} {...settings}>
                    <Image src="https://kenh14cdn.com/thumb_w/660/203336854389633024/2021/7/9/photo-1-16257989599561090737937.jpeg" />
                    <Image src="https://kenh14cdn.com/thumb_w/660/2020/4/9/photo-1-15864365280411955650737.jpg" />
                    <Image src="https://i.pinimg.com/originals/7e/da/a4/7edaa44235f5c717d6d7c65c14727ee4.png" />
                    <Image src="https://vcdn-giaitri.vnecdn.net/2021/02/24/j-4353-1614154422.jpg" />
                    <Image src="https://static2.yan.vn/YanNews/2167221/201908/iu-la-ai-tinh-yeu-su-nghiep-bai-hat-cua-iu-4bb930f2.jpg" />
                </Slider>
                <Button onClick={ClickPlay}>play</Button>
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
`;