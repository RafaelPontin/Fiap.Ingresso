using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.WebAPI.Core.Controllers
{
    [ApiController]
    public abstract class BaseController : Controller    
    {
        //protected ICollection<string> Erros = new List<string>();


        //protected ActionResult CustomResponse(object result = null)
        //{
        //    if (OperacaoValida())
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        //    {
        //        { "Mensagens", Erros.ToArray() }
        //    }));
        //}

        //protected bool OperacaoValida()
        //{
        //    return !Erros.Any();
        //}

        //protected void AdicionarErroProcessamento(string erro)
        //{
        //    Erros.Add(erro);
        //}
    }
}
