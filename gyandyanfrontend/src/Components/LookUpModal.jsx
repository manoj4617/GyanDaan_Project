import React from 'react';

export default function LookUpStudent(props) {
   console.log(typeof props.data);
   
    for(var item in props.data){
        console.log(item.studentProfile);
    }

  return (
        <>
            <div className="modal fade" id="lookUpStudent" tabIndex="-1" role="dialog" aria-labelledby="lookUpStudentTitle" aria-hidden="true">
                <div className="modal-dialog modal-dialog-centered" role="document">
                    <div className="modal-content" style={{"backgroundColor":"burlywood"}}>
                    <div className="modal-header">
                        <h5 className="modal-title text-black" id="lookUpStudentTitle">You are Tutoring</h5>
                        <button type="button" className="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div className="modal-body">
                      
                        {/* {studentData.map((item) => (
                            <ul className="list-group">
                            <li className="list-group-item fs-6 bg-dark">
                                    <div className="card" style={{"width": "24rem"}}>
                                        <div className="card-body" style={{"backgroundColor":"darkgray","borderRadius":"6px"}}>
                                            {item}
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        ))} */}
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                    </div>
                </div>
                </div>
        </>
    );
}
