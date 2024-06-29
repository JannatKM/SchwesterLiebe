using Beauty.Repository.Contracts;
using Beauty.Repository.Data;
using Beauty.Repository.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IEmployeeTimeRepository, EmployeeTimeRepository>();
builder.Services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepository>();
builder.Services.AddScoped<IEmployeeCalendarRepository, EmployeeCalendarRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), b => b.MigrationsAssembly("Beauty.Web"));
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"),
        b => b.MigrationsAssembly("Beauty.Web"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
