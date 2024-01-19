﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SistemaVentaR.Areas.Users.Models;
using SistemaVentaR.Data;
using SistemaVentaR.Library;
using SistemaVentaR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentaR.Controllers
{
    public class HomeController : Controller
    {
        //IServiceProvider _serviceProvider;

        private static InputModelLogin _model;
        private LUser _user;
        private SignInManager<IdentityUser> _signInManager;


        public HomeController(UserManager<IdentityUser> userManager,
          SignInManager<IdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IServiceProvider serviceProvider)
        {
            // _serviceProvider = serviceProvider;
            _signInManager = signInManager;
            _user = new LUser(userManager,signInManager, roleManager, context);
        }

        public async Task<IActionResult> Index()
        {
            // await CreateRolesAsync(_serviceProvider);

            if(_model != null)
            {
                return View(_model);
            }
            else
            {
                return View();
            }


        }

        [HttpPost]
        public async Task<IActionResult> Index(InputModelLogin model)
        {
            _model = model;

            if (ModelState.IsValid)
            {
                var result = await _user.UserLoginAsync(model);
                if (result.Succeeded)
                {
                    return Redirect("/Principal/Principal");
                }
                else
                {
                    model.ErrorMessage = "Correo o Contraseña invalido.";
                    return Redirect("/");
                }
               
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _model.ErrorMessage = error.ErrorMessage;
                    }
                }
                return Redirect("/");
            }
          
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "USer" };
            foreach (var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }

    }
}
