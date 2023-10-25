import { Client } from 'pg';
import postgresConnect from '@connections/postgres.connection';
import { selectAllAsObj } from './queries/fodmap.queries';

interface JSONResponse {
  id: number;
  category: string;
  name: string;
  freeUse: boolean;
  oligos: boolean;
  fructose: boolean;
  polyols: boolean;
  lactose: boolean;
  color: string;
  aliasPrimary: string;
  aliasList: string;
  maxIntake: string;
}

class FodMapRepository {
  private pool: Client;
  constructor() {
    this.pool = postgresConnect;
    
  }

  selectAll = async () => {
    const formatted: JSONResponse[] = [];
    const resp = await this.runQuery(selectAllAsObj());

    // console.log(resp);
    resp.rows.forEach((jbo) => {
      // console.log(jbo);
      const formattedAlias = [];
      // return {...jbo, aliasList: jbo.aliasList.split("&%&")}
      const aliasList = jbo.json_build_object.aliasList
        ? jbo.json_build_object.aliasList.split('&%&')
        : null;
      // formatted.push({ ...jbo.json_build_object, aliasList: jbo.json_build_object.aliasList.split('&%&') });
      formatted.push({ ...jbo.json_build_object, aliasList });
    });
    // return resp.rows;
    // console.log(formatted);
    return formatted;
  };

  runQuery = async (queryString: string) => {
    return await this.pool.query(queryString);
  };
}

export default FodMapRepository;
