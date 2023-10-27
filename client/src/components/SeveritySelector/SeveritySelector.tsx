import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import FormLabel from '@mui/material/FormLabel';
import Radio from '@mui/material/Radio';
import RadioGroup from '@mui/material/RadioGroup';
import { MouseEvent, useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import { Severity } from '../../types/dbTypes';
import styles from './SeveritySelector.module.css';
import ReactionAPI from '../../utils/ReactionAPI';

export default function SeveritySelector({
  reactionType,
  severities,
  categoryId,
  setLoadingSeverity,
}: {
  reactionType: any;
  categoryId: number;
  severities: Severity[];
  setLoadingSeverity: Function;
}) {
  const { appContext, setAppContext } = useContext(AppContext);
  const [value, setValue] = useState(0);

  const rAPI = new ReactionAPI(appContext.user.id);
  const handler = async (e: any) => {
    setValue(e.target.value);
    setLoadingSeverity(() => true);

    const updatedReaction = {
      severityId: Number(e.target.value),
      reactionTypeId: reactionType.id,
      elementId: appContext.activeFood.id,
    };

    const json = await rAPI.setReaction(updatedReaction);

    const updatedContext = await rAPI.refreshReactionContext(appContext);

    setAppContext({ setAppContext, appContext: updatedContext });

    setLoadingSeverity(() => false);
  };

  const existingReactions = appContext.activeFood.reactions || [];

  useEffect(() => {
    setValue(0);
    for (let reaction of existingReactions) {
      if (reaction.reactionType === reactionType.id) {
        setValue(reaction.severity);
      }
    }
  }, [appContext.activeFood.id]);

  return (
    <>
      <FormControl>
        <RadioGroup
          className={styles.radioRow}
          onChange={handler}
          value={value}
          row
          aria-labelledby="demo-row-radio-buttons-group-label"
          name="row-radio-buttons-group"
        >
          {severities.map((severity: Severity, index: number) => {
            return (
              <FormControlLabel
                key={index}
                value={severity.id}
                control={<Radio size="small" />}
                label={<div className={styles.radioLabel}>{severity.name}</div>}
              />
            );
          })}
        </RadioGroup>
      </FormControl>
    </>
  );
}
