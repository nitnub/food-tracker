import request from 'supertest';
import app from '@root/app';

const ROUTE = '/api/v1/food';

const expectedFoodObject = {
  id: expect.any(Number),
  name: expect.any(String),
  fodmapId: expect.toBeOneOf([expect.any(Number), null]),
  vegetarian: expect.any(Boolean),
  vegan: expect.any(Boolean),
  glutenFree: expect.any(Boolean),
};

const expectedResp = {
  status: 'success',
  data: expect.arrayContaining([expectedFoodObject]),
};

const testDBSize = 29;

describe('Food GET integration', () => {
  describe('GET all foods users', () => {
    it('sends 200 response on valid call', async () => {
      const res = await request(app).get(ROUTE);
      expect(res.statusCode).toEqual(200);
    });

    it('has results of proper length', async () => {
      const res = await request(app).get(ROUTE);
      const testRecords = res.body.data;

      expect(testRecords).toHaveLength(testDBSize);
    });

    it('has results of proper format', async () => {
      const res = await request(app).get(ROUTE);
      expect(res.body).toEqual(expectedResp);
    });
  });
});
