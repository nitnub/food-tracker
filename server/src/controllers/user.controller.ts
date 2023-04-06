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
    const data = await this.userService.addUser(req.body.data);
    res.status(200).json({
      status: 'success',
      data,
    });
  });

  deleteUser = catchAsync(async (req: Request, res: Response) => {
    await this.userService.deleteUser(req.params.id);

    res.status(202).json({
      status: 'success',
    });
  });
}

export default new UserController();
