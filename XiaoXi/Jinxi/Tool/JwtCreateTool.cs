﻿using Jinxi.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jinxi.Tool
{
    public class JwtCreateTool
    {
        private readonly IConfiguration _configuration;

        public JwtCreateTool(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  string CreateToken(LoginDTO input)
        {
            // 1. 定义需要使用到的Claims
            var claims = new[]
            {
            /*new Claim(ClaimTypes.Name, "n_admin"),
            new Claim(ClaimTypes.Role, "r_admin"),
            new Claim(JwtRegisteredClaimNames.Jti, "admin"),*/
            new Claim("user", input.User),
            new Claim("userid", input.Userid),
           /* new Claim("ak", "19330910714"),
            new Claim("ld", "16673474538")*/
        };

            // 2. 从 appsettings.json 中读取SecretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 根据以上，生成token
            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],     //Issuer
                _configuration["Jwt:Audience"],   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddDays(1),    //expires
                signingCredentials               //Credentials
            );
            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
