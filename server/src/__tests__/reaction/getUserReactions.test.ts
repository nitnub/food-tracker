import request from 'supertest';
import app from '@root/app';
import { validDateResponses } from '../../../.jest/test-utils';
// import { validDateResponses } from '@testRepo/test-utils';

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

describe('User GET integration', () => {
  describe('by id', () => {
    const userId = 34;
    xit('sends 200 response on valid call', async () => {
      const res = await request(app).get(`/api/v1/user/${userId}`);
      expect(res.statusCode).toEqual(200);
    });

    xit('has results of proper length', async () => {
      const res = await request(app).get(`/api/v1/user/${userId}`);
      const testRecords = res.body.data;

      expect(testRecords).toHaveLength(1);
    });

    xit('has results of proper format', async () => {
      const res = await request(app).get(`/api/v1/user/${userId}`);
      const testRecord = res.body.data[0];

      expect(testRecord).toEqual(expectedGetResponse);
    });

    it.skip('sends expected error response in invalid request', async () => {
      await request(app).get(`/api/v1/user/${10001}`).expect(400);
    });
  });

  describe('all users', () => {
    xit('sends 200 response on valid call', async () => {
      const res = await request(app).get('/api/v1/user');
      expect(res.statusCode).toEqual(200);
    });

    xit('has results of proper length', async () => {
      const res = await request(app).get('/api/v1/user');
      const testRecords = res.body.data;

      expect(testRecords).toHaveLength(testDBSize);
    });

    xit('has results of proper format', async () => {
      const res = await request(app).get('/api/v1/user');
      const testRecord = res.body.data[0];

      expect(testRecord).toEqual(expectedGetResponse);
    });
  });
});
