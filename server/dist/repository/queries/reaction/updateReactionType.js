"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (reactionId, type) => {
    return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
