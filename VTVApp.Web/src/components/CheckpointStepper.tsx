import React, { useState } from "react";
import {
  Stepper,
  Step,
  StepLabel,
  Button,
  Typography,
  styled,
  Paper,
} from "@mui/material";

import CheckpointForm from "./CheckpointForm";
import { INSPECTION_CHECKPOINTS } from "../types/constants/InspectionCheckpoints";
import { UpdateInspectionDto } from "../types/dtos/Inspections/UpdateInspectionDto";
import { InspectionCheckpointDetailDto } from "../types/dtos/Inspections/InspectionCheckpointDetailDto";
import { CheckpointListDto } from "../types/dtos/Checkpoints/CheckpointListDto";

// Styling for the Stepper container
const StepperContainer = styled(Paper)(({ theme }) => ({
  padding: theme.spacing(2),
  backgroundColor: "#fafafa",
  boxShadow: "none",
  marginBottom: theme.spacing(4),
}));

// Styling for the Step button, particularly for when it's active or completed
const StepButton = styled(Button)(({ theme, active, completed }) => ({
  fontWeight: active || completed ? "bold" : "normal",
  margin: theme.spacing(1),
}));

// Custom Stepper component with minimalistic styles
const CustomStepper = styled(Stepper)({
  padding: "24px 0",
  backgroundColor: "transparent", // Make the stepper transparent
  ".MuiStepIcon-root.Mui-active": {
    // Active step icon
    color: "#1976d2", // Use your primary color here
  },
  ".MuiStepIcon-root.Mui-completed": {
    // Completed step icon
    color: "#4caf50", // Use a color indicating completion
  },
});

interface CheckpointStepperProps {
  checkpoints: InspectionCheckpointDetailDto[];
  inspectionId: string;
  appointmentId: string;
  onComplete: (inspectionData: UpdateInspectionDto) => Promise<void>;
}

const CheckpointStepper: React.FC<CheckpointStepperProps> = ({
  checkpoints,
  inspectionId,
  appointmentId,
  onComplete,
}) => {
  const [activeStep, setActiveStep] = useState(0);
  const [inspectionData, setInspectionData] =
    useState<InspectionCheckpointDetailDto[]>(checkpoints);
  const steps = INSPECTION_CHECKPOINTS.map(
    (checkpoint) => checkpoint.checkpointName
  );
  const isLastStep = activeStep === steps.length - 1;

  const handleNext = async () => {
    if (isLastStep) {
      // Calculate the total score
      const totalScore = inspectionData.reduce(
        (sum, checkpoint) => sum + checkpoint.score,
        0
      );

      // Determine the result
      let result = "PASSED";
      if (
        inspectionData.some((checkpoint) => checkpoint.score <= 5) ||
        totalScore <= 40
      ) {
        result = "FAILED";
      } else if (totalScore < 80) {
        result = "IN_REVIEW";
      }

      // Prepare the UpdateInspectionDto
      const updateInspectionData: UpdateInspectionDto = {
        id: inspectionId,
        appointmentId: appointmentId,
        result: result,
        totalScore: totalScore,
        updatedCheckpoints: inspectionData.map((checkpoint) => {
          return {
            id: checkpoint.checkpointId,
            name: checkpoint.checkpointName,
            score: checkpoint.score,
            comment: checkpoint.comments,
          } as CheckpointListDto;
        }),
      };

      await onComplete(updateInspectionData);
    } else {
      setActiveStep((prevActiveStep) => prevActiveStep + 1);
    }
  };

  const handleBack = () => {
    setActiveStep((prevActiveStep) => prevActiveStep - 1);
  };

  const handleCheckpointChange =
    (index: number) => (score: number, comments: string) => {
      const newData = [...inspectionData];
      newData[index] = { ...newData[index], score, comments };
      setInspectionData(newData);
    };

  return (
    <StepperContainer>
      <CustomStepper activeStep={activeStep} alternativeLabel>
        {steps.map((label, index) => (
          <Step key={label}>
            <StepLabel>{label}</StepLabel>
          </Step>
        ))}
      </CustomStepper>
      {activeStep < steps.length && (
        <CheckpointForm
          checkpoint={inspectionData[activeStep]}
          onChange={handleCheckpointChange(activeStep)}
        />
      )}
      <div>
        <StepButton disabled={activeStep === 0} onClick={handleBack}>
          Atrás
        </StepButton>
        <StepButton
          variant="contained"
          color="primary"
          onClick={handleNext}
          disabled={inspectionData[activeStep].score === 0} // Disable the Next button if the current checkpoint score is not set.
        >
          {activeStep === steps.length - 1 ? "Finalizar" : "Siguiente"}
        </StepButton>
      </div>
      {isLastStep && (
        <Typography variant="caption" display="block" gutterBottom>
          * Todos los campos deben ser completados para finalizar la inspección.
        </Typography>
      )}
    </StepperContainer>
  );
};

export default CheckpointStepper;
