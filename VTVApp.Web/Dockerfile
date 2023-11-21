# Step 1: Build the application
FROM node:20-alpine as build

WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install all dependencies, including devDependencies
RUN npm install

# Copy the rest of the app
COPY . . 

# Modify the .env file
RUN echo "VITE_API_URL=http://vtvapp-aci.cahfcmhcbfb2hphd.brazilsouth.azurecontainer.io/" > .env

# Build the app
RUN npm run build

# Step 2: Serve the application from Nginx
FROM nginx:stable-alpine

# Copy the build output to replace the default nginx contents.
COPY --from=build /app/dist /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx and serve the application
CMD ["nginx", "-g", "daemon off;"]
