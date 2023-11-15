import { Client, Pool, PoolClient, PoolConfig } from 'pg';
import postgresConnect from '@connections/postgres.connection';
import { ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';
import {
  deleteReaction,
  insertReaction,
  insertReactionFormatted,
  selectReactionAll,
  selectUserReactions,
} from './queries/reaction';
import { isPGError } from '../utils/validators';

// interface PGError{
//   length: number
//   severity: 'ERROR'
//   code: '42703'
//   detail: undefined
//   hint: undefined
//   position: '64'
//   internalPosition: undefined
//   internalQuery: undefined
//   where: undefined
//   schema: undefined
//   table: undefined
//   column: undefined
//   dataType: undefined
//   constraint: undefined
//   file: 'parse_relation.c'
//   line: '3661'
//   routine: 'errorMissingColumn'
// }

interface PGError {
  length: number;
  severity: string;
  code: string;
  position: string;
  file: string;
  line: string;
  routine: string;
}

class ReactionRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
  }

  getReactionOptions = async () => {
    const reactionOptions = await this.runQuery(selectReactionAll());
    if (!Array.isArray(reactionOptions)) {
      return reactionOptions;
    }
    return {
      severities: reactionOptions[0].rows,
      categories: reactionOptions[1].rows,
      types: reactionOptions[2].rows,
    };
  };

  addReaction = async (reaction: ReactionDbEntry) => {
    const resp = await this.runQuery(insertReactionFormatted(reaction));

    if (!Array.isArray(resp)) {
      return resp;
    }

    if (resp[1].rows.length === 0) {
      throw new AppError('Unable to add reaction', 400);
    }
    return resp[1].rows;
  };

  addReactions = async (reactionsArray: ReactionDbEntry[]) => {
    const selectQuery = reactionsArray.length;
    const queryString = this.createReactionArrayQuery(reactionsArray);
    const resp = await this.runQuery(queryString);

    if (!Array.isArray(resp)) {
      return resp;
    }
    return resp[selectQuery].rows;
  };

  getUserReactions = async (userId: number) => {
    const resp = await this.runQuery(selectUserReactions(userId));
    return resp.rows.map((reaction) => reaction['entry']);
  };

  deleteReaction = async (reactionId: number) => {
    const resp = await this.runQuery(deleteReaction(reactionId));
    return resp.rowCount;
  };

  runQuery = async (queryString: string) => {
    return await this.pool.query(queryString);
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
