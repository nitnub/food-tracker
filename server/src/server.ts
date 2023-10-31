import 'module-alias/register';

// configure dotenv before module imports
import dotenv from 'dotenv';
dotenv.config({ path: `.env.${process.env.NODE_ENV || 'development'}` });

import app from './app';

const PORT = process.env.PORT;

app.listen(PORT, () => {
  console.log(`Listening on port ${PORT} in ${process.env.NODE_ENV} mode...`);
});
