import { join } from 'path';
import { ReactionEntry } from '../types/dbTypes';

interface ReactionUpdate {
  severityId: number;
  reactionTypeId: number;
  elementId: number;
}

export default class ReactionAPI {
  private userId;
  constructor(userId: number) {
    this.userId = userId;
  }

  getReactionTypeDetails = async () => {
    const resp = await fetch('http://localhost:3200/api/v1/reaction');
    const json = await resp.json();
    // return await resp.json()
    return json.data;
  };

  getShortReaction = () => {
    const res = this.fetchById();
  };

  getReactions = async () => {
    return await this.fetchById();
  };

  getReactiveFoods = async () => {
    const res = await this.fetchById();
    return res.reactiveFoods;
  };

  refreshReactionContext = async (appContext: any) => {
    const res = await this.getReactions();
    const reactionArr = res.reactions;
    // console.log('res');
    // console.log(res);

    // if (JSON.stringify(res).length > 1) {
    const reactions = await this.getReactionListByFood(
      Number(this.userId),
      appContext.activeFood.id,
      reactionArr
    );

    const contextCopy = { ...appContext };
    contextCopy.user = res;
    // console.log('reactions:')
    // console.log(reactions)
    // console.log('input Array')
    // console.log({
    //   userId: Number(this.userId),
    //   foodId: appContext.activeFood.id,
    //   reactionArr
    // })
    contextCopy.activeFood.reactions = reactions;
    // console.log('rf');
    // console.log(contextCopy.user.reactiveFoods);
    return contextCopy;
    // }
    return [];
  };

  getReactionListByFood = (
    userId: number,
    foodId: number,
    reactionList: any[]
  ) => {
    const rawReactions: any = [];
    const formattedReactions: any = [];

    reactionList.forEach((reaction: any) => {
      if (reaction.food.id === foodId) {
        rawReactions.push(reaction);
      }
    });

    // rawReactions.forEach((raw: any) => {
    // const { reaction } = entry;
    // const formattedReaction: ReactionEntry = {
    //   userId,
    //   elementId: foodId,
    //   foodGroupingId: reaction.foodGroupingId,
    //   reactionType: reaction.typeId,
    //   severity: reaction.severityId,
    //   active: reaction.active,
    // };
    // formattedReactions.push(formattedReaction);

    // formattedReactions.push({
    return rawReactions.map((raw: any) => {
      return {
        userId,
        elementId: foodId,
        foodGroupingId: raw.reaction.foodGroupingId,
        reactionType: raw.reaction.typeId,
        severity: raw.reaction.severityId,
        active: raw.reaction.active,
      };
    });
    // }
    // );

    return formattedReactions;
  };

  setReaction = async (updatedReaction: ReactionUpdate) => {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(updatedReaction),
    };

    const resp = await fetch(
      `http://localhost:3200/api/v1/reaction/${this.userId}`,
      requestOptions
    );

    return await resp.json();
  };

  fetchById = async () => {
    const res = await fetch(
      `http://localhost:3200/api/v1/reaction/${this.userId}`
    );
    const json = await res.json();
    if (json.status !== 'success') {
      throw new Error('Whoops - unable to fetch reactions!');
    }
    return json.data;
  };
}
