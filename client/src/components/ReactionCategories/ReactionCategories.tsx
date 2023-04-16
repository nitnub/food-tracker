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
  // const [reactions, setReactions] =
  //   useState<ReactionOptionProps>(reactionDefault);

  // useEffect(() => {
  //   const getReactionDetails = async () => {
  //     const resp = await fetch('http://localhost:3200/api/v1/reaction');
  //     const json = await resp.json();

  //     setReactions(() => json.data);
  //   };
  //   getReactionDetails();
  // }, []);

  // const ctx = useContext(AppContext)

  // console.log('context check', ctx)
  // ctx.cow = 'Moooooo!'
  return (
    <>
      {reactions.categories.map((category: Category) => {
        return (
          <>
            <h3>{category.name}</h3>
            {category.reactionTypes?.map((reactionType: ReactionType) => {
              return (
                <ReactionCategory
                  categoryId={category.id}
                  // foodItem={foodItem}
                  // foodState={foodState}
                  reactionType={reactionType}
                  severities={reactions.severities}
                />
              );
            })}
          </>
        );
      })}
    </>
  );
}
