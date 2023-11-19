import React from 'react';
import styled from '@emotion/styled';

const ContentContainer = styled.main`
  flex-grow: 1;
  overflow-y: auto; // Maintain this for scrolling
  margin: 1rem; // Keep the margin for spacing around the container
  padding: 1rem; // Padding for inner spacing
  display: flex; // Set display to flex to create a flex container
  flex-direction: column; // Align children vertically
`;

type MainContentProps = {
  children?: React.ReactNode;
};

const MainContent: React.FC<MainContentProps> = ({ children }) => {
  return <ContentContainer>{children}</ContentContainer>;
};

export default MainContent;