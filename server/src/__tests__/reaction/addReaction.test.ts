import request from 'supertest';
import app from '@root/app';
import postgresConnect from '@connections/postgres.connection';
import {
  getUniqueAdmin,
  getUniqueUser,
  validDateResponses,
} from '../../../.jest/test-utils';

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
  xit('sends 200 response on valid call (single-entry)', async () => {
    const res = await request(app).post('/api/v1/user').send([getUniqueUser()]);

    expect(res.statusCode).toEqual(200);
  });

  xit('sends 200 response on valid call (multi-entry)', async () => {
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

  xit('updates the db (single-entry)', async () => {
    const getResponse1 = await request(app).get('/api/v1/user');
    const initialLength = getResponse1.body.data.length;

    await request(app).post('/api/v1/user').send([getUniqueUser()]);

    const getResponse2 = await request(app).get('/api/v1/user');
    const finalLength = getResponse2.body.data.length;

    expect(finalLength).toBe(initialLength + 1);
  });

  xit('updates the db (multi-entry)', async () => {
    const getResponse1 = await request(app).get('/api/v1/user');
    const initialLength = getResponse1.body.data.length;

    const newUsers = [
      getUniqueAdmin(),
      getUniqueUser(),
      getUniqueUser(),
      getUniqueAdmin(),
    ];

    await request(app).post('/api/v1/user').send(newUsers);

    const getResponse2 = await request(app).get('/api/v1/user');
    const finalLength = getResponse2.body.data.length;

    expect(finalLength).toBe(initialLength + newUsers.length);
  });

  xit('has results of proper length (single-entry)', async () => {
    const res = await request(app).post('/api/v1/user').send([getUniqueUser()]);
    expect(res.body.data).toHaveLength(1);
  });

  xit('has results of proper length (multi-entry)', async () => {
    const getResponse = await request(app).get('/api/v1/user');
    const initialLength = getResponse.body.data.length;

    const req = [getUniqueUser(), getUniqueAdmin(), getUniqueUser()];
    const postResponse = await request(app).post('/api/v1/user').send(req);

    expect(postResponse.body.data).toHaveLength(initialLength + req.length);
  });

  xit('has results of proper format (single-entry)', async () => {
    const res = await request(app).post('/api/v1/user').send([getUniqueUser()]);
    const testRecord = res.body.data[0];

    expect(testRecord).toEqual(expectedAddResponse);
  });

  xit('has results of proper format (multi-entry)', async () => {
    const res = await request(app)
      .post('/api/v1/user')
      .send([getUniqueUser(), getUniqueUser(), getUniqueAdmin()])
    expect(res.body.data).toBeArray();

    const testRecord = res.body.data[0];
    expect(testRecord).toEqual(expectedAddResponse);
  });
});
