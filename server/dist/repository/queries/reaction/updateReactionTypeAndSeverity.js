"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = (reactionId, type, severity) => {
    return `
    UPDATE reaction
    SET reaction_type = ${type},
    SET severity = ${severity},
    SET last_modified_on = NOW()
    WHERE id = ${reactionId};
  `;
};
