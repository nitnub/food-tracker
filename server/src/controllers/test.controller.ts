import { Request, Response } from 'express';
import TestService  from '@services/test.service';

class TestController {
  private testService;

  constructor() {
    this.testService = new TestService();
  }
  add = (req: Request, res: Response) => {
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
}
// export const add = (req: Request, res: Response) => {
//   console.log(`request body:`, req.body);

//   // call service to handle update requst

//   this.reactionService.addReaction(req.body)

//   res.status(200).json({
//     status: 'success',
//   });
// };

export default new TestController();
