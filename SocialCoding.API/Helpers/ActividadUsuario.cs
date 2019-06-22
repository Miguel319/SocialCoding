using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SocialCoding.API.Data.IRepositorio;

namespace SocialCoding.API.Helpers
{
    public class ActividadUsuario : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultado = await next();
            var usuarioId = int.Parse(resultado.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultado.HttpContext.RequestServices.GetService<ICoderos>();
            var usuario = await repo.ObtenerUsuario(usuarioId);
            usuario.UltimaSesion = DateTime.Now;
            await repo.Guardar();
        }
    }
}