import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { Box, Theme, Typography, useTheme } from "@mui/material";
import styled from "@emotion/styled";
import vehicleInspectionImage from '../assets/images/vehicle_inspection.png';
import vehicleDiagnosticImage from '../assets/images/vehicle_diagnostic.png';
import vehicleEngingeInspectionImage from '../assets/images/vehicle_engine_inspection.png';
import customerSatisfactionImage from '../assets/images/customer_satisfaction.png';

const ImageTextBox = styled(Box)<{ theme?: Theme }>(() => ({
  maxHeight: "500px",
  position: "relative",
  color: "white",
  textAlign: "center",
  overflow: "hidden",

  "&:before": {
    // Simple dark overlay
    content: '""',
    position: "absolute",
    top: 0,
    right: 0,
    bottom: 0,
    left: 0,
    backgroundColor: "rgba(0,0,0,0.3)",
  },
  "& img": {
    width: "100%",
    verticalAlign: "middle",
    maxHeight: "500px",
    objectFit: "cover",
    display: "block",
  },
  "& > div": {
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    zIndex: 2,
  },
}));

const CarouselContainer = styled.div({
  maxWidth: "100vw",
  overflow: "hidden",
});

const CarouselComponent = () => {
  const theme = useTheme();
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 5000,
    cssEase: "linear",
  };

  return (
    <CarouselContainer>
      <Slider {...settings}>
        <ImageTextBox theme={theme}>
          <img
            src={vehicleInspectionImage}
            alt="Inspección Vehicular"
          />
          <div>
            <Typography variant="h4" gutterBottom>
              Confianza y seguridad para su vehículo
            </Typography>
            <Typography variant="subtitle1">
              Inspecciones exhaustivas para garantizar su tranquilidad en la
              carretera.
            </Typography>
          </div>
        </ImageTextBox>
        <ImageTextBox theme={theme}>
          <img
            src={vehicleDiagnosticImage}
            alt="Diagnóstico Preciso"
          />
          <div>
            <Typography variant="h4" gutterBottom>
              Diagnóstico Preciso
            </Typography>
            <Typography variant="subtitle1">
              Nuestros expertos utilizan la última tecnología para evaluar con
              precisión el estado de su vehículo.
            </Typography>
          </div>
        </ImageTextBox>
        <ImageTextBox>
          <img
            src={vehicleEngingeInspectionImage}
            alt="Inspección de Motor"
          />
          <div>
            <Typography variant="h4" gutterBottom>
              Inspección Completa del Motor
            </Typography>
            <Typography variant="subtitle1">
              Revisión detallada para asegurar el rendimiento óptimo de su
              automóvil y su longevidad.
            </Typography>
          </div>
        </ImageTextBox>
        <ImageTextBox>
          <img
            src={customerSatisfactionImage}
            alt="Servicio al Cliente Excepcional"
          />
          <div>
            <Typography variant="h4" gutterBottom>
              Atención Personalizada
            </Typography>
            <Typography variant="subtitle1">
              Brindamos un servicio excepcional con atención personal para cada
              cliente, asegurando su completa satisfacción.
            </Typography>
          </div>
        </ImageTextBox>
        <ImageTextBox>
          <img
            src="src\assets\images\vehicle_dashboard.png"
            alt="Tablero de Vehículo Óptimo"
          />
          <div>
            <Typography variant="h4" gutterBottom>
              Fiabilidad en Cada Indicador
            </Typography>
            <Typography variant="subtitle1">
              Garantizamos que todos los sistemas de su vehículo están en
              condiciones perfectas para su seguridad y confort.
            </Typography>
          </div>
        </ImageTextBox>
      </Slider>
    </CarouselContainer>
  );
};

export default CarouselComponent;
