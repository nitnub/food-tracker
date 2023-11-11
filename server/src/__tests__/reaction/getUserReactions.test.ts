import request from 'supertest';
import app from '@root/app';
import { validDateResponses } from '../../../.jest/test-utils';
// import { validDateResponses } from '@testRepo/test-utils';

const ROUTE = '/api/v1/reaction';

const expectedGetResponse = {
  id: expect.any(Number),
  globalUserId: expect.any(String),
  email: expect.any(String),
  admin: expect.any(Boolean),
  avatar: expect.any(String),
  active: expect.any(Boolean),
  createdOn: expect.toBeOneOf(validDateResponses),
  lastModifiedOn: expect.toBeOneOf(validDateResponses),
  deletedOn: expect.toBeOneOf(validDateResponses),
  lastModifiedBy: expect.any(String),
};
const testDBSize = 22;

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
  reactionId: expect.any(Number),
  active: expect.any(Boolean),
  subsidedOn: expect.toBeOneOf(validDateResponses),
  modifiedOn: expect.toBeOneOf(validDateResponses),
  identifiedOn: expect.toBeOneOf(validDateResponses),
  deletedOn: expect.toBeOneOf(validDateResponses),
  food,
  reaction,
};

const status = expect.toBeOneOf(['success', 'fail']);
const data = {
  reactiveFoods: expect.arrayContaining([expect.any(Number)]),
  resultCount: expect.any(Number),
  reactions: expect.arrayContaining([reactionDetails]),
};

const expectedResponse = { status, data };

describe('UserReaction GET integration', () => {
  const userId = 202;
  it('sends 200 response on valid call', async () => {
    await request(app).get(`${ROUTE}/${userId}`).expect(200);
  });

  it('has results of proper format', async () => {
    const res = await request(app).get(`${ROUTE}/${userId}`);
    expect(res.body).toEqual(expectedResponse);
  });

  xit('sends expected error response in invalid request', async () => {
    await request(app).get(`${ROUTE}/${10001}`).expect(400);
  });
});
