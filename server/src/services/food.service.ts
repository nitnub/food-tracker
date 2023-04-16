import FoodRepository from '@repository/food.repository'
import {FoodDBObject} from '../types/food.types'
class FoodService {
  private foodRepository;
  constructor() {
    this.foodRepository = new FoodRepository()
  }

  getAllFoods = async () => {
    const data = await this.foodRepository.getAllFoods();
    return data;
  };

  addFoods = async (foods: FoodDBObject) => {

    const data = await this.foodRepository.addFoods(foods)
    return data;
  }


}

export default FoodService;