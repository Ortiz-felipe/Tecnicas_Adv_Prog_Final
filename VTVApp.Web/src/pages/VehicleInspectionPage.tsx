import React, { useState, useEffect } from "react";
import {
  selectAppointmentDetails,
  startLoading as startAppointmentLoading,
  setError as setAppointmentError,
  setAppointmentDetails,
} from "../features/appointments/appointmentSlice";
import {
  startLoading as startInspectionLoading,
  setInspectionDetails,
  setError as setInspectionError,
  selectInspectionDetails,
} from "../features/inspections/inspectionSlice";
import AppointmentDetails from "../components/AppointmentDetails";
import CheckpointStepper from "../components/CheckpointStepper";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { getAppointmentById } from "../api/appointmentAPI";
import { CircularProgress } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { createInspection, saveInspectionResults } from "../api/inspectionAPI";
import { CreateInspectionDto } from "../types/dtos/Inspections/CreateInspectionDto";
import moment from "moment";
import { UpdateInspectionDto } from "../types/dtos/Inspections/UpdateInspectionDto";
import { INSPECTION_CHECKPOINTS } from "../types/constants/InspectionCheckpoints";
import { CheckpointListDto } from "../types/dtos/Checkpoints/CheckpointListDto";

const VehicleInspectionPage: React.FC = () => {
  const { appointmentId } = useParams<{ appointmentId: string }>();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const appointmentDetails = useAppSelector(selectAppointmentDetails);
  const inspectionDetails = useAppSelector(selectInspectionDetails);
  const [isLoading, setIsLoading] = useState(true);

  //   useEffect(() => {
  //     const fetchAppointmentDetails = async () => {
  //       if (appointmentId) {
  //         try {
  //           const details = await getAppointmentById(appointmentId);
  //           dispatch(setAppointmentDetails(details));
  //           setHasFetchedDetails(true); // Indicates successful fetch
  //         } catch (error: any) {
  //           dispatch(setAppointmentError(error.message));
  //         } finally {
  //           setIsLoading(false);
  //         }
  //       }
  //     };

  //     if (!appointmentDetails || appointmentDetails.id !== appointmentId) {
  //       fetchAppointmentDetails();
  //     } else {
  //       setHasFetchedDetails(true);
  //     }
  //   }, [dispatch, appointmentDetails]);

  //   useEffect(() => {
  //     if (appointmentDetails?.id === appointmentId) {
  //         const inspectionData = {
  //           appointmentId: appointmentId,
  //           result: 0, // Initial result status
  //           vehicleId: appointmentDetails.vehicle.id,
  //           inspectionDate: moment().toISOString(),
  //           checkpoints: INSPECTION_CHECKPOINTS.map(checkpoint => ({
  //             name: checkpoint.checkpointName,
  //             score: 0,
  //             comment: "",
  //           })),
  //         };

  //         createInspection(inspectionData, appointmentId)
  //           .then(inspectionResult => {
  //             if (inspectionResult.success) {
  //               dispatch(setInspectionDetails(inspectionResult.inspectionDetails));
  //             } else {
  //               dispatch(setInspectionError(inspectionResult.message));
  //             }
  //           })
  //           .catch(error => {
  //             dispatch(setInspectionError(error.message));
  //           });
  //       }
  //   }, [dispatch, appointmentDetails]);

  useEffect(() => {
    console.log("mount");
  }, []);

  const handleCompleteInspection = async (
    updateInspectionData: UpdateInspectionDto
  ) => {
    setIsLoading(true);
    try {
      // Assuming `updateInspectionData` already contains the necessary information
      const inspectionData: UpdateInspectionDto = {
        ...updateInspectionData,
        appointmentId: appointmentId,
        id: inspectionDetails.id,
      };
      const saveResultsResponse = await saveInspectionResults(
        inspectionData,
        inspectionData.id
      );
      if (saveResultsResponse.success) {
        dispatch(setInspectionDetails(saveResultsResponse.inspectionDetails));
        navigate("/inspections");
      } else {
        throw new Error(saveResultsResponse.message);
      }
    } catch (error: any) {
      dispatch(setInspectionError(error.message));
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <>
      {isLoading ? (
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
          }}
        >
          <CircularProgress />
        </div>
      ) : !appointmentDetails ? (
        <div>No appointment details available.</div>
      ) : (
        <>
          <AppointmentDetails
            user={appointmentDetails.user}
            vehicle={appointmentDetails.vehicle}
            appointmentDate={appointmentDetails.appointmentDate}
          />
          <CheckpointStepper onComplete={handleCompleteInspection} />
        </>
      )}
    </>
  );
};

export default VehicleInspectionPage;
