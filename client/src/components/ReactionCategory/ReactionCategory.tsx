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
}: {
  // foodItem: object;
  categoryId: number;
  reactionType: ReactionType;
  severities: Severity[];
}) {
  const reaction = {
    elementId: 11,
    reactionTypeId: reactionType.id,
    severityId: 1,
    // active: false
  };

  return (
    <>
      {reactionType.name}{' '}
      <SeveritySelector
        // foodItem={foodItem}
        reactionTypeId={reactionType.id}
        categoryId={categoryId}
        severities={severities}
      />
      <br />
    </>
  );
  // return <>{reactionType.name} </>
}
