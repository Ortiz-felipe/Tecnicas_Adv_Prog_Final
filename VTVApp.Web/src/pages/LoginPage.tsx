import React, { useEffect, useState } from "react";
import {
  Grid,
  Paper,
  Typography,
  TextField,
  Alert,
  Autocomplete,
  Stack,
  Button,
} from "@mui/material";
import LoadingButton from "@mui/lab/LoadingButton";
import styled from "@emotion/styled";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import {
  setAuthenticationError,
  setToken,
  setUser,
} from "../features/user/userSlice";
import { loginUser, registerUser } from "../api/userAPI";
import { useAppSelector } from "../app/hooks";
import {
  provincesFetchFailed,
  provincesReceived,
  selectProvinces,
  startLoading,
} from "../features/provinces/provinceSlice";
import { ProvinceDto } from "../types/dtos/Provinces/ProvinceDto";
import getProvinces from "../api/provinceAPI";
import { autocompleteFormatter } from "../helpers/autoCompleteFormatter";
import {
  citiesFetchFailed,
  citiesReceived,
  selectCities,
  startCityLoading,
} from "../features/cities/citySlice";
import { getCitiesByProvince } from "../api/cityAPI";
import { CityDetailsDto } from "../types/dtos/Cities/CityDetailsDto";
import { UserRole } from "../types/dtos/Users/UserRole";

const LoginPageContainer = styled(Grid)(({ theme }) => ({
  height: "100vh",
  padding: theme.spacing(3),
}));

const ImageColumn = styled(Grid)(({ theme }) => ({
  backgroundImage: "url(src/assets/images/login.png)",
  backgroundSize: "cover",
  backgroundPosition: "center",
}));

const FormColumn = styled(Grid)(({ theme }) => ({
  display: "flex",
  flexDirection: "column",
  justifyContent: "center",
  alignItems: "center",
  padding: theme.spacing(4),
}));

const StyledPaper = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(4), // Assuming theme.spacing is set with 'rem' units
  margin: `${theme.spacing(2)} auto`, // Centering the paper component
  width: "100%", // Use 100% width for mobile-first approach
  maxWidth: "37.5rem", // Adjust maximum width as needed, equivalent to 600px if 1rem = 16px
  display: "flex",
  flexDirection: "column",
  gap: theme.spacing(2),
}));

const StyledStack = styled(Stack)(({ theme }) => ({
  width: "100%", // Use 100% width for smaller screens
  maxWidth: "37.5rem", // Adjust maximum width as needed, equivalent to 600px if 1rem = 16px
  gap: theme.spacing(2),
  // Any other styles you want to apply to the Stack
}));

enum ActionState {
  None,
  LoggingIn,
  Registering,
}

const LoginPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const provinces = useAppSelector(selectProvinces);
  const cities = useAppSelector(selectCities);
  const [selectedProvince, setSelectedProvince] = useState<string | null>(null);
  const [selectedCity, setSelectedCity] = useState<string | null>(null);
  const [filteredProvinces, setFilteredProvinces] = useState<ProvinceDto[]>([]);
  const [filteredCities, setFilteredCities] = useState<CityDetailsDto[]>([]);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [fullName, setFullName] = useState("");
  const [actionState, setActionState] = useState<ActionState>(ActionState.None);
  const [alert, setAlert] = useState<{
    type: "success" | "error" | null;
    message?: string;
  }>({
    type: null,
  });
  const [isRegistering, setIsRegistering] = useState(false);
  const [registerLoading, setRegisterLoading] = useState(false);

  useEffect(() => {
    const fetchProvinces = async () => {
      dispatch(startLoading());
      try {
        const provinceData = await getProvinces();
        dispatch(provincesReceived(provinceData));
      } catch (error: any) {
        dispatch(provincesFetchFailed(error.message));
      }
    };
    if (!isRegistering && provinces.length === 0) {
      fetchProvinces();
    }
  }, [isRegistering, provinces]);

  useEffect(() => {
    const fetchCities = async () => {
      dispatch(startCityLoading());
      try {
        const citiesData = await getCitiesByProvince(selectedProvince?.id);
        dispatch(citiesReceived(citiesData));
      } catch (error: any) {
        dispatch(citiesFetchFailed(error.message));
      }
    };
    if (selectedProvince?.id) {
      fetchCities();
    }
  }, [selectedProvince]);

  useEffect(() => {
    let timerId;
  
    if (alert.type) {
      timerId = setTimeout(() => {
        setAlert({ type: null, message: "" });
      }, 15000); // 15 seconds
    }
  
    // Clear the timeout if the alert is closed manually or the component is unmounted
    return () => clearTimeout(timerId);
  }, [alert, setAlert]);

  const isRegisterButtonDisabled =
    !fullName ||
    !email ||
    !password ||
    !selectedProvince ||
    !selectedCity ||
    !phoneNumber;

  // Handlers for the dropdowns
  const handleProvinceChange = (event: any, newValue: string | null) => {
    setSelectedProvince(newValue);
    if (!newValue) {
      setFilteredProvinces(autocompleteFormatter(provinces)); // Reset to the full list when input is cleared
      setSelectedCity(null); // Clear selected city when province is cleared
      setFilteredCities([]); // Clear city options
    }
  };

  const handleProvinceInputChange = (event: any, newInputValue: string) => {
    setFilteredProvinces(() => {
      const filteredProvinces = provinces.filter((province) =>
        province.name.toLowerCase().includes(newInputValue.toLowerCase())
      );
      return autocompleteFormatter(filteredProvinces);
    });
  };

  const handleCityChange = (event: any, newValue: string | null) => {
    setSelectedCity(newValue);
    if (!newValue) {
      setFilteredCities(autocompleteFormatter(cities)); // Reset to the full list when input is cleared
    }
  };

  const handleCityInputChange = (event: any, newInputValue: string) => {
    setFilteredCities(() => {
      const filteredCities = cities.filter((city) =>
        city.name.toLowerCase().includes(newInputValue.toLowerCase())
      );
      return autocompleteFormatter(filteredCities);
    });
  };

  const handleLogin = async () => {
    setActionState(ActionState.LoggingIn);
    try {
      const userData = await loginUser({ email, password });
      if (userData.isAuthenticated) {
        dispatch(setUser(userData.user));
        dispatch(setToken(userData.token));
        if (userData.user.userRole == UserRole.Inspector) {
          navigate("/inspectorAppointments"); // Redirect to home page after successful login  
        } else {
          navigate("/home")
        }
      } else {
        dispatch(setAuthenticationError(userData.errorMessage));
        setAlert({
          type: "error",
          message:
            "Sus credenciales no son correctas, por favor intente nuevamente.",
        });
      }
    } catch (error) {
      dispatch(setAuthenticationError(error.message));
      setAlert({
        type: "error",
        message:
          "Ocurrió un error al iniciar sesión. Por favor, inténtelo de nuevo más tarde.",
      });
    }
    setActionState(ActionState.None);
  };

  const handleRegister = async () => {
    setActionState(ActionState.Registering);
    if (isRegisterButtonDisabled) {
      return; // Don't proceed if required fields are missing
    }
    try {
      setRegisterLoading(true); // Start the loading indicator
      setIsRegistering(true);
      // Assuming selectedProvince and selectedCity hold the entire object, not just the name
      const provinceId = selectedProvince ? selectedProvince.id : 0; // Replace with appropriate default/fallback value
      const cityId = selectedCity ? selectedCity.id : "";

      const registrationData = {
        name: fullName, // Assuming the name is derived from the email, adjust as necessary
        email,
        password,
        cityId,
        provinceId,
        phoneNumber,
      };

      const result = await registerUser(registrationData);

      // If the registration is successful, show a success message and/or redirect the user
      if (result.success) {
        setAlert({
          type: "success",
          message: "Registro exitoso. Ahora puede ingresar al portal.",
        });
        // Optionally, automatically log the user in or redirect to the login page
        // navigate("/login");
      } else {
        // If there is an error message from the result, display it
        setAlert({
          type: "error",
          message:
            result.message ||
            "No se pudo completar el registro. Por favor, intente nuevamente.",
        });
      }
    } catch (error) {
      // Handle any errors that occur during the API call
      setAlert({
        type: "error",
        message:
          error.message ||
          "Ocurrió un error durante el registro. Por favor, inténtelo de nuevo más tarde.",
      });
    }
    setRegisterLoading(false); // End the loading indicator
    setIsRegistering(false);
    setActionState(ActionState.None);
  };

  const toggleFormState = async () => {
    setIsRegistering(!isRegistering);
  };

  const isLoadingLogin = actionState === ActionState.LoggingIn;
  const isLoadingRegister = actionState === ActionState.Registering;

  return (
    <LoginPageContainer container>
      <ImageColumn item xs={false} sm={4} md={7} />
      <FormColumn item xs={12} sm={8} md={5} component={StyledPaper}>
        <Typography variant="h5" gutterBottom>
          {isRegistering ? "Registrarse en el portal" : "Ingresar al portal"}
        </Typography>
        {isRegistering ? (
          // Registration form
          <StyledStack>
            <TextField
              label="Correo electrónico"
              variant="outlined"
              fullWidth
              margin="normal"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <TextField
              label="Contraseña"
              type="password"
              variant="outlined"
              fullWidth
              margin="normal"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <TextField
              label="Nombre completo"
              variant="outlined"
              fullWidth
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
            />
            <Autocomplete
              options={
                filteredProvinces.length > 0
                  ? filteredProvinces
                  : autocompleteFormatter(provinces)
              }
              getOptionLabel={(option) => option.label}
              value={selectedProvince}
              onChange={handleProvinceChange}
              onInputChange={handleProvinceInputChange}
              renderInput={(params) => (
                <TextField {...params} label="Provincia" />
              )}
              freeSolo
            />
            <Autocomplete
              options={
                filteredCities.length > 0
                  ? filteredCities
                  : autocompleteFormatter(cities)
              }
              renderInput={(params) => <TextField {...params} label="Ciudad" />}
              value={selectedCity}
              getOptionLabel={(option) => (option ? option.label : "")}
              onChange={handleCityChange}
              onInputChange={handleCityInputChange}
              disabled={!selectedProvince}
              freeSolo
            />
            <TextField
              label="Número de teléfono"
              variant="outlined"
              fullWidth
              value={phoneNumber}
              onChange={(e) => setPhoneNumber(e.target.value)}
            />
            <LoadingButton
              loading={registerLoading}
              variant="contained"
              fullWidth
              onClick={handleRegister}
              disabled={isRegisterButtonDisabled || registerLoading}
            >
              Registrarse
            </LoadingButton>
            <Button variant="text" fullWidth onClick={toggleFormState}>
              ¿Ya tienes cuenta? Ingresar
            </Button>
          </StyledStack>
        ) : (
          <StyledStack>
            <TextField
              label="Correo electrónico"
              variant="outlined"
              fullWidth
              margin="normal"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <TextField
              label="Contraseña"
              type="password"
              variant="outlined"
              fullWidth
              margin="normal"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <Grid container spacing={2} marginTop="1rem">
              <Grid item xs={12} sm={6}>
                <LoadingButton
                  loading={isLoadingLogin}
                  variant="contained"
                  fullWidth
                  onClick={handleLogin}
                  disabled={isLoadingRegister}
                >
                  Ingresar
                </LoadingButton>
              </Grid>
              <Grid item xs={12} sm={6}>
                <Button variant="text" fullWidth onClick={toggleFormState}>
                  ¿No tienes cuenta? Registrarse
                </Button>
              </Grid>
            </Grid>
          </StyledStack>
        )}
        {alert.type && (
          <Alert severity={alert.type} style={{ marginTop: "1rem" }}>
            {alert.message}
          </Alert>
        )}
      </FormColumn>
    </LoginPageContainer>
  );
};

export default LoginPage;
