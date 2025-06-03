using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TodoAppv2.Komp; // <-- twoje komponenty i TodoContext

var builder = WebApplication.CreateBuilder(args);

// 1. Dodaj Razor Pages
builder.Services.AddRazorPages();

// 2. Zarejestruj kontekst bazy danych (np. SQLite, SQL Server, InMemory, itd.)
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseInMemoryDatabase("TodoMemoryDB"));

var app = builder.Build();

// 3. Middleware ASP.NET
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages(); // <-- to uruchamia /Pages

app.Run();
