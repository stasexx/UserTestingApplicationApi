using System.Security.Claims;
using Application.IRepositories;
using Application.IRepositories.Identity;
using Application.IServices;
using Application.IServices.Identity;
using Application.Models.Dtos;
using Application.Models.Identity;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    private readonly ITokenService _tokensService;

    private readonly IRefreshTokensRepository _refreshTokensRepository;

    private readonly IMapper _mapper;

    public UserService(IUserRepository? userRepository, ITokenService tokensService, IRefreshTokensRepository refreshTokensRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokensService = tokensService;
        _refreshTokensRepository = refreshTokensRepository;
        _mapper = mapper;
    }

    public async Task<TokensModel> AddUserAsync(UserLoginDto userDto, CancellationToken cancellationToken)
    {
        try
        {
            var user = new User
            {
                Name = userDto.Name,
                UserTests = new List<UserTest>(),
                Id = Guid.NewGuid()
            };
                
            
            await _userRepository.AddAsync(user, cancellationToken);
            
            var refreshToken = await AddRefreshToken(user.Id, cancellationToken);
            var tokens = GetUserTokens(user, refreshToken);

            return tokens;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<TokensModel> LoginAsync(UserLoginDto userDto, CancellationToken cancellationToken)
    {
        var user = await  _userRepository.GetOneAsync(u => u.Name == userDto.Name, cancellationToken);

        if (user == null)
        { 
            return await AddUserAsync(userDto, cancellationToken);
        }
        
        var refreshToken = await AddRefreshToken(user.Id, cancellationToken);

        var tokens = this.GetUserTokens(user, refreshToken);

        return tokens;
    }
    
    public async Task<UserDto> GetUserAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetOneAsync(x=>x.Name == name, cancellationToken);
        if (entity == null)
        {
            throw new Exception(name);
        }

        return _mapper.Map<UserDto>(entity);
    }
    
    private TokensModel GetUserTokens(User user, RefreshToken refreshToken)
    {
        var claims = this.GetClaims(user);
        var accessToken = this._tokensService.GenerateAccessToken(claims);


        return new TokensModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        };
    }
    
    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Name ?? string.Empty)
        };

        return claims;
    }
    
    private async Task<RefreshToken> AddRefreshToken(Guid userId, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = _tokensService.GenerateRefreshToken(),
            ExpiryDateUTC = DateTime.UtcNow.AddDays(10),
        };

        await this._refreshTokensRepository.AddAsync(refreshToken, cancellationToken);

        return refreshToken;
    }
}