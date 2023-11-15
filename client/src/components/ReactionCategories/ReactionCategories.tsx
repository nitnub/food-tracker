import {
  Category,
  ReactionOptionProps,
  ReactionType,
} from '../../types/dbTypes';
import ReactionCategory from '../ReactionCategory';

export default function ReactionCategories({
  reactions,
  userReactions,
}: {
  reactions: ReactionOptionProps;
  userReactions: any;
}) {
  return (
    <>
      {reactions.categories.map((category: Category, index: number) => {
        return (
          <div key={index}>
            <h3>{category.name}</h3>
            {category.reactionTypes?.map(
              (reactionType: ReactionType, index: number) => {
                return (
                  <ReactionCategory
                    key={index}
                    reactionType={reactionType}
                    severities={reactions.severities}
                  />
                );
              }
            )}
          </div>
        );
      })}
    </>
  );
}
