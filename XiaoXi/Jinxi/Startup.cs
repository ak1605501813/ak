using Jinxi.IService;
using Jinxi.Service;
using Jinxi.Tool;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Jinxi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigTool.InitConfig(configuration);
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            /*#region minio客户端注入
            var minioClient = new MinioClient(Configuration["MinIO:Endpoint"]
                , Configuration["MinIO:AccessKey"]
                , Configuration["MinIO:SecretKey"]);
            services.AddSingleton(minioClient);
            #endregion*/
            var minioClient = new MinioClient("11", "22" ,"33");
            services.AddSingleton(minioClient);
            Console.WriteLine(Configuration.GetSection("MysqlInfo").Get<string>());
            services.AddSingleton(new SqlsugarTool(Configuration.GetSection("MysqlInfo").Get<string>()));
            services.AddSingleton(new JwtCreateTool(Configuration));
            services.AddSingleton<IStructureStatisticsService, StructureStatisticsService>();
            services.AddSingleton<IEasyToForgetAccountService, EasyToForgetAccountService>();
            services.AddSingleton<IAuthorityAuthenticationService, AuthorityAuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<MinioTool>(); 
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true, //是否验证Issuer
                    ValidIssuer = Configuration["Jwt:Issuer"], //发行人Issuer
                    ValidateAudience = true, //是否验证Audience
                    ValidAudience = Configuration["Jwt:Audience"], //订阅人Audience
                    ValidateIssuerSigningKey = true, //是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])), //SecurityKey
                    ValidateLifetime = true, //是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
                    RequireExpirationTime = true,
                };
            });
            //配置swagger信息
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "不烦不扰 淡泊不失", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath, true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                //认证方式，此方式为全局添加
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference()
                    {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                    }
                    }, Array.Empty<string>() }
                    });
            });
            //解决跨域问题
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "MRJIANG",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("MRJIANG");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerUI();
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
           
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
