import Pool from 'pg-pool';
import config from '@configs/postgres.config'


export default new Pool(config)