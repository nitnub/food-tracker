import request from 'supertest';
import app from '@root/app';
import postgresConnect from '@connections/postgres.connection';
import { validDateResponses } from '../../../.jest/test-utils';

const ENDPOINT = '/api/v1/reaction';

const food = {
  id: expect.any(Number),
  reactionScope: expect.any(String),
  name: expect.any(String),
  vegetarian: expect.any(Boolean),
  vegan: expect.any(Boolean),
  glutenFree: expect.any(Boolean),
  fodMap: expect.toBeOneOf([expect.any(Number), null]),
};

const reaction = {
  id: expect.any(Number),
  category: expect.any(String),
  typeName: expect.any(String),
  typeId: expect.any(Number),
  severityName: expect.any(String),
  severityId: expect.any(Number),
  foodGroupingId: expect.any(Number),
};

const reactionDetails = {
  entry: {
    reactionId: expect.any(Number),
    active: expect.any(Boolean),
    subsidedOn: expect.toBeOneOf(validDateResponses),
    modifiedOn: expect.toBeOneOf(validDateResponses),
    identifiedOn: expect.toBeOneOf(validDateResponses),
    deletedOn: expect.toBeOneOf(validDateResponses),
    food,
    reaction,
  },
};

const expectedAddResponse = {
  status: expect.toBeOneOf(['success', 'fail']),
  result: expect.arrayContaining([reactionDetails]),
};

const reaction1 = {
  userId: 202,
  elementId: 10,
  active: true,
  reactionTypeId: 5,
  severityId: 5,
};
const reaction2 = {
  userId: 202,
  elementId: 11,
  active: true,
  reactionTypeId: 5,
  severityId: 5,
};
const reaction3 = {
  userId: 202,
  elementId: 12,
  active: true,
  reactionTypeId: 5,
  severityId: 5,
};

const reactionArray = [reaction1, reaction2, reaction3];

const reactionRequest = { reactions: [reaction1] };
const reactionArrayRequest = { reactions: reactionArray };

afterEach(() => {
  const trimTestDb = 'DELETE FROM reaction WHERE id > 100;';
  postgresConnect.query(trimTestDb);
});

describe('ReactionAdmin POST integration', () => {
  const userId = 202;

  describe('single entry', () => {
    it('sends 200 response on valid call', async () => {
      await request(app).post(ENDPOINT).send(reactionRequest).expect(200);
    });

    it('updates the db', async () => {
      const getResponse1 = await request(app).get(`${ENDPOINT}/${userId}`);
      const initialLength = getResponse1.body.data.resultCount;

      await request(app).post(ENDPOINT).send(reactionRequest);

      const getResponse2 = await request(app).get(`${ENDPOINT}/${userId}`);
      const finalLength = getResponse2.body.data.resultCount;

      expect(finalLength).toBe(initialLength + 1);
    });

    it('has response of proper format', async () => {
      const res = await request(app).post(ENDPOINT).send(reactionRequest);

      expect(res.body).toEqual(expectedAddResponse);
    });
  });

  describe('multi-entry', () => {
    it('sends 200 response on valid call', async () => {
      await request(app).post(ENDPOINT).send(reactionArrayRequest).expect(200);
    });

    it('updates the db', async () => {
      const getResponse1 = await request(app).get(`${ENDPOINT}/${userId}`);
      const initialLength = getResponse1.body.data.resultCount;

      await request(app).post(ENDPOINT).send(reactionArrayRequest);

      const getResponse2 = await request(app).get(`${ENDPOINT}/${userId}`);
      const finalLength = getResponse2.body.data.resultCount;

      expect(finalLength).toBe(initialLength + reactionArray.length);
    });

    it('has response of proper format', async () => {
      const res = await request(app).post(ENDPOINT).send(reactionArrayRequest);
      expect(res.body).toEqual(expectedAddResponse);
    });
  });
});
