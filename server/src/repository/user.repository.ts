import postgresConnect from '@connections/postgres.connection';
import { Client } from 'pg';
import AppError from '../utils/appError';
import { deleteUser, insertUser, selectAllUsers, selectUser } from './queries/user.queries';
class UserRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
  }

  userExists = async (userId: number) => {
    const user = await this.runQuery(selectUser(userId));
    if (Array.isArray(user) && user.length > 0) {
      // TODO: can check for deleted.
      return true;
    }
    return false;
  };

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
    queryString += selectAllUsers();

    const resp = await this.runQuery(queryString);
    return resp.rows;
  };

  updateUser = async (user: UserDbEntry) => {};
  deleteUser = async (userId: number) => {
    const resp = await this.runQuery(deleteUser(userId))
   
    return resp.rowCount;
  };

  runQuery = async (queryString: string) => {
    return await this.pool.query<UserDbEntry[]>(queryString)
    // .catch((resp) 
    // => {
      // console.log(resp)
      // throw new AppError(`${resp.message}.${resp.detail ? ' ' + resp.detail : ''}`, 400);
    // }
    // );
  };
}

export default UserRepository;
