import { Client } from 'pg';

import postgresConnect from '@connections/postgres.connection';
// import { ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';
import {
  deleteReaction,
  insertReaction,
  selectUserReactions,
} from './queries/reaction.queries';
import { insertFood, selectAllFoods } from './queries/food.queries';

class ReactionRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
  }

  addFoods = async (foodsArray: any) => {
    // const selectQuery = foodsArray.length;
    // let queryString = this.createReactionArrayQuery(foodsArray);

    const resp = await this.runQuery(this.createFoodArrayQuery(foodsArray));

    if (!Array.isArray(resp)) {
      return resp;
    }
    return foodsArray;
    // console.log(resp[selectQuery]);
    // return resp[selectQuery].rows;
  };

  getUserReactions = async (userId: number) => {
    const resp = await this.runQuery(selectUserReactions(userId));
    return resp.rows;
  };
  getAllFoods = async () => {
    const resp = await this.runQuery(selectAllFoods());
    console.log(resp.rows)
    return resp.rows;
  };

  deleteReaction = async (reactionId: number) => {
    const resp = await this.runQuery(deleteReaction(reactionId));
    return resp.rows;
  };

  runQuery = async (queryString: string) => {
    return await this.pool
      .query(queryString)
      // .catch((resp) => {
      //   throw new AppError(resp.message, 400);
      // });
  };

  createFoodArrayQuery = (foodArray: any[]) => {
    let queryString = '';
    for (let food of foodArray) {
      queryString += insertFood(food);
    }
    // queryString += selectAllFoods(reactionsArray[0].userId);

    return queryString;
  };
}

export default ReactionRepository;
