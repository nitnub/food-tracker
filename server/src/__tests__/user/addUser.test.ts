import request from 'supertest';
import app from '@root/app';
import postgresConnect from '@connections/postgres.connection';
import {
  getUniqueAdmin,
  getUniqueUser,
  validDateResponses,
} from '../../../.jest/test-utils';

const ROUTE = '/api/v1/user';

const expectedAddResponse = {
  id: expect.any(Number),
  globalUserId: expect.any(String),
  email: expect.any(String),
  admin: expect.any(Boolean),
  avatar: expect.any(String),
  active: expect.any(Boolean),
  createdOn: expect.toBeOneOf(validDateResponses),
  lastModifiedOn: expect.toBeOneOf(validDateResponses),
  deletedOn: expect.toBeOneOf(validDateResponses),
  lastModifiedBy: expect.toBeOneOf([expect.any(String), null]),
};

afterEach(() => {
  const trimTestDb = 'DELETE FROM app_user WHERE id > 100;';
  postgresConnect.query(trimTestDb);
});

describe('User POST integration', () => {
  describe('single entry', () => {
    it('sends 200 response on valid call', async () => {
      const res = await request(app).post(ROUTE).send([getUniqueUser()]);

      expect(res.statusCode).toEqual(200);
    });

    it('updates the db', async () => {
      const getResponse1 = await request(app).get(ROUTE);
      const initialLength = getResponse1.body.data.length;

      await request(app).post(ROUTE).send([getUniqueUser()]);

      const getResponse2 = await request(app).get(ROUTE);
      const finalLength = getResponse2.body.data.length;

      expect(finalLength).toBe(initialLength + 1);
    });

    it('has results of proper length', async () => {
      const res = await request(app).post(ROUTE).send([getUniqueUser()]);
      expect(res.body.data).toHaveLength(1);
    });

    it('has results of proper format', async () => {
      const res = await request(app).post(ROUTE).send([getUniqueUser()]);
      const testRecord = res.body.data[0];

      expect(testRecord).toEqual(expectedAddResponse);
    });
  });

  describe('multi-entry', () => {
    it('sends 200 response on valid call', async () => {
      const res = await request(app)
        .post('/api/v1/user')
        .send([
          getUniqueUser(),
          getUniqueUser(),
          getUniqueAdmin(),
          getUniqueAdmin(),
        ]);

      expect(res.statusCode).toEqual(200);
    });

    it('updates the db', async () => {
      const getResponse1 = await request(app).get(ROUTE);
      const initialLength = getResponse1.body.data.length;

      const newUsers = [
        getUniqueAdmin(),
        getUniqueUser(),
        getUniqueUser(),
        getUniqueAdmin(),
      ];

      await request(app).post(ROUTE).send(newUsers);

      const getResponse2 = await request(app).get(ROUTE);
      const finalLength = getResponse2.body.data.length;

      expect(finalLength).toBe(initialLength + newUsers.length);
    });

    it('has results of proper length', async () => {
      const getResponse = await request(app).get(ROUTE);
      const initialLength = getResponse.body.data.length;

      const req = [getUniqueUser(), getUniqueAdmin(), getUniqueUser()];
      const postResponse = await request(app).post(ROUTE).send(req);

      expect(postResponse.body.data).toHaveLength(initialLength + req.length);
    });

    it('has results of proper format', async () => {
      const res = await request(app)
        .post(ROUTE)
        .send([getUniqueUser(), getUniqueUser(), getUniqueAdmin()]);
      expect(res.body.data).toBeArray();

      const testRecord = res.body.data[0];
      expect(testRecord).toEqual(expectedAddResponse);
    });
  });
});
