import * as Yup from 'yup';

export const genderOptions = [
    { value: "Male", label: "Male" },
    { value: "Female", label: "Female" },
    { value: "Other", label: "Other" },
  ];

export const educationQualification =[
    {value:"Graduate", label:"Graduate"},
    {value:"UnderGraduate", label:"Under Graduate"},
    {value:"PostGraduate", label:"Post Graduate"},
]

export const loginValidationSchema = Yup.object().shape({
    email: Yup.string()
        .email('Invalid email address')
        .required('Required'),
    password: Yup.string()
        .required('Required')
});

export const validationSchema = Yup.object().shape({
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
        .required()
        .oneOf([Yup.ref('password'), null], 'Passwords must match'),
    gender: Yup.string()
        .required("Required"),
    educationQualification: Yup.string()
        .required("Required"),
    dateOfBirth: Yup.string()
        .required('Required'),
    mobileNumber: Yup.string()
        .required()
        .min(10, 'Phone number must be at least 10 digits')
        .max(10, 'Phone number must be at most 10 digits'),
    street: Yup.string(),
    city: Yup.string().required('Required'),
    state: Yup.string().required('Required'),   
    pin: Yup.number().required()
        .min(6, 'Pin code must be at least 6 digits')
        // .max(6, 'Pin code must be at most 6 digits'),
});   


