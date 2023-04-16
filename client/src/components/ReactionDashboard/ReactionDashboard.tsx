import { useContext, useEffect, useState } from 'react';
import { ReactionOptionProps } from '../../types/dbTypes';
import ReactionCategories from '../ReactionCategories';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Input from '@mui/material/OutlinedInput';
import FormHelperText from '@mui/material/FormHelperText';
import Button from '@mui/material/Button';
import AppContext from '../../context/AppContext';
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

const defaultReactionState = {};

export default function ReactionDashboard(
  // {  foodState,}: {  foodstate: object;}
  ) 
  {
  const [reactionState, setReactionState] = useState();
  const [userId, setUserId] = useState('');
  const [reactions, setReactions] =
    useState<ReactionOptionProps>(reactionDefault);
  const [userReactions, setUserReactions] =
    useState<ReactionOptionProps>(reactionDefault);

    const {appContext, setAppContext} = useContext(AppContext)
  useEffect(() => {
    const getReactionDetails = async () => {
      const resp = await fetch('http://localhost:3200/api/v1/reaction');
      const json = await resp.json();

      setReactions(() => json.data);
      console.log('reaction categories:', json.data);
    };
    getReactionDetails();
  }, []);

  // const {context, setContext} = useContext(AppContext)
  const clickHandler = async () => {
    if (userId === '') {
      return;
    }
    const res = await fetch(`http://localhost:3200/api/v1/reaction/${userId}`);
    const json = await res.json();
    console.log(json.data);

    if(JSON.stringify(json.data).length > 1) {
      setAppContext({...appContext, user:{id: Number(userId)}})
    }
    setUserReactions(() => json.data);
  };

  if (!appContext.activeFood) {
    return <></>;
  }
  return (
    <>
    {appContext?.activeFood?.name}
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
      {userId}
      {JSON.stringify(userReactions)}
      <ReactionCategories
        // foodState={foodState}
        reactions={reactions}
        userReactions={userReactions}
      />
    </>
  );
}
