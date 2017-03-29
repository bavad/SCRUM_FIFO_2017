﻿using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(FIFO.App_Start.Startup))]
namespace FIFO.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}