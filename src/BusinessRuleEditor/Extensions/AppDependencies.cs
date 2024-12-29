using BusinessRuleEditor.Implementation;
using BusinessRuleEditor.Repository;
using BusinessRuleEditor.Repository.Implementation;
using BusinessRuleEditor.Service;

namespace BusinessRuleEditor.Extensions
{
    public static class AppDependencies
    {
        public static void AddServiceAndRepository(this IServiceCollection services) 
        {
            #region Singleton
            services.AddSingleton<IConfigManagerRepository, ConfigManagerRepository>();
            
            #endregion

            #region Scoped
            //Repository
            services.AddScoped<IFileReaderRepository, JsonFileReaderRepository>();
            services.AddScoped<IFileWriterRepository, JsonFileReaderRepository>();
            services.AddScoped<IWorkflowDeleteRepository, WorkflowDeleteRepository>();
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
            services.AddScoped<IWorkflowWriteRepository, WorkflowWriteRepository>();


            //Services
            services.AddScoped<IWorkflowService, WorkflowService>();
            services.AddScoped<IWorkflowWriteService, WorkflowWriteService>();
            services.AddScoped<IWorkflowDeleteService, WorkflowDeleteService>();

            #endregion
        }

        public static void AddDbContextDependency(this IServiceCollection services)
        {
            //If need to use SQL sever, need to install packages:
            //Microsoft.EntityFrameworkCore.SqlServer
            //Microsoft.EntityFrameworkCore.Tools

            //services.AddDbContext<ApplicationDbContext>(
            //    options =>
            //    options.UseSqlServer(ApplicationBuilder.Configuration.GetConnectionString("DefaultConnection")));
        }

    }
}
