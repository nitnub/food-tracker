import request from 'supertest';
import app from '@root/app';

const ENDPOINT = '/api/v1/reaction';

const reactionTypes = expect.arrayContaining([
  expect.objectContaining({ id: expect.any(Number), name: expect.any(String) }),
]);

const categories = expect.arrayContaining([
  expect.objectContaining({
    id: expect.any(Number),
    name: expect.any(String),
    reactionTypes,
  }),
]);

const severities = expect.arrayContaining([
  expect.objectContaining({
    id: expect.any(Number),
    name: expect.any(String),
  }),
]);

const data = expect.objectContaining({
  severities,
  categories,
});

const expectedResponse = {
  status: expect.toBeOneOf(['success', 'fail']),
  data: data,
};

describe('Reaction GET integration', () => {
  it('sends 200 response on valid call', async () => {
    await request(app).get(ENDPOINT).expect(200);
  });

  it('has results of proper format', async () => {
    const res = await request(app).get(ENDPOINT);
    expect(res.body).toEqual(expectedResponse);
  });
});
