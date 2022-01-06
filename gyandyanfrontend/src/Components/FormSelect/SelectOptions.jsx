import React from 'react';
import Select from 'react-select';

export const  GenderSelect=({ onChange, options, value }) => {

    const defaultValue = (options, value) => {
        return options ? options.find(option => option.value === value) : "";
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

export const  EducationQualification=({ onChange, options, value }) => {

    const defaultValue = (options, value) => {
        return options ? options.find(option => option.value === value) : "";
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