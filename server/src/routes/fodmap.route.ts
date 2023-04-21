import { Router } from 'express';
import fodmapController from '@controllers/fodmap.controller';

const fodmapRouter = Router();

fodmapRouter.get('/', fodmapController.getAll);

export default fodmapRouter;
