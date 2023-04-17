// import dotenv from 'dotenv';
import { createContext, useState } from 'react';
import logo from './logo.svg';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import './App.css';
import Selector from './components/Selector';

import ReactionCategories from './components/ReactionCategories';
import ReactionDashboard from './components/ReactionDashboard';
import FoodPicker from './components/FoodPicker';
import { FoodDbResponse } from './types/food.types';
// import AppContext, {defaultContext} from './context/AppContext';
import AppContext, { defaultContext } from './context/AppContext';
import SignIn from './components/SignIn';

// dotenv.config({ path: `.env.${process.env.NODE_ENV || 'development'}` });

function App() {
  // const [activeFood, setActiveFood] = useState<FoodDbResponse>();
  // const foodState = { activeFood, setActiveFood };

//   type ActiveFood = FoodDbResponse | null
// interface DefaultContext {
//   cow: string;
//   activeFood: ActiveFood;
// }
// const defaultContext: DefaultContext | null = {
//   cow: 'Moo!',
//   activeFood: null
// };

  const [context, setAppContext] = useState(defaultContext)

  //@ts-nocheck

  
  return (
    <>
      <AppContext.Provider value={{appContext: context.appContext, setAppContext}}>
        {/* <Selector /> */}
        <SignIn />
        <FoodPicker />
        <>
        {/* {`This is a test string:` + context?.activeFood?.name} */}
        </>
        <ReactionDashboard  />
        {/* <FoodPicker foodState={foodState} />
        <ReactionDashboard foodState={foodState} /> */}
      </AppContext.Provider>
    </>
  );
}

export default App;
