import request from 'supertest';
import app from '@root/app';
import {
  validDateResponses,
  getUniqueUpdateRequest,
} from '../../../.jest/test-utils';

const userId = 100;
const invalidUserId = 500;
const ROUTE = '/api/v1/user';

const expectedUpdateResponse = {
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

describe('User PATCH integration', () => {
  it('sends 200 response on valid call', async () => {
    await request(app)
      .patch(`${ROUTE}/${userId}`)
      .send(getUniqueUpdateRequest())
      .expect(200);
  });

  it('updates a record', async () => {
    const initialRes = await request(app).get(`${ROUTE}/${userId}`);
    const initialRecord: UserDbEntry = initialRes.body.data[0];

    const newRecord = {
      ...getUniqueUpdateRequest(),
      admin: !initialRecord.admin,
      active: !initialRecord.active,
    };

    await request(app).patch(`${ROUTE}/${userId}`).send(newRecord);
    const finalGetRes = await request(app).get(`${ROUTE}/${userId}`);
    const finalDbRecord: UserDbEntry = finalGetRes.body.data[0];

    // expect(finalDbRecord.lastModifiedBy).toEqual(newRecord.modifiedBy);
    // expect(finalDbRecord.email).toEqual(newRecord.email);
    // expect(finalDbRecord.admin).toEqual(newRecord.admin);
    // expect(finalDbRecord.avatar).toEqual(newRecord.avatar);
    // expect(finalDbRecord.active).toEqual(newRecord.active);

    expect(finalDbRecord).toEqual({
      // ...newRecord,
      // lastModifiedBy: newRecord.modifiedBy,
      lastModifiedBy: newRecord.modifiedBy,
      email: newRecord.email,
      admin: newRecord.admin,
      avatar: newRecord.avatar,
      active: newRecord.active,
    });
    // expect(finalDbRecord).toBe({
    //   lastModifiedBy: newRecord.modifiedBy,
    //   email: newRecord.email,
    //   admin: newRecord.admin,
    //   avatar: newRecord.avatar,
    //   active: newRecord.active,
    // });
  });

  it('has response of proper length', async () => {
    const res = await request(app)
      .patch(`${ROUTE}/${userId}`)
      .send(getUniqueUpdateRequest());

    expect(res.body.data).toHaveLength(1);
  });

  it('has results of proper format', async () => {
    const updateRes = await request(app)
      .patch(`${ROUTE}/${userId}`)
      .send(getUniqueUpdateRequest());

    expect(updateRes.body.data[0]).toEqual(expectedUpdateResponse);
  });
});
