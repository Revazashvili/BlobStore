using System;

namespace API
{
    public class BlobNotExistsException : Exception
    {
        public BlobNotExistsException() : base("Blob with this name doesn't exists.")
        {
        }
    }
}