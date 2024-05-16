using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fiap.Ingresso.Evento.API.AtributosAutorizacao;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public CustomAuthorizeAttribute(string isAdminValue)
    {
        IsAdminValue = isAdminValue;
    }

    public string IsAdminValue { get; set; }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!context.HttpContext.User.HasClaim(c => c.Type == "IsAdmin" && c.Value.Equals(IsAdminValue, StringComparison.OrdinalIgnoreCase)))
        {
            context.Result = new ForbidResult();
            return;
        }

        await Task.CompletedTask;
    }
}
