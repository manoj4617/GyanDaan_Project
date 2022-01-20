import { useFormik } from "formik";
import { useEffect, useState } from "react";
import {
  educationQualification,
  validationSchema,
} from "./Form";
import {
  EducationQualification,
} from "./FormSelect/SelectOptions";
import { httpClient } from "../http/httpclient";
import { useSelector } from "react-redux";
import { decode } from "../utils/jwt";

export default function UpdateProfile(props) {
  const [userData, setuserData] = useState([]);
  const [message, setmessage] = useState();

  const authStatus = useSelector((state) => state.auth);
  var user = decode(authStatus.token);
  var role = user.Roles;


  useEffect(() => {
    if (role === "Student") {
      httpClient
        .get(`user/get-student-profile/${user.Id}`)
        .then((res) => {
          setuserData(res.data);
        })
        .catch((error) => {
          console.log(error);
        });
    }
    if (role === "Volunteer") {
      httpClient
        .get(`user/get-volunteer-profile/${user.Id}`)
        .then((res) => {
          setuserData(res.data);
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }, []);

  const formik = useFormik({
      enableReinitialize:true,
    initialValues: {
      firstName: userData.firstName,
      lastName: userData.lastName,
      email: userData.email,
      educationQualification: userData.educationQualification,
      mobileNumber: userData.mobileNumber,
      street: userData.street,
      city: userData.city,
      state: userData.state,
      pin: userData.pin,
    },
    // validationSchema: validationSchema,
    onSubmit: (values) => {
        console.log(values)
      if (role === "Student") {
        httpClient
          .put(`user/update-student-profile/${user.Id}`, values)
          .then((res) => {
            setmessage(res.data)
            console.log(res);
          })
          .catch((error) => {
            console.log(error);
          });
      }
      if (role === "Volunteer") {
        httpClient
          .put(`user/update-volunteer-profile/${user.Id}`, values)
          .then((res) => {
            setmessage(res.data)
            console.log(res);
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
  });

  return (
    <>
     {message != null ?  (
              
              <> 
              {/* // <!-- Modal HTML --> */}
                <div id="myModal" className="modal fade">
                  <div className="modal-dialog modal-confirm">
                    <div className="modal-content">
                      <div className="modal-header">
                        <div className="icon-box">
                          <i className="material-icons">&#xE876;</i>
                        </div>				
                        <h4 className="modal-title" style={{"paddingLeft":"88px"}}>Done!</h4>	
                      </div>
                      <div className="modal-body">
                        <p className="text-center">Your profile has been updated successfully.</p>
                      </div>
                      <div className="modal-footer">
                        <button className="btn btn-success btn-block" data-dismiss="modal">OK</button>
                      </div>
                    </div>
                  </div>
                </div>   
              </>  
            
            ) : null}    

      <div className="forms w-50 p-3 auto mx-auto my-auto mb-5">
        <h3>Update Profile</h3>
        <hr />
        <form onSubmit={formik.handleSubmit} className="p-2 m-4">
          <div className="row">
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* FirstName */}
                <label htmlFor="firstName">First Name</label>
                <input
                  type="text"
                  className="form-control"
                  name="firstName"
                  onChange={formik.handleChange}
                  value={formik.values.firstName}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.firstName && formik.errors.firstName ? (
                  <p className="text-danger">Firstname is required</p>
                ) : null}
              </div>
            </div>
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* Last Name */}
                <label htmlFor="lastName">Last Name</label>
                <input
                  type="text"
                  className="form-control"
                  name="lastName"
                  onChange={formik.handleChange}
                  value={formik.values.lastName}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.lastName && formik.errors.lastName ? (
                  <p className="text-danger">Lastname is required</p>
                ) : null}
              </div>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-4">
              <div className="form-group my-2 w-100">
                {/* Email */}
                <label htmlFor="email">Email</label>
                <input
                  type="text"
                  className="form-control"
                  name="email"
                  onChange={formik.handleChange}
                  value={formik.values.email}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.email && formik.errors.email ? (
                  <p className="text-danger">Email is required</p>
                ) : null}
              </div>
            </div>
            <div className="col-md-6 mb-4">
              {/* Phone Number */}
              <div className="form-group my-2">
                <label htmlFor="mobileNumber">Phone Number</label>
                <input
                  type="text"
                  className="form-control"
                  name="mobileNumber"
                  onChange={formik.handleChange}
                  value={formik.values.mobileNumber}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.mobileNumber && formik.errors.mobileNumber ? (
                  <p className="text-danger">Phone Number is required</p>
                ) : null}
              </div>
            </div>
          </div>
    
          {/* EducationQualification */}
          <div className="form-group my-2">
            <div className="p-2 flex-fill bd-highlight">
              <div className="form-group">
                <label htmlFor="educationQualification">
                  Select Education Qualification
                </label>
                <EducationQualification
                  className="input"
                  onChange={(value) =>
                    formik.setFieldValue("educationQualification", value.value)
                  }
                  value={formik.values.educationQualification}
                  options={educationQualification}
                />
                {formik.errors.job ? (
                  <div className="error">
                    {formik.errors.educationQualification}
                  </div>
                ) : null}
              </div>
            </div>
          </div>

          <div className="row">
            <div className="col-md-6 mb-4">
              {/* Street */}
              <div className="form-group my-2">
                <label htmlFor="street">Street</label>
                <input
                  type="text"
                  className="form-control"
                  name="street"
                  onChange={formik.handleChange}
                  value={formik.values.street}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.street && formik.errors.street ? (
                  <p className="text-danger">Street is required</p>
                ) : null}
              </div>
            </div>
            {/* City */}
            <div className="col-md-6 mb-4">
              <div className="form-group my-2">
                <label htmlFor="city">City</label>
                <input
                  type="text"
                  className="form-control"
                  name="city"
                  onChange={formik.handleChange}
                  value={formik.values.city}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.city && formik.errors.city ? (
                  <p className="text-danger">City is required</p>
                ) : null}
              </div>
            </div>
          </div>
          <div className="row">
            {/* State */}
            <div className="col-md-6 mb-4">
              <div className="form-group my-2">
                <label htmlFor="state">State</label>
                <input
                  type="text"
                  className="form-control"
                  name="state"
                  onChange={formik.handleChange}
                  value={formik.values.state}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.state && formik.errors.state ? (
                  <p className="text-danger">State is required</p>
                ) : null}
              </div>
            </div>
            <div className="col-md-6 mb-4">
              {/* Zip Code */}
              <div className="form-group my-2">
                <label htmlFor="pin">Zip Code</label>
                <input
                  type="number"
                  className="form-control"
                  name="pin"
                  onChange={formik.handleChange}
                  value={formik.values.pin}
                  onBlur={formik.handleBlur}
                />
                {formik.touched.pin && formik.errors.pin ? (
                  <p className="text-danger">Zip Code is required</p>
                ) : null}
              </div>
            </div>
          </div>
          <button  href="#myModal" className="btn btn-warning" data-toggle="modal" type="submit">
            Update
          </button>
        </form>
      </div>
    </>
  );
}
