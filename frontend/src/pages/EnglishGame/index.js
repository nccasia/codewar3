import * as React from 'react';
import styled from 'styled-components';
import Slider from 'react-slick';
import '../../assets/css/slick.css'


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

    var settings = {
        dots: true,
        infinite: true,
        slidesToShow: 3,
        centerMode: true,
        // centerPadding: "40px",
        slidesToScroll: 1,
        autoplay: true,
        speed: 1000,
        autoplaySpeed: 0,
        cssEase: "linear"
    };

    return (
        <>
            <div style={{ height: "100vh" }}>
                <h2>Auto Play</h2>
                <Slider {...settings}>
                    <div>
                        <Image src="https://kenh14cdn.com/thumb_w/660/203336854389633024/2021/7/9/photo-1-16257989599561090737937.jpeg" />
                    </div>
                    <div>
                        <Image src="https://kenh14cdn.com/thumb_w/660/2020/4/9/photo-1-15864365280411955650737.jpg" />
                    </div>
                    <div>
                        <Image src="https://i.pinimg.com/originals/7e/da/a4/7edaa44235f5c717d6d7c65c14727ee4.png" />
                    </div>
                    <div>
                        <Image src="https://vcdn-giaitri.vnecdn.net/2021/02/24/j-4353-1614154422.jpg" />
                    </div>
                    <div>
                        <Image src="https://static2.yan.vn/YanNews/2167221/201908/iu-la-ai-tinh-yeu-su-nghiep-bai-hat-cua-iu-4bb930f2.jpg" />
                    </div>
                    {/* <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>1</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>2</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>3</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>4</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>5</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>6</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>7</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>8</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>9</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>10</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>11</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>12</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>13</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>14</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>15</h3>
                    </div>
                    <div>
                        <h3 style={{ color: "white", border: "1px solid white" }}>16</h3>
                    </div> */}
                </Slider>
            </div>
        </>
    )
}

const Wrapper = styled.div`
    display: flex;
    justify-content: space-between;
`;

const Card = styled.div`
    height: 100px;
    width: 200px;
    background-color: ${props => props.numIndex === 2 ? 'blue' : 'white'};
    color: red;
`;

const Image = styled.img`
    height: 200px;
    border: 1px solid white;
`;

const Button = styled.div`
    width: 100px;
    height: 20px;
    background-color: white;
    color: red;
    cursor: pointer;
`;