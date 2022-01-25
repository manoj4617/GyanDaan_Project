import { useFormik } from "formik";
import { genderOptions,educationQualification,validationSchema } from './Form';
import {GenderSelect,EducationQualification} from './FormSelect/SelectOptions'
import {httpClient} from '../http/httpclient'
import { useNavigate } from 'react-router';

export default function SignupVolunteer(props) {

    const history = useNavigate();
    const formik = useFormik({
        initialValues: {
            firstName: '',  
            lastName: '',
            email: '',
            gender:'',
            password: '',
            confirmPassword: '',
            dateOfBirth: '',
            educationQualification:'',
            mobileNumber: '',
            street: '',
            city: '',
            state: '',
            pin: '',
        },
        validationSchema: validationSchema,
        onSubmit: values => {
            delete values.confirmPassword;
            console.log(values)
            httpClient.post("user/register-student", values).then((res) => {
                console.log(res)
                let role = "student";
                let message = res.data;
                history(`/login/${role}/${message}`);
              })
              .catch((error)=>{
                  console.log(error)
              });
        }
    });

    return (
        <>
            <div className='forms w-50 p-3 auto mx-auto my-auto mb-5'>
            <h3>Student Signup</h3>
              <hr />
              <form onSubmit={formik.handleSubmit} className='p-2 m-4'>
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
                    <div className="row">
                        <div className="col-md-6 mb-4">
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
                            <div className="col-md-6 mb-4">
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
                <div className="row">
                    <div className="col-md-6 mb-4">
                    {/* Gender */}
                    <div className="form-group my-2">
                        <div className="p-2 flex-fill bd-highlight">
                        <div className="form-group">
                            <label htmlFor="gender">Select gender</label>
                            <GenderSelect
                                className='input'
                                onChange={value=>formik.setFieldValue('gender',value.value)}
                                value={formik.values.gender}
                                options={genderOptions}
                                />
                            {formik.errors.gender ? <div className='error'>{formik.errors.gender}</div> : null}
                        </div>
                        </div>
                </div>
                </div>
                <div className="col-md-6 mb-4">
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
                <div className="form-group my-2">
                    <div className="p-2 flex-fill bd-highlight">
                    <div className="form-group">
                        <label htmlFor="educationQualification">Select Education Qualification</label>
                        <EducationQualification
                                className='input'
                                onChange={value=>formik.setFieldValue('educationQualification',value.value)}
                                value={formik.values.educationQualification}
                                options={educationQualification}
                                />
                            {formik.errors.job ? <div className='error'>{formik.errors.educationQualification}</div> : null}
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
                <button className="btn btn-primary" type="submit">
                  Submit
                </button>
              </form>
            </div>
        </>
    )
}
