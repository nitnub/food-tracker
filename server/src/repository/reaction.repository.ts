import { Client, QueryResult, QueryResultBase, QueryResultRow } from 'pg';

import postgresConnect from '@connections/postgres.connection';
import { ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';
import {
  deleteReaction,
  insertReaction,
  selectUserReactions,
} from './queries/reaction.queries';

class ReactionRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
  }

  addReactions = async (reactionsArray: ReactionDbEntry[]) => {
    const selectQuery = reactionsArray.length;
    let queryString = this.createReactionArrayQuery(reactionsArray);

    const resp = await this.runQuery(queryString);

    if (!Array.isArray(resp)) {
      return resp;
    }
    // console.log(resp[selectQuery]);
    return resp[selectQuery].rows;
  };

  getUserReactions = async (userId: number) => {
    const resp = await this.runQuery(selectUserReactions(userId));
    return resp.rows;
  };

  deleteReaction = async (reactionId: number) => {
    const resp = await this.runQuery(deleteReaction(reactionId));
    return resp.rows;
  };

  runQuery = async (queryString: string) => {
    return await this.pool
      .query<ReactionDbEntry[]>(queryString)
      // .catch((resp) => {
      //   throw new AppError(resp.message, 400);
      // });
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
