import request from 'supertest';
import app from '@root/app';
import { getUniqueFood } from '../../../.jest/test-utils';
import { FoodDBObject } from '../../types/food.types';

const ROUTE = '/api/v1/food';

const expectedFoodObject = {
  id: expect.any(Number),
  name: expect.any(String),
  fodmapId: expect.toBeOneOf([expect.any(Number), null]),
  vegetarian: expect.any(Boolean),
  vegan: expect.any(Boolean),
  glutenFree: expect.any(Boolean),
};

let newFood: FoodDBObject;
const userId = 10;
const expectedResp = {
  status: 'success',
  data: expect.arrayContaining([expectedFoodObject]),
};

beforeEach(() => {
  newFood = getUniqueFood();
});

describe('Food PATCH integration', () => {
  it('sends 200 response on valid call', async () => {
    const res = await request(app)
      .patch(`${ROUTE}/${userId}`)
      .send(getUniqueFood());

    expect(res.statusCode).toEqual(200);
  });

  it('updates a record', async () => {
    await request(app).patch(`${ROUTE}/${userId}`).send(newFood);

    const finalGetRes = await request(app).get(ROUTE);
    const finalDbRecord = finalGetRes.body.data.filter(
      (food: FoodDBObject) => food.id === userId
    )[0];

    expect(finalDbRecord).toEqual({
      ...newFood,
      id: expect.any(Number),
    });
  });

  it('has results of proper format', async () => {
    const updateRes = await request(app)
      .patch(`${ROUTE}/${userId}`)
      .send(newFood);

    expect(updateRes.body).toEqual(expectedResp);
  });

  it.skip('has results of proper format', () => {
    request(app).patch(`${ROUTE}/${23132}`).send(newFood).expect(400);
  });
});
