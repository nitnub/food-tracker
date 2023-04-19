import { useContext, useEffect, useState } from 'react';
import AppContext from '../../context/AppContext';
import PendingIcon from '@mui/icons-material/Pending';
import styles from './ReactionCategory.module.css';
import {
  ReactionOptionProps,
  ReactionType,
  Severity,
} from '../../types/dbTypes';
import SeveritySelector from '../SeveritySelector';

// export default function ReactionCategory(reactionType: ReactionType, severities: Severity[]) {
export default function ReactionCategory({
  // foodItem,
  categoryId,
  reactionType,
  severities,
}: // defaultValue,
{
  // foodItem: object;
  categoryId: number;
  reactionType: ReactionType;
  severities: Severity[];
  // defaultValue: number | null;
}) {
  const [loadingSeverity, setLoadingSeverity] = useState(false);
  // // const reaction = {
  // //   elementId: 11,
  // //   reactionTypeId: reactionType.id,
  // //   severityId: 1,
  // //   // active: false
  // };

  // const {appContext, setAppContext} = useContext(AppContext);
  // const [defaultValue, setDefaultValue] = useState(0)
  // // console.log('aaaaaaaaaaaafffff', appContext.activeFood.reactions)
  // const existingReactions = appContext.activeFood.reactions || []

  // // let defaultValue = null;

  // useEffect(() => {
  //   console.log('aa')
  //   // console.log(reaction)
  //   setDefaultValue(0)
  //   for(let reaction of existingReactions) {
  //     // console.log(reaction, reactionType)
  //     if (reaction.reactionType === reactionType.id) {
  //       // defaultValue = reactionType.id
  //       // console.log(reaction)
  //       console.log('bb');
  //       console.log(reaction.severity)
  //       setDefaultValue(reaction.severity)
  //     }
  //   }
  //   // console.log('render now')

  // })

  // console.log(defaultValue)

  return (
    <div className={styles.test}>
      <div className={styles.reactionLabelContainer}>
        <div className={styles.reactionLabel}>{reactionType.name}</div>
        {/* {loadingSeverity && <PendingIcon />} */}
        {loadingSeverity && <div className={styles.saveMessage}>Saving...</div>}
      </div>
      <SeveritySelector
        // foodItem={foodItem}
        // defaultValue={defaultValue}
        setLoadingSeverity={setLoadingSeverity}
        reactionType={reactionType}
        // reactionTypeId={reactionType.id}
        categoryId={categoryId}
        severities={severities}
      />
      <br />
    </div>
  );
  // return <>{reactionType.name} </>
}
