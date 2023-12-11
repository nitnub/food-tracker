import 'module-alias/register';
import dotenv from 'dotenv';
dotenv.config({ path: `.env.${process.env.NODE_ENV || 'development'}` }); // configure dotenv before module imports!

import app from './app';

const PORT = process.env.PORT;

app.listen(PORT, () => {
  console.log(`Listening on port ${PORT} in ${process.env.NODE_ENV} mode...`);
});
