using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using MACPortal.DAL;
using MACPortal.Filters;
using MACPortal.Helpers;
using MACPortal.Mailers;
using MACPortal.Models.Users;
using MACPortal.ViewModel;
using WebMatrix.WebData;

namespace MACPortal.Controllers
{
    [AuthorizeUserAttribute(Roles = "Member")]
    [RedirectToAgreeToTerms]
    public class MemberController : BootstrapBaseController
    {
        private PortalContext db = new PortalContext();

        /**
         * INDEX
         */
        public ActionResult Index()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);

            var employee = db.Employees.First(u => u.UserID == userID);
            var rewards = new RewardVM();
            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
            ranking.SetCurrentUser(employee);
            return View(new MemberHomeVM
                {
                    Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee)),
                    Rewards = rewards,
                    Ranking = ranking
                });
        }

        #region Pre Registration / Closed Site

        /**
         * PRE REGISTER
         */
        public ActionResult PreRegister()
        {
            try
            {
                var membership = (SimpleMembershipProvider) Membership.Provider;
                var userID = membership.GetUserId(Membership.GetUser().UserName);
                var employee = db.Employees.First(u => u.UserID == userID);
                return View(new UserVM().Start(employee));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreRegister(UserVM registration)
        {
            var employee = db.Employees.First(u => u.UserID == registration.UserID);
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                var user = employee.User;
                membership.ResetPasswordWithToken(membership.GeneratePasswordResetToken(user.UserName), registration.NewPassword);
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de sua senha, que não foi alterada.");
            }
            registration.Finish(employee);
            db.SaveChanges();
            return RedirectToAction("PreAvatar");
        }

        /**
         * PRE AVATAR
         */
        public ActionResult PreAvatar()
        {
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                var userID = membership.GetUserId(Membership.GetUser().UserName);
                var employee = db.Employees.First(u => u.UserID == userID);
                return View(new AvatarVM().Start(employee));
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreAvatar(AvatarVM registration)
        {
            var targetFolder = Server.MapPath("~/Content/uploaded/avatars");
            if (!System.IO.Directory.Exists(targetFolder))
            {
                System.IO.Directory.CreateDirectory(targetFolder);
            }
            string fileNameWitPath = targetFolder + "/avatar_" + registration.UserID + ".png";
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(registration.AvatarFile);
                    bw.Write(data);
                    bw.Close();
                }
            }

            var employee = db.Employees.First(u => u.UserID == registration.UserID);
            registration.Finish(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /**
         * PRE REGISTER **DONE**
         */
        public ActionResult PreRegisterDone()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);
            return View(new UserVM().Start(employee, EditUserMode.COMPLETE, "Salvar"));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreRegisterDone(UserVM registration)
        {
            var employee = db.Employees.First(u => u.UserID == registration.UserID);
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                var user = employee.User;
                membership.ResetPasswordWithToken(membership.GeneratePasswordResetToken(user.UserName), registration.NewPassword);
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de sua senha, que não foi alterada.");
            }
            
            registration.Finish(employee);
            db.SaveChanges();
            return View(new UserVM().Start(employee, EditUserMode.COMPLETE, "Salvar"));
        }

        #endregion

        /**
         * CONTACT
         */
        public ActionResult Contact()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);

            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
            var contact = new ContactVM
                {
                    Name = employee.ComercialName,
                    CPF = employee.CPF,
                    Email = employee.Email
                };
            return View(new MemberContactVM
            {
                Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee)),
                Contact = contact
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactVM contact)
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);

            contact.Name = employee.ComercialName;
            contact.CPF = employee.CPF;
            contact.Email = employee.Email;

            var mailer = new UserMailer();
            mailer.Contact(contact.Name, contact.CPF, contact.Email, contact.Title, contact.Message, Server.MapPath("~/Content/images/logo-with-products-smaller.png")).Send();
            Success("Mensagem enviada com sucesso! Em breve entraremos em contato com você.");
            return RedirectToAction("Contact");
        }

        /**
         * RANKING
         */
        public ActionResult Ranking()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employees = db.Employees.ToList();
            var employee = employees.First(u => u.UserID == userID);

            var brokersRanking = new RankingVM(employees, employees.First(e => e is Broker).GetType());
            var managersRanking = new RankingVM(employees, employees.First(e => e is Manager).GetType());
            var coordinatorsRanking = new RankingVM(employees, employees.First(e => e is Coordinator).GetType());

            int ranking;
            if (employee is Broker)
            {
                ranking = brokersRanking.RankingFor(employee);
                brokersRanking.SetCurrentUser(employee);
            } 
            else if (employee is Manager)
            {
                ranking = managersRanking.RankingFor(employee);
                managersRanking.SetCurrentUser(employee);
            } 
            else
            {
                ranking = coordinatorsRanking.RankingFor(employee);
                coordinatorsRanking.SetCurrentUser(employee);
            }

            var memberRanking = new MemberRankingVM
                {
                    Employee = new EmployeeVM().Start(employee, ranking),
                    BrokersRanking = brokersRanking,
                    ManagersRanking = managersRanking,
                    CoordinatorsRanking = coordinatorsRanking,
                    LastUpdate = db.Sales.Max(s => s.SaleDate)
                };
            return View(memberRanking);
        }

        /**
         * POINTS
         */
        public ActionResult Points()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);

            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
            var chartHelper = new EmployeeChartHelper(employee);
            var chart = new DotNet.Highcharts.Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" },
                    Title = new XAxisTitle {Text = "Mês"}
                })
                .SetYAxis(new [] { new YAxis
                    {
                        Title = new YAxisTitle {Text = "Pontos"},
                        Id = "pontos"
                    },
                    new YAxis
                    {
                        Title = new YAxisTitle {Text = "Vendas"},
                        Id = "vendas",
                        AllowDecimals = false
                    }
                })
                .SetSeries(new [] { 
                    new Series
                    {
                        Data = new Data(chartHelper.PointsTotal),
                        Name = "Pontos Totais",
                        YAxis = "pontos",
                        Color = Color.Teal
                    },
                    new Series
                    {
                        Data = new Data(chartHelper.SalesTotal),
                        Name = "Vendas Totais",
                        YAxis = "vendas",
                        Color = Color.DarkRed
                    },
                    new Series
                    {
                        Data = new Data(chartHelper.Points),
                        Name = "Pontos Mensais",
                        YAxis = "pontos",
                        Color = Color.Teal,
                        Type = ChartTypes.Column
                    },
                    new Series
                    {
                        Data = new Data(chartHelper.Sales),
                        Name = "Vendas Mensais",
                        YAxis = "vendas",
                        Color = Color.DarkRed,
                        Type = ChartTypes.Column
                    }
                })
                .SetTitle(new Title{Text = ""})
                .SetCredits(new Credits{Enabled = false});
            return View(new MemberPointsVM
            {
                Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee)),
                Chart = chart
            });
        }

        /**
         * REWARDS
         */
        public ActionResult Rewards(int rewardId = -1, int rewardTier = -1)
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);

            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
            return View(new MemberRewardsVM(new RewardVM())
                {
                    Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee)),
                    GoToId = rewardId > 0 ? rewardId.ToString(CultureInfo.InvariantCulture) : null,
                    GoToTier = rewardTier > 0 ? rewardTier.ToString(CultureInfo.InvariantCulture) : null
                });
        }

        /**
         * REGULATION
         */
        public ActionResult Regulation()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);

            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
            return View(new BaseMemberVM
            {
                Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee))
            });
        }

        #region User Info

        /**
         * EDIT PROFILE
         */
        public ActionResult EditProfile()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);
            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());

            return View(new MemberEditProfileVM
                {
                    User = new UserVM().Start(employee, EditUserMode.INFO, "Salvar"),
                    Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee))
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserVM registration)
        {
            try
            {
                var employee = db.Employees.First(u => u.UserID == registration.UserID);
                registration.Finish(employee);
                db.SaveChanges();
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de seu perfil, por favor tente novamente mais tarde.");
                return RedirectToAction("Index");
            }
            Success("Seu Perfil foi atualizado com sucesso.");
            return RedirectToAction("Index");
        }

        /**
         * EDIT AVATAR
         */
        public ActionResult EditAvatar()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);
            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());

            return View(new MemberEditAvatarVM
            {
                Avatar = new AvatarVM().Start(employee),
                Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee))
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAvatar(AvatarVM registration)
        {
            var targetFolder = Server.MapPath("~/Content/uploaded/avatars");
            if (!System.IO.Directory.Exists(targetFolder))
            {
                System.IO.Directory.CreateDirectory(targetFolder);
            }
            string fileNameWitPath = targetFolder + "/avatar_" + registration.UserID + ".png";
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(registration.AvatarFile);
                    bw.Write(data);
                    bw.Close();
                }
            }

            var employee = db.Employees.First(u => u.UserID == registration.UserID);
            registration.Finish(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /**
         * CHANGE PASSWORD
         */
        public ActionResult ChangePassword()
        {
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var userID = membership.GetUserId(Membership.GetUser().UserName);
            var employee = db.Employees.First(u => u.UserID == userID);
            var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
            return View(new MemberEditProfileVM
            {
                User = new UserVM().Start(employee, EditUserMode.PASSWORD, "Modificar"),
                Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee))
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserVM registration)
        {
            var employee = db.Employees.First(u => u.UserID == registration.UserID);
            try
            {
                var membership = (SimpleMembershipProvider)Membership.Provider;
                var user = employee.User;
                if (!membership.ChangePassword(user.UserName, registration.OldPassword, registration.NewPassword))
                {
                    Error("A senha está incorreta.");
                    var ranking = new RankingVM(db.Employees.ToList(), employee.GetType());
                    return View(new MemberEditProfileVM
                    {
                        User = new UserVM().Start(employee, EditUserMode.PASSWORD, "Modificar"),
                        Employee = new EmployeeVM().Start(employee, ranking.RankingFor(employee))
                    });
                }
            }
            catch (Exception)
            {
                Error("Houve um erro na modificação de sua senha, por favor tente novamente mais tarde.");
                return RedirectToAction("Index");
            }
            Success("Sua senha foi alterada com sucesso.");
            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
