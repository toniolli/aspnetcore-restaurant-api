using Application.Dto;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Infra.Identity
{
    public class UsuarioService : IusuarioService
    {
        private readonly UserManager<ApplicationUser> _applicationUser;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly IusuarioPerfilRepository _usuarioPerfilRepository;
        private readonly IperfilPermissaoRepository _perfilPermissaoRepository;

        public UsuarioService(
            UserManager<ApplicationUser> applicationUser,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IusuarioPerfilRepository usuarioPerfilRepository,
            IperfilPermissaoRepository perfilPermissaoRepository
            )
        {
            _applicationUser = applicationUser;
            _signInManager = signInManager;
            _configuration = configuration;
            _usuarioPerfilRepository = usuarioPerfilRepository;
            _perfilPermissaoRepository = perfilPermissaoRepository;
        }

        // Trocar senha
        public async Task<string> ChangePassword(
            string userIdLogado,
            ChangePasswordDto dto,
            bool isAdmin)
        {
            var user = await _applicationUser.FindByIdAsync(userIdLogado);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            if (dto.NovaSenha != dto.ConfirmacaoNovaSenha)
                throw new Exception("Senha e confirmação não conferem");

            IdentityResult result;

            if (!isAdmin)
            {
                if (string.IsNullOrWhiteSpace(dto.PasswordAtual))
                    throw new Exception("Senha atual é obrigatória");

                result = await _applicationUser.ChangePasswordAsync(
                    user,
                    dto.PasswordAtual,
                    dto.NovaSenha);
            }
            else
            {
                var token = await _applicationUser
                    .GeneratePasswordResetTokenAsync(user);

                result = await _applicationUser.ResetPasswordAsync(
                    user,
                    token,
                    dto.NovaSenha);
            }

            if (!result.Succeeded)
            {
                var erros = string.Join(
                    " | ",
                    result.Errors.Select(e => e.Description));

                throw new Exception(erros);
            }

            return "Senha alterada com sucesso";
        }

        // Buscar todos
        public async Task<IEnumerable<CreateUserDto>> BuscarTodos()
        {
            var usuarios = await _applicationUser.Users
                .Select(u => new CreateUserDto
                {
                    Username = u.UserName!,
                    Email = u.Email!,
                    Password = string.Empty,
                    PasswordConfirmation = string.Empty
                })
                .ToListAsync();

            return usuarios;
        }

        // Criar usuário
        public async Task<CreateUserDto> CriarUsuario(
            CreateUserDto createDTO)
        {
            if (createDTO.Password != createDTO.PasswordConfirmation)
                throw new Exception("As senhas não são iguais");

            var user = new ApplicationUser
            {
                UserName = createDTO.Username,
                Email = createDTO.Email
            };

            var result = await _applicationUser.CreateAsync(
                user,
                createDTO.Password);

            if (!result.Succeeded)
            {
                var erros = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw new Exception(erros);
            }

            

            return createDTO;
        }

        // Login
        public async Task<string> Login(LoginDto loginDTO)
        {
            var user = await _applicationUser
                .FindByNameAsync(loginDTO.UserName);

            if (user == null)
                return null!;

            var result = await _signInManager
                .CheckPasswordSignInAsync(
                    user,
                    loginDTO.Password,
                    false);

            if (!result.Succeeded)
                return null!;

            return await GenerateJwtToken(user);
        }

        // Excluir usuário
        public async Task<CreateUserDto?> Excluir(string id)
        {
            var user = await _applicationUser
                .FindByIdAsync(id);

            if (user == null)
                return null;

            var usuarioDTO = new CreateUserDto
            {
                Username = user.UserName!,
                Email = user.Email!,
                Password = string.Empty,
                PasswordConfirmation = string.Empty
            };

            var result = await _applicationUser.DeleteAsync(user);

            if (!result.Succeeded)
                return null;

            return usuarioDTO;
        }

        // Gerar JWT
        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings =
                _configuration.GetSection("Jwt");

            var roles =
                await _applicationUser.GetRolesAsync(user);

            var perfis =
                await _usuarioPerfilRepository
                    .BuscarPerfisUsuario(user.Id);

            var claims = new List<Claim>
             {
                new Claim(
                    ClaimTypes.Email,
                    user.Email ?? string.Empty),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName ?? string.Empty),

                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id)
             };

            // Roles do Identity
            claims.AddRange(
                roles.Select(role =>
                    new Claim(
                        ClaimTypes.Role,
                        role)));

            // Perfis e permissões
            foreach (var perfil in perfis)
            {
                claims.Add(
                    new Claim(
                        "Perfil",
                        perfil.Nome));

                var permissoes =
                    await _perfilPermissaoRepository
                        .BuscarPermissoesPerfil(perfil.Id_Perfil);

                foreach (var permissao in permissoes)
                {
                    if (!claims.Any(c =>
                        c.Type == "Permissao" &&
                        c.Value == permissao.Nome))
                    {
                        claims.Add(
                            new Claim(
                                "Permissao",
                                permissao.Nome));
                    }
                }
            }

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtSettings["Key"]!));

                var creds = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler()
                    .WriteToken(token);
        }

    }
}