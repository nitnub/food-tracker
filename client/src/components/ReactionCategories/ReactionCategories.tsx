import { useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import {
  Category,
  ReactionOptionProps,
  ReactionType,
} from '../../types/dbTypes';
import ReactionCategory from '../ReactionCategory';

const reactionDefault: ReactionOptionProps = {
  severities: [
    {
      id: 0,
      name: '',
    },
  ],
  categories: [
    {
      id: 0,
      name: '',
      reactionTypes: [
        {
          id: 0,
          name: '',
        },
      ],
    },
  ],
};

export default function ReactionCategories(
 {
  // foodItem,
  // foodState,
  reactions,
  userReactions}: {
    // foodItem: any, 
    // foodState: object, 
    reactions: ReactionOptionProps, userReactions: any}
) {

  // const {appContext, setAppContext} = useContext(AppContext);
  // // console.log('aaaaaaaaaaaafffff', appContext.activeFood.reactions)
  // const existingReactions = appContext.activeFood.reactions || []
  
  // const [activeFood, setActiveFood] = useState(appContext.activeFood)
  // const existingReactions = activeFood.reactions || []
//   useEffect(()=> {
// console.log(activeFood)
//   },[activeFood])

  return (
    <>
      {reactions.categories.map((category: Category, index: number) => {
        return (
          <div key={index}>
            <h3>{category.name}</h3>
            {category.reactionTypes?.map((reactionType: ReactionType, index: number) => {

              // let defaultValue = null;
              // for(let reaction of existingReactions) {
              //   // console.log(reaction, reactionType)
              //   if (reaction.reactionType === reactionType.id) {
              //     defaultValue = reactionType.id
              //   }
              // }
              // console.log('render now')
              return (
                <ReactionCategory
                // defaultValue={defaultValue}
                key={index}
                  categoryId={category.id}
                  // foodItem={foodItem}
                  // foodState={foodState}
                  reactionType={reactionType}
                  severities={reactions.severities}
                />
              );
            })}
          </div>
        );
      })}
    </>
  );
}
