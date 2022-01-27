import React from 'react';
import Select from 'react-select';

export const  GenderSelectUpdate=({ onChange, options, value }) => {

    const defaultValue = (options, value) => {
        return options ? options.find(option => option.label === value) : "";
    };

    return (
        <div className="my-2">
            <Select
                value={defaultValue(options, value)}
                onChange={value => {
                    onChange(value)

                }} options={options} />
        </div>

    )
}

export const  EducationQualificationUpdate =({ onChange, options, value }) => {
   
    const defaultValue = (options, value) => {
        return options ? options.find(option => option.label === value) : "";
    };

    return (
        <div className="my-2">
            <Select
                value={defaultValue(options, value)}
                onChange={value => {
                    onChange(value)

                }} options={options} />
        </div>

    )
}

export const  DaysUpdate=({ onChange, options, value }) => {
    const defaultValue = (options, value) => {

        return options ? options.find(option => option.label === value) : "";
    };

    return (
        <div className="my-2">
            <Select
                value={defaultValue(options, value)}
                onChange={value => {
                    onChange(value)

                }} options={options} />
        </div>

    )
}

export const  TypeOfClassUpdate=({ onChange, options, value }) => {

    const defaultValue = (options, value) => {
        return options ? options.find(option => option.label === value) : "";
    };

    return (
        <div className="my-2">
            <Select
                value={defaultValue(options, value)}
                onChange={value => {
                    onChange(value)

                }} options={options} />
        </div>

    )
}

export const  TopicSelectUpdate =({ onChange, options, value }) => {

    const defaultValue = (options, value) => {
        return options ? options.find(option => option.label === value) : "";
    };

    return (
        <div className="my-2">
            <Select
                value={defaultValue(options, value)}
                onChange={value => {
                    onChange(value)

                }} options={options} />
        </div>

    )
}

export const  SubjectSelectUpdate =({ onChange, options, value }) => {

    const defaultValue = (options, value) => {
        return   options ? options.find(option => option.label === value) : "";
    };

    return (
        <div className="my-2">
            <Select
                value={defaultValue(options, value)}
                onChange={value => {
                    onChange(value)

                }} options={options} />
        </div>

    )
}