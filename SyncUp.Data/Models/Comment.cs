using System.Text.RegularExpressions;

namespace IntelliTect.SyncUp.Data.Models;

public class Comment : TenantedBase
{
    public long CommentId { get; init; }

    [ListText]
    public required string Body { get; set; }

    public long PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post? Post { get; set; }

    public int LikeCount { get; set; }

    [DefaultDataSource]
    public class CommentsForPost(CrudContext<AppDbContext> context) : StandardDataSource<Comment, AppDbContext>(context)
    {
        [Coalesce]
        public long? PostId { get; set; }

        public override IQueryable<Comment> GetQuery(IDataSourceParameters parameters)
        {
            return base.GetQuery(parameters).Where(c => c.PostId == PostId);
        }

        public override IQueryable<Comment> ApplyListDefaultSorting(IQueryable<Comment> query)
        {
            return base.ApplyListDefaultSorting(query).OrderByDescending(x => x.CreatedOn);
        }
    }
}