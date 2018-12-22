using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Week02.common;
using Week02.Models;
namespace Week02.Controllers
{
    public class AccountController : Controller
    {
        private const string FIREBASE_APP = "https://carfirebase-1001.firebaseio.com/";
        private FirebaseClient firebase;

        // GET: Account
        ShoeStoreEntities1 db = new ShoeStoreEntities1();
        User user;

        public AccountController()
        {
            InitFirebase();
        }

        private void InitFirebase()
        {
            firebase = new FirebaseClient(FIREBASE_APP);
        }

        private async Task<bool> CheckAuthenticationUser(string email, string password)
        {
            var fbUsers = await firebase.Child("User_WebShoes").OnceAsync<User>();
            foreach (var item in fbUsers)
                if (item.Object.Email_kh == email && item.Object.Mk_kh == password)
                    return true;
            return false;
        }

        private async Task<bool> CheckAuthenticationAdmin(string email, string password)
        {
            var fbUsers = await firebase.Child("User_WebShoes").OnceAsync<User>();
            foreach (var item in fbUsers)
                if (item.Object.Email_kh == email && item.Object.Mk_kh == password && item.Object.PQuyen_kh == "ad")
                    return true;
            return false;
        }

        private async Task<UserNode> GetUserByEmail(string email)
        {
            var fbUsers = await firebase.Child("User_WebShoes").OnceAsync<User>();
            foreach (var item in fbUsers)
                if (item.Object.Email_kh == email)
                    return new UserNode(item.Key, item.Object);
            return null;
        }

        public ActionResult Login()
        {
            return View();

        }
        public ActionResult Logout()
        {
            Session["email"] = null;
            Session["cart"] = null;
            return Redirect("/");
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginFormModel model)
        {

            string email = Request["Email"];
            string password = Request["Password"];
            if (ModelState.IsValid)
            {
                string encrypt_password = Encrypt.getHashSha256(password);
                //var confirmAccountUser = db.Khach_hang.Where(n => n.Email_kh == email && n.Mk_kh == encrypt_password).Select(n => n).Count();
                //var confirmAccountAdmin = db.Khach_hang.Where(n => n.Email_kh == email && n.Mk_kh == encrypt_password && n.PQuyen_kh == "ad").Select(n => n).Count();

                //Authentication User Login
                bool result_user = await CheckAuthenticationUser(email, encrypt_password);
                bool result_admin = await CheckAuthenticationAdmin(email, encrypt_password);

                if (result_admin == true)
                {
                    Session.Add("email", email);
                    Session.Add("password", password);

                    return RedirectToAction("Index", "Admin");
                }
                else if (result_user == true)
                {

                    Session.Add("email", email);
                    Session.Add("password", password);

                    return Redirect("/");

                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại hoặc mật khẩu sai");
                }

                //if (confirmAccountAdmin == 1)
                //{
                //    Session.Add("email", email);
                //    Session.Add("password", password);
                //    //~/ Views / Admin / Index.cshtml
                //    return RedirectToAction("Index", "Admin");
                //}
                //else if (confirmAccountUser == 1)
                //{
                //    Session.Add("email", email);
                //    Session.Add("password", password);

                //    return Redirect("/");
                //}
                //else if (confirmAccountUser == 0)
                //{
                //    ModelState.AddModelError("", "Tài khoản không tồn tại hoặc mật khẩu sai");
                //}
                //else if (Session["cart"] == null && Session["email"] == null)
                //{
                //    ModelState.AddModelError("", "Vui lòng đăng nhập để có thể thêm hàng vào giỏ hàng !");
                //}

            }
            else
            {

            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        //public bool CheckEmail(String email)
        //{
        //    return db.Khach_hang.Any(n => n.Email_kh == email);
        //}
        private async Task<bool> CheckEmail(string email)
        {
            var fbUsers = await firebase.Child("User_WebShoes").OnceAsync<User>();
            foreach (var item in fbUsers)
                if (item.Object.Email_kh == email)
                    return true;
            return false;
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            string email = model.Email;
            string password = Encrypt.getHashSha256(model.Password);
            string name = model.NameCus;
            string phone = model.PhoneNumberCus;
            string address = model.AddressCus;
            string doB = model.DateCus;
            if (ModelState.IsValid)
            {
                if (await CheckEmail(email) == true)
                {
                    ModelState.AddModelError("", "Account already exists !!!");
                }
                else
                {
                    //kh.Email_kh = email;
                    //kh.Mk_kh = password;
                    //kh.Ten_kh = model.NameCus;
                    //kh.Sdt_kh = model.PhoneNumberCus;
                    //kh.Diachi_kh = model.AddressCus;
                    //kh.Ngaysinh_kh = DateTime.ParseExact(model.DateCus, "dd/MM/yyyy", null);
                    //kh.PQuyen_kh = "kh";

                    user = new User
                    {
                        Diachi_kh = address,
                        Email_kh = email,
                        ID_kh = "",
                        Mk_kh = Encrypt.getHashSha256(password),
                        Ngaysinh_kh = doB,
                        PQuyen_kh = "kh",
                        Sdt_kh = phone,
                        Ten_kh = name
                    };
                    var nodeuser = await firebase.Child("User_WebShoes").PostAsync(user);
                    
                    //UPDATE ID_KH
                    UserNode nuser = await GetUserByEmail(email);
                    if (nuser != null)
                    {
                        nuser.User.ID_kh = nuser.Key;
                        await firebase.Child("User_WebShoes").Child(nuser.Key).PutAsync(nuser.User);
                    }

                    ModelState.AddModelError("", "Register Success !!!");
                    return View("Login");
                }
            }
            else
            {

            }
            return View();
        }
        public async Task<ActionResult> AccountDetail()
        {
            if (Session["Email"] == null)
            {
                return View("Login");
            }
            else
            {
                string email = Session["Email"].ToString();
                UserNode nuser = await GetUserByEmail(email);

                user = new User
                {
                    ID_kh = nuser.User.ID_kh,
                    Email_kh = nuser.User.Email_kh,
                    Diachi_kh = nuser.User.Diachi_kh,
                    Mk_kh = nuser.User.Mk_kh,
                    Ngaysinh_kh = nuser.User.Ngaysinh_kh,
                    PQuyen_kh = nuser.User.PQuyen_kh,
                    Sdt_kh = nuser.User.Sdt_kh,
                    Ten_kh = nuser.User.Ten_kh
                };

                ViewData["detail_customer"] = user;
            }

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AccountDetail(DetailAccountViewModel model)
        {
            string name = model.NameCus;
            string email = model.Email;
            string phone = model.PhoneNumberCus;
            string address = model.AddressCus;
            string doB = model.DateCus;
            if (ModelState.IsValid)
            {

                UserNode nuser = await GetUserByEmail(email);

                if (nuser != null)
                {
                    nuser.User.Ten_kh = name;
                    nuser.User.Sdt_kh = phone;
                    nuser.User.Diachi_kh = address;
                    nuser.User.Ngaysinh_kh = doB;

                }

                // Update Customer
                await firebase.Child("User_WebShoes").Child(nuser.Key).PutAsync(nuser.User);

                ModelState.AddModelError("", "Account already saved !!!");
                return View();
            }
            else
            {
                return View();
            }
        }

        private async Task<bool> CheckExistsPassword(string email, string password)
        {
            var fbUsers = await firebase.Child("User_WebShoes").OnceAsync<User>();
            foreach (var item in fbUsers)
                if (item.Object.Email_kh == email && item.Object.Mk_kh == password)
                    return true;
            return false;
        }

        public ActionResult ChangePassword()
        {
            if (Session["email"] == null)
            {
                return View("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            string email = Session["Email"].ToString();
            string opassword = Encrypt.getHashSha256(model.OPassword);
            string npassword = Encrypt.getHashSha256(model.NPassword);


            if (ModelState.IsValid)
            {
                // CHECK OLD PASSWORD
                if (await CheckExistsPassword(email, opassword) == false)
                {
                    ModelState.AddModelError("", "Old password is wrong, please enter your old password !!!");
                }
                // CHECK NEW PASSWORD HAS A SAME WITH OLD PASSWORD
                else if (await CheckExistsPassword(email, npassword))
                {
                    ModelState.AddModelError("", "A new password is the same old password, please enter different password !!!");
                }
                // SAVE NEW PASSWORD
                else if (await CheckExistsPassword(email, opassword) == true && await CheckExistsPassword(email, npassword) == false)
                {
                    UserNode nuser = await GetUserByEmail(email);
                    if (nuser != null)
                    {
                        nuser.User.Mk_kh = npassword;
                    }

                    await firebase.Child("User_WebShoes").Child(nuser.Key).PutAsync(nuser.User);

                    ModelState.AddModelError("", "Update your password successfull !!!");
                }
            }

            return View(model);
        }

        //public bool checkExistsPassword(string email, string pass)
        //{
        //    if (db.Khach_hang.Any(n => n.Email_kh.Equals(email) && n.Mk_kh.Equals(pass)))
        //        return true;
        //    return false;
        //}
        public ActionResult Forgot()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Forgot(ForgotAccountViewModel model)
        {
            string senderEmail = model.Email;
            string senderPassword, hashPassword;
            if (ModelState.IsValid)
            {
                if (await CheckEmail(senderEmail) == false)
                {
                    ModelState.AddModelError("", "Email not exists !!!");
                }
                else
                {
                    // Get Password for email
                    UserNode nuser = await GetUserByEmail(senderEmail);

                    if (nuser != null)
                    {
                        //RESET PASSWORD
                        senderPassword = GenerateString.GetRandomString(6);
                        hashPassword = Encrypt.getHashSha256(senderPassword);

                        //UPDATE PASSWORD USER WHEN USER FORGOT
                        nuser.User.Mk_kh = hashPassword;
                        await firebase.Child("User_WebShoes").Child(nuser.Key).PutAsync(nuser.User);

                        try
                        {
                            MailMessage msg = new MailMessage();
                            msg.From = new MailAddress(senderEmail);
                            msg.To.Add(senderEmail);
                            msg.Subject = "Recover your password";
                            msg.Body = ("Your email is: " + senderEmail + "<br/><br/>"
                                + "Your password is: " + senderPassword);
                            msg.IsBodyHtml = true;

                            SmtpClient smt = new SmtpClient();
                            smt.Host = "smtp.gmail.com";
                            System.Net.NetworkCredential ntwd = new System.Net.NetworkCredential("contact.shoestore@gmail.com", "shoestoremyadmin");
                            smt.UseDefaultCredentials = false;
                            smt.Credentials = ntwd;
                            smt.Port = 587;
                            smt.EnableSsl = true;
                            smt.Send(msg);

                            ModelState.AddModelError("", "Email sent successfully. Please check your email !!!");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Error while sending mail " + ex.Message + " !!!");
                        }
                    }
                }
            }
            return View();
        }
    }
}