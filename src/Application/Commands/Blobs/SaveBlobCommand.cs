using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Requests;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;

namespace Application.Commands.Blobs
{
    public record SaveBlobCommand(SaveBlobRequest SaveBlobRequest) : IRequestWrapper<Uri>{}
    
    public class SaveBlobCommandHandler : IHandlerWrapper<SaveBlobCommand,Uri>
    {
        private readonly IBlobService _blobService;

        public SaveBlobCommandHandler(IBlobService blobService) => _blobService = blobService;

        public Task<IResponse<Uri>> Handle(SaveBlobCommand request, CancellationToken cancellationToken) =>
            _blobService.SaveAsync(request.SaveBlobRequest.Container, request.SaveBlobRequest.Blob, request.SaveBlobRequest.BlobName);
    }
}