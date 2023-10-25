"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (reactionId) => {
    //   SET last_modified_on = NOW()
    return `
    UPDATE reaction
    SET deleted_on = now()
    WHERE id = ${reactionId};
  `;
};
