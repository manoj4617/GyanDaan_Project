import React from 'react'
import { days, typeOfClass } from "./Constants/Constants";

export default function PendingReq(props) {
    return (
        <>
            <div className="container">
                <div className="row">
                <div className="col-md-offset-1 col-md-12">
                    <div className="panel">
                    <div className="panel-heading">
                        <div className="row">
                        <div className="col col-sm-3 col-xs-12">
                            <p className="text-white">Pending Requests</p>
                        </div>
                        <div className="col-sm-9 col-xs-12 text-right">
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
                            <th>View</th>


                            </tr>
                        </thead>
                        <tbody>
                            {props.result.map((item, index) => (
                            <tr>
                                <td>{index + 1}</td>
                                <td>{item.subject}</td>
                                <td>{item.topic}</td>
                                <td>{days[item.startDay]}</td>
                                <td>{days[item.endDay]}</td>
                                <td>{item.startTime}</td>
                                <td>{item.endTime}</td>
                                <td>{typeOfClass[item.typeOfClass]}</td>
                                <td className="text-warning">Pending</td>
                                <td><i className="fa fa-eye" aria-hidden="true" href="#"></i></td>

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
