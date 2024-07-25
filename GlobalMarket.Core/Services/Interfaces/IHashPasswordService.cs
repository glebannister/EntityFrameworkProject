namespace GlobalMarket.Core.Services.Interfaces
{
    public interface IHashPasswordService
    {
        public byte[] HashPassword(string password, byte[] salt);
    }
}
