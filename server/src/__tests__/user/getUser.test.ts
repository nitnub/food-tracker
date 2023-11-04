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

describe('getUser endpoint integration', () => {
  const userId = 34;
  it('sends 200 response on valid call', async () => {
    const res = await request(app).get(`/api/v1/user/${userId}`);
    expect(res.statusCode).toEqual(200);
  });

  it('has results of proper length', async () => {
    const res = await request(app).get(`/api/v1/user/${userId}`);
    const testRecords = res.body.data;

    expect(testRecords).toHaveLength(1);
  });

  it('has results of proper format', async () => {
    const res = await request(app).get(`/api/v1/user/${userId}`);
    const testRecord = res.body.data[0];

    expect(testRecord).toEqual(expectedGetResponse);
  });

  it.skip('sends expected error response in invalid request', async () => {
    await request(app)
    .get(`/api/v1/user/${10001}`)
    .expect(400);
  });
});
