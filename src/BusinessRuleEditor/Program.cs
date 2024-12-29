using BusinessRuleEditor.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddServiceAndRepository();

builder.Services.AddHttpClient();

var app = builder.Build();

//builder.Services.AddSwaggerGen();
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI(c => {
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BRE API V1");
    //});
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();