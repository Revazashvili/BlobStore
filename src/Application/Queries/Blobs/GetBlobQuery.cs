using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.Responses;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;

namespace Application.Queries.Blobs
{
    public record GetBlobQuery(string Container,string BlobName) : IRequestWrapper<GetBlobResponse>{}

    public class GetBlobQueryHandler : IHandlerWrapper<GetBlobQuery, GetBlobResponse>
    {
        private readonly IBlobService _blobService;

        public GetBlobQueryHandler(IBlobService blobService) => _blobService = blobService;

        public Task<IResponse<GetBlobResponse>> Handle(GetBlobQuery request, CancellationToken cancellationToken) =>
            _blobService.GetAsync(request.BlobName, request.Container);
    }
}