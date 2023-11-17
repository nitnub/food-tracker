// import dotenv from 'dotenv';
import { useState } from 'react';
import { Outlet, Link } from "react-router-dom";
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import './App.css';
import AppContext, { defaultContext } from './context/AppContext';
import SignIn from './components/SignIn';
import TabbedContainer from './components/TabbedContainer';
import NavBar from './components/NavBar';
// dotenv.config({ path: `.env.${process.env.NODE_ENV || 'development'}` });

function App() {
  const [context, setAppContext] = useState(defaultContext);
  const [route, setRoute] = useState({ signIn: <SignIn /> });
  return (
    <>
      <AppContext.Provider
        value={{ appContext: context.appContext, setAppContext }}
      >
        <NavBar />
        {/* <SignIn /> */}
        {/* <TabbedContainer /> */}
        {/* {route.signIn} */}
        <Outlet />
      </AppContext.Provider>
    </>
  );
}

export default App;
