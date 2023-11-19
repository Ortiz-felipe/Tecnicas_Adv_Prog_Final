import React from "react";
import { Card, CardContent, Typography, useTheme } from "@mui/material";
import styled from "@emotion/styled";

const StyledCard = styled(Card)(({ theme }) => ({
  marginBottom: theme.spacing(2),
  boxShadow: "0 4px 20px rgba(0,0,0,0.1)", // Subtle shadow for depth
  borderRadius: theme.shape.borderRadius, // Consistent border radius
  "&:last-child": {
    marginBottom: 0,
  },
}));

const CardTitle = styled(Typography)({
    fontSize: '1.25rem', // Larger font size for titles
    marginBottom: '0.5rem',
  });

interface CardComponentProps<T> {
  title: string;
  content: T;
  renderContent: (content: T) => React.ReactNode;
}

function CardComponent<T>({ title, content, renderContent }: CardComponentProps<T>): JSX.Element {
    const theme = useTheme(); // Access the theme directly via useTheme hook
    
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
  }

export default CardComponent;
