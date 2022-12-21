namespace LojaDeRoupasAPI.Services.Intefaces
{
    public interface IAuthService
    {
        public void TransformarSenhaEmHashESalt(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        public Task<string> CriarTokenAsync(string email);
    }
}
