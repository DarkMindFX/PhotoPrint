# -- BUILD --
FROM node:16.13.0-alpine as build

WORKDIR /usr/src/app

COPY package* ./

COPY . .

RUN npm install
RUN npm run build

# -- RELEASE --
FROM nginx:stable-alpine as release

COPY ./start-nginx.sh /usr/bin/start-nginx.sh
RUN chmod +x /usr/bin/start-nginx.sh

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /usr/src/app/build /usr/share/nginx/html


RUN apk add --update nodejs
RUN apk add --update npm
RUN apk add --no-cache bash

WORKDIR /usr/share/nginx/html

COPY env.* ./

EXPOSE 80

ENTRYPOINT [ "start-nginx.sh" ]