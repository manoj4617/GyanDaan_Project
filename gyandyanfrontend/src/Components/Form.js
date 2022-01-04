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
        .min(8, 'Password must be at least 8 characters')
        .matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$@%&? \"]).*$", 'Password must contain at least one letter, one number and one special character')
});

export const validationSchema = Yup.object().shape({
    firstname: Yup.string()
        .required('Required'),
    lastname: Yup.string()
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


