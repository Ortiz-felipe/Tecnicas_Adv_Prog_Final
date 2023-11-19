import React, { useEffect } from "react";
import {
  selectAppointmentDetails,
} from "../features/appointments/appointmentSlice";
import AppointmentDetails from "../components/AppointmentDetails";
import { useAppSelector } from "../app/hooks";
import { CircularProgress } from "@mui/material";
import { useParams } from "react-router-dom";

const VehicleInspectionPage: React.FC = () => {
  useParams<{ appointmentId: string; }>();
  const appointmentDetails = useAppSelector(selectAppointmentDetails);
  const isLoading = true;

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

  // const handleCompleteInspection = async (
  //   updateInspectionData: UpdateInspectionDto
  // ) => {
  //   setIsLoading(true);
  //   try {
  //     // Assuming `updateInspectionData` already contains the necessary information
  //     const inspectionData: UpdateInspectionDto = {
  //       ...updateInspectionData,
  //       appointmentId: appointmentId,
  //       id: inspectionDetails!.id,
  //     };
  //     const saveResultsResponse = await saveInspectionResults(
  //       inspectionData,
  //       inspectionData.id
  //     );
  //     if (saveResultsResponse.success) {
  //       dispatch(setInspectionDetails(saveResultsResponse.inspectionDetails));
  //       navigate("/inspections");
  //     } else {
  //       throw new Error(saveResultsResponse.message);
  //     }
  //   } catch (error: any) {
  //     dispatch(setInspectionError(error.message));
  //   } finally {
  //     setIsLoading(false);
  //   }
  // };

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
          {/* <CheckpointStepper onComplete={handleCompleteInspection} /> */}
        </>
      )}
    </>
  );
};

export default VehicleInspectionPage;
