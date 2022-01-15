import React from 'react'
import home from './Home/images/Untitled-1.png';
import '../Components/Home/Home.css';
import '../Components/Home/nicepage.css';

export default function Home() {
    return (
        <>

            <div className='u-body'>
                <section className="u-align-center u-clearfix u-white u-section-1" id="carousel_d9b3">
                    <h2 className='text1'>Welcome To Gyan Dyan</h2>
                <p className="u-large-text u-text u-text-variant u-text-2">
                            "Alone we can do so little; together we can do so much."
                        </p>
                    <div className="u-clearfix u-sheet u-sheet-1">
                        
                        <img className="u-image u-image-contain u-image-default u-image-1"
                            alt=""
                            data-image-width="1000"
                            data-image-height="300"
                            src={home} data-animation-name="zoomIn"
                            data-animation-duration="2000"
                            data-animation-direction="" />
                        
                    </div>
                </section>
            </div>





        </>


    )
}
