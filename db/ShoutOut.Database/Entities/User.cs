namespace ShoutOut.Database.Entities;

public class User
{
    public long Id { get; set; }

    
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string NickName { get; set; }
    public Guid AvatarId { get; set; }
}