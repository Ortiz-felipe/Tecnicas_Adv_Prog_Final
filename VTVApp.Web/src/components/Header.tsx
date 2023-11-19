import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import React from 'react';
import { Link } from 'react-router-dom';
import styled from '@emotion/styled';

// Styled components for a minimalistic look
const StyledAppBar = styled(AppBar)({
  backgroundColor: 'white',
  boxShadow: 'none',
  color: 'black',
});

const StyledToolbar = styled(Toolbar)({
  justifyContent: 'flex-end',
});

const StyledLink = styled(Link)({
  textDecoration: 'none',
  marginLeft: '20px',
});

const HeaderTitle = styled(Typography)({
  flexGrow: 1,
  color: 'black',
  fontWeight: 'bold',
});

const Header: React.FC = () => {
  return (
    <StyledAppBar position="static">
      <StyledToolbar>
        <HeaderTitle variant="h6">
          VTVApp
        </HeaderTitle>
        <Button color="inherit" component={StyledLink} to="/">
          Inicio
        </Button>
        <Button color="inherit" component={StyledLink} to="/">
          ¿Quienes somos?
        </Button>
        <Button color="inherit" component={StyledLink} to="/login">
          Iniciar sesion
        </Button>
      </StyledToolbar>
    </StyledAppBar>
  );
};

export default Header;
