using Jinxi.IService;
using Jinxi.Service;
using Jinxi.Tool;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jinxi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton(Configuration);
            Console.WriteLine(Configuration["Jwt:SecretKey"]);
            var test=Configuration["MysqlInfo"];
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton(new SqlsugarTool(Configuration.GetSection("MysqlInfo").Get<string>()));
            services.AddSingleton(new JwtCreateTool(Configuration));
            services.AddSingleton<IStructureStatisticsService, StructureStatisticsService>();
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
                //��֤��ʽ���˷�ʽΪȫ������
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //    { new OpenApiSecurityScheme
                //    {
                //    Reference = new OpenApiReference()
                //    {
                //    Id = "Bearer",
                //    Type = ReferenceType.SecurityScheme
                //    }
                //    }, Array.Empty<string>() }
                //    });
            });
            //�����������
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "MyAllowSpecificOrigins",
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
            app.UseSwaggerUI();
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}