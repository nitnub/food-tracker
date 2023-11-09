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
      id: expect.any(Number),
      // id: 100,
      globalUserId: expect.any(String),
      // globalUserId: 'guid_17dad8bc-ff7b-4f5a-949c-709c5e3e4451',
      email: newRecord.email,
      // email: 'cae035cc-5bb3-49e9-b34d-250a969d60c1@user.com',
      admin: newRecord.admin,
      // admin: true,
      avatar: newRecord.avatar,
      // avatar: 'mysite.com/avatar/cae035cc-5bb3-49e9-b34d-250a969d60c1',
      active: newRecord.active,
      // active: true,
      createdOn: expect.toBeOneOf(validDateResponses),
      // createdOn: '2023-11-02T17:57:12.715Z',
      lastModifiedOn: expect.toBeOneOf(validDateResponses),
      // lastModifiedOn: '2023-11-09T16:21:18.977Z',
      deletedOn: expect.toBeOneOf(validDateResponses),
      // deletedOn: null,
      lastModifiedBy: newRecord.modifiedBy,
      // lastModifiedBy: 'cae035cc-5bb3-49e9-b34d-250a969d60c1@tester.com',
    });
    // expect(finalDbRecord).toEqual({
    //   // ...newRecord,
    //   // lastModifiedBy: newRecord.modifiedBy,
    //   lastModifiedBy: newRecord.modifiedBy,
    //   email: newRecord.email,
    //   admin: newRecord.admin,
    //   avatar: newRecord.avatar,
    //   active: newRecord.active,
    // });
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
