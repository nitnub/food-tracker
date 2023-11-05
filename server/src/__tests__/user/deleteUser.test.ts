import request, { Response } from 'supertest';
import app from '@root/app';
// import { validDateResponses } from '../../../.jest/test-utils';
// import {getUniqueUser,  getUniqueUpdateRequest } from '../../../.jest/test-utils';
import { getUniqueUser } from '../../../.jest/test-utils';

let postRes: Response;

beforeEach(async () => {
  postRes = await request(app).post('/api/v1/user').send(getUniqueUser());
});

describe('user delete integration', () => {
  it('sends 200 response on valid call', async () => {
    const newRecord = postRes.body.data[0];
    const userId = newRecord.id;

    // confirm user is in db
    const getRes = await request(app).get(`/api/v1/user/${userId}`);
    expect(getRes.body.status).toEqual('success');
    
    // delete user
    const delRes = await request(app).delete(`/api/v1/user/${userId}`);
    
    // confirm user is not in db
    expect(delRes.body.status).toEqual('success');
    expect(delRes.statusCode).toEqual(200);
  });

  it('has results of proper format', async () => {
    const newRecord = postRes.body.data[0];
    const userId = newRecord.id;

    // confirm user is in db
    const getRes = await request(app).get(`/api/v1/user/${userId}`);
    expect(getRes.body.status).toEqual('success');
    
    // delete user
    const delRes = await request(app).delete(`/api/v1/user/${userId}`);
    
    // confirm user is not in db
    expect(delRes.body).toEqual({ status: 'success' });
  });

  it('deletes a record', async () => {
    const newRecord = postRes.body.data[0];
    const userId = newRecord.id;

    // confirm user is in db
    const getRes = await request(app).get(`/api/v1/user`);
    const startingCount = getRes.body.data.length;
    expect(getRes.body.status).toEqual('success');

    // delete user
    await request(app).delete(`/api/v1/user/${userId}`);

    // confirm row was deleted
    const getResAfter = await request(app).get(`/api/v1/user`);
    const endingCount = getResAfter.body.data.length;
    expect(endingCount).toBe(startingCount - 1);
  });
});
