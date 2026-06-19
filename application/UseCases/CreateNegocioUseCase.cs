using application.DTOs.Entrada;
using application.DTOs.Saida;
using application.Interfaces;
using domain.Entities;
using domain.Ports;

namespace application.UseCases
{
    public class CreateNegocioUseCase : ICreateNegocioUseCase
    {
        private readonly INegocioRepository _negocioRepository;
        private readonly IServicoSeguranca _servicoSeguranca;

        public CreateNegocioUseCase(INegocioRepository negocioRepository, IServicoSeguranca servicoSeguranca    )
        {
            _negocioRepository = negocioRepository;
            _servicoSeguranca = servicoSeguranca;
        }

        public async Task<CreateNegocioResponse> ExecuteAsync(CreateNegocioRequest req)
        {
            var senhaCriptografada = _servicoSeguranca.HashSenha(req.Senha);
            var negocio = new NegocioEntity(req.Nome, req.Email, req.Documento, req.Telefone, senhaCriptografada);
            var createdNegocio = await _negocioRepository.CreateAsync(negocio);
            return new CreateNegocioResponse(createdNegocio.Id, createdNegocio.Nome, createdNegocio.Email, createdNegocio.Documento, createdNegocio.Telefone);
        }
    }
}
