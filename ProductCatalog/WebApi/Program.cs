using Application.CategoryService;
using Application.ProductService;
using Application.ServiceLifecycleDemo;
using Persistence.CategoryRepository;
using Persistence.ProductRepository;
using Persistence.Utilities;
using WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


builder.Services.AddSingleton<SingletonNumberService>();
builder.Services.AddScoped<ScopedNumberService>();
builder.Services.AddTransient<TransientNumberService>();

// Register application service as scoped
builder.Services.AddScoped<IApplicationService, ApplicationService>();


builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");


app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
