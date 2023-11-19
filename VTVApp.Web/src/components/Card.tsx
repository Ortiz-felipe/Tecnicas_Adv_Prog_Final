// CardComponent.tsx
import React from "react";
import { Card, CardContent, Typography, useTheme, Theme } from "@mui/material";
import styled from "@emotion/styled";

const StyledCard = styled(Card)<{ theme?: Theme }>(({ theme }) => ({
  marginBottom: theme.spacing(2),
  boxShadow: "0 4px 20px rgba(0,0,0,0.1)",
  borderRadius: theme.shape.borderRadius,
  "&:last-child": {
    marginBottom: 0,
  },
  display: 'flex',
  flexDirection: 'column',
  justifyContent: 'center', // Center content for consistent height
  height: '100%', // Make card take full height of its container
}));

const CardTitle = styled(Typography)({
  fontSize: '1.25rem',
  marginBottom: '0.5rem',
});

interface CardComponentProps<T> {
  title: string;
  content: T;
  renderContent: (content: T) => React.ReactNode;
}

const CardComponent = <T extends unknown>({ title, content, renderContent }: CardComponentProps<T>) => {
  const theme = useTheme();
  
  return (
    <StyledCard>
      <CardContent>
        <CardTitle variant="h5" gutterBottom color={theme.palette.primary.main}>
          {title}
        </CardTitle>
        {renderContent(content)}
      </CardContent>
    </StyledCard>
  );
};

export default CardComponent;
