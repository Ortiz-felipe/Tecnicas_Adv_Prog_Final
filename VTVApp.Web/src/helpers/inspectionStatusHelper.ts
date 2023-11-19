export enum InspectionStatus {
    Expired,
    CompletedApproved,
    CompletedFailed,
    Scheduled,
    InReview,
  }
  
  export const getInspectionStatusDescription = (status: InspectionStatus): string => {
    switch (status) {
      case InspectionStatus.Expired:
        return "Vencido";
      case InspectionStatus.CompletedApproved:
        return "Completado y Aprobado";
      case InspectionStatus.CompletedFailed:
        return "Completado y Reprobado";
      case InspectionStatus.Scheduled:
        return "Agendado";
      case InspectionStatus.InReview:
        return "En Revision";
      default:
        return "Desconocido";
    }
  };