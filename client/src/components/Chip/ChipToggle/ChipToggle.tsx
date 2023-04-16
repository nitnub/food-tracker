import { useContext, useState } from 'react';
import Chip from '@mui/material/Chip';
// import { FoodCategory } from '../../../../models/dishModel';
import styles from './ChipToggle.module.css';
import { FoodDbResponse } from '../../../types/food.types';
import AppContext from '../../../context/AppContext';

export default function ChipToggle({
  // query,
  // setQuery,
  // foodState,
  foodItem,
}: {
  foodItem: FoodDbResponse;
  // foodState: object
}) {
  const [active, setActive] = useState(false);
  const { appContext, setAppContext } = useContext(AppContext);

  const handler = () => {
    // if (ctx === null) return;
    // ctx.setContext({...ctx.context, activeFood: foodItem})
    // ctx.activeFood = foodItem
    // // ctx.bird = 'chirp  '
    setAppContext({ ...appContext, activeFood: foodItem });
    console.log('Clicked on:', foodItem);
    // ctx.cow ='Chip toggled!'
    // console.log(ctx)
    // console.log(foodItem)
    // foodState.setFoodState(foodItem)
    // setActive(() => !active);
    // if (!active) {
    //   const qCopy = { ...query };
    //   const sCopy = qCopy.styles;
    //   sCopy.push(foodStyle);
    //   setQuery(() => {
    //     return { ...qCopy, styles: sCopy };
    //   });
    // } else {
    //   const qCopy = { ...query };
    //   let sCopy = qCopy.styles;
    //   sCopy = sCopy.filter((el: FoodCategory) => el !== foodStyle);
    //   setQuery(() => {
    //     return { ...qCopy, styles: sCopy };
    //   });
    // }
  };

  // <Chip label={props.foodStyle} color="success" size="small" />
  return (
    // <div className={styles.chipToggle}>
    <Chip
      // key={foodStyle}
      style={{ margin: '4px' }}
      className={styles.chipToggle}
      onClick={handler}
      variant={active ? 'filled' : 'outlined'}
      label={foodItem.name}
      // color="success"
      color="success"
      size="small"
    />
    // </div>
  );
}
