import { createContext } from 'react';
import { FoodDbResponse } from '../types/food.types';

// interface ActiveFood extends FoodDbResponse {
interface ActiveFood {
  id: number;
  name: string;
  fodmapId: number;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
}

// interface DefaultContext {
interface DefaultContext {
  // context: {
    appContext: {
      // activeFood: ActiveFood;
      activeFood: {
        id: number;
        name: string;
        fodmapId: number;
        vegetarian: boolean;
        vegan: boolean;
        glutenFree: boolean;
      };
      user: {
        id: number
      }
    };
  // };
  setAppContext: Function;
}

// export const defaultContext: DefaultContext = {
export const defaultContext: any = {
  // context: {
    appContext: {
      activeFood: {
        id: 0,
        name: '',
        fodmapId: 0,
        vegetarian: false,
        vegan: false,
        glutenFree: false,
      },
      user: {
        id: 0
      }
    },
  // },
  setAppContext: (a: any): any => {},
};

// export default createContext(null)
export default createContext(defaultContext);
