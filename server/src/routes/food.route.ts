import { Router } from 'express';
import foodController from '@controllers/food.controller';

const foodRouter = Router();

foodRouter
  .route('/')
  .get(foodController.getAllFoods)
  .post(foodController.addFoods);

foodRouter
  .route('/:id')
  .patch(foodController.updateFood)
  .delete(foodController.deleteFood);

export default foodRouter;
