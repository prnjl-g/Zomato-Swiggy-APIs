using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Service.Database;
using Microsoft.AspNetCore.Identity;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI;
using graphql_create.GraphQLModel;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation;
using Test.GraphQL.MyCoreAPI.Helper;
using Microsoft.OpenApi.Models;


namespace graphql_create
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAuthorization();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<StoreContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultUI(UIFramework.Bootstrap4).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreContext>();
            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIssueRepository, IssueRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ISprintRepository, SprintRepository>();
            services.AddTransient<ILabelRepository, LabelRepository>();
            services.AddTransient<ITimeLoggingRepository, TimeLoggingRepository>();
            services.AddScoped<ICakeRepository, CakeRepository>();
            services.AddScoped<RootQueryType>();
            services.AddScoped<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
            services.AddScoped<ISchema, RootSchema>();
            services.AddScoped<IValidationRule, AuthValidationRule>();
            services.AddScoped<CakeType>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager)
        {
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/graphql", Path = "/graphql"});
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            ApplicationDbInitializer.SeedUsers(userManager);
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            
            app.UseMvc();
        }
    }
}
