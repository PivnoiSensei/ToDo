# Build
FROM node:22-alpine AS build

WORKDIR /app

COPY frontend/package*.json ./

RUN npm ci

COPY frontend .

RUN npm run build


# Runtime
FROM nginx:alpine

COPY docker/nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/ToDoClient/browser /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]