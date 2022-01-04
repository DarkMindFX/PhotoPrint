
docker rm ppt.photoprint.ui

docker run --name ppt.photoprint.ui -it -p 3000:80 ^
	-e "ENVIRONMENT=dev"^
	globus000/ppt.photoprint.ui