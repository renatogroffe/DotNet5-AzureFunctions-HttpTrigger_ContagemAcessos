using System;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionAppContagem.Models;

namespace FunctionAppContagem
{
    public class ValorAtual
    {
        private Contador _contador;

        public ValorAtual(Contador contador)
        {
            _contador = contador;
        }

        [Function("ValorAtual")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("ValorAtual");
            logger.LogInformation("Requisição HTTP recebida...");

            int valorAtualContador;
            lock (_contador)
            {
                _contador.Incrementar();
                valorAtualContador = _contador.ValorAtual;
            }

            if (Convert.ToBoolean(Environment.GetEnvironmentVariable("SimularFalha")) &&
                valorAtualContador % 4 == 0)
            {
                logger.LogError("Simulando falha...");
                throw new Exception("Simulação de falha!");
            }

            logger.LogInformation($"Contador - Valor atual: {valorAtualContador}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(new ResultadoContador(valorAtualContador));
            return response;
        }
    }
}