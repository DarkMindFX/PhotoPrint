
docker rm ppt.photoprint.ui

docker run --name ppt.photoprint.ui --add-host=ppt_photoprint_api:192.168.56.1 -it -p 3000:80 ^
	-e "REACT_APP_PPT_API_HOST=http://ppt_photoprint_api:8082"^
	-e "REACT_APP_PPT_API_VERSION=v1"^
	globus000/ppt.photoprint.ui