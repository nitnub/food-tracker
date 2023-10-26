import { Client } from 'pg';

import postgresConnect from '@connections/postgres.connection';
import { deleteFood, insertFood, selectAllFoods } from './queries/food';

class FoodRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
  }

  addFoods = async (foodsArray: any) => {
    const resp = await this.runQuery(this.createFoodArrayQuery(foodsArray));

    if (!Array.isArray(resp)) {
      return resp;
    }
    return foodsArray;
  };

  getAllFoods = async () => {
    const resp = await this.runQuery(selectAllFoods());
    console.log(resp.rows);
    return resp.rows;
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
