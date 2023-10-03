﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ThesisOct2023.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _manager= roleManager;  
        }
        public IActionResult Index()
        {
            //List of roles
            var roles = _manager.Roles; 
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            //Check if the role exists
            if (!_manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
