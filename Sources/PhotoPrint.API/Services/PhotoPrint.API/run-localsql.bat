docker rm ppt.photoprint.api

docker run 	--name ppt.photoprint.api^
			--add-host=ppt_photoprint_sqldb:192.168.56.1^
			--env ServiceConfig__DalInitParams__ConnectionString="Data Source=ppt_photoprint_sqldb; Initial Catalog=PhotoPrint; User ID=ppt_svc_api; Password=PPTServiceApi2021!"^
			--env ServiceConfig__StorageInitParams__StorageConnectionString="BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;"^
			--env ServiceConfig__StorageInitParams__StorageUrl="http://127.0.0.1:10000/devstoreaccount1"^
			--env ServiceConfig__StorageInitParams__ContainerName="photoprint-images-dev"^
			-it -p 8082:8082 globus000/ppt.photoprint.api