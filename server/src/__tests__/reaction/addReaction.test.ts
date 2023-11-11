import request from 'supertest';
import app from '@root/app';
import postgresConnect from '@connections/postgres.connection';
import {
  getUniqueAdmin,
  getUniqueUser,
  validDateResponses,
} from '../../../.jest/test-utils';

const ROUTE = '/api/v1/reaction';

// const expectedAddResponse_OLD = {
//   id: expect.any(Number),
//   globalUserId: expect.any(String),
//   email: expect.any(String),
//   admin: expect.any(Boolean),
//   avatar: expect.any(String),
//   active: expect.any(Boolean),
//   createdOn: expect.toBeOneOf(validDateResponses),
//   lastModifiedOn: expect.toBeOneOf(validDateResponses),
//   deletedOn: expect.toBeOneOf(validDateResponses),
//   lastModifiedBy: expect.toBeOneOf([expect.any(String), null]),
// };

const expectedAddResult = {
  id: expect.any(Number),
  userId: expect.any(Number),
  active: expect.any(Boolean),
  subsidedOn: expect.toBeOneOf(validDateResponses),
  modifiedOn: expect.toBeOneOf(validDateResponses),
  identifiedOn: expect.toBeOneOf(validDateResponses),
  deletedOn: expect.toBeOneOf(validDateResponses),
  reactionType: expect.any(String),
  reactionCategory: expect.any(String),
  reactionScope: expect.any(String),
  foodId: expect.any(Number),
  foodName: expect.any(String),
  vegetarian: expect.any(Boolean),
  vegan: expect.any(Boolean),
  glutenFree: expect.any(Boolean),
  reactionSeverity: expect.toBeOneOf([
    'None',
    'Mild',
    'Moderate',
    'Strong',
    'Severe',
  ]),
  fodId: expect.any(Number),
  fodCategory: expect.any(String),
  fodName: expect.any(String),
  fodFreeUse: expect.any(Boolean),
  fodOligos: expect.any(Boolean),
  fodFructose: expect.any(Boolean),
  fodPolyols: expect.any(Boolean),
  fodLactose: expect.any(Boolean),
  fodColor: expect.toBeOneOf(['Green', 'Yellow', 'Red']),
  maxIntake: expect.any(String),
  maxIntakeTest: expect.any(String),
};

const expectedAddResponse = {
  status: expect.toBeOneOf(['success', 'fail']),
  result: expect.arrayContaining([expectedAddResult]),
};

const newReaction = {
  elementId: 14,
  reactionTypeId: 2,
  severityId: 1,
  active: true,
  foodGroupingId: 1,
};

afterEach(() => {
  const trimTestDb = 'DELETE FROM reaction WHERE id > 100;';
  postgresConnect.query(trimTestDb);
});

describe('UserReaction POST integration', () => {
  const userId = 202;

  it('sends 200 response on valid call', async () => {
    await request(app)
      .post(`${ROUTE}/${userId}`)
      .send(newReaction)
      .expect(200);
  });

  it('updates the db', async () => {
    const getResponse1 = await request(app).get(`${ROUTE}/${userId}`);
    const initialLength = getResponse1.body.data.resultCount;

    await request(app).post(`${ROUTE}/${userId}`).send(newReaction);

    const getResponse2 = await request(app).get(`${ROUTE}/${userId}`);
    const finalLength = getResponse2.body.data.resultCount;

    expect(finalLength).toBe(initialLength + 1);
  });

  it('has response of proper format', async () => {
    const res = await request(app)
      .post(`${ROUTE}/${userId}`)
      .send(newReaction);

    expect(res.body).toEqual(expectedAddResponse);
  });
});
