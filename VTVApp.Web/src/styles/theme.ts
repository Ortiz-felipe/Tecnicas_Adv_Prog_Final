import { createTheme } from '@mui/material/styles';

// Create a minimalistic theme instance.
const theme = createTheme({
  palette: {
    primary: {
      light: '#63a4fff',
      main: '#1976d2',
      dark: '#004ba0',
      contrastText: '#ecfad8',
    },
    secondary: {
      light: '#ffddc1',
      main: '#ffab91',
      dark: '#c97b63',
      contrastText: '#000',
    },
    background: {
      default: '#fff',
      paper: '#fafafa',
    },
    text: {
      primary: '#212121',
      secondary: '#757575',
    },
  },
  typography: {
    fontFamily: [
      '-apple-system',
      'BlinkMacSystemFont',
      '"Segoe UI"',
      'Roboto',
      '"Helvetica Neue"',
      'Arial',
      'sans-serif',
      '"Apple Color Emoji"',
      '"Segoe UI Emoji"',
      '"Segoe UI Symbol"',
    ].join(','),
    h1: {
      fontSize: '2.5rem',
      fontWeight: 300,
    },
    h2: {
      fontSize: '2rem',
      fontWeight: 400,
    },
    h3: {
      fontSize: '1.75rem',
      fontWeight: 400,
    },
    // and so on for h4, h5, h6
    body1: {
      fontSize: '1rem',
    },
    button: {
      textTransform: 'none',
      fontWeight: 400,
    },
  },
  components: {
    // Define custom styles for MUI components here, if needed.
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: '4px', // Rounded corners for buttons
        },
      },
    },
    // Continue with other component customizations as needed.
  },
});

export default theme;
