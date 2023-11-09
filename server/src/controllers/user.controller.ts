import UserService from '@services/user.service';
import { Request, Response } from 'express';
import catchAsync from '../utils/catchAsync';
class UserController {
  private userService;

  constructor() {
    this.userService = new UserService();
  }

  getAllUsers = catchAsync(async (req: Request, res: Response) => {
    const data = await this.userService.getAllUsers();
    res.status(200).json({
      status: 'success',
      data,
    });
  });

  getUser = catchAsync(async (req: Request, res: Response) => {
    const data = await this.userService.getUser(req.params.id);
    res.status(200).json({
      status: 'success',
      data,
    });
  });

  addUser = catchAsync(async (req: Request, res: Response) => {
    const data = await this.userService.addUser(req.body);
    res.status(200).json({
      status: 'success',
      data,
    });
  });

  updateUser = catchAsync(async (req: Request, res: Response) => {
    const data = await this.userService.updateUser(req.params.id, req.body);
    res.status(200).json({
      status: 'success',
      data,
    });
  });

  deleteUser = catchAsync(async (req: Request, res: Response) => {
    await this.userService.deleteUser(req.params.id);
    res.status(200).json({
      status: 'success',
    });
  });
}

export default new UserController();
