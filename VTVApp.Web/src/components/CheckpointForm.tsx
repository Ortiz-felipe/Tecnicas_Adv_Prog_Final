import React, { useState } from "react";
import { TextField, Typography, styled } from "@mui/material";
import { InspectionCheckpointDetailDto } from "../types/dtos/Inspections/InspectionCheckpointDetailDto";

interface CheckpointFormProps {
  checkpoint: InspectionCheckpointDetailDto;
  onChange: (score: number, comments: string) => void;
}

const FormContainer = styled('div')({
    display: 'flex',
    flexDirection: 'column',
    marginBottom: '1rem',
  });
  
  const StyledTypography = styled(Typography)({
    fontWeight: 'bold',
    marginBottom: '0.5rem',
  });
  
  const StyledTextField = styled(TextField)(({ theme }) => ({
    margin: theme.spacing(1, 0),
    '& .MuiInputBase-root': {
      backgroundColor: 'white', // Ensures the input stands out from any background
    },
  }));

const CheckpointForm: React.FC<CheckpointFormProps> = ({
  checkpoint,
  onChange,
}) => {
  const { checkpointName, score, comments } = checkpoint;

  const handleScoreChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newScore = parseInt(event.target.value, 10);
    onChange(newScore, comments);
  };

  const handleCommentsChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newComments = event.target.value;
    onChange(score, newComments);
  };

  return (
    <FormContainer>
      <StyledTypography>{checkpoint.checkpointName}</StyledTypography>
      <StyledTextField
        label="Puntuación"
        type="number"
        InputProps={{ inputProps: { min: 0, max: 10 } }}
        value={checkpoint.score}
        onChange={(e) => onChange(parseInt(e.target.value, 10), checkpoint.comments)}
        margin="normal"
      />
      <StyledTextField
        label="Comentarios"
        multiline
        rows={4}
        value={checkpoint.comments}
        onChange={(e) => onChange(checkpoint.score, e.target.value)}
        inputProps={{ maxLength: 500 }}
        margin="normal"
        fullWidth
      />
    </FormContainer>
  );
};

export default CheckpointForm;
