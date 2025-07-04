using System.Web.Mvc;

namespace QuestionAsking.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        
        public ActionResult AddQuestions()
        {
            return View();
        }

      
    }
}