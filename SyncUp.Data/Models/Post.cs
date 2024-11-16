using System.Text.RegularExpressions;

namespace IntelliTect.SyncUp.Data.Models;

public class Post : TenantedBase
{
    public long PostId { get; init; }

    [MaxLength(500)]
    [Required]
    [ListText]
    [Search(SearchMethod = SearchMethods.Contains)]
    public required string Title { get; set; }

    [Required]
    [Search(SearchMethod = SearchMethods.Contains)]
    public required string Body { get; set; }

    [Required]
    public long GroupId { get; set; }

    [ForeignKey(nameof(GroupId))]
    public Group? Group { get; set; }

    public int PostLikeCount { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];

    [DefaultDataSource]
    public class PostsForGroup(CrudContext<AppDbContext> context) : StandardDataSource<Post, AppDbContext>(context)
    {
        [Coalesce]
        public long? GroupId { get; set; }

        public override IQueryable<Post> GetQuery(IDataSourceParameters parameters)
        {
            if (GroupId is null)
            {
                return base.GetQuery(parameters);
            }

            return base.GetQuery(parameters).Where(x => x.GroupId == GroupId);
        }

        public override IQueryable<Post> ApplyListDefaultSorting(IQueryable<Post> query)
        {
            return base.ApplyListDefaultSorting(query).OrderByDescending(x => x.CreatedOn);
        }
    }
}