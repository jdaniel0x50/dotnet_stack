using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using message_wall.Models;
using message_wall.Factory;
using message_wall.ActionFilters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace message_wall.Controllers
{
    public class WallController : Controller
    {
        // ########## ROUTES ##########
        //  /wall
        //  /wall/message/create
        //  /wall/message/{msg_id}/edit
        //  /wall/message/{msg_id}/update
        //  /wall/message/{msg_id}/delete
        //  /wall/message/{msg_id}/destroy
        //  /wall/message/{msg_id}/comment/create
        //  /wall/message/{msg_id}/comment/{cmmnt_id}/edit
        //  /wall/message/{msg_id}/comment/{cmmnt_id}/update
        //  /wall/message/{msg_id}/comment/{cmmnt_id}/delete
        //  /wall/message/{msg_id}/comment/{cmmnt_id}/destroy
        // ########## ROUTES ##########

        private readonly MessageFactory messageFactory;
        private readonly CommentFactory commentFactory;
        private const string LOGGED_IN_ID = "LoggedIn_Id";
        private readonly DbConnector _dbConnector;

        public WallController(DbConnector connect)
        {
            messageFactory = new MessageFactory();
            commentFactory = new CommentFactory();
        }

        // GET: /wall/
        [HttpGet]
        [Route("wall")]
        [ImportModelState]
        public IActionResult Wall()
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // collect all messages and comments
            IEnumerable<MessageWithUser> allMessages = messageFactory.FindAllMessages();
            ViewBag.messages = allMessages;
            try
            {
                IEnumerable<CommentWithUser> allComments = commentFactory.FindAllComments();
                ViewBag.comments = allComments;
            }
            catch {}

            if (TempData["messageErrors"] != null)
            {
                ViewBag.messageErrors = ModelState;
            }
            if (TempData["commentErrors"] != null)
            {
                ViewBag.commentErrors = ModelState;
            }
            return View();
        }

        // POST: /wall/message/create
        [HttpPost]
        [Route("wall/message/create")]
        [ExportModelState]
        public IActionResult Message_Create(WallFormModel wallVM)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TryValidateModel(wallVM.messageVM))
            {
                // message is valid
                int user_id = (int)HttpContext.Session.GetInt32(LOGGED_IN_ID);
                messageFactory.Add(user_id, wallVM.messageVM);
            }
            else
            {
                TempData["messageErrors"] = true;
            }
            return RedirectToAction("Wall");
        }

        // POST: /wall/message/{msg_id}/edit
        [HttpPost]
        [Route("wall/message/{msg_id}/edit")]
        [ExportModelState]
        public IActionResult Message_Edit(int msg_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Message_Update");
        }

        // GET: /wall/message/{msg_id}/update
        [HttpGet]
        [Route("wall/message/{msg_id}/update")]
        public IActionResult Message_Update(int msg_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Wall");
        }

        // POST: /wall/message/{msg_id}/delete
        [HttpPost]
        [Route("wall/message/{msg_id}/delete")]
        [ExportModelState]
        public IActionResult Message_Delete(int msg_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Message_Destroy");
        }

        // GET: /wall/message/{msg_id}/destroy
        [HttpGet]
        [Route("wall/message/{msg_id}/destroy")]
        public IActionResult Message_Destroy(int msg_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            messageFactory.Delete(msg_id);
            return RedirectToAction("Wall");
        }

        // POST: /wall/message/{msg_id}/comment/create
        [HttpPost]
        [Route("wall/message/{msg_id}/comment/create")]
        [ExportModelState]
        public IActionResult Comment_Create(int msg_id, WallFormModel wallVM)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TryValidateModel(wallVM.commentVM))
            {
                // comment is valid
                int user_id = (int)HttpContext.Session.GetInt32(LOGGED_IN_ID);
                commentFactory.Add(user_id, msg_id, wallVM.commentVM);
            }
            else
            {
                TempData["commentErrors"] = true;
            }

            return RedirectToAction("Wall");
        }


        //  /wall/message/{msg_id}/comment/{cmmnt_id}/edit
        //  /wall/message/{msg_id}/comment/{cmmnt_id}/update
        //  /wall/message/{msg_id}/comment/{cmmnt_id}/delete

        // GET: /wall/message/{msg_id}/comment/{cmmnt_id}/destroy
        [HttpGet]
        [Route("wall/message/{msg_id}/comment/{cmmnt_id}/destroy")]
        public IActionResult Comment_Destroy(int msg_id, int cmmnt_id)
        {
            if (HttpContext.Session.GetInt32(LOGGED_IN_ID) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            commentFactory.Delete(cmmnt_id);
            return RedirectToAction("Wall");
        }











    }
}