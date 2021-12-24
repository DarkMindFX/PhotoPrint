#!/usr/bin/env bash

printf "*** ENV VARS ***\n"
printenv

cp /usr/share/nginx/html/env.js.$ENVIRONMENT /usr/share/nginx/html/env.js

printf "*** Folder /usr/share/nginx/html/ ***\n"
ls /usr/share/nginx/html/

printf "*** File /usr/share/nginx/html/.env ***\n"
cat /usr/share/nginx/html/env.js

printf "\n**************************************\n"

printf "*** Starting NGINX ********************\n"

nginx -g 'daemon off;'
