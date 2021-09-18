using System;

namespace API
{
    public class BlobContainerNotExistsException : Exception
    {
        public BlobContainerNotExistsException() : base("Blob container doesn't exists.")
        {
        }
    }
}