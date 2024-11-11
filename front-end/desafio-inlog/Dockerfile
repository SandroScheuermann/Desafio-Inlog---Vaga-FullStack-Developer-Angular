FROM node:18-alpine AS build
WORKDIR /app

COPY src ./src/
COPY package*.json .
COPY angular.json .
COPY tsconfig*.json .

RUN npm cache clean --force
RUN npm install
RUN npm run build -- --configuration production

# Etapa de runtime
FROM nginx:alpine

COPY --from=build /app/dist/desafio-inlog/browser /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
