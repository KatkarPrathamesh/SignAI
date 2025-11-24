//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Register application services
//builder.Services.AddScoped<SignAi.Services.AuthService>();
//builder.Services.AddScoped<SignAi.Repositories.UserRepository>();
//builder.Services.AddScoped<SignAi.Services.DbConnectionFactory>();
//var app = builder.Build();


//app.UseSwagger(c =>

//{

//    // c.RouteTemplate = "swagger/{documentname}/swagger.json";

//    c.RouteTemplate = "SignAi/swagger/{documentname}/swagger.json";

//});

//// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),

//// specifying the Swagger JSON endpoint.

//app.UseSwaggerUI(c =>

//{

//    //c.RoutePrefix = "swagger";

//    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

//    c.RoutePrefix = "SignAi/swagger";

//    c.SwaggerEndpoint("/SignAi/swagger/v1/swagger.json", ".Net Framework 8.0");

//    c.DisplayRequestDuration();

//});


//// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())

//{

//    app.UseSwagger();

//    app.UseSwaggerUI();

//}

//app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

//app.UseHttpsRedirection();

//// app.UseSerilogRequestLogging();

//app.UseAuthentication();

//app.UseAuthorization();

//app.UseCors("CorsPolicy");

//app.MapControllers();

//app.Run();


//using Microsoft.OpenApi.Models;
//using SignAI.Repositories.IRepositories;
//using SignAI.Repositories;
//using SignAI.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container
//builder.Services.AddControllers();

//// Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "SignAi API",
//        Version = "v1",
//        Description = "API for user registration, login, and meeting management"
//    });
//});

//// CORS (allow all origins for testing)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//        builder.AllowAnyOrigin()
//               .AllowAnyHeader()
//               .AllowAnyMethod());
//});

//// Register application services
//builder.Services.AddSingleton<DbConnectionFactory>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<AuthMeetingService>();

//var app = builder.Build();

//// Swagger middleware
//app.UseSwagger(); // JSON endpoint at /swagger/v1/swagger.json
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignAi API v1");
//    c.RoutePrefix = "swagger"; // UI at /swagger/index.html
//    c.DisplayRequestDuration();
//});

//// Configure middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

//app.UseHttpsRedirection();
//// app.UseCors("CorsPolicy");
//app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.OpenApi.Models;
using SignAI.Repositories.IRepositories;
using SignAI.Repositories;
using SignAI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SignAi API",
        Version = "v1",
        Description = "API for user registration, login, and meeting management"
    });
});

// =========================
// 🔥 FIXED CORS POLICY
// =========================
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//        builder.WithOrigins(
//                "http://localhost:3000",
//                "http://127.0.0.1:3000"
//            )
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials()
//        );
//});

builder.Services.AddCors(options =>

{

    options.AddPolicy("CorsPolicy", cors =>

        cors.WithOrigins(

            "http://localhost:5173",

            "http://127.0.0.1:5173",

            // keep 3000 if you sometimes run CRA/Next:

            "http://localhost:3000",

            "http://127.0.0.1:3000"

        // add your deployed UI later, e.g.:

        // "https://tornatitans.shauryatechnosoft.com"

        )

        .AllowAnyHeader()

        .AllowAnyMethod()

    // .AllowCredentials() // <— ONLY if you actually use cookies/sessions

    );

});


// Register services
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthMeetingService>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignAi API v1");
    c.RoutePrefix = "swagger";
});

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// use FIXED policy
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


