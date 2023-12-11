import { Router } from 'express';
import c from '@controllers/food.controller';

const foodRouter = Router();

foodRouter
  .route('/')
  .get(c.getAllFoods)
  .post(c.addFoods);

foodRouter
  .route('/:id')
  .patch(c.updateFood)
  .delete(c.deleteFood);

export default foodRouter;
