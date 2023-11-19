import { Box, Typography } from '@mui/material';
import React from 'react';

const Footer: React.FC = () => {
  return (
    <Box component="footer" sx={{ textAlign: 'center', py: 2 }}>
      <Typography variant="body2">
        © {new Date().getFullYear()} VTVApp. Reservados todos los derechos.
      </Typography>
    </Box>
  );
};

export default Footer;
