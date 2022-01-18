import React from 'react'
import { days, typeOfClass } from "./Constants/Constants";
import { Nav  } from 'react-bootstrap'
import { useNavigate } from 'react-router';


export default function ReqTable(props) {
    const navigate = useNavigate();
    const sendData=(data)=>{
        data = JSON.stringify(data);
        navigate(`/update-requirement/${data}`);
    }
    return (
        <>
            <div className="container">
                    <div className="row">
                    <div className="col-md-offset-1 col-md-12">
                        <div className="panel">

                        <div className="panel-heading">
                            <div className="row">
                            {props.userData === null ? (
                                <div className="col col-sm-3 col-xs-12">
                                <p className="text-white">No Requirements</p>
                                </div>
                            ) : (
                                <div className="col col-sm-3 col-xs-12">
                                <p className="text-white">Your Requirements</p>
                                </div>
                            )}


                            <div className="col-sm-9 col-xs-12 text-left">
                                <div className="btn_group">
                                <input type="text" className="form-control" placeholder="Search" />
                                </div>
                            </div>
                            </div>
                        </div>
                        <div className="panel-body table-responsive">
                            <table className="table">
                            <thead>
                                <tr>
                                <th>SL No</th>
                                <th>Subject</th>
                                <th>Topic</th>
                                <th>From Day</th>
                                <th>Till Day</th>
                                <th>From</th>
                                <th>Till</th>
                                <th>Type Of Class</th>
                                <th>Status</th>
                                <th>Edit</th>
                                </tr>
                            </thead>

                            <tbody>
                                {props.userData.map((item, index) => (
                                <tr>
                                    <td>{index + 1}</td>
                                    <td>{item.subject}</td>
                                    <td>{item.topic}</td>
                                    <td>{days[item.startDay]}</td>
                                    <td>{days[item.endDay]}</td>
                                    <td>{item.startTime}</td>
                                    <td>{item.endTime}</td>
                                    <td>{typeOfClass[item.typeOfClass]}</td>
                                    {item.oneToOne != null ? (
                                    <>
                                        <td className="text-success p-1">
                                        <button
                                            className="btn btn-success"
                                            type="button">
                                            Accepted
                                        </button>
                                        </td>
                                    </>
                                    ) : item.group != null ? (
                                    <>
                                        <td className="text-success p-1">
                                        <a
                                            href="/"
                                            className="text-success"
                                        >
                                            Accepted
                                        </a>
                                        </td>
                                    </>
                                    ) : item.oneToOne === null && item.group === null ? (
                                    <>
                                        <td className="text-warning p-1">Not yet Accepted</td>
                                        <td>
                                                <i className="fa fa-edit" onClick={()=>sendData(item)}/>
                                        </td>
                                    </>
                                    ) : null}
                                </tr>
                                ))}
                            </tbody>

                            </table>
                        </div>
                        </div>
                    </div>
                    </div>
                </div>
        </>
    )
}
