import React, {Suspense} from "react";
import {Navigate, Route, Routes, useParams} from "react-router-dom";
import Environment from "./env";

import Collections from "./containers/Collections";
import DataFiles from "./containers/DataFiles";
import DataFileMetadata from "./containers/DataFileMetadata";
import DefineDataType from "./containers/DefineDataType";
import DefineDataTypes from "./containers/DefineDataTypes";
import Home from "./containers/Home";
import Login from "./containers/Login";
import SetPassword from "./containers/SetPassword";
import Users from "./containers/Users";
import User from "./containers/User";
import ViewData from "./containers/ViewData";

const AppRoutes = ({childProps}) => (
  <Suspense fallback={<div>Loading ...</div>}>
    <Routes>
      <Route path="/"
             exact
             element={<Home {...childProps}/>}
      />

      <Route path="/collection/:name"
             exact
             element={
               <RequireAuthentication
                 child={<WithParams Component={ViewData} childProps={childProps}/>}
                 childProps={childProps}
               />
             }
      />

      <Route path="/collections"
             exact
             element={<RequireAuthentication child={<Collections {...childProps}/>} childProps={childProps}/>}
      />

      <Route path="/datafile/metadata/:DataPointerId"
             exact
             element={
               <RequireAuthentication
                 child={<WithParams Component={DataFileMetadata} childProps={childProps}/>}
                 childProps={childProps}
               />
             }
      />

      <Route path="/datafiles"
             exact
             element={<RequireAuthentication child={<DataFiles {...childProps}/>} childProps={childProps}/>}
      />

      <Route path="/definition/:name"
             exact
             element={
               <RequireAuthentication
                 child={<WithParams Component={DefineDataType} childProps={childProps}/>}
                 childProps={childProps}
               />
             }
      />
      <Route path="/definitions"
             exact
             element={<RequireAuthentication child={<DefineDataTypes {...childProps}/>} childProps={childProps}/>}
      />
      <Route path="/login"
             exact
             element={<Login {...childProps}/>}
      />
      <Route path="/users"
             exact
             element={<RequireAuthentication child={<Users {...childProps}/>} childProps={childProps}/>}
      />
      <Route path="/user/:userId"
             exact
             element={
               <RequireAuthentication
                 child={<WithParams Component={User} childProps={childProps}/>}
                 childProps={childProps}
               />
             }
      />
      <Route path="/user/setpassword/:userId"
             exact
             element={
               <RequireAuthentication
                 child={<WithParams Component={SetPassword} childProps={childProps}/>}
                 childProps={childProps}
               />
             }
      />
    </Routes>
  </Suspense>
);

function RequireAuthentication({child, childProps, redirectTo}) {
  if (redirectTo == null) {
    redirectTo = `${Environment.BASE_URL}`
  }

  return childProps.userHasAuthenticated ? child : <Navigate to={redirectTo}/>;
}

function WithParams({Component, childProps}) {
  return (<Component {...childProps} params={useParams()}/>);
}

export default AppRoutes;