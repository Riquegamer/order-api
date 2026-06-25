using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Entities;
using domain.Ports;

namespace application.UseCases
{
    public class CriarNegocioUseCase : ICriarNegocioUseCase
    {
        #region Propriedades

        private readonly INegocioRepository _negocioRepository;
        private readonly IServicoSeguranca _servicoSeguranca;

        #endregion

        #region Construtores

        public CriarNegocioUseCase(INegocioRepository negocioRepository, IServicoSeguranca servicoSeguranca)
        {
            _negocioRepository = negocioRepository;
            _servicoSeguranca = servicoSeguranca;
        }

        #endregion

        #region Metodos

        public async Task<NegocioResponse> ExecuteAsync(CreateNegocioRequest req)
        {
            var negocioExistente = _negocioRepository.GetByEmailAsync(req.Email);

            if (negocioExistente != null) 
            {
                throw new Exception($"O e-mail {req.Email} já esta sendo ultilizado por outro negocio");
            }

            var senhaCriptografada = _servicoSeguranca.HashSenha(req.Senha);
            var negocio = new NegocioEntity(req.Nome, req.Email, req.Documento, req.Telefone, senhaCriptografada);
            var createdNegocio = await _negocioRepository.CreateAsync(negocio);
            return new NegocioResponse(createdNegocio.Id, createdNegocio.Nome, createdNegocio.Email, createdNegocio.Documento.Valor, createdNegocio.Telefone.Valor);

        }

        #endregion
    }
}
