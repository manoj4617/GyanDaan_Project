import React, { Fragment } from 'react';
import { useSelector } from 'react-redux';
import { Navigate, Route } from 'react-router';
import { isInRole } from '../utils/jwt';

const AuthorizedRoute = ({ component: Component, role, ...rest }) => {
  const authStatus = useSelector((state) => state.auth);
  return (
    <Fragment>
      <Route
        {...rest}
        render={(props) => {
          console.log(authStatus.isLoggedIn);
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
          
          if (!isInRole(role, authStatus.currentUser)) {
            return (
              <Navigate
                to={{
                  pathname: '/unauthorized',
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

export default AuthorizedRoute;
