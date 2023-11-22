import React from "react";
import { CssBaseline, ThemeProvider } from "@mui/material";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import theme from "./styles/theme"; // Replace with the correct path to your theme file
import NavBar from "./components/layout/NavBar";
import SidePanel from "./components/layout/SidePanel";
import MainContent from "./components/layout/MainContent";
import Footer from "./components/layout/Footer";
import HomePage from "./pages/HomePage.tsx";
import AppointmentsPage from "./pages/AppointmentsPage";
import VehiclesPage from "./pages/VehiclesPage";
import InspectionsPage from "./pages/InspectionsPage";
import styled from "@emotion/styled";
import LandingPage from "./pages/LandingPage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import { Provider } from "react-redux";
import { store } from "./app/store.ts";
import ProtectedRoute from "./components/routing/ProtectedRoute.tsx";
import BookAppointmentPage from "./pages/BookingAppointmentPage.tsx";
import InspectorAppointmentsPage from "./pages/InspectorAppointmentsPage.tsx";
// @ts-ignore
import appInsights from "./utils/appInsights.ts";
// import VehicleInspectionPage from "./pages/VehicleInspectionPage.tsx";

// Define the container for the entire app
const AppContainer = styled.div`
  display: flex;
  flex-direction: column;
  height: 100vh; // Set the height of the app to the full viewport height
`;

// Define the layout for the main area, including the sidebar and content
const MainLayout = styled.div`
  display: flex;
  flex: 1; // This will make the main layout take up the remaining space
  overflow: hidden; // Prevents scrollbars if the content overflows
`;

const SideBar = styled.div`
  width: 240px; // Width of the side panel
  overflow-y: auto; // Allows scrolling in the sidebar if the content overflows
`;

const App: React.FC = () => {
  
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Router>
          <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route
              path="/*"
              element={
                <AppContainer>
                  <NavBar />{" "}
                  {/* This can be NavBar if you want a different nav for the rest of the pages */}
                  <MainLayout>
                    <SideBar>
                      <SidePanel />
                    </SideBar>
                    <MainContent>
                      <Routes>
                        <Route element={<ProtectedRoute />}>
                          <Route path="/home" element={<HomePage />} />
                          <Route
                            path="/appointments"
                            element={<AppointmentsPage />}
                          />
                          <Route
                            path="/appointments/newAppointment"
                            element={<BookAppointmentPage />}
                          />
                          <Route path="/vehicles" element={<VehiclesPage />} />
                          <Route
                            path="/inspections"
                            element={<InspectionsPage />}
                          />
                          <Route
                            path="inspectorAppointments"
                            element={<InspectorAppointmentsPage />}
                          />
                          {/* <Route
                            path="/inspections/:appointmentId"
                            element={<VehicleInspectionPage />}
                          /> */}
                          {/* Add more protected routes as needed */}
                        </Route>
                      </Routes>
                    </MainContent>
                  </MainLayout>
                  <Footer />
                </AppContainer>
              }
            />
          </Routes>
        </Router>
      </ThemeProvider>
    </Provider>
  );
};

export default App;
