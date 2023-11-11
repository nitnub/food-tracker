import request from 'supertest';
import app from '@root/app';
import { getUniqueFood } from '../../../.jest/test-utils';
import postgresConnect from '@connections/postgres.connection';

const ROUTE = '/api/v1/food';

let foodId: number;
let initiailCount: number;

beforeEach(async () => {
  const postRes = await request(app)
    .post(ROUTE)
    .send({ data: [getUniqueFood()] });

  const { data } = postRes.body;

  initiailCount = data.length;
  foodId = data[initiailCount - 1].id;
});

afterEach(() => {
  const trimTestDb = 'DELETE FROM food WHERE id > 100;';
  postgresConnect.query(trimTestDb);
});

describe('Food DELETE integration', () => {
  it('sends 200 response on valid call', async () => {
    request(app).delete(`${ROUTE}/${foodId}`).expect(200);
  });

  it('has results of proper format', async () => {
    const delRes = await request(app).delete(`${ROUTE}/${foodId}`);
    expect(delRes.body).toEqual({ status: 'success' });
  });

  it('deletes a record', async () => {
    await request(app).delete(`${ROUTE}/${foodId}`);

    const res = await request(app).get(ROUTE);
    const endingCount = res.body.data.length;
    expect(endingCount).toBe(initiailCount - 1);
  });
});
