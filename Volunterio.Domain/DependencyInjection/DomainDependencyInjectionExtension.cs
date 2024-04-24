using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volunterio.Domain.Mapper.Profiles;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Change;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Set;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Services.Realization;
using Volunterio.Domain.Settings.Abstraction;
using Volunterio.Domain.Settings.Realization;
using Volunterio.Domain.Validators;

namespace Volunterio.Domain.DependencyInjection;

public static class DomainDependencyInjectionExtension
{
    public static IServiceCollection RegisterDomainLayer(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddServices()
        .AddValidators()
        .AddSettings(configuration)
        .AddMapper();

    private static IServiceCollection AddServices(
        this IServiceCollection services
    ) => services
        .AddHttpContextAccessor()
        .AddScoped<IValidationService, ValidationService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IRoleService, RoleService>()
        .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
        .AddScoped<IEmailService, EmailService>()
        .AddScoped<ICurrentUserService, CurrentUserService>()
        .AddScoped<IUserAccessService, UserAccessService>();

    private static IServiceCollection AddValidators(
        this IServiceCollection services
    ) => services
        .AddValidator<LoginModel, LoginModelValidator>()
        .AddValidator<ChangePasswordModel, ChangePasswordModelValidator>()
        .AddValidator<CreateRoleModel, CreateRoleModelValidator>()
        .AddValidator<CreateUserModel, CreateUserModelValidator>()
        .AddValidator<RegisterUserModel, RegisterUserModelValidator>()
        .AddValidator<ResetPasswordModel, ResetPasswordModelValidator>()
        .AddValidator<SetNewPasswordModel, SetNewPasswordModelValidator>()
        .AddValidator<UpdateRoleModel, UpdateRoleModelValidator>()
        .AddValidator<UpdateUserModel, UpdateUserModelValidator>()
        .AddValidator<ChangeUserRoleModel, ChangeUserRolesModelValidator>()
        .AddValidator<UpdateProfileModel, UpdateProfileModelValidator>()
        .AddValidator<CompleteRegistrationModel, CompleteRegistrationModelValidator>();

    private static IServiceCollection AddValidator<TModel, TValidator>(
        this IServiceCollection services
    )
        where TModel : class, IValidatableModel
        where TValidator : class, IValidator<TModel> => services
        .AddTransient<IValidator<TModel>, TValidator>();

    private static IServiceCollection AddMapper(
        this IServiceCollection services
    ) => services
        .AddAutoMapper(config => config.AddProfiles(
        [
            new RoleMapperProfile(),
            new UserMapperProfile()
        ]));

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var emailSettings = new EmailSettings();
        var urlSettings = new UrlSettings();
        var jwtSettings = new JwtSettings();

        configuration
            .GetSection(nameof(EmailSettings))
            .Bind(emailSettings);

        configuration
            .GetSection(nameof(UrlSettings))
            .Bind(urlSettings);

        configuration
            .GetSection(nameof(JwtSettings))
            .Bind(jwtSettings);

        services
            .AddSingleton<IEmailSettings, EmailSettings>(_ => emailSettings)
            .AddSingleton<IUrlSettings, UrlSettings>(_ => urlSettings)
            .AddSingleton<IJwtSettings, JwtSettings>(_ => jwtSettings);

        return services;
    }
}