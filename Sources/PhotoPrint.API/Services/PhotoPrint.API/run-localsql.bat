docker rm ppt.photoprint.api

docker run 	--name ppt.photoprint.api^
			--add-host=ppt_photoprint_sqldb:192.168.56.1^
			--env ServiceConfig__DalInitParams__ConnectionString="Data Source=ppt_photoprint_sqldb; Initial Catalog=PhotoPrint; User ID=ppt_svc_api; Password=PPTServiceApi2021!"^
			--env ServiceConfig__StorageInitParams__StorageConnectionString="BlobEndpoint=https://photoprintstorage.blob.core.windows.net/;QueueEndpoint=https://photoprintstorage.queue.core.windows.net/;FileEndpoint=https://photoprintstorage.file.core.windows.net/;TableEndpoint=https://photoprintstorage.table.core.windows.net/;SharedAccessSignature=sv=2020-08-04&ss=bfqt&srt=sco&sp=rwdlacupitfx&se=2022-12-05T04:11:36Z&st=2021-12-04T20:11:36Z&spr=https&sig=kZlrAy53M2nZF7YXrcKuOQuR70%%2Bea15mwD4i3ib860A%%3D"^
			--env ServiceConfig__StorageInitParams__StorageUrl="https://photoprintstorage.blob.core.windows.net"^
			--env ServiceConfig__StorageInitParams__ContainerName="photoprint-images-dev"^
			-it -p 8082:8082 globus000/ppt.photoprint.api