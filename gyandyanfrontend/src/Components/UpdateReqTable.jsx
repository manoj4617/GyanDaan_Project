import React,{useState,useMemo,useCallback} from "react";
import { useFormik } from "formik";
import { DaysUpdate, TypeOfClassUpdate } from "./FormSelect/UpdateSelect";
import { days, typeOfClass } from "./Form";
import { subjects } from "./FormSelect/subjects/subjects";
import {TopicSelectUpdate,SubjectSelectUpdate} from "./FormSelect/UpdateSelect";
import { httpClient } from "../http/httpclient";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";
import { useParams } from 'react-router-dom';

export default function UpdateReqTable(props) {
    var {data} = useParams();
    data = JSON.parse(data);

    const [selectedTopic, setselectedTopic] = useState([]);
    const [errorMessage, seterrorMessage] = useState();
    const authStatus = useSelector((state) => state.auth);
    const userInfo = decode(authStatus.token);

    const subs = useMemo(
        () =>
        subjects.map((oneItem) => ({
            label: oneItem.label,
            key: oneItem.name,
          })),
    )
    let options = null;
    if (selectedTopic) {
      options = selectedTopic.map((oneItem) => ({
        label: oneItem.label,
        key: oneItem.name,
      }))
    }
  const formik = useFormik({
    initialValues: {
      subject: data.subject,
      topic: data.topic,
      startDay: data.startDay,
      endDay: data.endDay,
      startTime: data.startTime,
      endTime: data.endTime,
      timeOfStart: data.timeOfStart,
      typeOfClass: data.typeOfClass,
    },
    // validationSchema: requirementValidations,
    onSubmit: (values) => {
      console.log(values);
      if(userInfo.Roles === "Student"){
        httpClient.put(`requirement/update-student-requirement/${data.id}`, values).then((res) => {
          console.log(res)
          seterrorMessage(res.data);

        })
        .catch((error)=>{
            console.log(error);
            if(error.response.status === 500){
              seterrorMessage("Invalid Credentials");
            }
        });
      }

      if(userInfo.Roles === "Volunteer"){
        httpClient.put(`requirement/update-volunteer-requirement/${data.id}`, values).then((res) => {
          console.log(res)
          seterrorMessage(res.data);

        })
        .catch((error)=>{
            console.log(error);
            if(error.response.status === 500){
              seterrorMessage("Invalid Credentials");
            }
        });
      }
     formik.resetForm();
    },
  });
    const onSelectTopic = useCallback((selectedItem) => {
        formik.setFieldValue("topic", selectedItem.label);
      }); 

    const onSelect = useCallback((selectedItem) => {
      formik.setFieldValue("subject", selectedItem.label);
      const fileName = selectedItem.key;
      import(`./FormSelect/subjects/topics/${fileName}`).then((module) => {
            setselectedTopic(module.default);
        });
    }, []);


  return (
    <>
        {errorMessage != null ?  (
              <div class="card w-25 mx-auto mb-3 h-25 bg-transparent text-center font-weight-bold fs-5">
                <div class="card-body">
                    {errorMessage}
                </div>
              </div>  
            ) : null}
      <div className="forms w-50 p-3 auto mx-auto my-auto mb-5">
        <h3>Update Requirement</h3>
        <hr />
        <form onSubmit={formik.handleSubmit} className="p-2 m-4">
          <div className="row">
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* Subject */}
                <label htmlFor="subject">Choose a Subject</label>
                <SubjectSelectUpdate
                      className='input'
                      onChange={onSelect}
                      value={formik.values.subject}
                      options={subs}
                    />
                {formik.touched.subject && formik.errors.subject ? (
                  <p className="text-danger">Subject is required</p>
                ) : null}
              </div>
            </div>
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* Topic */}
                <label htmlFor="topic">Choose a topic</label>
                {options !== null ? (
                   <TopicSelectUpdate
                      className='input'
                      onChange={onSelectTopic}
                      value={formik.values.topic}
                      options={options}
                    />
                    )
                 : null}
                 {formik.touched.topic && formik.errors.topic ? (
                    <p className="text-danger">Topic is required</p>
                  ) : null}
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* Start Day */}
                <label htmlFor="startDay">Pick a day you wish to start</label>
                <DaysUpdate
                  className="input"
                  onChange={(value) =>
                    formik.setFieldValue("startDay", value.value)
                  }
                  value={formik.values.startDay}
                  options={days}
                />
                {formik.errors.startDay ? (
                  <div className="error">{formik.errors.startDay}</div>
                ) : null}
              </div>
            </div>
            <div className="col-md-6 mb-4">
              {/* End Day */}
              <div className="form-group my-2">
                <label htmlFor="endDay">Pick a day you wish to end</label>
                <DaysUpdate
                  className="input"
                  onChange={(value) =>
                    formik.setFieldValue("endDay", value.value)
                  }
                  value={formik.values.endDay}
                  options={days}
                />
                {formik.errors.endDay ? (
                  <div className="error">{formik.errors.endDay}</div>
                ) : null}
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-4">
              {/* Start time */}
              <div className="form-group my-2">
                <label htmlFor="startTime">Start Time</label>
                <input
                  type="time"
                  className="form-control"
                  name="startTime"
                  onChange={formik.handleChange}
                  value={formik.values.startTime}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.startTime && formik.errors.startTime ? (
                  <p className="text-danger">StartTime is required</p>
                ) : null}
              </div>
            </div>
            <div className="col-md-6 mb-4">
              {/* End Time */}
              <div className="form-group my-2">
                <label htmlFor="endTime">End Time</label>
                <input
                  type="time"
                  className="form-control"
                  name="endTime"
                  onChange={formik.handleChange}
                  value={formik.values.endTime}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.endTime && formik.errors.endTime ? (
                  <p className="text-danger">End Time is required</p>
                ) : null}
              </div>
            </div>

            <div className="col-md-6 mb-4">
              {/* Date to start */}
              <div className="form-group my-2">
                <div className="p-2 flex-fill bd-highlight">
                  <div className="form-group">
                    <label htmlFor="timeOfStart">Time Of Start</label>
                    <input
                      type="date"
                      className="form-control"
                      name="timeOfStart"
                      onChange={formik.handleChange}
                      value={formik.values.timeOfStart}
                      onBlur={formik.handleBlur}
                    />
                    {formik.touched.timeOfStart && formik.errors.timeOfStart ? (
                      <p className="text-danger">Time Of Start is required</p>
                    ) : null}
                  </div>
                </div>
              </div>
            </div>
            <div className="col-md-6 mb-4">
              {/* Type of class */}
              <div className="form-group my-2">
                <label htmlFor="typeOfClass">Choose type of class</label>
                <TypeOfClassUpdate
                  className="input"
                  onChange={(value) =>
                    formik.setFieldValue("typeOfClass", value.value)
                  }
                  value={formik.values.typeOfClass}
                  options={typeOfClass}
                />
                {formik.errors.typeOfClass ? (
                  <div className="error">{formik.errors.typeOfClass}</div>
                ) : null}
              </div>
            </div>
          </div>
          <button className="btn btn-primary" type="submit">
            Update
          </button>
        </form>
      </div>
    </>
  );
}
