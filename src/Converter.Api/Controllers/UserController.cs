using Converter.Application.Contracts;
using Converter.Application.Models.User;
using Converter.Application.Services.User;
using Converter.Domain.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Converter.Api.Controllers;

[Route("[controller]")]
public class UserController : ApiControllerBase
{
    private readonly IUserService userService;
    private readonly IJwtHandler jwtHandler;
    private readonly IValidator<RegistrationRequest> registrationValidator;
    private readonly IValidator<LoginRequest> loginValidator;

    public UserController(
        IUserService userService,
        IJwtHandler jwtHandler,
        IValidator<RegistrationRequest> registrationValidator,
        IValidator<LoginRequest> loginValidator)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.jwtHandler = jwtHandler ?? throw new ArgumentNullException(nameof(jwtHandler));
        this.registrationValidator = registrationValidator ?? throw new ArgumentNullException(nameof(registrationValidator));
        this.loginValidator = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));;
    }

    [Authorize]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAsync(
        [FromBody] RegistrationRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await registrationValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(ResponseFromValidationResult(validationResult));
        }

        var user = new User(request.Login, request.Password);
        await userService.RegisterUser(user, cancellationToken);

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody]LoginRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await loginValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(ResponseFromValidationResult(validationResult));
        }
        
        var userExist = await userService.IsExistsAsync(request.Login, request.Password, cancellationToken);
        if (!userExist)
        {
            return Unauthorized();
        }
        
        var token = jwtHandler.GenerateToken(request.Login);
        
        return Ok(new LoginResponse { Token = token });
    }
}
