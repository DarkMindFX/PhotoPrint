docker rm ppt.photoprint.api

docker run 	--name ppt.photoprint.api^
			--env ServiceConfig__DalInitParams__ConnectionString="Data Source=photoprint-sqlsrv.database.windows.net; Initial Catalog=PhotoPrint-DEV; User ID=ppt_svc_api; Password=PhotoPrint2021!"^
			--env ServiceConfig__StorageInitParams__StorageConnectionString="BlobEndpoint=https://photoprintstorage.blob.core.windows.net/;QueueEndpoint=https://photoprintstorage.queue.core.windows.net/;FileEndpoint=https://photoprintstorage.file.core.windows.net/;TableEndpoint=https://photoprintstorage.table.core.windows.net/;SharedAccessSignature=sv=2020-08-04&ss=bfqt&srt=sco&sp=rwdlacupitfx&se=2022-12-30T23:48:54Z&st=2021-12-24T15:48:54Z&spr=https&sig=PPe8R8zlkhlF6XyEJjluwRdsGzRtvral2UOvTaKO1Y4%%3D"^
			--env ServiceConfig__StorageInitParams__StorageUrl="https://photoprintstorage.blob.core.windows.net"^
			--env ServiceConfig__StorageInitParams__ContainerName="photoprint-images-dev"^
			-it -p 8082:8082 globus000/ppt.photoprint.api