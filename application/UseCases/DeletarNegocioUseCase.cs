using application.DTOs.Entrada;
using application.Interfaces;
using domain.Ports;

namespace application.UseCases
{
    public class DeletarNegocioUseCase : IDeletarNegocioUseCase
    {
        #region Propriedades
        
        private readonly INegocioRepository _negocioRepository;
        private readonly IServicoSeguranca _servicoSeguranca;

        #endregion

        #region Construtores

        public DeletarNegocioUseCase(INegocioRepository negocioRepository, IServicoSeguranca servicoSeguranca)
        {
            _negocioRepository = negocioRepository;
            _servicoSeguranca = servicoSeguranca;
        }

        #endregion

        #region Metodos

        public async Task<bool> ExecuteAsync(Guid id, DeletarNegocioRequest req)
        {
            var negocio = await _negocioRepository.GetByIdAsync(id);
            if (negocio == null) return false;
            
            bool senha = _servicoSeguranca.VerificarSenha(req.Senha, negocio.SenhaHash);
            if(!senha) return false;

            negocio.Deletar();

            var deletedNegocio = await _negocioRepository.UpdateAsync(negocio);
            return true;
        }

        #endregion
    }
}
