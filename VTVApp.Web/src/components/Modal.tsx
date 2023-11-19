import React from "react";
import {
  Dialog,
  DialogTitle,
  IconButton,
  DialogContent,
  styled,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

interface ModalProps {
  open: boolean;
  onClose: () => void;
  title?: string;
  children: React.ReactNode;
}

const StyledDialog = styled(Dialog)({
  "& .MuiDialog-paper": {
    width: "auto", // Adjust to your needs
    minWidth: "600px", // Minimum width of the modal
    maxWidth: "80%", // Maximum width of the modal
    margin: "auto", // This will center the modal horizontally
  },
});

const StyledDialogTitle = styled(DialogTitle)(({ theme }) => ({
  backgroundColor: theme.palette.background.paper,
  padding: "1rem",
  "& .MuiTypography-root": {
    color: theme.palette.text.primary,
  },
  "& .MuiIconButton-root": {
    position: "absolute",
    right: "8px",
    top: "8px",
  },
}));

const StyledDialogContent = styled(DialogContent)(({ theme }) => ({
  display: "flex", // Enable flex container
  flexDirection: "column", // Stack children vertically
  justifyContent: "center", // Center vertically
  alignItems: "center", // Center horizontally
  padding: theme.spacing(3), // Adjust padding as needed
}));

const StyledCloseButton = styled(IconButton)(({ theme }) => ({
  position: "absolute",
  right: theme.spacing(1),
  top: theme.spacing(1),
  color: theme.palette.grey[500],
}));

const Modal: React.FC<ModalProps> = ({ open, onClose, title, children }) => {
  return (
    <StyledDialog open={open} onClose={onClose} aria-labelledby="modal-title">
      <StyledDialogTitle id="modal-title">
        {title}
        <StyledCloseButton
          aria-label="close"
          onClick={onClose}
          style={{ position: "absolute", right: 8, top: 8 }}
        >
          <CloseIcon />
        </StyledCloseButton>
      </StyledDialogTitle>
      <StyledDialogContent dividers>{children}</StyledDialogContent>
    </StyledDialog>
  );
};

export default Modal;
