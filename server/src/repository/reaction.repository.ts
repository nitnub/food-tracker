import { Client, QueryResult, QueryResultBase, QueryResultRow } from 'pg';

import postgresConnect from '@connections/postgres.connection';
import { ReactionDbEntry } from '../types/reaction.types';
import AppError from '../utils/appError';
import {
  deleteReaction,
  insertReaction,
  insertReactionWithFormattedReturn,
  selectReactionCategories,
  selectReactionSeverities,
  selectReactionSeveritiesAndTypes,
  selectReactionTypes,
  selectUserReactions,
} from './queries/reaction.queries';

class ReactionRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
  }

  getReactionOptions = async () => {
    // const severities = await this.runQuery(selectReactionSeverities());
    // const reactionTypes = await this.runQuery(selectReactionTypes());
    const reactionOptions = await this.runQuery(selectReactionSeveritiesAndTypes());
    // const reactionCategories = await this.runQuery(selectReactionCategories());
    if (!Array.isArray(reactionOptions)) {
      return reactionOptions
    }
    return {
      severities: reactionOptions[0].rows,
      categories: reactionOptions[1].rows,
      types: reactionOptions[2].rows
      // reactionCategories: reactionCategories.rows,
    };
  };

  addReaction = async (reaction: ReactionDbEntry) => {
    // const selectQuery = reactionsArray.length;
    // let queryString = this.insertReaction(reaction);
    // console.log('reaction:', reaction)
    console.log('reaction1:')
    console.log(reaction)
    console.log(typeof reaction)
    const resp = await this.runQuery(insertReactionWithFormattedReturn(reaction));

    if (!Array.isArray(resp)) {
      return resp;
    }

    if (resp[1].rows.length === 0) {
      throw new AppError('Unable to add reaction', 400)
    }
    // console.log(resp.rows);
    return resp[1].rows;
  };

  addReactions = async (reactionsArray: ReactionDbEntry[]) => {
    console.log('adding reaction array...')
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
    return await this.pool.query<ReactionDbEntry[]>(queryString);
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
