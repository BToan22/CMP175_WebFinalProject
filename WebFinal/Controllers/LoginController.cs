using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using WebFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace WebFinal.Controllers
{
	public class LoginController : Controller
	{
		private string _key;

		public LoginController()
		{
			_key = "E546C8DF278CD5931069B522E695D4F2";
		}
		public IActionResult Index()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
			{
				var model = GetProvinces();


				return View(model);
			}
			else
			{
				return Redirect("home");
			}

		}

		[HttpPost]
		public IActionResult Register(IFormCollection f)
		{


			User u = new User();
			u.Fullname = f["txtFullName"].ToString();
			u.Username = f["txtUserName1"].ToString();
			u.Password = f["txtPassword1"].ToString();
			u.City = f["selCity"];
			var obj = CreateUser(u);

			return View(obj);
		}

		[HttpPost]
		public IActionResult doLogin(LoginData login)
		{
			LoginResponse res = new LoginResponse();
			if (login != null)
			{
				Users? usr = checkLogin(login);

				if (usr != null)
				{
					//var passHash = EncryptString(login.Password, _key);
					var decryptedPass = DecryptString(usr.Password, _key);

					if (decryptedPass == login.Password)
					{
						res.Message = "Đăng nhập thành công !!";
						res.Success = true;
						res.User = usr;
						HttpContext.Session.SetString("Username", usr.Username);
						HttpContext.Session.SetString("Fullname", usr.Fullname);

					}
					else
					{
						res.Message = "Sai mật khẩu !!";
						res.Success = false;
						res.User = null;
					}

				}
				else
				{
					res.Message = "Sai MK hoặc tên ĐN !!";
					res.Success = false;
				}
			}
			else
			{
				res.Message = "KHông có dữ liệu !!";
				res.Success = false;
			}
			return Json(res);
		}
		[HttpPost]
		public IActionResult signOut()
		{
			LoginResponse res = new LoginResponse();
			HttpContext.Session.Remove("Username");
			HttpContext.Session.Remove("Fullname");
			res.Message = "";
			res.Success = true;
			return Json(res);

		}
		[HttpPost]
		public IActionResult change_Pass(string username, string oldPass, string newPass)
		{
			var obj = ChangePass(username, oldPass, newPass);
			return Json(obj);

		}
		public Users? checkLogin(LoginData login)
		{
			Users? usr = new Users();
			if (login != null)
			{
				string cnStr = "Server = LAPTOP-MPPBE2T0; Database = WebFinal;User id = danh;password = 123;";
				SqlConnection cmn = new SqlConnection(cnStr);
				try
				{
					cmn.Open();
					SqlCommand cmd = cmn.CreateCommand();
					cmd.Connection = cmn;

					string sql = "Select* from Users";
					sql += " where Username='" + login.Username + "' ";
					//sql += " and [Password]='" + login.Password + "'";

					cmd.CommandText = sql;
					cmd.CommandType = CommandType.Text;
					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						usr.Id = int.Parse(reader["Id"].ToString());
						usr.Username = reader["Username"].ToString();
						usr.Password = reader["Password"].ToString();
						usr.Fullname = reader["Fullname"].ToString();
					}
					reader.Close();
					if (!(usr.Id > 0))
					{
						usr = null;
					}

				}
				catch (Exception ex)
				{
					usr = null;
				}
				if (cmn.State == ConnectionState.Open)
				{
					cmn.Close();
				}
			}
			return usr;
		}
		private string EncryptString(string text, string keyString)
		{
			var key = Encoding.UTF8.GetBytes(keyString);

			using (var aesAlg = Aes.Create())
			{
				using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
				{
					using (var msEncrypt = new MemoryStream())
					{
						using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
						using (var swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(text);
						}

						var iv = aesAlg.IV;

						var decryptedContent = msEncrypt.ToArray();

						var result = new byte[iv.Length + decryptedContent.Length];

						Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
						Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

						return Convert.ToBase64String(result);
					}
				}
			}
		}
		private string DecryptString(string cipherText, string keyString)
		{
			var fullCipher = Convert.FromBase64String(cipherText);

			var iv = new byte[16];
			var cipher = new byte[16];

			Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
			Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
			var key = Encoding.UTF8.GetBytes(keyString);

			using (var aesAlg = Aes.Create())
			{
				using (var decryptor = aesAlg.CreateDecryptor(key, iv))
				{
					string result;
					using (var msDecrypt = new MemoryStream(cipher))
					{
						using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
						{
							using (var srDecrypt = new StreamReader(csDecrypt))
							{
								result = srDecrypt.ReadToEnd();
							}
						}
					}

					return result;
				}
			}
		}
		private object? CreateUser(User u)
		{
			try
			{
				var db = new WebFinalContext();
				var usr = db.Users.Where(x => x.Username == u.Username).FirstOrDefault();
				if (usr != null)
				{
					return new
					{
						success = false,
						message = "User already exist !!!",
						data = ""
					};
				}
				else
				{
					var hashPass = EncryptString(u.Password, _key);
					u.Password = hashPass;
					db.Users.Add(u);
					db.SaveChanges();
					var t = GetProvinceID(u.City);
					return new
					{
						success = true,
						message = "Create User Success !!!",
						data = u,
						province = t
					};
				}
			}
			catch (Exception ex)
			{
				return new
				{
					success = false,
					message = ex.Message
				};
			}
		}

		private object? GetProvinces()
		{
			var db = new WebFinalContext();
			var res = db.Provinces.ToList();
			return res;
		}
		private Province GetProvinceID(String Id)
		{
			int id = int.Parse(Id);
			var db = new WebFinalContext();
			var res = db.Provinces.Where(x => x.Id == id).FirstOrDefault();
			return res;
		}
		private object? ChangePass(string uid, string oPass, string nPass)
		{
			try
			{
				var db = new WebFinalContext();
				var usr = db.Users.Where(x => x.Username == uid).FirstOrDefault();
				if (usr == null)
				{
					return new
					{
						success = false,
						message = "User Không tồn tại nha !!"
					};
				}
				else
				{
					var cPass = DecryptString(usr.Password, _key);
					if (cPass != oPass)
					{
						return new
						{
							success = false,
							message = "Pass cũ không chính xác!!"
						};

					}
					else
					{
						var hashPass = EncryptString(nPass, _key);
						usr.Password = hashPass;
						db.Users.Update(usr);
						db.SaveChanges();
						HttpContext.Session.Remove("Username");
						HttpContext.Session.Remove("Fullname");
						return new
						{
							success = true,
							message = "Update Pass Completed  !!"
						};
					}
				}
			}
			catch (Exception ex)
			{
				return new
				{
					success = false,
					message = ex.Message
				};
			}
		}
	}
}
public class LoginData
{
	public string Username { get; set; }
	public string Password { get; set; }
}
public class LoginResponse
{
	public bool Success { get; set; }
	public string Message { get; set; }
	public Users? User { get; set; }

}

public class Users
{
	public int? Id { get; set; }

	public string Username { get; set; }

	public string Password { get; set; }
	public string Fullname { get; set; }

}