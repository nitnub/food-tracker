import UserRepository from '@repository/user.repository';
import AppError from '../utils/appError';
class UserService {
  private userRepository;
  constructor() {
    this.userRepository = new UserRepository();
  }

  userExists = async (userId: number) => {
    return await this.userRepository.userExists(userId);
  };

  getAllUsers = async () => {
    const users = await this.userRepository.getAllUsers();

    return users;
  };

  getUser = async (userId: number) => {
    const user = await this.userRepository.getUser(userId);
    if (!Array.isArray(user) || user.length === 0) {
      throw new AppError(`Unable to find user ${userId}`, 401);
    }
    return user;
  };

  addUser = async (userArray: UserDbEntry[]) => {
    // check for admin to allow more than one entry
    return await this.userRepository.addUser(userArray);
  };

  deleteUser = async (userId: number) => {
    const result = await this.userRepository.deleteUser(userId);

    if (result === 0) {
      throw new AppError(
        `Unable to find any results for reactionId ${userId}`,
        401
      );
    }
  };
}

export default UserService;
