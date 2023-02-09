using API.Cache;
using API.Supabase;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICachingService, CachingService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("localhost");
    options.InstanceName = "SampleInstance";
});

builder.Services.AddAuthorization();

var url = "https://ykufzapzxvesgbkgynae.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlrdWZ6YXB6eHZlc2dia2d5bmFlIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY3NTc3NDIzOCwiZXhwIjoxOTkxMzUwMjM4fQ.KdRWFf_0oJ3esbJYmjpbYJV_fjMnEasbP4o9wDgKzQM";

builder.Services.AddScoped<Client>(_ => new Client(
        url,
        key,
        new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true,
            PersistSession = true,
            SessionHandler = new DefaultSupabaseSessionHandler()
        }
    )
);

builder.Services.AddScoped<ISupabaseService, SupabaseService>();

var app = builder.Build();

app.Map("localhost:5019/supabase/create", HandleMapTest1);

static void HandleMapTest1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        Console.WriteLine(context.Request.Body);
    });
}

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