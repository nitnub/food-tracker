import FodMapRepository from '../repository/fodmap.repository';

class FodMapService {
  private fodMapRepository: FodMapRepository;

  constructor() {
    this.fodMapRepository = new FodMapRepository();
  }

  getAll = async () => {
    return await this.fodMapRepository.selectAll();
  };
}

export default FodMapService;
