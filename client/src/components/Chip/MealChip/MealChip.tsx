import { useContext, useEffect, useState } from 'react';
import Chip from '@mui/material/Chip';
import styles from './MealChip.module.css';
import AppContext from '../../../context/AppContext';
import { MealItem } from '../../MealModal/MealModal';

interface ToggleState {
  active: number;
  setActive: Function;
}

type ChipColor =
  | 'success'
  | 'default'
  | 'primary'
  | 'secondary'
  | 'error'
  | 'info'
  | 'warning'
  | undefined;

export default function MealChip({
  foodItem,
  clickHandler,
}: {
  foodItem: MealItem;
  toggleState: ToggleState;
  toggleId: number;
  clickHandler: Function;
}) {
  const { appContext, setAppContext } = useContext(AppContext);
  const [chipColor, setChipColor] = useState<ChipColor>('success');

  useEffect(() => {
    if (appContext.user.reactiveFoods.includes(foodItem.id)) {
      setChipColor('error');
    } else {
      setChipColor('success');
    }
  }, [appContext, foodItem.id]);

  return (
    <Chip
      className={styles.mealChip}
      onClick={() => clickHandler(foodItem.id)}
      variant={'outlined'}
      // variant={toggleState.active === toggleId ? 'filled' : 'outlined'}
      label={foodItem.name}
      color={chipColor}
      size="small"
    />
  );
}
