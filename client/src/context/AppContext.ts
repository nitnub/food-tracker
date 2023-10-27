import { Dispatch, SetStateAction, createContext } from 'react';
import { ReactionEntry } from '../types/dbTypes';

interface ActiveFood {
  id: number;
  name: string;
  fodmapId: number;
  vegetarian: boolean;
  vegan: boolean;
  glutenFree: boolean;
  reactions: ReactionEntry[];
}

interface DefaultContext {
  appContext: AppContext;
  setAppContext: Dispatch<SetStateAction<DefaultContext>>;
}

interface UserContext {
  id: number;
  reactiveFoods: number[];
}
export interface AppContext {
  activeFood: ActiveFood;
  user: UserContext;
}

export const defaultContext: DefaultContext = {
  appContext: {
    activeFood: {
      id: 0,
      name: '',
      fodmapId: 0,
      vegetarian: false,
      vegan: false,
      glutenFree: false,
      reactions: [],
    },
    user: {
      id: 0,
      reactiveFoods: [],
    },
  },
  setAppContext: (a: any): any => {},
};

export default createContext(defaultContext);
