import { AppBar, Toolbar, Typography } from '@mui/material';
import React from 'react';

const NavBar: React.FC = () => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6">VTVApp</Typography>
      </Toolbar>
    </AppBar>
  );
};

export default NavBar;
