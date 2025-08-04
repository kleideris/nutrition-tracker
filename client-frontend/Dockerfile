# 🔧 Build stage
FROM node:20 AS build

WORKDIR /app

# Copy source code
COPY . .

# Inject build-time API URL
# ARG VITE_API_URL=http://localhost:5000
# ENV VITE_API_URL=$VITE_API_URL

# Install dependencies and build
RUN npm install
RUN npm run build

# 🧊 Serve with Nginx
FROM nginx:stable-alpine

# Copy built frontend
COPY --from=build /app/dist /usr/share/nginx/html

# Copy custom Nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

RUN echo "Building with VITE_API_URL=$VITE_API_URL"