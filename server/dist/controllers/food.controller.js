"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.add = void 0;
const add = (req, res) => {
    console.log(`request body:`, req.body);
    res.status(200).json({
        status: 'success',
    });
};
exports.add = add;
