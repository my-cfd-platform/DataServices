using CommentFlows;
using DataServices.Extensions;
using DataServices.Models;
using DataServices.Services.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;

namespace DataServices.Services;

public class CommentsService : ICommentsService
{
    private readonly CommentService.CommentServiceClient? _commentClient;
    public CommentsService(DataServicesSettings settings)
    {
        if (settings.CommentGrpcServiceUrl.IsNotNullOrEmpty())
        {
            _commentClient = new CommentService.CommentServiceClient(
                GrpcChannel.ForAddress(settings.CommentGrpcServiceUrl));
        }
    }

    public async Task PostCommentAsync(CommentGrpcModel model)
    {
        await _commentClient!.SetCommentAsync(model);
    }

    public async Task<IEnumerable<CommentGrpcModel>> GetCommentsAsync(string category)
    {
        return
            (await _commentClient!.GetCommentsAsync(
                new RequestByCategory { Category = category }
            ))
            .Data?.ToList()!;
    }

    public async Task<Dictionary<string, List<CommentGrpcModel>>> SearchCommentsAsync(SearchComments search)
    {
        var dataStream =
            _commentClient!.Search(search).ResponseStream;

        var data = new Dictionary<string, List<CommentGrpcModel>>();
        while (await dataStream.MoveNext())
        {
            var traderId = dataStream.Current.Category!;
            data.TryAdd(traderId, new ()); 
            data[traderId].Add(dataStream.Current);
        }
        return data;

    }

}
