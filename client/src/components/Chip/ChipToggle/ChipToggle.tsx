import { useContext, useEffect, useState } from 'react';
import Chip from '@mui/material/Chip';
import styles from './ChipToggle.module.css';
import { FoodDbResponse } from '../../../types/food.types';
import AppContext from '../../../context/AppContext';
import ReactionAPI from '../../../utils/ReactionAPI';

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

export default function ChipToggle({
  foodItem,
  toggleState,
  toggleId,
  onClick
}: {
  foodItem: FoodDbResponse;
  toggleState: ToggleState;
  toggleId: number;
  onClick?: Function;
}) {
  const [active, setActive] = useState(false);
  const { appContext, setAppContext } = useContext(AppContext);
  const [chipColor, setChipColor] = useState<ChipColor>('success');

  const rAPI = new ReactionAPI(appContext.user.id);

  const handler = async () => {
    toggleState.setActive(toggleId);
    appContext.activeFood = foodItem;
    const updatedContext = await rAPI.refreshReactionContext(appContext);

    setAppContext({ setAppContext, appContext: updatedContext });
  };

  function getChipColor() {
    return 'success';
  }

  useEffect(() => {
    if (appContext.user.reactiveFoods.includes(foodItem.id)) {
      setChipColor('error');
    } else {
      setChipColor('success');
    }
  }, [appContext, foodItem.id]);

  return (
    <Chip
      className={styles.chipToggle}
      onClick={handler}
      variant={toggleState.active === toggleId ? 'filled' : 'outlined'}
      label={foodItem.name}
      color={chipColor}
      size="small"
    />
  );
}
