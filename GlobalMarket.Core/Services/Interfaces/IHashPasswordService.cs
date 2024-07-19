namespace GlobalMarket.Core.Services.Interfaces
{
    public interface IHashPasswordService
    {
        public string HashPassword(string password, byte[] salt);
    }
}
