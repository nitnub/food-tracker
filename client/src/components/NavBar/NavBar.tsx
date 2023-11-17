import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import RamenDiningIcon from '@mui/icons-material/RamenDining';
import { Link } from 'react-router-dom';
import { useState } from 'react';
import LoginButton from './LoginButton';
import ProfileButton from './ProfileButton';

interface SettingsRoute {
  name: string;
  route: string;
}

// const pages = ['Products', 'Pricing', 'Blog'];
const pages: string[] = [];

const title = 'Allergy';
const homeRoute = 'dashboard';
const signInRoute = 'signin';

export default function ResponsiveAppBar() {
  const [loggedIn, setLoggedIn] = useState<boolean>(false);
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  return (
    <AppBar
      position="static"
      sx={{
        backgroundColor: 'red',
      }}
    >
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <Link to={homeRoute} aria-label="home button" color="inherit">
            <Button aria-label="home button" color="inherit">
              <RamenDiningIcon
                sx={{
                  display: { xs: 'none', md: 'flex' },
                  mr: 1,
                  color: 'white',
                }}
              />

              <Typography
                variant="h6"
                noWrap
                component="a"
                href={homeRoute}
                sx={{
                  mr: 2,
                  display: { xs: 'none', md: 'flex' },
                  fontFamily: 'monospace',
                  fontWeight: 700,
                  letterSpacing: '.3rem',
                  color: 'white',
                  textDecoration: 'none',
                }}
              >
                {title}
              </Typography>
            </Button>
          </Link>
          <Box
            sx={{
              flexGrow: 1,
              display: { xs: 'flex', md: 'none' },
              color: 'white',
            }}
          >
            {/* </Menu> */}
          </Box>

          <Link to={homeRoute}>
            <Button aria-label="home button" color="inherit">
              <RamenDiningIcon
                sx={{
                  display: { xs: 'flex', md: 'none' },
                  mr: 1,
                  color: 'white',
                }}
              />
              <Typography
                variant="h5"
                noWrap
                component="a"
                href="/"
                sx={{
                  mr: 2,
                  display: { xs: 'flex', md: 'none' },
                  flexGrow: 1,
                  fontFamily: 'monospace',
                  fontWeight: 700,
                  letterSpacing: '.3rem',
                  color: 'white',
                  textDecoration: 'none',
                }}
              >
                {title}
              </Typography>
            </Button>
          </Link>

          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
            {/* {pages.map((page) => (
              <Button
                key={page}
                onClick={handleCloseNavMenu}
                sx={{ my: 2, color: 'red', display: 'block' }}
              >
                {page}
              </Button>
            ))} */}
          </Box>

          {loggedIn ? (
            <ProfileButton />
          ) : (
            <LoginButton signInRoute={signInRoute} />
          )}
        </Toolbar>
      </Container>
    </AppBar>
  );
}
