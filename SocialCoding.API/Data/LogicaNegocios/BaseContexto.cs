namespace SocialCoding.API.Data.LogicaNegocios
{
    public class BaseContexto
    {
        protected readonly SocialCodingContext _contexto;

        public BaseContexto(SocialCodingContext contexto) => _contexto = contexto;
    }
}