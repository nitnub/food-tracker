import { useContext, useEffect, useState } from 'react';
import { ReactionOptionProps } from '../../types/dbTypes';
import ReactionCategories from '../ReactionCategories';
import AppContext from '../../context/AppContext';
import ReactionAPI from '../../utils/ReactionAPI';
import styles from './ReactionDashboard.module.css';

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
    const rAPI = new ReactionAPI(appContext.user.id);
    const getReactionDetails = async () => {
      const res = await rAPI.getReactionTypeDetails();
      setReactions(() => res);
    };
    getReactionDetails();
  }, [appContext]);

  if (appContext.activeFood?.id === 0) {
    return <>Please select a food item!</>;
  }

  return (
    <div className={styles.container}>
      {appContext?.activeFood?.name}
      <ReactionCategories reactions={reactions} userReactions={userReactions} />
    </div>
  );
}
