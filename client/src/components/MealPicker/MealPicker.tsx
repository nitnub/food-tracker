import { useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';

import { MealItem } from '../MealModal/MealModal';
import MealToggleContainer from '../Chip/MealToggleContainer';

export default function MealPicker({
  foodOptions,
  clickHandler,
}: {
  foodOptions: MealItem[];
  clickHandler: Function;
}) {

  const foodArr = foodOptions.filter((el: MealItem) => !el.partOfMeal);
  const { appContext } = useContext(AppContext);

  if (appContext.user.id === 0) {
    return <>Please select a valid user!</>;
  }
  return (
    <MealToggleContainer clickHandler={clickHandler} foodArray={foodArr} />
  );
}
