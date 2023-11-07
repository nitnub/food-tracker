import request from 'supertest';
import app from '@root/app';
import postgresConnect from '@connections/postgres.connection';
import { v4 as uuidv4 } from 'uuid';

const FOOD_ENDPOINT = '/api/v1/food';

const expectedFoodObject = {
  id: expect.any(Number),
  name: expect.any(String),
  fodmapId: expect.any(Number),
  vegetarian: expect.any(Boolean),
  vegan: expect.any(Boolean),
  glutenFree: expect.any(Boolean),
};

const dataExpected = expect.arrayContaining([expectedFoodObject]);

afterEach(() => {
  const trimTestDb = 'DELETE FROM food WHERE id > 100;';
  postgresConnect.query(trimTestDb);
});

const foodItemT1 = {
  name: `food_name_t1${uuidv4()}`,
  fodmapId: null,
  vegetarian: true,
  vegan: true,
  glutenFree: true,
};

const foodItemT2 = {
  name: `food_name_t2_${uuidv4()}`,
  fodmapId: 100,
  vegetarian: false,
  vegan: false,
  glutenFree: false,
};

const foodItemExisting = {
  name: 'Test Pizza',
  fodmapId: null,
  vegetarian: true,
  vegan: true,
  glutenFree: false,
};

const REQ_S1 = { data: [foodItemT1] };
const REQ_EXISTING = { data: [foodItemExisting] };
const REQ_M1 = { data: [foodItemT1, foodItemT2] };
const REQ_M1_LENGTH = REQ_M1.data.length;

describe('Food POST integration', () => {
  it('sends 200 response on valid call (single-entry)', async () => {
    const res = await request(app).post(FOOD_ENDPOINT).send(REQ_S1);
    expect(res.statusCode).toEqual(200);
  });

  it('sends 200 response on valid call (multi-entry)', async () => {
    const res = await request(app).post(FOOD_ENDPOINT).send(REQ_M1);
    expect(res.statusCode).toEqual(200);
  });

  it('updates the db (single-entry)', async () => {
    const getResponse1 = await request(app).get(FOOD_ENDPOINT);
    const initialLength = getResponse1.body.data.length;

    await request(app).post(FOOD_ENDPOINT).send(REQ_S1);

    const getResponse2 = await request(app).get(FOOD_ENDPOINT);
    const finalLength = getResponse2.body.data.length;

    expect(finalLength).toBe(initialLength + 1);
  });

  it('updates the db (multi-entry)', async () => {
    const getResponse1 = await request(app).get(FOOD_ENDPOINT);
    const initialLength = getResponse1.body.data.length;

    await request(app).post(FOOD_ENDPOINT).send(REQ_M1);

    const getResponse2 = await request(app).get(FOOD_ENDPOINT);
    const finalLength = getResponse2.body.data.length;

    expect(finalLength).toBe(initialLength + REQ_M1_LENGTH);
  });

  it('has results of proper length', async () => {
    const res = await request(app).post(FOOD_ENDPOINT).send(REQ_S1);
    expect(res.body.data).toHaveLength(30);
  });

  it('has results of proper format (new food)', async () => {
    const res = await request(app).post(FOOD_ENDPOINT).send(REQ_S1);
    expect(res.body).toEqual({ status: 'success', data: dataExpected });
  });

  it('has results of proper format (existing food)', async () => {
    const res = await request(app).post(FOOD_ENDPOINT).send(REQ_EXISTING);
    expect(res.body).toEqual({ status: 'fail' });
  });
});
