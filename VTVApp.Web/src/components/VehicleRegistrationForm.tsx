import React, { useState } from "react";
import { TextField, Button, FormControl, styled } from "@mui/material";

interface VehicleRegistrationFormProps {
  onSubmit: (vehicleData: {
    licensePlate: string;
    brand: string;
    model: string;
    year: number;
  }) => void;
  onCancel: () => void;
}

const StyledFormControl = styled(FormControl)(({ theme }) => ({
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  justifyContent: "center",
  padding: theme.spacing(3),
  borderRadius: theme.shape.borderRadius,
  border: `1px solid ${theme.palette.divider}`,
  margin: theme.spacing(2),
  "& .MuiTextField-root": {
    margin: theme.spacing(1),
    width: "100%", // Use the full width of the form control
  },
  "& .MuiButton-root": {
    margin: theme.spacing(1),
    width: "calc(50% - 8px)", // Half the width minus the default spacing
  },
}));

const StyledTextField = styled(TextField)(({ theme }) => ({
  "& label.Mui-focused": {
    color: theme.palette.primary.main,
  },
  "& .MuiInput-underline:after": {
    borderBottomColor: theme.palette.primary.main,
  },
  "& .MuiOutlinedInput-root": {
    "& fieldset": {
      borderColor: theme.palette.divider,
    },
    "&:hover fieldset": {
      borderColor: theme.palette.primary.light,
    },
    "&.Mui-focused fieldset": {
      borderColor: theme.palette.primary.main,
    },
  },
}));

const StyledRegisterButton = styled(Button)(({ theme }) => ({
  fontWeight: 600,
  padding: theme.spacing(1.5),
  boxShadow: "none",
}));

const StyledCancelButton = styled(Button)(({ theme }) => ({
  fontWeight: 600,
  padding: theme.spacing(1.5),
  boxShadow: "none",
  "&:hover": {
    border: `1px solid ${theme.palette.secondary.dark}`,
  },
}));

const VehicleRegistrationForm: React.FC<VehicleRegistrationFormProps> = ({
  onSubmit,
  onCancel,
}) => {
  const [vehicleForm, setVehicleForm] = useState({
    licensePlate: "",
    brand: "",
    model: "",
    year: "",
    color: "",
  });

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setVehicleForm((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleSubmit = () => {
    // Convert year to a number and call the onSubmit prop
    onSubmit({
      ...vehicleForm,
      year: Number(vehicleForm.year),
    });
  };

  return (
    <StyledFormControl fullWidth component="fieldset" margin="normal">
      <StyledTextField
        label="Patente numero"
        name="licensePlate"
        value={vehicleForm.licensePlate}
        onChange={handleInputChange}
        margin="normal"
        fullWidth
      />
      <StyledTextField
        label="Marca"
        name="brand"
        value={vehicleForm.brand}
        onChange={handleInputChange}
        margin="normal"
        fullWidth
      />
      <StyledTextField
        label="Modelo"
        name="model"
        value={vehicleForm.model}
        onChange={handleInputChange}
        margin="normal"
        fullWidth
      />
      <StyledTextField
        label="Color"
        name="color"
        value={vehicleForm.color}
        onChange={handleInputChange}
        margin="normal"
        fullWidth
      />
      <StyledTextField
        label="Año"
        name="year"
        value={vehicleForm.year}
        onChange={handleInputChange}
        margin="normal"
        fullWidth
      />
      <div style={{ display: 'flex', justifyContent: 'center', width: '100%' }}>
        <StyledRegisterButton
          variant="contained"
          color="primary"
          onClick={handleSubmit}
          style={{ marginTop: "1rem" }}
        >
          Registrar
        </StyledRegisterButton>
        <StyledCancelButton
          variant="outlined"
          color="secondary"
          onClick={onCancel}
          style={{ marginTop: "1rem", marginLeft: "1rem" }}
        >
          Cancelar
        </StyledCancelButton>
      </div>
    </StyledFormControl>
  );
};

export default VehicleRegistrationForm;
