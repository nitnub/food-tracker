import FoodRepository from '@repository/food.repository';
import { FoodDBObject, FoodUpdateObject } from '../types/food.types';
import AppError from '../utils/appError';

interface AddFoodResponse {
  status: 'success' | 'fail';
  data?: FoodDBObject;
}
class FoodService {
  private foodRepository;
  constructor() {
    this.foodRepository = new FoodRepository();
  }

  getAllFoods = async () => {
    const data = await this.foodRepository.getAllFoods();
    return data;
  };

  addFood = async (food: FoodDBObject) => {
    let resp: AddFoodResponse = { status: 'fail' };
    let data;
    const success = await this.foodRepository.addFood(food);

    if (success) {
      data = await this.foodRepository.getAllFoods();
      resp = { status: 'success', data };
    }

    return resp;
  };

  addFoods = async (foods: FoodDBObject) => {
    const data = await this.foodRepository.addFoods(foods);
    return data;
  };

  updateFood = async (id: number, foodUpdate: FoodUpdateObject) => {
    if (Object.keys(foodUpdate).length === 0) {
      throw new AppError(
        'Food updates must include at least one parameter',
        400
      );
    }

    return await this.foodRepository.updateFood(id, foodUpdate);
  };

  deleteFood = async (foodId: number) => {
    const result = await this.foodRepository.deleteFood(foodId);

    if (result !== 1) {
      console.log(result);
      throw new AppError(`Unable to find a food with ID ${foodId}.`, 401);
    }
  };
}
export default FoodService;
