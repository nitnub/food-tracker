"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deleteTest = exports.addTest = exports.getAllMatchingTests = exports.getAllTests = void 0;
const getAllTests = () => {
    return `
    SELECT * FROM Test;
  `;
};
exports.getAllTests = getAllTests;
const getAllMatchingTests = (name) => {
    return `
    SELECT * FROM Test WHERE LOWER(name) LIKE '%${name}%';
  `;
};
exports.getAllMatchingTests = getAllMatchingTests;
const addTest = (TestItem) => {
    const { id, testInt, testBool, testText } = TestItem;
    // return `
    //   INSERT INTO Test(name, fodmap_id, vegetarian, vegan, gluten_free) 
    //   VALUES (
    //     '${name}'
    //     , ${fodmapId}
    //     , ${vegetarian}
    //     , ${vegan}
    //     , ${glutenFree}      
    //   );
    // `;
};
exports.addTest = addTest;
const deleteTest = (TestId) => {
    return `
      DELETE FROM test_table
      WHERE id = ${TestId};
    `;
};
exports.deleteTest = deleteTest;
