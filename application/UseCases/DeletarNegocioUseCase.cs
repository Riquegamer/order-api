using application.DTOs.Entrada;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class DeletarNegocioUseCase : IDeletarNegocioUseCase
    {
        private readonly INegocioRepository _negocioRepository;
        private readonly IServicoSeguranca _servicoSeguranca;

        public DeletarNegocioUseCase(INegocioRepository negocioRepository, IServicoSeguranca servicoSeguranca)
        {
            _negocioRepository = negocioRepository;
            _servicoSeguranca = servicoSeguranca;
        }

        public async Task<bool> ExecuteAsync(Guid id, DeletarNegocioRequest req)
        {
            var negocio = await _negocioRepository.GetByIdAsync(id);
            if (negocio == null) return false;
            
            bool senha = _servicoSeguranca.VerificarSenha(req.senha, negocio.SenhaHash);
            if(!senha) return false;

            negocio.Deletar();

            var deletedNegocio = await _negocioRepository.UpdateAsync(negocio);
            return true;
        }
    }
}
