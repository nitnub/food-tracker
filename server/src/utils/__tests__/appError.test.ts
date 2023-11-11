import AppError from '../appError';

describe('appError test suite', () => {
  it('responds with correct code on fail', () => {
    const sut = new AppError('TEST FAIL', 400);

    expect(sut.message).toBe('TEST FAIL');
    expect(sut.status).toBe('fail');
  });

  it('responds with correct code on internal error', () => {
    const sut = new AppError('TEST ERROR', 500);

    expect(sut.message).toBe('TEST ERROR');
    expect(sut.status).toBe('error');
  });
});
