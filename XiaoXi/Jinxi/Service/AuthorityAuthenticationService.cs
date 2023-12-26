using Jinxi.DTO;
using Jinxi.Entity;
using Jinxi.IService;
using Jinxi.Tool;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Jinxi.Service
{
    public class AuthorityAuthenticationService : IAuthorityAuthenticationService
    {
        private SqlsugarTool _sqlsugarTool;
        JwtCreateTool _jwtCreateTool;
        public AuthorityAuthenticationService(SqlsugarTool sqlsugarTool, JwtCreateTool jwtCreateTool)
        {
            _sqlsugarTool = sqlsugarTool;
            _jwtCreateTool = jwtCreateTool;
        }
        public IActionResult Login(LoginDTO input)
        {
            try
            {
                if (string.IsNullOrEmpty(input.User) || string.IsNullOrEmpty(input.Password))
                {
                    return MstResultTool.Error("登录信息错误");
                }
                string userid = _sqlsugarTool.GetDb().Queryable<UserInfo>().Where(x => x.User == input.User && x.Password == input.Password).Select(x => x.Userid).First();
                if (string.IsNullOrEmpty(userid))
                {
                    return MstResultTool.Error("账号或密码不存在");
                }
                input.Userid = userid;
                string token = _jwtCreateTool.CreateToken(input);
                return MstResultTool.Success(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
