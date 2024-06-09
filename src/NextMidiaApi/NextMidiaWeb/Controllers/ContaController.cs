﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NextMidiaWeb.Domain.Entities;
using NextMidiaWeb.Models.Input;
using NextMidiaWeb.Models.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace NextMidiaWeb.Controllers
{
    public class ContaController : Controller
    {
        private readonly ILogger<ContaController> _logger;
        private readonly UsuarioService _service;

        public ContaController(ILogger<ContaController> logger, UsuarioService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Conta")]
        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("UserEmail") ?? "";
            string senha = HttpContext.Session.GetString("UserPassword") ?? "";

            var user = _service.FindByEmailAndSenha(email, senha);
            if (user != null)
            {
                ContaVidewModel model = new ContaVidewModel
                {
                    Userid = user.Id,
                    Username = user.Nome,
                    Email = user.Email,
                    Password = user.Senha
                };
                return View("~/Views/Conta/Index.cshtml", model);
            }
            else
                return Content("Erro ao carregar perfil de usuário, tente novamente.");
        }

        [Route("Conta/CriarConta")]
        public IActionResult CriarConta()
        {
            return View();
        }

        [Route("Conta/CriarConta")]
        [HttpPost]
        public IActionResult CriarConta([FromForm] UsuarioInput usuarioInpt)
        {
            var temErro = false;
            if (usuarioInpt != null
                && usuarioInpt.Nome != null
                && usuarioInpt.Senha != null
                && usuarioInpt.Email != null)
            {
                //Nome
                if (usuarioInpt.Nome.Length < 5)
                {
                    temErro = true;
                    ViewBag.UsuarioError = "O nome deve ter no mínimo 5 caracteres.";
                }
                //Email
                else if (usuarioInpt.Email.Length < 8)
                {
                    temErro = true;
                    ViewBag.EmailError = "O e-mail deve ter no mínimo 5 caracteres.";
                }
                else if (!usuarioInpt.Email.Contains("@") || !usuarioInpt.Email.Contains(".com"))
                {
                    temErro = true;
                    ViewBag.EmailError = "Insira um endereço de e-mail válido";
                }
                //Senha
                else if (usuarioInpt.Senha.Length < 8)
                {
                    temErro = true;
                    ViewBag.SenhaError = "A senha deve conter no mínimo 8 caracteres.";
                }
                //Confirmação Senha
                else if (usuarioInpt.ConfirmacaoSenha.Length < 8)
                {
                    temErro = true;
                    ViewBag.ConfirmacaoSenhaError = "A senha deve conter no mínimo 8 caracteres.";
                }
                else if (usuarioInpt.ConfirmacaoSenha.Trim() != usuarioInpt.Senha.Trim())
                {
                    temErro = true;
                    ViewBag.ConfirmacaoSenhaError = "A senha e a confirmação de senha devem ser iguais.";
                }
            }
            else
            {
                ViewBag.GeneralError = "Preencha os campos para criar um usuário.";
                return View();
            }

            if (!temErro)
            {
                if (_service.FindAll()
                    .Where(user => user.Nome == usuarioInpt.Nome).Count() > 0)
                    ViewBag.GeneralError = "Já existe um usuário com este username cadastrado.";
                if (_service.FindAll()
                   .Where(user => user.Email == usuarioInpt.Email).Count() > 0)
                    ViewBag.GeneralError = "Já existe um usuário com este email cadastrado.";
                else
                    _service.Create(new Usuario
                    {
                        Nome = usuarioInpt.Nome,
                        Email = usuarioInpt.Email,
                        Senha = usuarioInpt.Senha
                    });
            }
            else
                return View("~/Views/Login/CriarConta.cshtml");

            return View("~/Views/Login/Login.cshtml");
        }
    }
}