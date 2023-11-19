export enum AppointmentStatus {
  Pending,
  Cancelled,
  Attended,
  Completed,
}

export const getAppointmentStatusDescription = (status: AppointmentStatus) => {
  switch (status) {
    case AppointmentStatus.Pending:
      return "Programada";
    case AppointmentStatus.Cancelled:
      return "Cancelada";
    case AppointmentStatus.Attended:
      return "Cliente atendido";
    case AppointmentStatus.Completed:
      return "Cita completada";
    default:
      return "Unknown";
  }
};
