import { Router } from 'express';
import c from '@controllers/fodmap.controller';

const fodmapRouter = Router();

fodmapRouter.get('/', c.getAll);

export default fodmapRouter;
