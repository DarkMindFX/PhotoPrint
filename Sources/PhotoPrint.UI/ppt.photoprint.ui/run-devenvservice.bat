
docker rm ppt.photoprint.ui

docker run --name ppt.photoprint.ui --add-host=ppt_photoprint_api:192.168.0.248 -it -p 3000:80 ^
	-e "ENVIRONMENT=dev"^
	globus000/ppt.photoprint.ui