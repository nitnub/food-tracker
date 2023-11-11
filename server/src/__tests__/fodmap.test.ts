import request from 'supertest';
import app from '@root/app';

const ROUTE = '/api/v1/fodmap';

describe('FODMAP GET integration', () => {
  it('sends 200 response on valid call', async () => {
    const res = await request(app).get(ROUTE);
    expect(res.statusCode).toEqual(200);
  });

  it('results are of proper length', async () => {
    const res = await request(app).get(ROUTE);
    const testRecords = res.body.data;

    expect(testRecords).toHaveLength(432);
  });

  it('results are of proper format', async () => {
    const res = await request(app).get(ROUTE);
    const testRecord = res.body.data[0];

    expect(testRecord).toEqual({
      id: expect.any(Number),
      category: expect.any(String),
      name: expect.any(String),
      freeUse: expect.any(Boolean),
      oligos: expect.any(Boolean),
      fructose: expect.any(Boolean),
      polyols: expect.any(Boolean),
      lactose: expect.any(Boolean),
      color: expect.any(String),
      aliasPrimary: expect.any(String),
      aliasList: expect.arrayContaining([expect.any(String)]),
      maxIntake: expect.toBeOneOf([expect.any(String), null]),
    });
  });
});
