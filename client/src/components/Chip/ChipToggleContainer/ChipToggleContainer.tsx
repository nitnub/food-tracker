
import { useState } from 'react';
import { FoodDbResponse } from '../../../types/food.types';
import ChipToggle from '../ChipToggle/ChipToggle';
import styles from './ChipToggleContainer.module.css';

export default function ChipToggleContainer({ 
  // query, 
  // setQuery, 
  foodArray, 
  // foodState 
}: {
  foodArray: FoodDbResponse[], 
  // foodState: object
}) {

  const [active, setActive] = useState(-1)
  const toggleState = {active, setActive}
  return (
    <div className={styles.container}>
      <div style={{ display: 'flex', flexWrap: 'wrap', padding: '8px' }}>
        {foodArray.map((foodItem: FoodDbResponse, index: number) => (
          <ChipToggle
          // foodState={foodState}
          toggleState={toggleState}
            key={index}
            toggleId={index}
            // query={query}
            // setQuery={setQuery}
            // foodStyle={name}
            foodItem={foodItem}
          />
        ))}
      </div>
    </div>
  );
}
