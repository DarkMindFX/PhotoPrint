using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PPT.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;

namespace PPT.Storage.Azure
{
    class StorageInitParams : IInitParams
    {
        public StorageInitParams()
        {
            Parameters = new Dictionary<string, string>();
            Parameters["StorageConnectionString"] = null;
            Parameters["ContainerName"] = null;
            Parameters["StorageUrl"] = null;
        }

        public Dictionary<string, string> Parameters
        {
            get; set;
        }
    }

    [Export("Azure", typeof(IBinaryStorage))]
    public class Storage : IBinaryStorage
    {
        StorageInitParams _initParams = null;
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _containerClient;
 
        public IInitParams CreateInitParams()
        {
            return new StorageInitParams();
        }

        public bool Delete(string blobName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);
            return blobClient.DeleteIfExists();
        }

        public void Init(IInitParams initParams)
        {
            if(!(initParams is StorageInitParams))
            {
                throw new ArgumentException($"Object of invalid type provided as initParams. Expected type: StorageInitParams, provided: {initParams.GetType()}");
            }
            _initParams = initParams as StorageInitParams;

            var storageConnString = _initParams.Parameters["StorageConnectionString"].ToString();

            _blobServiceClient = new BlobServiceClient(storageConnString);
            var containers = _blobServiceClient.GetBlobContainers();

            string containerName = _initParams.Parameters["ContainerName"].ToString();

            var enumContainers = containers.GetEnumerator();
            while (_containerClient == null && enumContainers.MoveNext())
            {
                if(enumContainers.Current != null && enumContainers.Current.Name.Equals(containerName))
                {
                    _containerClient = new BlobContainerClient(storageConnString, containerName);
                }
            }

            if(_containerClient == null)
            {
                _containerClient = _blobServiceClient.CreateBlobContainer(containerName);
            }

        }

        public string Upload(string blobName, Stream data)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(blobName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = GetFileContentType(blobName) };

            var response = blobClient.Upload(data, new BlobUploadOptions { HttpHeaders = blobHttpHeader });

            var url = Path.Combine(this._initParams.Parameters["StorageUrl"],
                                    this._initParams.Parameters["ContainerName"],
                                    blobName);

            return url;
        }

        private static string GetFileContentType(string filePath)
        {
            string ContentType = String.Empty;
            string Extension = Path.GetExtension(filePath).ToLower();

            switch (Extension)
            {
                case Constants.FILE_EXTENSION_PDF:
                    ContentType = "application/pdf";
                    break;
                case Constants.FILE_EXTENSION_TXT:
                    ContentType = "text/plain";
                    break;
                case Constants.FILE_EXTENSION_BMP:
                    ContentType = "image/bmp";
                    break;
                case Constants.FILE_EXTENSION_GIF:
                    ContentType = "image/gif";
                    break;
                case Constants.FILE_EXTENSION_PNG:
                    ContentType = "image/png";
                    break;
                case Constants.FILE_EXTENSION_JPG:
                    ContentType = "image/jpeg";
                    break;
                case Constants.FILE_EXTENSION_JPEG:
                    ContentType = "image/jpeg";
                    break;
                case Constants.FILE_EXTENSION_XLS:
                    ContentType = "application/vnd.ms-excel";
                    break;
                case Constants.FILE_EXTENSION_XLSX:
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case Constants.FILE_EXTENSION_CSV:
                    ContentType = "text/csv";
                    break;
                case Constants.FILE_EXTENSION_HTML:
                    ContentType = "text/html";
                    break;
                case Constants.FILE_EXTENSION_XML:
                    ContentType = "text/xml";
                    break;
                case Constants.FILE_EXTENSION_ZIP:
                    ContentType = "application/zip";
                    break;
                default:
                    ContentType = "application/octet-stream";
                    break;

            }


            return ContentType;
        }


    }
}
