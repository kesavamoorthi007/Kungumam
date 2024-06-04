using Microsoft.Extensions.DependencyInjection.Extensions;
using Kungumam.Interface;
using Kungumam.Services;
using Microsoft.AspNetCore.Http.Features;
using Kungumam.Interface.Admin;
using Kungumam.Services.Admin;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.TryAddSingleton<ILoginService, LoginService>();
		builder.Services.TryAddSingleton<IMagazineService, MagazineService>();
		builder.Services.TryAddSingleton<IWrapperService, WrapperService>();
		builder.Services.TryAddSingleton<IRoadBlockService, RoadBlockService>();
		builder.Services.TryAddSingleton<ICategoryService, CategoryService>();
        builder.Services.TryAddSingleton<ISubCategoryService, SubCategoryService>();
        builder.Services.TryAddSingleton<IGalleryService, GalleryService>();
        builder.Services.TryAddSingleton<IEBookService, EBookService>();
        builder.Services.TryAddSingleton<IEBook2Service, EBook2Service>();
        builder.Services.TryAddSingleton<IArticalService, ArticalService>();


        builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		



		builder.Services.AddSession();


		builder.Services.AddControllers();
		builder.Services.Configure<FormOptions>(x =>
		{
			x.ValueCountLimit = int.MaxValue;
		});
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}


		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();


		app.UseAuthorization();
		app.UseSession();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Account}/{action=Login}/{fid?}");



		app.Run();
	}


	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
			webBuilder.UseWebRoot("wwwroot");
			webBuilder.UseStartup<IStartup>();

		});

	[Obsolete]
	public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
	{
		// Other configurations...

		app.UseStaticFiles(); // Enable static file serving, e.g., for wwwroot folder

		// More configurations...
	}
}
