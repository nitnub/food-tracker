"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (reactionId, active) => {
    return `
    UPDATE reaction
    SET active = ${active},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
