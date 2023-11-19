import React, { useEffect, useState } from "react";
import {
  Button,
  Typography,
  IconButton,
  CircularProgress,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import StarIcon from "@mui/icons-material/Star";
import StarBorderIcon from "@mui/icons-material/StarBorder";
import SearchIcon from "@mui/icons-material/Search";
import styled from "@emotion/styled";
import TableComponent from "../components/Table";
import { VehicleDto } from "../types/dtos/Vehicles/VehicleDto";
import { getInspectionStatusDescription } from "../helpers/inspectionStatusHelper";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import {
  selectVehicles,
  selectVehiclesLoading,
  setError,
  setFavoriteVehicle,
  setVehicles,
  startLoading,
} from "../features/vehicles/vehicleSlice";
import {
  createNewVehicle,
  getAllVehiclesForUserId,
  markVehicleAsFavorite,
} from "../api/vehicleAPI";
import { selectUser } from "../features/user/userSlice";
import { UpdateVehicleDto } from "../types/dtos/Vehicles/UpdateVehicleDto";
import Modal from "../components/Modal";
import VehicleRegistrationForm from "../components/VehicleRegistrationForm";
import { CreateVehicleDto } from "../types/dtos/Vehicles/CreateVehicleDto";
import VehicleDetailsModal from "../components/VehicleDetailsModal";

const VehiclesPageContainer = styled.div`
  padding: 1rem;
`;

const HeaderSection = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
`;

const Overlay = styled.div`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(255, 255, 255, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 2; // Ensure it's above other content
`;

// Define your columns in the component where TableComponent is used
const columns = [
  // {
  //   id: "favorite",
  //   label: "Favorite",
  //   minWidth: 50,
  // },
  { id: "licensePlate", label: "Patente", minWidth: 100 },
  { id: "brand", label: "Marca", minWidth: 100 },
  { id: "model", label: "Modelo", minWidth: 100 },
  { id: "color", label: "Color", minWidth: 100 },
  { id: "year", label: "Año", minWidth: 50 },
  {
    id: "vehicleStatus",
    label: "Estado de Inspección",
    minWidth: 100,
    format: getInspectionStatusDescription,
  },
];

const VehiclesPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const userDetails = useAppSelector(selectUser);
  const vehicles = useAppSelector(selectVehicles);
  const isLoading = useAppSelector(selectVehiclesLoading);

  const [isSubmitting, setSubmitting] = useState(false);
  const [isModalOpen, setModalOpen] = useState(false);
  const [selectedVehicle, setSelectedVehicle] = useState<VehicleDto | null>(
    null
  );
  const [isDetailsModalOpen, setDetailsModalOpen] = useState(false);

  useEffect(() => {
    const fetchUserVehicles = async () => {
      if (vehicles.length === 0 && userDetails) {
        try {
          dispatch(startLoading());
          const vehicles = await getAllVehiclesForUserId(userDetails.id);
          dispatch(setVehicles(vehicles));
        } catch (error: any) {
          dispatch(setError(error.message));
        }
      }
    };

    fetchUserVehicles();
  }, [dispatch, vehicles]);

  const handleRegisterVehicle = async (createVehicleData: CreateVehicleDto) => {
    try {
      setSubmitting(true); // Start the progress indicator
      const vehicleData: CreateVehicleDto = {
        ...createVehicleData,
        userId: userDetails!.id,
      };
      const newVehicle = await createNewVehicle(vehicleData);
      dispatch(
        setVehicles([
          ...vehicles,
          {
            ...vehicleData,
            id: newVehicle.id,
            isFavorite: false,
            vehicleStatus: 0,
          },
        ])
      ); // Save the new vehicle in Redux
      handleCloseModal(); // Close the modal
    } catch (error: any) {
      // You might want to display the error to the user
      console.error(error);
      dispatch(setError(error.message)); // Set the error in the Redux state
    } finally {
      setSubmitting(false); // Stop the progress indicator
    }
  };

  const handleMoreInfo = (vehicle: VehicleDto) => {
    // Set the selected vehicle and open the modal
    setSelectedVehicle(vehicle);
    setDetailsModalOpen(true);
  };

  const handleCloseModal = () => {
    // Close the modal
    setModalOpen(false);
  };

  const handleSetFavorite = async (vehicleId: string) => {
    // Find the vehicle to be updated
    const vehicleToUpdate = vehicles.find((v) => v.id === vehicleId);
    if (!vehicleToUpdate) {
      console.error("Vehicle not found");
      return;
    }

    // Create the update data
    const updateData: UpdateVehicleDto = {
      ...vehicleToUpdate,
      isFavorite: !vehicleToUpdate.isFavorite, // Toggle the current favorite state
    };

    try {
      // Invoke the API
      const result = await markVehicleAsFavorite(updateData, vehicleId);
      if (result.success) {
        // Update the local state to reflect the changes immediately
        dispatch(
          setVehicles(
            vehicles.map((v) =>
              v.id === vehicleId
                ? { ...v, isFavorite: updateData.isFavorite }
                : v
            )
          )
        );
      } else {
        // Handle the case where the API operation was not successful
        console.error(result.message);
      }
    } catch (error: any) {
      // Handle any errors
      console.error(error.message);
    }
  };

  const renderFavoriteIcon = (vehicle: VehicleDto) => {
    return vehicle.isFavorite ? (
      <StarIcon style={{ color: "gold" }} />
    ) : (
      <StarBorderIcon
        style={{ cursor: "pointer" }}
        onClick={() => handleSetFavorite(vehicle.id)}
      />
    );
  };

  // Add the actions for the table
  const actions = [
    {
      icon: <SearchIcon />,
      tooltip: "Más información",
      onClick: handleMoreInfo,
    },
    // Add other actions here if needed
  ];

  return (
    <VehiclesPageContainer>
      <HeaderSection>
        <Typography variant="h4">Your Vehicles</Typography>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => setModalOpen(true)}
        >
          Registrar Vehículo
        </Button>
      </HeaderSection>
      {isLoading ? (
        <Overlay>
          <CircularProgress />
        </Overlay>
      ) : vehicles.length > 0 ? (
        <TableComponent
          columns={columns}
          data={vehicles}
          renderFavoriteIcon={renderFavoriteIcon}
          actions={actions}
        />
      ) : (
        <Typography variant="subtitle1">
          No tienes vehículos registrados.
        </Typography>
        // You can style this message as an overlay as needed
      )}
      {isModalOpen && (
        <Modal
          open={isModalOpen}
          onClose={handleCloseModal}
          title="Registra un nuevo vehiculo"
          children={
            isSubmitting ? (
              <Overlay>
                <CircularProgress />
              </Overlay>
            ) : (
              <VehicleRegistrationForm
                onSubmit={handleRegisterVehicle}
                onCancel={handleCloseModal}
              />
            )
          }
        />
      )}
      {isDetailsModalOpen && (
        <VehicleDetailsModal
          open={isDetailsModalOpen}
          onClose={() => setDetailsModalOpen(false)}
          vehicle={selectedVehicle}
        />
      )}
    </VehiclesPageContainer>
  );
};

export default VehiclesPage;
