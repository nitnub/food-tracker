import UserRepository from '@repository/user.repository';
import AppError from '../utils/appError';

class UserService {
  private userRepository;

  constructor() {
    this.userRepository = new UserRepository();
  }

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
    if (!Array.isArray(userArray)) {
      userArray = [userArray];
    }

    for (let user of userArray) {
      if (!isValidNewUser(user)) {
        throw new AppError(
          `Database was not updated. Invalid user: ${JSON.stringify(user)}.`,
          400
        );
      }
    }

    return await this.userRepository.addUser(userArray);
  };

  updateUser = async (globalUserId: string, userUpdates: UserDbUpdateEntry) => {
    return await this.userRepository.updateUser(globalUserId, userUpdates);
  };

  deleteUser = async (userId: number) => {
    const result = await this.userRepository.deleteUser(userId);

    if (result === 0) {
      throw new AppError(
        `Unable to find any results for User ID ${userId}.`,
        401
      );
    }
  };
}

export default UserService;

function isValidNewUser(user: UserDbEntry) {
  const props = ['globalUserId', 'email', 'admin', 'avatar'];
  const hasProps = props.every((p) => user.hasOwnProperty(p));
  const isCorrectLength = Object.keys(user).length === props.length;

  return isObject(user) && hasProps && isCorrectLength;
}

function isObject(candidate: UserDbEntry) {
  return (
    typeof candidate === 'object' &&
    !Array.isArray(candidate) &&
    candidate !== null
  );
}
