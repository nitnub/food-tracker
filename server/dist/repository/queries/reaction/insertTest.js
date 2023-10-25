"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (porkInt, porkBool, porkText) => {
    return `
    INSERT INTO test_table(pork_int, pork_bool, pork_text) 
    VALUES (
      ${porkInt}
      , ${porkBool}
      , '${porkText}'
    )
  `;
};
