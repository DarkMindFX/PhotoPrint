#!/usr/bin/env bash
printenv

cp /usr/share/nginx/html/.env.$ENVIRONMENT /usr/share/nginx/html/.env

nginx -g 'daemon off;'