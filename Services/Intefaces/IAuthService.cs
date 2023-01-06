namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IAuthService
    {
        public Task<string> CriarTokenAsync(string email);
    }
}
