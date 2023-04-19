import { useContext, useEffect, useState } from 'react';
import { ReactionOptionProps } from '../../types/dbTypes';
import ReactionCategories from '../ReactionCategories';
import AppContext from '../../context/AppContext';
import ReactionAPI from '../../utils/ReactionAPI';
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

export default function ReactionDashboard() {
  const [reactions, setReactions] =
    useState<ReactionOptionProps>(reactionDefault);

  const [userReactions, setUserReactions] =
    useState<ReactionOptionProps>(reactionDefault);

  const { appContext, setAppContext } = useContext(AppContext);

  useEffect(() => {
    const rAPI = new ReactionAPI(appContext.user.userId);
    const getReactionDetails = async () => {
      const res = await rAPI.getReactionTypeDetails();
      // const resp = await fetch('http://localhost:3200/api/v1/reaction');
      // const json = await resp.json();

      setReactions(() => res);
    };
    getReactionDetails();
  }, [appContext]);

  if (appContext.activeFood?.id === 0) {
    return <>Please select a food item!</>;
  }

  return (
    <>
      {appContext?.activeFood?.name}

      <ReactionCategories reactions={reactions} userReactions={userReactions} />
    </>
  );
}
