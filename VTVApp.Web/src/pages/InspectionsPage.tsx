import React, { useEffect } from 'react';
import { Typography } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import styled from '@emotion/styled';
import TableComponent from '../components/Table';
import { formatDate } from '../helpers/dateHelpers';
import { useAppDispatch, useAppSelector } from '../app/hooks';
import { selectUser } from '../features/user/userSlice';
import { selectInspectionList, selectInspectionsLoading, setError, setInspectionList, startLoading } from '../features/inspections/inspectionSlice';
import { getAllInspections } from '../api/inspectionAPI';

const InspectionsPageContainer = styled.div`
  padding: 1rem;
`;

const HeaderSection = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
`;

const columns = [
  {
    id: 'licencePlate',
    label: 'Patente #',
    minWidth: 170,
    align: 'left' as const,
  },
  {
    id: 'inspectionDate',
    label: 'Fecha de Inspeccion',
    minWidth: 170,
    align: 'left' as const,
    format: formatDate
  },
  {
    id: 'status',
    label: 'Estado',
    minWidth: 100,
    align: 'left' as const,
  },
];

const InspectionsPage: React.FC = () => {
  const dispatch = useAppDispatch();
  const userDetails = useAppSelector(selectUser);
  const inspections = useAppSelector(selectInspectionList);
  const isLoading = useAppSelector(selectInspectionsLoading);

  useEffect(() => {
    const fetchInspections = async () => {
      if (userDetails) {
        dispatch(startLoading());
        try {
          const inspectionsData = await getAllInspections(userDetails.id);
          dispatch(setInspectionList(inspectionsData));
        } catch (error: any) {
          dispatch(setError(error.message));
        }
      }
    };

    fetchInspections();
  }, [dispatch, userDetails!.id]);

  const handleMoreInfo = () => {
    // Logic to handle showing more information about the inspection
    return;
  };

  const actions = [
    {
      icon: <SearchIcon />,
      tooltip: 'More info',
      onClick: handleMoreInfo,
    },
    // Add other actions here if needed
  ];

  return (
    <InspectionsPageContainer>
      <HeaderSection>
        <Typography variant="h4">Tus Inspecciones</Typography>
      </HeaderSection>
      {isLoading ? (
        // Render a loading indicator or return null if you do not wish to render anything while loading
        <Typography>Loading...</Typography>
      ) : inspections.length > 0 ? (
        <TableComponent columns={columns} data={inspections} actions={actions} />
      ) : (
        <Typography>No hay inspecciones para mostrar.</Typography>
      )}
    </InspectionsPageContainer>
  );
};

export default InspectionsPage;
