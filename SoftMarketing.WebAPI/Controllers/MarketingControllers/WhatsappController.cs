//using Microsoft.AspNetCore.Mvc;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using QRCoder;
//using SoftMarketing.Model.MarketingModels;
//using SoftMarketing.Services.Marketing;
//using SoftMarketing.WebAPI.Core;
//using SoftMarketing.WebAPI.Model;
//using System.Diagnostics;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Management;
//using System.Text.RegularExpressions;

//namespace SoftMarketing.WebAPI.Controllers.MarketingControllers
//{
//    [Route("[controller]")]
//    [ApiController]
//    public class WhatsappController : ApiControllerBase
//    {
//        IWebDriver driver;
//        ChromeOptions options = new ChromeOptions();
//        public static string logininfo = "";
//        int process_id = -1;
//        string folderpath = @"C:\Users\JoelZapp\source\repos\Marketing-crm\SoftMarketing.WebAPI\bin\Debug\";
//        WhatsappService whatsappService { get; set; }
//        Whatsapp result;
//        public static IntPtr CurrentBrowserHwnd = IntPtr.Zero;
//        public static int CurrentBrowserPID = -1;
//        public string SessionName;

//        public WhatsappController()
//        {
//            whatsappService = new WhatsappService();
//        }
//        // GET: api/<WhatsappController>
//        //[HttpGet]
//        //public IEnumerable<string> Get()
//        //{
//        //    return new string[] { "value1", "value2" };
//        //}

//        // GET api/<WhatsappController>/5
//        //[HttpGet("{id}")]
//        //public string Get(int id)
//        //{
//        //    return "value";
//        //}
//        [HttpGet]
//        [Route("/GetLastCustomer")]
//        public ActionResult GetLast()
//        {
//            try
//            {
//                var result = whatsappService.GetAll();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }
//        [HttpGet]
//        [Route("/GetWhatsappUser")]
//        public ActionResult GetAll()
//        {
//            try
//            {
//                var result = whatsappService.GetAll();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        // POST api/<WhatsappController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<WhatsappController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<WhatsappController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//        [HttpGet]
//        [Route("/Instance")]
//        public ActionResult Add(string session_name, int userid, int process_id)
//        {
//            try
//            {
//                //var result = CustomerService.Add(customer);
//                SessionName = session_name;
//                loading(session_name, process_id, userid);
//                string qr_data = run(session_name, process_id, userid);
//                if (qr_data.Equals("1"))
//                {
//                    return Ok("success");
//                }
//                using (MemoryStream memoryStream = new MemoryStream())
//                {
//                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
//                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qr_data, QRCodeGenerator.ECCLevel.Q);
//                    QRCode qRCode = new QRCode(qRCodeData);
//                    using (Bitmap bitmap = qRCode.GetGraphic(20))
//                    {
//                        bitmap.Save(memoryStream, ImageFormat.Png);
//                        qr_data = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
//                    }
//                }
//                Instance instance = new Instance()
//                {
//                    Process_id = process_id,
//                    Session_name = session_name,
//                    Data = qr_data
//                };
//                return Ok(instance);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }
//        public void loading(string session_name, int processId, int userid)
//        {
         
//             //checking if user already have whatsapp instance 
//             try
//             {
//                 result = whatsappService.Get(userid);
//                 if(result != null)
//                 {
//                     processId = result.ProcessId;
//                    SessionName = result.SessionPath;
//                 }
//                if (Directory.Exists(folderpath + session_name))
//                {
//                    #region Finding opened browser id
//                    ManagementObjectSearcher searcher = new ManagementObjectSearcher
//                     ("Select * From Win32_Process Where ParentProcessID=" + processId);
//                    ManagementObjectCollection moc = searcher.Get();
//                    foreach (ManagementObject mo in moc)
//                    {
//                        int proc = Convert.ToInt32(mo["ProcessID"]);
//                        Process process1 = Process.GetProcessById(proc);
//                        if (process1 != null)
//                            process1.Kill();
//                    }

//                    #endregion
//                    //Kill the browser if already open to avoid exceptions bcoz chrome can't run same session at once
//                    Process process = Process.GetProcessById(processId);
//                    if (process != null)
//                        process.Kill();
//                }
//             }
//             catch(Exception ex)
//             {
//                 Console.WriteLine(ex.Message);
//             }
                
            
//            ChromeDriverService cService = ChromeDriverService.CreateDefaultService(@"C:\Users\JoelZapp\source\repos\Marketing-crm\SoftMarketing.WebAPI\bin\Debug\");
//            cService.HideCommandPromptWindow = true;
//            options.AddArgument("--user-data-dir=" + folderpath + SessionName);


            
//            driver = new ChromeDriver(cService, options);
//            process_id = cService.ProcessId;
//            try
//            {
//                Whatsapp whatsapp = new Whatsapp()
//                {
//                    UserId = userid,
//                    ApiKey = SessionName,
//                    SessionPath = SessionName,
//                    Status = "NOTLOGIN",
//                    ProcessId = process_id,
//                    Active = 1
//                };
//                if(result == null)
//                {
//                    var res = whatsappService.Add(whatsapp);
//                }
//                else
//                {
//                    whatsapp.Id = result.Id;
//                    var res = whatsappService.Update(whatsapp);
//                }
                
                


//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//        public string run(string session_name, int processId, int userid)
//        {
//            string qr_data = "";
//            var currenturl = driver.Url;
//            if (currenturl.Contains("web.whatsapp.com") == false)
//            {
//                driver.Navigate().GoToUrl("https://web.whatsapp.com");
//            }
//            Console.WriteLine("Login to WhatsApp Web Scan The QR CODE From Browser");
//            CheckLoggedIn();
//            if (logininfo != "LOGIN")
//            {
               
//                var data = driver.FindElements(By.XPath("//*[@id='app']/div/div/div[2]/div[1]/div/div[2]/div")).SingleOrDefault();
//                if(data != null)
//                    qr_data = data.GetAttribute("data-ref");
//                else
//                {
//                    Thread.Sleep(10000);
//                    data = driver.FindElements(By.XPath("//*[@id='app']/div/div/div[2]/div[1]/div/div[2]/div")).SingleOrDefault();
//                    qr_data = data.GetAttribute("data-ref");
//                }
//            }
//            else if (logininfo == "LOGIN")
//            {
//                return "1";
//            }
//            return qr_data;
//        }
//        public void CheckLoggedIn()
//        {
//            Thread.Sleep(7000);
//            var checked1 = driver.FindElements(By.XPath("//*[@id='side']/header/div[2]/div/span/div[3]/div/span")).SingleOrDefault();
//            if (checked1 != null)
//            {
                
//                logininfo = "LOGIN";
//                Whatsapp whatsapp = new Whatsapp()
//                {
//                    UserId = result.UserId,
//                    ApiKey = SessionName,
//                    SessionPath = SessionName,
//                    Status = "LOGIN",
//                    ProcessId = process_id,
//                    Active = 1
//                };
//                if (result == null)
//                {
//                    var res = whatsappService.Add(whatsapp);
//                }
//                else
//                {
//                    whatsapp.Id = result.Id;
//                    var res = whatsappService.Update(whatsapp);
//                }

//            }
//            else
//            {
//                var data = driver.FindElements(By.XPath("//*[@id='app']/div/div/div[2]/div[1]/div/div[2]/div")).SingleOrDefault();
//                if (data == null)
//                {
//                    CheckLoggedIn();
//                }
//                else
//                {
//                    logininfo = "NOTLOGIN";
//                }
                
//            }
//        }
//        [HttpPost]
//        [Route("/SendMessage")]
//        public void SendMessage([FromBody] Message messages)
//        {
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
//            var checked1 = driver.FindElements(By.XPath("//*[@id='app']/div/div/div[2]/div[1]/div/div[2]/div")).SingleOrDefault();
//            if (checked1 == null)
//            {
//                Console.WriteLine("ENTER THE MOBILE NUMBER ");
//                var mobile = Console.ReadLine();
//                Console.WriteLine("ENTER THE MESSAGE ");
//                var msg = Console.ReadLine();
//                string url = "https://web.whatsapp.com/send?phone=" + mobile + "&text=" + Uri.EscapeDataString(msg);
//                driver.Navigate().GoToUrl(url);
//                Thread.Sleep(2000);
//                var errormsg = driver.FindElements(By.XPath("//*[@id='app']/div/span[2]/div/span/div/div/div/div/div/div[2]/div/div/div/div"));
//                if (errormsg != null && errormsg.Count > 0)
//                {
//                    Thread.Sleep(2000);
//                    var messageshow = driver.FindElement(By.XPath("//*[@id='app']/div/span[2]/div/span/div/div/div/div/div/div[1]")).Text;
//                    Console.WriteLine("Message Failed Due To " + messageshow);
//                }
//                else
//                {
//                    var sendButton = driver.FindElements(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[2]/button")).SingleOrDefault(); //Click SEND Arrow Button
//                    if (sendButton != null)
//                    {
//                        sendButton.Click();
//                    }
//                    else
//                    {
//                        Thread.Sleep(1000);
//                        sendButton = driver.FindElements(By.XPath("//*[@id='main']/footer/div[1]/div/span[2]/div/div[2]/div[2]/button")).SingleOrDefault();
//                        sendButton.Click();
//                    }
//                    Console.WriteLine("Message Send Successfully ");
//                }
//            }
//            else
//            {
//                logininfo = "NOTLOGIN";
//            }
//        }
//    }
//}
