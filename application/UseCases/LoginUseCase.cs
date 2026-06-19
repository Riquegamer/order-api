using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly INegocioRepository _negocioRepository;
        private readonly IServicoSeguranca _servicoSeguranca;

        public LoginUseCase (INegocioRepository negocioRepository, IServicoSeguranca servicoSeguranca)
        {
            _negocioRepository = negocioRepository;
            _servicoSeguranca = servicoSeguranca;
        }

        public async Task<LoginResponse?> ExecuteAsync(LoginRequest req)
        {
            var negocio = await _negocioRepository.GetByEmailAsync(req.Email);

            if (negocio == null) return null;

            var senhaValida = _servicoSeguranca.VerificarSenha(req.Senha, negocio.SenhaHash);

            if (!senhaValida) return null;

            var token = _servicoSeguranca.GerarToken(negocio.Id, negocio.Email.Valor);

            return new LoginResponse(negocio.Email.Valor, token);
        }
    }
}
