"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const test_service_1 = __importDefault(require("@services/test.service"));
class TestController {
    constructor() {
        this.add = (req, res) => {
            console.log(`request body:`, req.body);
            // console.log('Reaction controller!');
            // call service to handle update requst
            const result = this.testService.addTest(req.body);
            const status = result.length > 0 ? 'success' : 'fail';
            res.status(200).json({
                status,
                result
            });
        };
        this.testService = new test_service_1.default();
    }
}
// export const add = (req: Request, res: Response) => {
//   console.log(`request body:`, req.body);
//   // call service to handle update requst
//   this.reactionService.addReaction(req.body)
//   res.status(200).json({
//     status: 'success',
//   });
// };
exports.default = new TestController();
