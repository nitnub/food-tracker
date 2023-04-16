import { useEffect, useState } from 'react';

interface foodItem {
  name: string;
}

const defaultReactionResponse = {

    userId: 202,
    resultCount: 5,
    reactions: [
      {
        reactionId: 1,
        active: false,
        subsidedOn: null,
        modifiedOn: '2023-04-14T02:30:15.837Z',
        identifiedOn: '2023-04-08T02:45:44.733Z',
        deletedOn: null,
        food: {
          id: 1,
          reactionScope: 'food',
          name: 'Apple',
          vegetarian: true,
          vegan: true,
          glutenFree: true,
          fodMap: {
            id: 16,
            category: 'Fresh Fruit',
            name: 'apple',
            freeUse: false,
            oligos: false,
            fructose: true,
            polyols: true,
            lactose: false,
            color: 'Red',
            maxIntake: '0g',
          },
        },
        reaction: {
          category: 'Stomach',
          type: 'Nausea',
          severity: 'None',
        },
      },
    ],

};

export default function Selector() {
  const [foodList, setFoodList] = useState([]);
  const [reactionResponse, setReactionResponse] = useState(
    defaultReactionResponse
  );
  const [selection, setSelection] = useState<string | null>(null);
  const [reactionSelection, setReactionSelection] = useState<string | null>(
    null
  );

  // let val;

  useEffect(() => {
    // val = `document.getElementById('selector').value`;
    const getFood = async () => {
      const resp = await fetch(`http://localhost:3200/api/v1/food`);
      const json = await resp.json();

      const foodArr = json.data.map((el: foodItem) => {
        return el.name;
      });

      setFoodList(() => foodArr);
    };
    const getReactions = async () => {
      const resp = await fetch(`http://localhost:3200/api/v1/reaction/202`);
      const json = await resp.json();

      setReactionResponse(() => json.data);
      // const reactionArr = json.data.reactions.map((el ) => {
      //   return `${el.food.name} - ${el.reaction.type}`;
      // });

      // setReactionList(() => reactionArr);
    };

    getFood();
    getReactions();
    // console.log(val);
  }, []);

  return (
    <>
      {/* <div>{foodList}</div> */}
      <select
        id="selector"
        onChange={(e) => setReactionSelection(e.target.value)}
      >
        {reactionResponse.reactions.map((reaction, index) => (
          <option key={index} 
            value={reaction.reactionId}
          >{`${reaction.food.name} - ${reaction.reaction.type}`}</option>
        ))}
      </select>
      <div>
        {reactionResponse.reactions.map((reaction) => {
          if (reaction.reactionId.toString() === reactionSelection) {
            return JSON.stringify(reaction);
          }
          return ''
        })}
      </div>
        <h2>Reactions</h2>
        {reactionResponse.reactions.map((reaction) => {
          if (reaction.reactionId.toString() === reactionSelection) {
            return JSON.stringify(reaction);
          }
          return ''
        })}
      <br />
      <br />
      <br />
      <select
        id="reactionSelector"
        onChange={(e) => setSelection(e.target.value)}
      >
        {foodList.map((food, index) => (
          <option key={index} value={food}>{food}</option>
        ))}
      </select>
      <div>{selection}</div>
    </>
  );
}
