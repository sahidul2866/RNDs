using Microsoft.EntityFrameworkCore;
using ProjectApi.CustomOptions;
using SQL_CRUD_X_HashiCorp_Vault.Data;
using System.Configuration;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddVault(options =>
{
    var vaultOptions = builder.Configuration.GetSection("Vault");
    options.Address = vaultOptions["Address"];
    options.Role = vaultOptions["Role"];
    options.MountPath = vaultOptions["MountPath"];
    options.SecretType = vaultOptions["SecretType"];
    options.Secret = vaultOptions["Secret_Id"];
});

builder.Services.Configure<VaultOptions>(builder.Configuration.GetSection("Vault"));
var dbBuilder = new SqlConnectionStringBuilder(
       builder.Configuration.GetConnectionString("Database")
     );


//dbBuilder.UserID = "sa";
//dbBuilder.Password = "oLdViCtOrY2008";
dbBuilder.UserID = builder.Configuration["database:userID"];
dbBuilder.Password = builder.Configuration["database:password"];

builder.Configuration.GetSection("ConnectionStrings")["Database"] = dbBuilder.ConnectionString;

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
