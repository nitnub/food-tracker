"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (reactionId, severity) => {
    return `
    UPDATE reaction
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
