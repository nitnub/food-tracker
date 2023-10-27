import AppContext from '../../context/AppContext';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Input from '@mui/material/OutlinedInput';
import FormHelperText from '@mui/material/FormHelperText';
import Button from '@mui/material/Button';
import { useContext, useState } from 'react';
import { ReactionOptionProps } from '../../types/dbTypes';
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

export default function SignIn() {
  const [userId, setUserId] = useState('');
  const [userReactions, setUserReactions] =
    useState<ReactionOptionProps>(reactionDefault);
  const { appContext, setAppContext } = useContext(AppContext);

  const rAPI = new ReactionAPI(Number(userId));

  const clickHandler = async () => {
    if (userId === '') {
      return;
    }

    const updatedContext = await rAPI.refreshReactionContext(appContext);
    setAppContext({ setAppContext, appContext: updatedContext });

    // setUserReactions(() => updatedContext.user.reactiveFoods);
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
      <br />
    </>
  );
}
