import React from 'react';
import { Typography, Button, Box, Grid, Paper } from '@mui/material';
import Carousel from '../components/Carousel'; // Import or create a Carousel component
import Header from '../components/Header';
import Footer from '../components/layout/Footer';

const LandingPage: React.FC = () => {
    return (
      <>
        <Header />
        <Carousel />  
        <Box p={4} textAlign="center">
          <Typography variant="h4" gutterBottom>
            Inspecciones vehiculares confiables
          </Typography>
          <Typography>
            Ofrecemos un servicio completo de inspección para asegurarnos de que su vehículo esté en las mejores condiciones.
          </Typography>
        </Box>
  
        <Grid container spacing={4} justifyContent="center" alignItems="center">
          <Grid item xs={12} sm={6} md={4}>
            <Paper elevation={3} sx={{ padding: 2 }}>
              <Typography variant="h5">Seguridad Primero</Typography>
              <Typography>
                La seguridad de nuestros clientes es nuestra máxima prioridad. Nos aseguramos de que su vehículo sea seguro para la carretera.
              </Typography>
            </Paper>
          </Grid>
          <Grid item xs={12} sm={6} md={4}>
            <Paper elevation={3} sx={{ padding: 2 }}>
              <Typography variant="h5">Precisión en el Diagnóstico</Typography>
              <Typography>
                Utilizamos tecnología de punta para proporcionar diagnósticos precisos y detallados.
              </Typography>
            </Paper>
          </Grid>
          <Grid item xs={12} sm={6} md={4}>
            <Paper elevation={3} sx={{ padding: 2 }}>
              <Typography variant="h5">Servicio al Cliente</Typography>
              <Typography>
                Nuestro equipo de atención al cliente está siempre disponible para responder a sus preguntas y guiarlo a través del proceso de inspección.
              </Typography>
            </Paper>
          </Grid>
        </Grid>
  
        <Box p={4} textAlign="center">
          <Typography variant="h5" gutterBottom>
            ¿Qué dicen nuestros clientes?
          </Typography>
          <Typography fontStyle="italic">
            "El proceso de inspección fue rápido y exhaustivo. Me siento mucho más seguro al conducir mi coche ahora. ¡Gracias!"
          </Typography>
          <Typography fontStyle="italic">
            "El personal fue amable y profesional. Respondieron todas mis preguntas y me ayudaron a entender todo el informe de inspección."
          </Typography>
        </Box>
  
        <Box p={4} textAlign="center" bgcolor="primary.main" color="primary.contrastText">
          <Typography variant="h5" gutterBottom>
            Listo para la inspección de su vehículo?
          </Typography>
          <Button variant="contained" color="secondary">
            Reserve su cita ahora
          </Button>
        </Box>
  
        <Footer />
      </>
    );
  };

export default LandingPage;
