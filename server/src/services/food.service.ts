import FoodRepository from '@repository/food.repository';
import { FoodDBObject } from '../types/food.types';
import AppError from '../utils/appError';
class FoodService {
  private foodRepository;
  constructor() {
    this.foodRepository = new FoodRepository();
  }

  getAllFoods = async () => {
    const data = await this.foodRepository.getAllFoods();
    return data;
  };

  addFoods = async (foods: FoodDBObject) => {
    // const foodsChecked = [...foods]

    // TODO: account for blank strings

    const data = await this.foodRepository.addFoods(foods);
    return data;
  };

  deleteFood = async (foodId: number) => {
    const result = await this.foodRepository.deleteFood(foodId);
    // issue where resp.rowCount returns 1, but resp.rows is blank. This was causing issues with original logic. Updating to accommodate missing row info.
    // if (Array.isArray(result) && result.length === 0) {
    //   console.log(result);
    //   throw new AppError(`Unable to find a food with ID ${foodId}.`, 401);
    // }
    if (result !== 1) {
      console.log(result);
      throw new AppError(`Unable to find a food with ID ${foodId}.`, 401);
    }
  };
}
export default FoodService;
