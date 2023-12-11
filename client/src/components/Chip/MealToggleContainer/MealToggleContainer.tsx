import { useState } from 'react';
import { FoodDbResponse } from '../../../types/food.types';
import MealChip from '../MealChip/MealChip';
import styles from './MealToggleContainer.module.css';

export default function MealToggleContainer({
  foodArray,
  clickHandler
}: {
  foodArray: FoodDbResponse[];
  clickHandler: Function;
}) {
  const [active, setActive] = useState(-1);
  const toggleState = { active, setActive };

  return (
    <div className={styles.container}>
      <div style={{ display: 'flex', flexWrap: 'wrap', padding: '8px' }}>
        {foodArray.map((foodItem: FoodDbResponse, index: number) => (
          <MealChip
            toggleState={toggleState}
            key={index}
            toggleId={index}
            foodItem={foodItem}
            clickHandler={clickHandler}
          />
        ))}
      </div>
    </div>
  );
}
