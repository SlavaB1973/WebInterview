using Serilog;
using ToDoServer.Middleware;
using ToDoServer.Model;
using ToDoServer.Observer;
using ToDoServer.Services;

namespace ToDoServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, configuration) =>
                 configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            builder.Services.AddSingleton<IFileSaveObserver, ConsoleFileSaveObserver>();
            builder.Services.AddSingleton<IFileSaveObserver, LogFileSaveObserver>();

            builder.Services.AddSingleton<ITodoService,TodoService>(); 

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

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ResponseTimeMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
