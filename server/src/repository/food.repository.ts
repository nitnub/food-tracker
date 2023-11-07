import { Client } from 'pg';
import postgresConnect from '@connections/postgres.connection';
import { deleteFood, insertFood, selectAllFoods } from './queries/food';
import { FoodUpdateObject } from '../types/food.types';
import AppError from '../utils/appError';
import updateFood from './queries/food/updateFood';

class FoodRepository {
  private pool: Client;

  constructor() {
    this.pool = postgresConnect;
  }

  addFood = async (food: any) => {
    const resp = await this.runQuery(insertFood(food));

    return resp.rowCount === 1;
  };

  addFoods = async (foodsArray: any) => {
    const resp = await this.runQuery(this.createFoodArrayQuery(foodsArray));

    if (!Array.isArray(resp)) {
      return resp;
    }
    return resp.rows;
  };

  getAllFoods = async () => {
    const resp = await this.runQuery(selectAllFoods());
    return resp.rows;
  };

  updateFood = async (id: number, foodUpdate: FoodUpdateObject) => {
    const resp = await this.runQuery(updateFood(id, foodUpdate));

    if (!Array.isArray(resp)) {
      throw new AppError(`Unable to update food ${id}`, 400);
    }
    if (resp[1].rows.length === 0) {
      throw new AppError(
        `Unable to update user with id ${id}; user not found.`,
        400
      );
    }
    return resp[1].rows;
  };

  deleteFood = async (foodId: number) => {
    const resp = await this.runQuery(deleteFood(foodId));

    return resp.rowCount;
  };

  runQuery = async (queryString: string) => {
    return await this.pool.query(queryString);
  };

  createFoodArrayQuery = (foodArray: any[]) => {
    let queryString = '';
    for (let food of foodArray) {
      queryString += insertFood(food);
    }
    return queryString;
  };
}

export default FoodRepository;
