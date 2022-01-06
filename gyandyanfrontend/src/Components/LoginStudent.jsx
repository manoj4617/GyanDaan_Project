import React ,{useState}from 'react'
import { useFormik } from 'formik'
import {loginValidationSchema} from './Form'
import {httpClient} from '../http/httpclient'
import {decode} from '../utils/jwt'
import { useDispatch } from 'react-redux';
import { authSlice } from '../redux/auth'
import { useNavigate } from 'react-router'

export default function LoginStudent(props) {

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [errorMessage, seterrorMessage] = useState(null);
    const formik = useFormik({
        initialValues: {
            email: '',
            password: ''
        },
        validationSchema: loginValidationSchema,
        onSubmit: values => {
          httpClient.post("user/student-login", values).then((res) => {
            sessionStorage.setItem("token", res.data.jwt);
            const userInfo = decode(res.data.jwt);
            console.log(userInfo.Roles);
            dispatch(authSlice.actions.login({ userInfo, token: res.data.jwt }));
            navigate('/student-dash')
          })
          .catch(err => {
            console.log(err.response.status);
            if(err.response.status === 500){
              seterrorMessage("Invalid Credentials");
            }
          });
            
        }
    });


    return (
        <>  
          {errorMessage != null ?  (
              <div class="card w-25 mx-auto mb-3 h-25 bg-transparent text-center font-weight-bold fs-5">
              <div class="card-body">
                  {errorMessage}
              </div>
            </div>  
            ) : null}
            <div className='forms w-50 p-3 auto mx-auto my-auto'>
            <h3>Student Login Form</h3>
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
