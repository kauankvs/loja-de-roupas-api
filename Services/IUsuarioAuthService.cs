namespace LojaDeRoupasAPI.Services
{
    public interface IUsuarioAuthService
    {
        public Task<bool> VerificarSeSenhaECorretaAsync(string senha, string email);
        public Task<bool> VerificarSeUsuarioExisteAsync(string email);
    }
}
