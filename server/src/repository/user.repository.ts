import postgresConnect from '@connections/postgres.connection';
import { Client } from 'pg';
import AppError from '../utils/appError';
import {
  deleteUser,
  insertUser,
  selectAllUsers,
  selectUser,
  selectUserByEmail,
  updateUser,
} from './queries/user';
class UserRepository {
  private pool: Client;

  constructor() {
    this.pool = postgresConnect;
  }

  getUser = async (userId: number) => {
    const resp = await this.runQuery(selectUser(userId));
    return resp.rows;
  };

  getAllUsers = async () => {
    const resp = await this.runQuery(selectAllUsers());
    return resp.rows;
  };

  addUser = async (userArray: UserDbEntry[]) => {
    let queryString = '';

    for (let user of userArray) {
      queryString += insertUser(user);
    }

    // add select statement at end of query
    if (userArray.length === 1) {
      queryString += selectUserByEmail(userArray[0].email);
    } else {
      queryString += selectAllUsers();
    }

    const resp = await this.runQuery(queryString);
    if (!Array.isArray(resp)) {
      return resp;
    }

    return resp[userArray.length].rows;
  };

  updateUser = async (id: string, userUpdates: UserDbUpdateEntry) => {
    const resp = await this.runQuery(updateUser(id, userUpdates));

    if (!Array.isArray(resp)) {
      throw new AppError(`Unable to update user with id ${id}`, 400);
    }
    if (resp[1].rows.length === 0) {
      throw new AppError(
        `Unable to update user with id ${id}; user not found.`,
        400
      );
    }
    return resp[1].rows;
  };

  deleteUser = async (userId: number) => {
    const resp = await this.runQuery(deleteUser(userId));
    return resp.rowCount;
  };

  runQuery = async (queryString: string) => {
    return await this.pool.query<UserDbEntry[]>(queryString);
  };
}

export default UserRepository;
