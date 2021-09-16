using System;

namespace API
{
    public class BlobAlreadyExistsException : Exception
    {
        public BlobAlreadyExistsException() : base("Blob already exists.")
        {
        }

        public BlobAlreadyExistsException(string blobName) : base($"Blob with name {blobName} already exists.")
        {
        }
    }
}