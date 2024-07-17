using AutoMapper;
using Data;
using Data.Contract;
using Service;
using Service.Contracts;

namespace CompanyClaimsApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<IDataLoader, DataLoader>();
        builder.Services.AddTransient<ICompanyService, CompanyService>();
        builder.Services.AddTransient<IClaimService, ClaimService>();

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.CreateMap<Entities.Models.Company, Entities.Dto.CompanyDto>().ReverseMap();
            mc.CreateMap<Entities.Models.Claim, Entities.Dto.ClaimDto>().ReverseMap();
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company Claims API V1");
        });

        app.Run();
    }
}
