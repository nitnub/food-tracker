import { useState } from 'react';
import styles from './ReactionCategory.module.css';
import { ReactionType, Severity } from '../../types/dbTypes';
import SeveritySelector from '../SeveritySelector';

export default function ReactionCategory({
  categoryId,
  reactionType,
  severities,
}: {
  categoryId: number;
  reactionType: ReactionType;
  severities: Severity[];
}) {
  const [loadingSeverity, setLoadingSeverity] = useState(false);

  return (
    <div className={styles.test}>
      <div className={styles.reactionLabelContainer}>
        <div className={styles.reactionLabel}>{reactionType.name}</div>
        {loadingSeverity && <div className={styles.saveMessage}>Saving...</div>}
      </div>
      <SeveritySelector
        setLoadingSeverity={setLoadingSeverity}
        reactionType={reactionType}
        categoryId={categoryId}
        severities={severities}
      />
      <br />
    </div>
  );
}
