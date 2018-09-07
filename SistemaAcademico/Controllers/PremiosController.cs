using SistemaAcademico.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaAcademico.Controllers
{
    public class PremiosController : Controller
    {
        // GET: Premios
        public ActionResult Index()
        {
            //Listar 5 melhores notas
            AlunoDAO alunoDAO = new AlunoDAO();
            return View(alunoDAO.Busca(null, 5));
        }

        //Gerar nota para os 5 primeiros alunos 
        public ActionResult GeraNotaEspecial()
        {
            

            return RedirectToAction("Index");
        }
    }
}