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
            /*#region minio�ͻ���ע��
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
                    ValidateIssuer = true, //�Ƿ���֤Issuer
                    ValidIssuer = Configuration["Jwt:Issuer"], //������Issuer
                    ValidateAudience = true, //�Ƿ���֤Audience
                    ValidAudience = Configuration["Jwt:Audience"], //������Audience
                    ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])), //SecurityKey
                    ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
                    ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
                    RequireExpirationTime = true,
                };
            });
            //����swagger��Ϣ
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "�������� ������ʧ", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath, true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ���·�����Bearer {token} ���ɣ�ע������֮���пո�",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });
                //��֤��ʽ���˷�ʽΪȫ�����
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
            //�����������
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
