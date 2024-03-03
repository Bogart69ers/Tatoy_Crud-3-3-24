using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tatoy_Crud.Repository;
namespace Tatoy_Crud.Controllers
{
    public class BaseController : Controller
    {
        public CRUDEntities _db;
        public BaseRepository<User> _userRepo;
        public BaseController()
        {
            _db = new CRUDEntities();
            _userRepo = new BaseRepository<User>();
        }
    }
}