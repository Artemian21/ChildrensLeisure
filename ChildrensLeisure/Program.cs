using ChildrensLeisure.DataAccess;
using ChildrensLeisure.Domain.Abstractions;
using ChildrensLeisure.DataAccess.Repositories;
using ChildrensLeisure.Domain.Mapper;
using Microsoft.EntityFrameworkCore;
using ChildrensLeisure.BLL.Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
    containerBuilder.RegisterType<OrderRepository>().As<IOrderRepository>();
    containerBuilder.RegisterType<AttractionRepository>().As<IAttractionRepository>();
    containerBuilder.RegisterType<EventProgramRepository>().As<IEventProgramRepository>();
    containerBuilder.RegisterType<FairyCharacterRepository>().As<IFairyCharacterRepository>();
    containerBuilder.RegisterType<ZoneRepository>().As<IZoneRepository>();
    containerBuilder.RegisterType<AttractionService>().As<IAttractionService>();
    containerBuilder.RegisterType<EventProgramService>().As<IEventProgramService>();
    containerBuilder.RegisterType<FairyCharacterService>().As<IFairyCharacterService>();
    containerBuilder.RegisterType<ZoneService>().As<IZoneService>();
    containerBuilder.RegisterType<OrderService>().As<IOrderService>();
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<ChildrensLeisureDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ChildrensLeisureDBContext")));

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
