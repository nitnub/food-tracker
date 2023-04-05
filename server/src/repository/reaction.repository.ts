import { query } from 'express';
import { QueryResult, QueryResultBase, QueryResultRow } from 'pg';
import Pool from 'pg-pool';
import { ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';
import {
  deleteReaction,
  insertReaction,
  selectUserReactions,
} from './queries/reaction.queries';
// const Pool = require('pg-pool');
// const url = require('url')

// const params = url.parse(process.env.DATABASE_URL);
// const auth = params.auth.split(':');

// const config = {
//   user: auth[0],
//   password: auth[1],
//   host: params.hostname,
//   port: params.port,
//   database: params.pathname.split('/')[1],
//   ssl: true
// };

// console.log(process.env.NODE_ENV)
// // console.log(process.env)
// console.log(process.env.PORT)
// console.log(process.env.DATABASE_PORT)

if (
  typeof process.env.DATABASE_PORT !== 'string' &&
  process.env.NODE_ENV === 'development'
) {
  throw Error('Error identifying DB Port!');
}

// console.log(`ssl= ${process.env.DATABASE_SSL_SETTING}`)
// console.log(process.env.DATABASE_PASSWORD)
// const port = process.env.DATABASE_PORT as unknown as number

const config = {
  user: process.env.DATABASE_USER,
  password: process.env.DATABASE_PASSWORD,
  host: process.env.DATABASE_HOST_NAME,
  port: process.env.DATABASE_PORT as unknown as number,
  database: process.env.DATABASE_NAME,
  ssl: process.env.DATABASE_SSL_SETTING === 'true',
};

// const pool = new Pool(config);

class ReactionRepository {
  private pool;
  constructor() {
    this.pool = new Pool(config);
  }

  addReactions = async (reactionsArray: ReactionDbEntry[]) => {
    const selectQuery = reactionsArray.length;
    let queryString = this.createReactionArrayQuery(reactionsArray);

    const resp = await this.runQuery(queryString);

    if (!Array.isArray(resp)) {
      return resp;
    }
    console.log(resp[selectQuery]);
    return resp[selectQuery].rows;
  };

  getUserReactions = async (userId: number) => {
    const resp = await this.runQuery(selectUserReactions(userId));
    // const resp = await this.pool
    //   .query<ReactionDbEntry[]>(selectUserReactions(userId))
    //   .catch((resp) => {
    //     throw new AppError(resp.message, 400);
    //   });

    return resp.rows;
  };

  deleteReaction = async (reactionId: number) => {
    const resp = await this.runQuery(deleteReaction(reactionId));
    console.log('Delete Respo:');
    console.log(resp);
    return resp;
  };

  runQuery = async (queryString: string) => {
    return await this.pool
      .query<ReactionDbEntry[]>(queryString)
      .catch((resp) => {
        throw new AppError(resp.message, 400);
      });
  };

  createReactionArrayQuery = (reactionsArray: ReactionDbEntry[]) => {
    let queryString = '';
    for (let reaction of reactionsArray) {
      queryString += insertReaction(reaction);
    }
    queryString += selectUserReactions(reactionsArray[0].userId);

    return queryString;
  };
}

export default ReactionRepository;
