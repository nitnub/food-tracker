import { Request, Response, NextFunction } from "express";

export default (fn: Function) => {
  return (req: Request, res: Response, next: NextFunction) => {
    // fn(req, res, next).catch((err) => next(err));
    fn(req, res, next).catch(next); // shorthand of the above line
  };
};