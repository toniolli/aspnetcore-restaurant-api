using Domain.Entities;
using Domain.Validation;

public class UsuarioPerfil
{
    public string UsuarioId {get; private set;}
    public int PerfilId {get; private set;}
    public Perfil Perfil {get; private set;}

    protected UsuarioPerfil() { }

    public UsuarioPerfil(string usuarioId,int perfilId)
    {
        ValidateDomain(usuarioId, perfilId);

        UsuarioId = usuarioId;
        PerfilId = perfilId;
    }

    private void ValidateDomain(string usuarioId,int perfilId)
    {
        DomainExceptionValidation.when(string.IsNullOrWhiteSpace(usuarioId), "Usuário inválido");
        DomainExceptionValidation.when(perfilId <= 0,"Perfil inválido");
    }
}