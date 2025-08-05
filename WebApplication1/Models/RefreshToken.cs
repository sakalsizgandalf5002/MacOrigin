namespace WebApplication1.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public Guid Token { get; set; }
        public DateTime Expires { get; set; }
        public bool Revoked { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? RevokedDate { get; set; }
        public string RevokedByIp { get; set; }
        public Guid? ReplacedById { get; set; }
    }
}
