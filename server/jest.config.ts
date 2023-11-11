import type { Config } from '@jest/types';

const baseDir = '<rootDir>/src';
const baseTestDir = '<rootDir>/src';
const testResources = '<rootDir>/.jest';

const config: Config.InitialOptions = {
  preset: 'ts-jest',
  testEnvironment: 'node',
  verbose: true,
  collectCoverage: true,
  moduleNameMapper: {
    '\\.(css|less|scss|sass|svg)$': 'identity-obj-proxy',
    '^@testRepo(.*)$': `${testResources}/$1`,
    '^@root(.*)$': `${baseDir}/$1`,
    '^@configs(.*)$': `${baseDir}/configs/$1`,
    '^@connections(.*)$': `${baseDir}/connections/$1`,
    '^@controllers(.*)$': `${baseDir}/controllers/$1`,
    '^@libs(.*)$': `${baseDir}/libs/$1`,
    '^@models(.*)$': `${baseDir}/models/$1`,
    '^@routes(.*)$': `${baseDir}/routes/$1`,
    '^@repository(.*)$': `${baseDir}/repository/$1`,
    '^@services(.*)$': `${baseDir}/services/$1`,
    '^@types(.*)$': `${baseDir}/types/$1`,
    '^@utils(.*)$': `${baseDir}/utils/$1`,
  },
  collectCoverageFrom: [`${baseDir}/**/*.ts`],
  testMatch: [`${baseTestDir}/**/*.test.ts`],
  // testPathIgnorePatterns: [`${baseTestDir}/repository/queries/**/*.*`],
  coveragePathIgnorePatterns: [`${baseTestDir}/repository/queries/`],
  setupFiles: [`${testResources}/test-setup.ts`],
  setupFilesAfterEnv: ['jest-extended/all'],
};

export default config;
