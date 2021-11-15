docker rm ppt.photoprint.api

docker run --name ppt.photoprint.api --add-host=ppt_photoprint_sqldb:192.168.56.1 --env ServiceConfig__DalInitParams__ConnectionString="Data Source=ppt_photoprint_sqldb; Initial Catalog=PhotoPrint; User ID=ppt_svc_api; Password=PPTServiceApi2021!" -it -p 8082:8082 globus000/ppt.photoprint.api