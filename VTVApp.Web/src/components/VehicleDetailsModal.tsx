import React, { useEffect, useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogTitle,
  Typography,
  IconButton,
  CircularProgress,
  Box,
  Paper,
  TableContainer,
  TableRow,
  TableCell,
  Table,
  TableHead,
  TableBody,
  styled,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { VehicleDto } from "../types/dtos/Vehicles/VehicleDto";
import { InspectionDetailsDto } from "../types/dtos/Inspections/InspectionDetailsDto";
import {
  getInspectionDetails,
  getLatestVehicleInspection,
} from "../api/inspectionAPI";
import { TableVirtuoso, TableComponents } from "react-virtuoso";
import { getInspectionStatusDescription } from "../helpers/inspectionStatusHelper";
import { formatDate } from "../helpers/dateHelpers";

interface VehicleDetailsModalProps {
  open: boolean;
  onClose: () => void;
  vehicle: VehicleDto | null;
}

interface Column {
  id: string;
  label: string;
  minWidth?: number;
  align?: "right" | "left" | "center";
  format?: (value: any) => string;
}

const StyledDialog = styled(Dialog)(({ theme }) => ({
  "& .MuiDialog-paper": {
    width: "60%", // A narrower width for a more focused view
    maxWidth: "none",
    height: "auto",
    maxHeight: "80vh",
    overflowY: "unset",
    backgroundColor: theme.palette.background.default, // Use a neutral background
    boxShadow: "none", // Remove shadows for a flatter appearance
    borderRadius: "8px", // Slightly rounded corners for a modern look
  },
}));

const StyledDialogContent = styled(DialogContent)(({ theme }) => ({
  overflowY: "unset", // Remove the scroll bar from the DialogContent
  "&:first-child": {
    paddingTop: theme.spacing(2),
  },
  "&:last-child": {
    paddingBottom: theme.spacing(2),
  },
  padding: theme.spacing(3), // Uniform padding
}));

const SectionContainer = styled("div")({
  marginBottom: "24px", // Add some spacing at the bottom of each section
});

const SectionTitle = styled(Typography)({
  fontSize: "1.25rem", // Larger font size for section titles
  fontWeight: "bold",
  color: "rgba(0, 0, 0, 0.87)", // Strong contrast for readability
  paddingBottom: "8px", // Space below the section title
  borderBottom: "1px solid #e0e0e0", // A subtle separator
});

const VehicleInspectionInfoContainer = styled("div")({
  display: 'flex',
  justifyContent: 'space-between',
  marginBottom: '24px',
  minHeight: '200px', // Ensure the container has a minimum height
});

const InfoContainerStyles = {
  paddingTop: "4px", // Top padding to avoid text looking cropped
  paddingRight: "4px", // Right padding for spacing
  flex: "1",
};

const VehicleInfo = styled("div")(InfoContainerStyles);

const InspectionInfo = styled("div")(InfoContainerStyles);

const Divider = styled("hr")({
  margin: "24px 0", // Add vertical space around the divider
  border: "none",
  borderTop: "1px solid #e0e0e0", // Light grey color for the divider line
});

const StyledTableContainer = styled(TableContainer)({
  minHeight: "200px", // Set a minimum height
  display: "flex", // Use flex layout to control size dynamically
  flexDirection: "column", // Stack children vertically
  height: "100%", // Allow the container to expand to the full height of its parent
  maxHeight: "65vh", // Set a maximum height to the container
  boxShadow: "none", // No shadow for tables
  "& .MuiTableCell-head": {
    backgroundColor: "#f5f5f5", // A subtle head row background
    color: "#212121", // Slightly darker font for contrast
  },
  "& .MuiTableCell-body": {
    borderBottom: "1px solid #e0e0e0", // Clean lines between rows
  },
});

const EmptyDataPlaceholder = styled("div")({
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  padding: "20px",
  fontStyle: "italic",
  color: "rgba(0, 0, 0, 0.54)", // Lighter text to indicate a placeholder
});

const VehicleDetailsModal: React.FC<VehicleDetailsModalProps> = ({
  open,
  onClose,
  vehicle,
}) => {
  const [latestInspection, setLatestInspection] =
    useState<InspectionDetailsDto | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchInspectionDetails = async () => {
      if (vehicle) {
        setLoading(true);
        try {
          const inspections = await getLatestVehicleInspection(vehicle.id);
          if (inspections) {
            const latestInspectionId = inspections.id; // Assuming the first one is the latest
            const details = await getInspectionDetails(latestInspectionId);
            setLatestInspection(details);
          } else {
            setLatestInspection(null);
          }
        } catch (error) {
          console.error("Failed to fetch inspection details:", error);
        } finally {
          setLoading(false);
        }
      }
    };

    fetchInspectionDetails();
  }, [vehicle]);

  const checkpointColumns = [
    { dataKey: "checkpointName", label: "Punto de chequeo", width: 100 },
    { dataKey: "score", label: "Puntuacion", numeric: true, width: 50 },
    { dataKey: "comments", label: "Comentarios", width: 200 },
    {
      dataKey: "recommendedAction",
      label: "Acciones recomendadas",
      width: 100,
    },
  ];

  const VirtuosoTableComponents = {
    Scroller: React.forwardRef<HTMLDivElement>((props, ref) => (
      <TableContainer component={Paper} {...props} ref={ref} />
    )),
    Table: React.forwardRef((props, ref) => (
      <Table
        {...props}
        ref={ref}
        sx={{ borderCollapse: "separate", tableLayout: "fixed" }}
      />
    )),
    TableHead,
    TableRow: React.forwardRef((props, ref) => (
      <TableRow {...props} ref={ref} />
    )),
    TableBody: React.forwardRef((props, ref) => (
      <TableBody {...props} ref={ref} />
    )),
    // Add other component overrides as necessary
  };

  const FixedHeaderContent = () => (
    <TableRow>
      {checkpointColumns.map((column) => (
        <TableCell
          key={column.dataKey}
          align={column.numeric ? "right" : "left"}
          style={{ width: column.width, position: "sticky", top: 0 }}
        >
          {column.label}
        </TableCell>
      ))}
    </TableRow>
  );

  const RowContent = (index, checkpoint) => (
    <React.Fragment>
      {checkpointColumns.map((column) => (
        <TableCell
          key={column.dataKey}
          align={column.numeric ? "right" : "left"}
        >
          {checkpoint[column.dataKey]}
        </TableCell>
      ))}
    </React.Fragment>
  );

  return (
    <StyledDialog
      open={open}
      onClose={onClose}
      aria-labelledby="vehicle-details-title"
    >
      <DialogTitle id="vehicle-details-title">
        Detalles del Vehículo
        <IconButton
          aria-label="close"
          onClick={onClose}
          sx={{
            position: "absolute",
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      <StyledDialogContent dividers>
        {loading ? (
          <CircularProgress />
        ) : (
          <Box sx={{ overflowY: 'auto', maxHeight: 'calc(80vh - 96px)' }}>
            <VehicleInspectionInfoContainer>
              <VehicleInfo>
                <SectionTitle variant="h6">
                  Información del Vehículo
                </SectionTitle>
                <Typography variant="body2">
                  Patente: {vehicle?.licensePlate}
                </Typography>
                <Typography variant="body2">Marca: {vehicle?.brand}</Typography>
                <Typography variant="body2">
                  Modelo: {vehicle?.model}
                </Typography>
                <Typography variant="body2">Color: {vehicle?.color}</Typography>
                <Typography variant="body2">Año: {vehicle?.year}</Typography>
                {vehicle?.isFavorite && (
                  <Typography variant="body2" color="gold">
                    Vehículo Favorito
                  </Typography>
                )}
              </VehicleInfo>
              <InspectionInfo>
                {latestInspection && (
                  <>
                    <SectionTitle variant="h6">
                      Detalles de la Última Inspección
                    </SectionTitle>
                    <Typography variant="body2">
                      Inspector: {latestInspection.inspectorName}
                    </Typography>
                    <Typography variant="body2">
                      Fecha: {formatDate(latestInspection.inspectionDate)}
                    </Typography>
                    <Typography variant="body2">
                      Comentarios: {latestInspection.overallComments}
                    </Typography>
                    <Typography variant="body2">
                      Estado: {getInspectionStatusDescription(latestInspection.status)}
                    </Typography>
                  </>
                )}
              </InspectionInfo>
            </VehicleInspectionInfoContainer>

            <StyledTableContainer>
              {latestInspection ? (
                <TableVirtuoso
                  data={latestInspection.checkpoints}
                  components={VirtuosoTableComponents}
                  fixedHeaderContent={FixedHeaderContent}
                  itemContent={RowContent}
                  style={{ height: "400px" }}
                />
              ) : (
                <EmptyDataPlaceholder>
                  <Typography variant="body2">
                    Este vehículo no ha sido inspeccionado aún.
                  </Typography>
                </EmptyDataPlaceholder>
              )}
            </StyledTableContainer>
          </Box>
        )}
      </StyledDialogContent>
    </StyledDialog>
  );
};

export default VehicleDetailsModal;
