import React,{useState,useMemo,useCallback} from "react";
import { useFormik } from "formik";
import { Days, TypeOfClass } from "./FormSelect/SelectOptions";
import { days, typeOfClass } from "./Form";
import { requirementValidations } from "./Form";
import { subjects } from "./FormSelect/subjects/subjects";
import {TopicSelect,SubjectSelect} from "./FormSelect/SelectOptions";
import { httpClient } from "../http/httpclient";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";

export default function RequirementAdding() {

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
      profileId: userInfo.Id,
      subject: "",
      topic: "",
      startDay: "",
      endDay: "",
      startTime: "",
      endTime: "",
      timeOfStart: "",
      typeOfClass: "",
    },
    // validationSchema: requirementValidations,
    onSubmit: (values) => {
      console.log(values);
      if(userInfo.Roles === "Student"){
        httpClient.post("requirement/new-student-requirement", values).then((res) => {
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
        httpClient.post("requirement/new-volunteer-requirement", values).then((res) => {
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
      console.log("sub key "+selectedItem.key);
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
        <h3>Submit New Requirement</h3>
        <hr />
        <form onSubmit={formik.handleSubmit} className="p-2 m-4">
          <div className="row">
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* Subject */}
                <label htmlFor="subject">Choose a Subject</label>
                <SubjectSelect
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
                   <TopicSelect
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
                <Days
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
                <Days
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
                <TypeOfClass
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
            Submit
          </button>
        </form>
      </div>
    </>
  );
}
