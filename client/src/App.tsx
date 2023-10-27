// import dotenv from 'dotenv';
import { useState } from 'react';

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import './App.css';

import AppContext, { defaultContext } from './context/AppContext';
import SignIn from './components/SignIn';
import TabbedContainer from './components/TabbedContainer';
// dotenv.config({ path: `.env.${process.env.NODE_ENV || 'development'}` });

function App() {
  const [context, setAppContext] = useState(defaultContext);

  return (
    <>
      <AppContext.Provider
        value={{ appContext: context.appContext, setAppContext }}
      >
        <SignIn />
        <TabbedContainer />
      </AppContext.Provider>
    </>
  );
}

export default App;
