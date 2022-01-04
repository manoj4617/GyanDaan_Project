import React from 'react'
import { useFormik } from 'formik'
import * as Yup from 'yup'


export default function LoginVolunteer() {

    const validationSchema = Yup.object().shape({
        email: Yup.string()
            .email('Invalid email address')
            .required('Required'),
        password: Yup.string()
            .required('Required')
            .min(8, 'Password must be at least 8 characters')
            .matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$@%&? \"]).*$", 'Password must contain at least one letter, one number and one special character')
    });


    const formik = useFormik({
        initialValues: {
            email: '',
            password: ''
        },
        validationSchema: validationSchema,
        onSubmit: values => {
            console.log(values)
            
        }
    });


    return (
        <> 
            <div className='w-50 p-3 auto mx-auto my-auto border border-success rounded-1'>
            <h3>Volunteer Login Form</h3>
              <hr />
              <form onSubmit={formik.handleSubmit} className='p-2 m-4'>
                <div className="form-group my-2 w-100">
                  {/* Username */}
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

                <button className="btn btn-primary" type="submit">
                  Submit
                </button>
              </form>
            </div>
        </>
    )
}
