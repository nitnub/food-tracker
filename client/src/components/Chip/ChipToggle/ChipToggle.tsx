import { useContext, useEffect, useState } from 'react';
import Chip from '@mui/material/Chip';
// import { FoodCategory } from '../../../../models/dishModel';
import styles from './ChipToggle.module.css';
import { FoodDbResponse } from '../../../types/food.types';
import AppContext from '../../../context/AppContext';
import { ReactionEntry } from '../../../types/dbTypes';
import { isCompositeComponent } from 'react-dom/test-utils';
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
}: {
  foodItem: FoodDbResponse;
  toggleState: ToggleState;
  toggleId: number;
}) {
  const [active, setActive] = useState(false);
  const { appContext, setAppContext } = useContext(AppContext);
  const [chipColor, setChipColor] = useState<ChipColor>('success');

  // const rAPI = new ReactionAPI(appContext.user.userId);
  // console.log('appContext:', appContext.user);
  const rAPI = new ReactionAPI(appContext.user.id);
  console.log('appContext.user.id');
  console.log(appContext.user);
  // console.log('appContext');
  // console.log(appContext);

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
    // const effect = () => {
    if (appContext.user.reactiveFoods.includes(foodItem.id)) {
      setChipColor('error');
    } else {
      setChipColor('success');
    }
    // };
    // effect();
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
