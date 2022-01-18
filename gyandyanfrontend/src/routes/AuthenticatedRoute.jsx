import React, { Fragment } from 'react';
import { useSelector } from 'react-redux';
import { Navigate, Route } from 'react-router';

const AuthenticatedRoute = ({ component: Component, role, ...rest }) => {
  const authStatus = useSelector((state) => state.auth);
  return (
    <Fragment>
      <Route
        {...rest}
        render={(props) => {
          if (!authStatus.isLoggedIn) {
            return (
              <Navigate
                to={{
                  pathname: '/login',
                  state: { returnUrl: props.location.pathname },
                }}
              />
            );
          }

          return <Component />;
        }}
      ></Route>
    </Fragment>
  );
};

export default AuthenticatedRoute;
