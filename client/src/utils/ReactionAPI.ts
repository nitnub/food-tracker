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
    return json.data;
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
    const reactions = await this.getReactionListByFood(
      Number(this.userId),
      appContext.activeFood.id,
      reactionArr
    );

    const contextCopy = { ...appContext, user: res };
    contextCopy.activeFood.reactions = reactions;

    return contextCopy;
  };

  getReactionListByFood = (
    userId: number,
    foodId: number,
    reactionList: any[]
  ) => {
    const rawReactions: any = [];

    reactionList.forEach((reaction: any) => {
      if (reaction.food.id === foodId) {
        rawReactions.push(reaction);
      }
    });

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
    console.log('json:');
    console.log(json);
    console.log('this.userId:');
    console.log(this.userId);
    if (json.status !== 'success') {
      throw new Error('Whoops - unable to fetch reactions!');
    }
    return json.data;
  };
}
