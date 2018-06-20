using PRSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PRSMVC.Controllers {
	public class HomeController : Controller {

		PRSAppContext db = new PRSAppContext();

		private static byte[] Get_SALT() {
			return Get_SALT(saltLengthLimit);
		}

		private static byte[] Get_SALT(int maximumSaltLength) {
			var salt = new byte[maximumSaltLength];

			//Require NameSpace: using System.Security.Cryptography;
			using (var random = new RNGCryptoServiceProvider()) {
				random.GetNonZeroBytes(salt);
			}

			return salt;
		}

		public static string Get_HASH_SHA512(string password, string username, byte[] salt) {
			try {
				//required NameSpace: using System.Text;
				//Plain Text in Byte
				byte[] plainTextBytes = Encoding.UTF8.GetBytes(password + username);

				//Plain Text + SALT Key in Byte
				byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

				for (int i = 0; i < plainTextBytes.Length; i++) {
					plainTextWithSaltBytes[i] = plainTextBytes[i];
				}

				for (int i = 0; i < salt.Length; i++) {
					plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
				}

				HashAlgorithm hash = new SHA512Managed();
				byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
				byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

				for (int i = 0; i < hashBytes.Length; i++) {
					hashWithSaltBytes[i] = hashBytes[i];
				}

				for (int i = 0; i < salt.Length; i++) {
					hashWithSaltBytes[hashBytes.Length + i] = salt[i];
				}

				return Convert.ToBase64String(hashWithSaltBytes);
			} catch {
				return string.Empty;
			}
		}

		public ActionResult Index() {
			return View();
		}

		[HttpGet]
		public ActionResult Login(string returnURL) {
			var userinfo = new User();

			try {
				EnsureLoggedOut();
				return View(userinfo);
			} catch {
				throw;
			}
		}

		public void EnsureLoggedOut() {
			if (Request.IsAuthenticated) {
				Logout();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Logout() {
			try {
				FormsAuthentication.SignOut();
				HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
				Session.Clear();
				System.Web.HttpContext.Current.Session.RemoveAll();
				return RedirectToLocal();
			} catch {
				throw;
			}
		}

		private void SignInRememeber(string userName, bool isPersistent = false) {
			FormsAuthentication.SignOut();
			FormsAuthentication.SetAuthCookie(userName, isPersistent);
		}

		private ActionResult RedirectToLocal(string returnURL = "") {
			try {
				if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL)) {
					return Redirect(returnURL);
				}
				return RedirectToAction("Index", "Home");
			} catch {
				throw;
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(User entity) {
			string OldHASHValue = string.Empty;
			byte[] SALT = new byte[saltLengthLimit];

			try {
				using (db = new PRSAppContext()) {
					if (!ModelState.IsValid) {
						return View(entity);
					}
					var userInfo = db.Users.Where(u => u.Username == entity.Username.Trim()).FirstOrDefault();
					if (userInfo != null) {
						OldHASHValue = userInfo.GetHashCode().ToString();
						SALT = userInfo.SALT;
					}
					bool isLogin = CompareHashValue(entity.Password, entity.Username, OldHASHValue, SALT);
					if (isLogin) {
						SignInRememeber(entity.Username, entity.IsRemember);
						Session["UserID"] = userInfo.Id;
						return RedirectToLocal(entity.ReturnURL);
					} else {
						TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
						return View(entity);
					}
				}
			} catch {
				throw;
			}
		}

		public static bool CompareHashValue(string password, string username, string OldHASHValue, byte[] SALT) {
			try {
				string expectedHashString = Get_HASH_SHA512(password, username, SALT);
				return (OldHASHValue == expectedHashString);
			} catch {
				return false;
			}
		}

		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}