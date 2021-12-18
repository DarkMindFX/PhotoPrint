################## BUILD ########################################
FROM node:16.13.0-buster as build

ENV JQ_VERSION=1.6
RUN wget --no-check-certificate https://github.com/stedolan/jq/releases/download/jq-${JQ_VERSION}/jq-linux64 -O /tmp/jq-linux64
RUN cp /tmp/jq-linux64 /usr/bin/jq
RUN chmod +x /usr/bin/jq

WORKDIR /code

COPY package.json package.json
COPY package-lock.json package-lock.json

RUN npm ci --production

COPY . .

RUN jq 'to_entries | map_values({ (.key) : ("$" + .key) }) | reduce .[] as $item ({}; . + $item)' ./src/config.json > ./src/config.tmp.json && mv ./src/config.tmp.json ./src/config.json

RUN npm run build

################## SERVER ########################################
FROM nginx:1.12-alpine as prod

ENV JSFOLDER=/usr/share/nginx/html/static/js/*.js

COPY ./start-nginx.sh /usr/bin/start-nginx.sh
RUN chmod +x /usr/bin/start-nginx.sh
WORKDIR /usr/share/nginx/html

COPY --from=build /code/build /usr/share/nginx/html

EXPOSE 80

COPY nginx.conf /etc/nginx/nginx.conf

ENTRYPOINT [ "start-nginx.sh" ]