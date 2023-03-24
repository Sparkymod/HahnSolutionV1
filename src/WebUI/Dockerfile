
FROM node as build
WORKDIR /apps
COPY . .
RUN npm install
COPY . /apps
ARG configuration=production
RUN npm run build — — outputPath=./dist/out — configuration $configuration
RUN npm run start

FROM nginx:alpine
COPY /nginx-custom.conf /etc/nginx/conf.d/
RUN rm -rf /usr/share/nginx/html/*
COPY — from=build /apps/dist/out/ /usr/share/nginx/html
