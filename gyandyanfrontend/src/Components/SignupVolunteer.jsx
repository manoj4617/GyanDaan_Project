import React, { useState } from 'react'
import { useFormik } from "formik";
import * as Yup from "yup";
import Select from 'react-select'
import stylingObject from './style';

export default function SignupVolunteer() {

    const genderOptions = [
        { value: "Male", label: "Male" },
        { value: "Female", label: "Female" },
        { value: "Other", label: "Other" },
      ];

    const educationQualification =[
        {value:"Graduate", label:"Graduate"},
        {value:"UnderGraduate", label:"Under Graduate"},
        {value:"PostGraduate", label:"Post Graduate"},
    ]

    const validationSchema = Yup.object().shape({
        firstName: Yup.string()
            .required('Required'),
        lastName: Yup.string()
            .required('Required'),
        email: Yup.string()
            .email('Invalid email address')
            .required('Required'),
        password: Yup.string()
            .required('Required')
            .min(8, 'Password must be at least 8 characters')
            .matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$@%&? \"]).*$", 'Password must contain at least one letter, one number and one special character'),
        confirmPassword: Yup.string()
            .required('Required')
            .oneOf([Yup.ref('password'), null], 'Passwords must match'),
        dateOfBirth: Yup.string()
            .required('Required'),
        phoneNumber: Yup.string()
            .required('Required'),
        street: Yup.string(),
        city: Yup.string().required('Required'),
        state: Yup.string().required('Required'),   
        zip: Yup.string().required('Required'), 
    });   

    const formik = useFormik({
        initialValues: {
            firstName: '',  
            lastName: '',
            email: '',
            password: '',
            dateOfBirth: '',
            phoneNumber: '',
            street: '',
            city: '',
            state: '',
            zip: '',
        },
        validationSchema: validationSchema,
        onSubmit: values => {
            console.log(values)
        }
    });

    const [selectedGenderOption, setSelectedGenderOption] = useState([]);
    const [selectedEducation, setselectedEducation] = useState([]);


    return (
        <>
            <div className='w-50 p-3 auto mx-auto my-auto shadow-lg p-3 mb-5 bg-warning rounded'>
            <h3>Volunteer Login Form</h3>
              <hr />
              <form onSubmit={formik.handleSubmit}  className='p-2 m-4'>
              <div class="row">
              <div class="col-md-6 mb-4">
                <div className="form-group my-2 w-100">
                    {/* FirstName */}
                    <label htmlFor="firstname">First Name</label>
                    <input
                        type="text"
                        className="form-control"
                        name="firstname"
                        onChange={formik.handleChange}
                        value={formik.values.firstname}
                        onBlur={formik.handleBlur}
                    />
                    {formik.touched.firstname && formik.errors.firstname ? (
                        <p className="text-danger">Firstname is required</p>
                    ) : null}
                    </div>
                </div>
                <div class="col-md-6 mb-4">
                    <div className="form-group my-2 w-100">
                    {/* Last Name */}
                    <label htmlFor="lastname">Last Name</label>
                    <input
                        type="text"
                        className="form-control"
                        name="lastname"
                        onChange={formik.handleChange}
                        value={formik.values.lastname}
                        onBlur={formik.handleBlur}
                    />
                    {formik.touched.email && formik.errors.lastname ? (
                        <p className="text-danger">Lastname is required</p>
                    ) : null}
                    </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 mb-4">
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
                    <div class="col-md-6 mb-4">
                        {/* Phone Number */}
                        <div className="form-group my-2">
                            <label htmlFor="phoneNumber">Phone Number</label>
                            <input
                                type="text"
                                className="form-control"
                                name="phoneNumber"
                                onChange={formik.handleChange}
                                value={formik.values.phoneNumber}
                                onBlur={formik.handleBlur}
                            />
                            {formik.touched.phoneNumber && formik.errors.phoneNumber ? (
                                <p className="text-danger">Phone Number is required</p>
                            ) : null}
                        </div>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-4">
                        {/* Password */}
                            <div className="form-group my-2">
                            <label htmlFor="password">Password</label>
                            <input
                                type="password"
                                className="form-control"
                                name="password"
                                onChange={formik.handleChange}
                                value={formik.values.password}
                                onBlur={formik.handleBlur}
                            />
                            {formik.touched.password && formik.errors.password ? (
                                <p className="text-danger">Password is required</p>
                            ) : null}
                            </div>
                            </div>
                            <div class="col-md-6 mb-4">
                        {/* Confirm Password */}
                        <div className="form-group my-2">
                        <label htmlFor="confirmPassword">Confirm Password</label>
                        <input
                            type="password"
                            className="form-control"
                            name="confirmPassword"
                            onChange={formik.handleChange}
                            value={formik.values.confirmPassword}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.confirmPassword && formik.errors.confirmPassword ? (
                            <p className="text-danger">Confirm Password is required</p>
                        ) : null}
                        </div>
                        </div>
                </div>
                <div class="row">
                    <div class="col-md-6 mb-4">
                    {/* Gender */}
                    <div class="form-group my-2 bd-highlight">
                        <div class="p-2 flex-fill bd-highlight">
                        <div className="form-group">
                            <label htmlFor="gender">Select gender</label>
                            <Select
                            id="gender"
                            name="gender"
                            className="my-2"
                            value={selectedGenderOption}
                            onChange={setSelectedGenderOption}
                            options={genderOptions}
                            />
                        </div>
                        </div>
                </div>
                </div>
                <div class="col-md-6 mb-4">
                    {/* Date of Birth */}
                    <div className="form-group my-2">
                        <label htmlFor="dateOfBirth">Date of Birth</label>
                        <input
                            type="date"
                            className="form-control"
                            name="dateOfBirth"
                            onChange={formik.handleChange}
                            value={formik.values.dateOfBirth}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.dateOfBirth && formik.errors.dateOfBirth ? (
                            <p className="text-danger">Date of Birth is required</p>
                        ) : null}
                    </div>
                    </div>
                </div>
                {/* EducationQualification */}
                <div class="form-group my-2 bd-highlight">
                    <div class="p-2 flex-fill bd-highlight">
                    <div className="form-group">
                        <label htmlFor="educationQualification">Select Education Qualification</label>
                        <Select
                        id="educationQualification"
                        name="educationQualification"
                        className="my-2"
                        value={selectedEducation}
                        onChange={setselectedEducation}
                        options={educationQualification}
                        />
                    </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-6 mb-4">
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
                <div class="col-md-6 mb-4">
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
                <div class="row">
                {/* State */}
                <div class="col-md-6 mb-4">
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
                <div class="col-md-6 mb-4">
                
                {/* Zip Code */}
                <div className="form-group my-2">
                    <label htmlFor="zipCode">Zip Code</label>
                    <input
                        type="text"
                        className="form-control"
                        name="zipCode"
                        onChange={formik.handleChange}
                        value={formik.values.zipCode}
                        onBlur={formik.handleBlur}
                    />
                    {formik.touched.zipCode && formik.errors.zipCode ? (
                        <p className="text-danger">Zip Code is required</p>
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
    )
}
