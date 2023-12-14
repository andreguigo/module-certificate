using System.Security.Claims;
using System.Text;
using Certificate;
using Certificate.Models;
using Certificate.Repositories;
using Certificate.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var key  = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthentication( x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization( options => {
    options.AddPolicy("Admin", police => police.RequireRole("admin"));
    options.AddPolicy("User", police => police.RequireRole("user"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var repositoryAttested = new AttestedRepository(true);
var repositoryTestimonial = new TestimonialRepository(true);

// Login
app.MapPost("/login", (User model) => 
{
    var user = UserRepository.Get(model.Username, model.Password);

    if (user == null)
        return Results.NotFound(new { message = "Username or password is invalid" });
    
    var token = TokenService.GenerateToken(user);

    user.Password = "";

    return Results.Ok(new 
    {
        user = user,
        token = token
    });
});

// app.MapGet("/anonymous", () => { Results.Ok("anonymous"); });
app.MapGet("/anonymous", () => { Results.Ok("anonymous"); }).AllowAnonymous();

app.MapGet("/authenticated", (ClaimsPrincipal user) => 
{ 
    Results.Ok(new { message = $"Authenticaded as {user.Identity.Name}" }); 
}).RequireAuthorization();

app.MapGet("/user", (ClaimsPrincipal user) => 
{ 
    Results.Ok(new { message = $"Authenticaded as {user.Identity.Name}" }); 
}).RequireAuthorization("User");

app.MapGet("/admin", (ClaimsPrincipal user) => 
{ 
    Results.Ok(new { message = $"Authenticaded as {user.Identity.Name}" }); 
}).RequireAuthorization("Admin");

// Attested
app.MapGet("/attested", () => { 
    return repositoryAttested.SelectAllAttested(); 
});

// Testimonial
app.MapGet("/testimonial", () => 
{ 
    return repositoryTestimonial.SelectAllTestimonial(); 
});

app.MapGet("/testimonial/{id}", (int id) => 
{
    return repositoryTestimonial.SelectTestimonial(id);
});

// Check Authenticity
app.MapGet("/{licensed}", (string licensed) => 
{
    var att = repositoryAttested.SelectAttested(licensed);
    var tes = repositoryTestimonial.SelectTestimonial(att.TestimonialId);

    return new CertificateModel() { Name = att.Name, Licensed = att.Licensed, Event = tes.Event, TextEvent = tes.Text, DateEvent = tes.DateEvent, Workload = tes.Workload, DateSign = att.DateSign } ;
});

app.Run();
