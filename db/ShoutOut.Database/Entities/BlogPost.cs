namespace ShoutOut.Database.Entities;

public class BlogPost
{
    public long Id { get; set; }
    public User User { get; set; }
    public long UserId { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public DateTime DateAdded { get; set; }
}