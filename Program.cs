using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enums;
using ToDoApp.Infraestructure.Data;
using ToDoApp.Infraestructure.Repositories;


namespace ToDoAppWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var useInMemoryDB = builder.Configuration.GetValue<bool>("UseInMemoryDB");

       

           //builder.Services.AddControllers(options =>
           //{
           //    options.Filters.Add<GlobalExceptionFilter>();
           //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ToDoApp API",
                    Version = "v1",
                     Description = "API para gestionar tareas",
                });
            });

            builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IToDoListService, ToDoListService>();

            builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();


            if (useInMemoryDB)
            {
                builder.Services.AddInMemoryDatabase();
            }
            else
            {
                //use this for real database on your sql server
                builder.Services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure()
                    );
                }
                  );
            }
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(); // si vas a usar autenticación más adelante
            builder.Services.AddAuthorization();  // esto sí es obligatorio si usás [Authorize]

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoApp API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();



            app.MapControllers();

            if (useInMemoryDB)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    SeedData(context); 
                }
            }
            else
            {
                
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated(); 
                }
            }

            app.Run();
        }

        private static void SeedData(AppDbContext context)
        {
            context.Users.AddRange(
                new User { UserId = 1, Username = "JohnDoe", Email = "john@example.com", CreatedAt = DateTime.Now },
                new User { UserId = 2, Username = "JaneDoe", Email = "jane@example.com", CreatedAt = DateTime.Now }
            );

            context.ToDoLists.AddRange(
                new ToDoList { ToDoListId = 1, UserId = 1, Title = "Groceries", Description = "Things to buy", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ToDoList { ToDoListId = 2, UserId = 1, Title = "Work", Description = "Work-related tasks", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            context.ToDoItems.AddRange(
                new ToDoItem { ToDoItemId = 1, ToDoListId = 1, Title = "Buy milk", Description = "Get whole milk", DueDate = DateTime.Now.AddDays(2), IsCompleted = false, Priority = PriorityLevel.Medium, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new ToDoItem { ToDoItemId = 2, ToDoListId = 1, Title = "Buy eggs", Description = "Get a dozen eggs", DueDate = DateTime.Now.AddDays(3), IsCompleted = false, Priority = PriorityLevel.High, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            );

            context.SaveChanges(); // Save changes to the in-memory database
        }
    }
}