using Abc.MvcWebUI.Entity;
using Abc.MvcWebUI.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Home
        public ActionResult Index(int? PagedNo)
        {
            int pageIndex = PagedNo ?? 1;
            var urunler = _context.Products
                                .Where(i => i.IsHome && i.IsApproved)
                                .OrderByDescending(i => i.Id)
                                .Select(i => new ProductModel()
                                {
                                    Id=i.Id,
                                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                                    Price = i.Price,
                                    Stock=i.Stock,
                                    Image=i.Image,
                                    CategoryId=i.CategoryId
                                }).ToPagedList(pageIndex, 6);
            return View(urunler);
        }

        public ActionResult Details(int id)
        {
            return View(_context.Products.Where(i=>i.Id==id).FirstOrDefault());
        }

        public ActionResult List(int? id, int? PagedNo)
        {
            int pageIndex = PagedNo ?? 1;
            var urunler = _context.Products
                                .Where(i => i.IsApproved)
                                .OrderByDescending(i => i.Id)
                                .Select(i => new ProductModel()
                                {
                                    Id = i.Id,
                                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                                    Price = i.Price,
                                    Stock = i.Stock,
                                    Image = i.Image ?? "1.jpg", //varsayılan resim
                                    CategoryId = i.CategoryId
                                }).AsQueryable();
            if (id != null)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }
            return View(urunler.ToPagedList(pageIndex, 6));
        }

        public PartialViewResult GetCategories()
        {
            var cat = _context.Categories.ToList();
            return PartialView(cat);
        }

        // GET: Home
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(EpostaModel model)
        {
            string server = ConfigurationManager.AppSettings["server"];
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            bool ssl = ConfigurationManager.AppSettings["ssl"].ToString() == "1" ? true : false;
            string from = ConfigurationManager.AppSettings["from"];
            string password = ConfigurationManager.AppSettings["password"];
            string fromname = ConfigurationManager.AppSettings["fromname"];
            string to = ConfigurationManager.AppSettings["to"];
            string copyto = ConfigurationManager.AppSettings["epostacopy"];
            var client = new SmtpClient();
            client.Host = server;
            client.Port = port;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(from, password);
            var email = new MailMessage();
            email.From = new MailAddress(from, fromname);
            email.To.Add(to);
            //email.To.Add("brkyknc@hotmail.com");

            if (string.IsNullOrEmpty(copyto) == false)
            {
                string[] mails = copyto.Split(',');
                foreach (var item in mails)
                {
                    email.Bcc.Add(item);
                }
            }

            email.Subject = model.konu;
            email.IsBodyHtml = true;
            email.Body = $"ad soyad : {model.adsoyad} \n konu: {model.konu} \n mesaj: {model.mesaj} \n eposta : {model.email} ";
            try
            {
                client.Send(email);
                ViewData["result"] = true;
            }
            catch (Exception)
            {
                ViewData["result"] = false;
            }
            return View();
        }
    }
}