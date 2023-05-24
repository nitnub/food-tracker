import { Client } from 'pg';

import postgresConnect from '@connections/postgres.connection';
// import { ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';
import {
  deleteReaction,
  insertReaction,
  selectUserReactions,
} from './queries/reaction.queries';
import { deleteFood, insertFood, selectAllFoods } from './queries/food.queries';

class FoodRepository {
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

  // getUserReactions = async (userId: number) => {
  //   const resp = await this.runQuery(selectUserReactions(userId));
  //   return resp.rows;
  // };
  getAllFoods = async () => {
    const resp = await this.runQuery(selectAllFoods());
    console.log(resp.rows);
    return resp.rows;
  };

  deleteFood = async (foodId: number) => {
    const resp = await this.runQuery(deleteFood(foodId));
    // issue where resp.rowCount returns 1, but resp.rows is blank. This was causing issues with original logic. Updating to accommodate missing row info.
    // console.log('response!');
    // console.log(resp);
    // console.log('response!');
    // return resp.rows;
    return resp.rowCount;
  };

  runQuery = async (queryString: string) => {
    return await this.pool.query(queryString);
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

export default FoodRepository;
