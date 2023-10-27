import {
  Category,
  ReactionOptionProps,
  ReactionType,
} from '../../types/dbTypes';
import ReactionCategory from '../ReactionCategory';

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
                    categoryId={category.id}
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
