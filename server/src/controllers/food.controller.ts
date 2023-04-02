import { Request, Response } from 'express';

export const add = (req: Request, res: Response) => {
  console.log(`request body:`, req.body);

  
  res.status(200).json({
    status: 'success',
  });
};
