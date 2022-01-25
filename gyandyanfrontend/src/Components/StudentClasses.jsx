import React,{useEffect, useState} from 'react';
import { httpClient } from '../http/httpclient';
import { useSelector } from 'react-redux';
import {decode} from '../utils/jwt';
import {days } from "./Constants/Constants";

export default function StudentClasses() {

const authStatus = useSelector((state) => state.auth);
var id = decode(authStatus.token);
var name = id.unique_name;
id = id.Id;

const [studentReqData, setStudentReqData] = useState();

useEffect(()=>{
    httpClient.get(`requirement/get-student-requirement/${id}`).then((res) => {
        console.log(res)
        setStudentReqData(res.data);
      })
      .catch((error)=>{
          console.log(error)
      });
},[]);

  return (
        <>  
            <h2>{name} your classes</h2>
            <div className="container h-75">
        <div className="row">
          <div className="col-md-6 p-3">
                <h4 style={{"color":"black"}}>One to One classes</h4>
                <hr />
                {studentReqData.map((item, index) => (
                    <div className="row  justify-content-center">
                        {item.typeOfClass === 0 ? (
                            <div className="col-lg-8">
                                <div className="card class-card p-3">
                                    <div className="d-flex justify-content-between">
                                        <div className="d-flex flex-row align-items-center">
                                            <div className="ms-2 c-details">
                                                <h6 className="mb-0">{item.oneToOne.volunteerProfile.firstName} {item.oneToOne.volunteerProfile.lastName}</h6> 
                                                <span className="font-weight-light">{item.oneToOne.volunteerProfile.email}<br/>
                                                {item.oneToOne.volunteerProfile.mobileNumber}</span>
                                            </div>
                                        </div>
                                        <div className="badge"> <span>Tutored</span> </div>
                                    </div>
                                    <div className="mt-0">
                                        <h4 className="heading">Subject: {item.subject}<br/>Topic: {item.topic}</h4>
                                        <div className="mt-3">
                                            <p className="font-italic">{days[item.startDay]} to {days[item.endDay]}<br/>
                                        {item.startTime} to {item.endTime}</p>
                                        </div>
                                    </div>
                                    
                                </div>
                            </div> 
                        ) : null}
                    </div>
                ))}
          </div>
          </div>
          </div>  
        </>
    );
}
