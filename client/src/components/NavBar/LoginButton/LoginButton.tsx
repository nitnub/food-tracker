import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import { Link } from 'react-router-dom';

interface Props {
  signInRoute: string;
}

export default function LoginButton({ signInRoute }: Props) {
  return (
    <Box sx={{ flexGrow: 0 }}>
      <Tooltip title="Open guest options">
        <Link to={signInRoute}>
          <Button color="inherit">Login</Button>
        </Link>
      </Tooltip>
    </Box>
  );
}
