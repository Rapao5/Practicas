# 📦 Etapa 1: Construcción con Node.js
FROM node:20-alpine AS build
WORKDIR /app

# Instalamos las dependencias
COPY package*.json ./
RUN npm install

# Copiamos el código fuente y compilamos (Vite generará la carpeta 'dist')
COPY . .
RUN npm run build

# 🚀 Etapa 2: Servidor Web con Nginx
FROM nginx:alpine

# Copiamos el resultado de Vite a la carpeta que sirve Nginx
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]