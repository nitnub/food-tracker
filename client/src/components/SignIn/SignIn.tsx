import AppContext from '../../context/AppContext';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Input from '@mui/material/OutlinedInput';
import FormHelperText from '@mui/material/FormHelperText';
import Button from '@mui/material/Button';

import { useContext, useState } from 'react';
import { ReactionEntry, ReactionOptionProps } from '../../types/dbTypes';

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

export default function SignIn() {
  const [userId, setUserId] = useState('');
  const [userReactions, setUserReactions] =
    useState<ReactionOptionProps>(reactionDefault);
  const { appContext, setAppContext } = useContext(AppContext);

  const clickHandler = async () => {
    if (userId === '') {
      return;
    }
    const res = await fetch(`http://localhost:3200/api/v1/reaction/${userId}`);
    const json = await res.json();
    const reactionArr = json.data.reactions;
    // console.log(json.data);

    if (JSON.stringify(json.data).length > 1) {
      const dataCopy = { ...appContext };
      const reactions = getReactionListByFood(
        Number(userId),
        appContext.activeFood.id,
        reactionArr
      );

      dataCopy.user = json.data; // {id: Number(userId)}
      dataCopy.activeFood.reactions = reactions;
      setAppContext({ setAppContext, appContext: dataCopy });
      // setAppContext({...appContext, user:{id: Number(userId)}})
    }

    // Get REaction Test
    // console.log(`User reactions for ${userId}: `, json.data.reactions);
    function getReactionListByFood(
      userId: number,
      foodId: number,
      reactionList: any[]
    ) {
      const outputList: any = [];
      const rawReactions: any = [];

      reactionList.forEach((reaction: any) => {
        if (reaction.food.id === foodId) {
          rawReactions.push(reaction);
        }
      });
      rawReactions.forEach((entry: any) => {
        const { reaction } = entry;
        // console.log('RR', reaction);
        // console.log('RR', entry);
        const formattedReaction: ReactionEntry = {
          // formattedReaction.userId = userId; // Included in API_params
          userId,
          elementId: foodId,
          foodGroupingId: reaction.foodGroupingId,
          reactionType: reaction.typeId,
          severity: reaction.severityId,
          active: reaction.active,
        };
        outputList.push(formattedReaction);
      });

      // console.log('ITEMS:');
      // console.log(outputList);
      return outputList;
    }

    setUserReactions(() => json.data);
  };
  return (
    <>
      <FormControl>
        <InputLabel htmlFor="my-input">User ID</InputLabel>
        <Input
          id="my-input"
          aria-describedby="my-helper-text"
          onChange={(e) => setUserId(e.target.value)}
        />
        <FormHelperText id="my-helper-text">
          This is some helper text
        </FormHelperText>
        <Button onClick={clickHandler}>Text</Button>
      </FormControl>
      <br />
      {userId}
      <br />
      {/* {appContext. .userId && 'Logged in user: ' + JSON.stringify(userReactions.userId)} */}
      <br />
    </>
  );
}
