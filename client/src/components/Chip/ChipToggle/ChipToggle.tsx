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
  // query,
  // setQuery,
  // foodState,
  foodItem,
  toggleState,
  toggleId,
}: {
  foodItem: FoodDbResponse;
  toggleState: ToggleState;
  toggleId: number;
  // foodState: object
}) {
  const [active, setActive] = useState(false);
  const { appContext, setAppContext } = useContext(AppContext);
  const [chipColor, setChipColor] = useState<ChipColor>('success');

  const rAPI = new ReactionAPI(appContext.user.userId)


  const handler = async () => {
    toggleState.setActive(toggleId);

    const contextCopy = { ...appContext };
    // console.log('contextCopy:', contextCopy)

    // console.log('Inputs:', {
    //   userid: Number(appContext.user.userId),
    //   foodId: foodItem.id,
    //   reactionList: appContext.user.reactions
    // })



    const updatedContext = await rAPI.refreshReactionContext(appContext);
    updatedContext.activeFood = foodItem;

    setAppContext({ setAppContext, appContext: updatedContext });




    // const reactions = rAPI.getReactionListByFood(
    //   Number(appContext.user.userId),
    //   foodItem.id,
    //   appContext.user.reactions
    // );
    // // foodItem.reactions = reactions;
    // contextCopy.activeFood = foodItem;
    // contextCopy.activeFood.reactions = reactions;
    // //       console.log('mState')
    // // console.log(contextCopy.activeFood)
    // // console.log(reactions)

    // setAppContext({ appContext: contextCopy, setAppContext });
  };

  // console.log(foodItem.id)

  function getChipColor() {
    return 'success';
    // return 'black';
  }

  useEffect(() => {
    if (appContext.user.reactiveFoods.includes(foodItem.id)) {
      setChipColor('error')
    } else{
      
      setChipColor('success');
    }
  }, [appContext.user.reactiveFoods]);

  const cow = 'success';

  // function getReactionListByFood(
  //   userId: number,
  //   foodId: number,
  //   reactionList: any[]
  // ) {
  //   const outputList: any = [];
  //   const rawReactions: any = [];

  //   reactionList.forEach((reaction: any) => {
  //     if (reaction.food.id === foodId) {
  //       rawReactions.push(reaction);
  //     }
  //   });
  //   rawReactions.forEach((entry: any) => {
  //     const { reaction } = entry;
  //     // console.log('RR', reaction);
  //     // console.log('RR', entry);
  //     const formattedReaction: ReactionEntry = {
  //       // formattedReaction.userId = userId; // Included in API_params
  //       userId,
  //       elementId: foodId,
  //       foodGroupingId: reaction.foodGroupingId,
  //       reactionType: reaction.typeId,
  //       severity: reaction.severityId,
  //       active: reaction.active,
  //     };
  //     outputList.push(formattedReaction);
  //   });

  //   // console.log('ITEMS:');
  //   // console.log();
  //   // return {userId, foodId, reactionList};
  //   return outputList;
  // }

  // <Chip label={props.foodStyle} color="success" size="small" />
  return (
    // <div className={styles.chipToggle}>
    <Chip
      // key={foodStyle}
      style={{ margin: '4px' }}
      className={styles.chipToggle}
      onClick={handler}
      variant={toggleState.active === toggleId ? 'filled' : 'outlined'}
      label={foodItem.name}
      // color="success"
      // color="success"
      // color={cow}
      color={chipColor}
      size="small"
    />
    // </div>
  );
}
