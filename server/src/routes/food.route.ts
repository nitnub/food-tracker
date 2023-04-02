import {Router} from 'express';
import {add} from '@controllers/food.controller'

const foodRouter = Router()

foodRouter.route('/').post(add)



export default foodRouter;