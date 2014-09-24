using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using MACPortal.DAL;
using MACPortal.Extensions;
using MACPortal.Filters;
using MACPortal.Models;
using MACPortal.Models.Users;
using OfficeOpenXml;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace MACPortal.Controllers
{
    [AuthorizeUser(Roles = "Admin")]
    public class AdminController : BootstrapBaseController
    {
        private PortalContext db = new PortalContext();
        private SimpleRoleProvider roles;
        private SimpleMembershipProvider membership;

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            var employees = db.Employees;
            ViewBag.PreRegisteredCount = employees.Count(e => e.CurrentAcceptedAgreement == UserAgreement.CurrentVersion);
            ViewBag.AvatarCount = employees.Count(e => e.AvatarHair != null);
            return View();
        }
        
        public ActionResult ResetUser(string name, bool full = false)
        {
            try
            {
                var employee = db.Employees.First(e => e.ComercialName.ToUpper() == name.ToUpper());
                employee.CurrentAcceptedAgreement = null;
                employee.AvatarHair = null;
                employee.AvatarFace = null;
                employee.AvatarEyes = null;
                employee.AvatarEars = null;
                employee.AvatarMouth = null;
                employee.AvatarNose = null;
                employee.AvatarClothes = null;
                employee.AvatarAccessoryBody = null;
                employee.AvatarAccessoryHead = null;
                employee.AvatarAccessoryFace = null;
                employee.AvatarHairColor = null;
                employee.AvatarClothesColor = null;
                employee.AvatarEyesColor = null;
                employee.AvatarSkinColor = null;
                if (full)
                {
                    employee.Gender = null;
                    employee.Birthday = null;
                    employee.HomePhone = null;
                    employee.CellPhone = null;
                    employee.CEP = null;
                    employee.Street = null;
                    employee.Number = null;
                    employee.Complement = null;
                    employee.Neighborhood = null;
                    employee.City = null;
                    employee.State = null;
                    employee.AcceptSMS = null;
                    employee.AcceptEmail = null;
                }
                db.SaveChanges();
            }
            catch (Exception)
            {
                Error("O Usuário com nome comercial '" + name + "' não foi encontrado");
                return RedirectToAction("Index");
            }
            Success("O Usuário com nome comercial '" + name + "' foi reiniciado!");
            return RedirectToAction("Index");
        }

        public ActionResult CoordinatorEnterprises(string name)
        {
            var coord = (Coordinator)db.Employees.FirstOrDefault(e => e is Coordinator && e.ComercialName.ToUpper() == name.ToUpper());
            if (coord == null)
            {
                var contentNull = db.Enterprises.ToList().Aggregate("Empreendimentos: ", (current, enterprise) => current + (enterprise.Name + ", "));
                Success(contentNull);
                return RedirectToAction("Index");
            }
            var content = coord.Enterprises.Aggregate("Empreendimentos: ", (current, enterprise) => current + (enterprise.Name + ", "));
            Success(content);
            return RedirectToAction("Index");
        }

        public ActionResult SetAllActives()
        {
            db.Employees.ForEach(e => e.Active = true);
            db.SaveChanges();
            Success("Usuario resetados como Ativos!");
            return RedirectToAction("Index");
        }

        public ActionResult CleanEnterprises(bool allEnterprises = false)
        {
            var entreprisesNames = new List<string>();
            var removeCount = 0;
            foreach (var enterprise in db.Enterprises)
            {
                if (entreprisesNames.Contains(enterprise.Name) || allEnterprises)
                {
                    db.Enterprises.Remove(enterprise);
                    removeCount++;
                }
                else
                {
                    entreprisesNames.Add(enterprise.Name);
                }
            }
            db.SaveChanges();
            Success("Foram Removidos " + removeCount + " Empreendimentos Duplicados.");
            return RedirectToAction("Index");
        }

        public ActionResult ResetSales()
        {
            db.Sales.RemoveRange(db.Sales);
            db.Employees.ForEach(e => e.TempPoints = 0);
            db.SaveChanges();
            Success("As vendas foram reiniciadas com sucesso!");
            return RedirectToAction("Index");
        }


        Dictionary<string, string[]> alias = new Dictionary<string, string[]>();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportExcell(HttpPostedFileBase Excell)
        {
            var httpPostedFileBase = Excell;
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(httpPostedFileBase.FileName);
                var targetFolder = Server.MapPath("~/Content/uploaded/excell");
                if (!System.IO.Directory.Exists(targetFolder))
                {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }
                string path = string.Format("{0}/{1}", targetFolder,
                                            DateTime.Now.GetTimestamp() + "_" + httpPostedFileBase.FileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                httpPostedFileBase.SaveAs(path);

                string filePath = path;
                var existingFile = new FileInfo(filePath);

                // Open and read the XlSX file.
                using (var package = new ExcelPackage(existingFile))
                {
                    roles = (SimpleRoleProvider) Roles.Provider;
                    membership = (SimpleMembershipProvider) Membership.Provider;
                    try
                    {
                        // Get the work book in the file
                        ExcelWorkbook workBook = package.Workbook;
                        if (workBook != null)
                        {

                            if (workBook.Worksheets.Count > 0)
                            {
                                Dictionary<string, int> sheetHeaders;
                                ExcelWorksheet currentWorksheet;
                                int columnNumber;
                                int rowNumber;

                                //Global Variables
                                currentWorksheet = workBook.Worksheets.FirstOrDefault(e => e.Name == "Global");
                                var onlineCoordinatorName = "XX";
                                if (currentWorksheet != null)
                                {
                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                        columnNumber <= currentWorksheet.Dimension.End.Column;
                                        columnNumber++)
                                    {
                                        var cell = currentWorksheet.Cells[1, columnNumber];
                                        if (cell == null) continue;
                                        var cellValue = cell.Value;
                                        if (cellValue == null) continue;
                                        sheetHeaders.Add((string)cellValue,
                                            columnNumber);
                                    }
                                    onlineCoordinatorName =
                                                currentWorksheet.Cells[2, sheetHeaders["Coordenador Online"]]
                                                    .Value.ToString()
                                                    .Trim();
                                    for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 2].Value == null)
                                        {
                                            continue;
                                        }
                                        if (sheetHeaders.ContainsKey("Alias"))
                                        {
                                            var rawAlias = currentWorksheet.Cells[rowNumber, sheetHeaders["Alias"]].Value.ToString()
                                                .Trim().Split(';');
                                            var aliasKey = rawAlias[0];
                                            var aliasList = new List<string>();
                                            for (int i = 1; i < rawAlias.Length; i++)
                                            {
                                                aliasList.Add(rawAlias[i]);
                                            }
                                            alias[aliasKey] = aliasList.ToArray();
                                        }
                                    }
                                }

                                //Coordinators
                                var coordinators = new List<Coordinator>();
                                currentWorksheet = workBook.Worksheets.FirstOrDefault(e => e.Name == "Coordenadores");
                                if (currentWorksheet != null)
                                {
                                    //If the coordinators table is in this sheet, reset all employees active status
                                    db.Employees.ForEach(e => e.Active = false);

                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                         columnNumber <= currentWorksheet.Dimension.End.Column;
                                         columnNumber++)
                                    {
                                        sheetHeaders.Add((string) currentWorksheet.Cells[1, columnNumber].Value,
                                                         columnNumber);
                                    }
                                    for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                                        {
                                            continue;
                                        }
                                        var CPF =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["CPF"]].Value.ToString()
                                                                                                  .Replace(".",
                                                                                                           String.Empty)
                                                                                                  .Replace("-",
                                                                                                           String.Empty)
                                                                                                  .Trim();
                                        var employee = GetEmployee<Coordinator>(CPF);
                                        var brokerTypeRaw =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Tipo"]].Value.ToString().ToUpper();
                                        employee.BrokerType = brokerTypeRaw == "ONLINE"
                                                           ? BrokerType.ONLINE
                                                           : (brokerTypeRaw == "PRESENCIAL"
                                                                  ? BrokerType.PRESENCIAL
                                                                  : BrokerType.INVALID);
                                        if (String.IsNullOrEmpty(employee.Name)) employee.Name =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome Completo"]].Value
                                                                                                            .ToString()
                                                                                                            .Trim();
                                        employee.ComercialName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome Comercial"]].Value
                                                                                                             .ToString()
                                                                                                             .Trim();
                                        employee.CPF = CPF;
                                        if (String.IsNullOrEmpty(employee.Email)) employee.Email =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["E-mail"]].Value.ToString()
                                                                                                     .Trim();

                                        employee.Active =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Ativo"]].Value.ToString()
                                                                                                     .Trim() == "SIM";

                                        coordinators.Add(employee);
                                        if (db.Employees.Any(e => e is Coordinator && e.CPF == CPF))
                                        {
                                            db.Employees.Attach(employee);
                                            ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager
                                                                        .ChangeObjectState(employee,
                                                                                           EntityState.Modified);
                                        }
                                        else
                                        {
                                            db.Employees.Add(employee);
                                        }
                                    }
                                    db.SaveChanges();
                                }

                                //Managers
                                var managers = new List<Manager>();
                                currentWorksheet = workBook.Worksheets.FirstOrDefault(e => e.Name == "Gerentes");
                                if (currentWorksheet != null)
                                {

                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                         columnNumber <= currentWorksheet.Dimension.End.Column;
                                         columnNumber++)
                                    {
                                        sheetHeaders.Add((string) currentWorksheet.Cells[1, columnNumber].Value,
                                                         columnNumber);
                                    }
                                    for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                                        {
                                            continue;
                                        }
                                        var CPF =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["CPF"]].Value.ToString()
                                                                                                  .Replace(".",
                                                                                                           String.Empty)
                                                                                                  .Replace("-",
                                                                                                           String.Empty)
                                                                                                  .Trim();
                                        var employee = GetEmployee<Manager>(CPF);

                                        var brokerTypeRaw =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Tipo"]].Value.ToString().ToUpper();
                                        employee.BrokerType = brokerTypeRaw == "ONLINE"
                                                           ? BrokerType.ONLINE
                                                           : (brokerTypeRaw == "PRESENCIAL"
                                                                  ? BrokerType.PRESENCIAL
                                                                  : BrokerType.INVALID);

                                        if (String.IsNullOrEmpty(employee.Name))employee.Name =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome Completo"]].Value
                                                                                                            .ToString()
                                                                                                            .Trim();
                                        employee.ComercialName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome Comercial"]].Value
                                                                                                             .ToString()
                                                                                                             .Trim();
                                        employee.CPF = CPF;
                                        if (String.IsNullOrEmpty(employee.Email)) employee.Email =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["E-mail"]].Value.ToString().Trim();
                                        
                                        employee.Active =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Ativo"]].Value.ToString()
                                                                                                     .Trim() == "SIM";                                                            

                                        managers.Add(employee);
                                        if (db.Employees.Any(e => e is Manager && e.CPF == CPF))
                                        {
                                            db.Employees.Attach(employee);
                                            ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager
                                                                        .ChangeObjectState(employee,
                                                                                           EntityState.Modified);
                                        }
                                        else
                                        {
                                            db.Employees.Add(employee);
                                        }
                                    }
                                    db.SaveChanges();
                                }

                                //Brokers
                                currentWorksheet = workBook.Worksheets.FirstOrDefault(e => e.Name == "Corretores");
                                if (currentWorksheet != null)
                                {
                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                         columnNumber <= currentWorksheet.Dimension.End.Column;
                                         columnNumber++)
                                    {
                                        sheetHeaders.Add((string) currentWorksheet.Cells[1, columnNumber].Value,
                                                         columnNumber);
                                    }
                                    for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                                        {
                                            continue;
                                        }
                                        var CPF =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["CPF"]].Value.ToString()
                                                                                                  .Replace(".",
                                                                                                           String.Empty)
                                                                                                  .Replace("-",
                                                                                                           String.Empty)
                                                                                                  .Trim();
                                        var brokerTypeRaw =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Tipo"]].Value
                                                                                                            .ToString()
                                                                                                            .ToUpper();
                                        var brokerType = brokerTypeRaw == "ONLINE"
                                                           ? BrokerType.ONLINE
                                                           : (brokerTypeRaw == "PRESENCIAL"
                                                                  ? BrokerType.PRESENCIAL
                                                                  : BrokerType.INVALID);
                                        if (brokerType == BrokerType.INVALID)
                                        {
                                            throw new InvalidDataException(String.Format("Invalid Broker Type '{0}'! (At row {1})",
                                                                                         brokerType, rowNumber));
                                        }
                                        var employee = GetEmployee<Broker>(CPF);
                                        var manager =
                                            managers.FirstOrDefault(
                                                m =>
                                                m.ComercialName.ToUpper() ==
                                                currentWorksheet.Cells[rowNumber, sheetHeaders["Gerente"]].Value
                                                                                                          .ToString()
                                                                                                          .Trim()
                                                                                                          .ToUpper());
                                        if (String.IsNullOrEmpty(employee.Name)) employee.Name =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome Completo"]]
                                                .Value
                                                .ToString
                                                ().Trim();
                                        employee.ComercialName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome Comercial"]]
                                                .Value
                                                .ToString
                                                ().Trim();
                                        employee.CPF = CPF;
                                        employee.Manager = manager;
                                        employee.ManagerID = manager != null ? manager.UserID : 0;
                                        if (String.IsNullOrEmpty(employee.Email)) employee.Email =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["E-mail"]].Value
                                                                                                     .ToString().Trim();
                                        employee.Active =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Ativo"]].Value.ToString()
                                                                                                     .Trim() == "SIM";
                                        employee.BrokerType = brokerType;
                                        if (db.Employees.Any(e => e is Broker && e.CPF == CPF))
                                        {
                                            db.Employees.Attach(employee);
                                            ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager
                                                                        .ChangeObjectState(employee,
                                                                                           EntityState.Modified);
                                        }
                                        else
                                        {
                                            db.Employees.Add(employee);
                                        }
                                    }
                                    db.SaveChanges();
                                }

                                //Enterprises
                                currentWorksheet =
                                    workBook.Worksheets.FirstOrDefault(e => e.Name == "Empreendimentos");
                                if (currentWorksheet != null)
                                {
                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                         columnNumber <= currentWorksheet.Dimension.End.Column;
                                         columnNumber++)
                                    {
                                        sheetHeaders.Add((string) currentWorksheet.Cells[1, columnNumber].Value,
                                                         columnNumber);
                                    }
                                    for (rowNumber = 2;
                                         rowNumber <= currentWorksheet.Dimension.End.Row;
                                         rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                                        {
                                            continue;
                                        }
                                        var rawCoordinators =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Coordenadores"]];
                                        var strCoordinators = rawCoordinators == null
                                                                  ? null
                                                                  : (rawCoordinators.Value == null
                                                                         ? null
                                                                         : rawCoordinators.Value.ToString());
                                        strCoordinators = strCoordinators == null
                                                              ? null
                                                              : strCoordinators.Trim().ToUpper();
                                        var eCoordinators = new List<Employee>();
                                        if (!String.IsNullOrEmpty(strCoordinators))
                                        {
                                            var coordinatorsNames = strCoordinators.Split(';');
                                            foreach (var coordinatorsName in coordinatorsNames)
                                            {
                                                var targetName = coordinatorsName.Trim();
                                                var employee = GetEmployeeWithAlias(targetName);
                                                if (employee != null)
                                                {
                                                    eCoordinators.Add(employee);
                                                }
                                            }
                                        }
                                        var modelId =
                                            Convert.ToInt32(
                                                currentWorksheet.Cells[rowNumber, sheetHeaders["Código"]].Value);
                                        var enterprise =
                                            db.Enterprises.SingleOrDefault(e => e.Code == modelId) ??
                                            new Enterprise
                                                {
                                                    Code = modelId
                                                };
                                        

                                        if (db.Enterprises.Any(e => e.EnterpriseID == enterprise.EnterpriseID))
                                        {
                                            db.Enterprises.Attach(enterprise);

                                            enterprise.Name =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome"]].Value.ToString
                                                ().Trim();

                                            enterprise.Modifier =
                                                Convert.ToDecimal(
                                                    currentWorksheet.Cells[rowNumber, sheetHeaders["Modificador"]].Value);

                                            enterprise.CoordinatorsAsEmployees = new Collection<Employee>();
                                            foreach (var eCoordinator in eCoordinators)
                                            {
                                                enterprise.CoordinatorsAsEmployees.Add(eCoordinator);
                                            }
                                            ((IObjectContextAdapter) db).ObjectContext.ObjectStateManager
                                                                        .ChangeObjectState(enterprise,
                                                                                           EntityState.Modified);
                                        }
                                        else
                                        {
                                            enterprise.Name =
                                               currentWorksheet.Cells[rowNumber, sheetHeaders["Nome"]].Value.ToString
                                                   ().Trim();

                                            enterprise.Modifier =
                                                Convert.ToDecimal(
                                                    currentWorksheet.Cells[rowNumber, sheetHeaders["Modificador"]].Value);

                                            if (enterprise.CoordinatorsAsEmployees == null)
                                            {
                                                enterprise.CoordinatorsAsEmployees = new Collection<Employee>();
                                            }
                                            foreach (var eCoordinator in eCoordinators)
                                            {
                                                enterprise.CoordinatorsAsEmployees.Add(eCoordinator);
                                            }
                                            db.Enterprises.Add(enterprise);
                                        }
                                        db.SaveChanges();
                                    }
                                }

                                //Sales
                                currentWorksheet =
                                    workBook.Worksheets.FirstOrDefault(e => e.Name == "Acumulado dos Corretores") ??
                                    workBook.Worksheets.FirstOrDefault(e => e.Name == "Plan1") ??
                                    workBook.Worksheets.FirstOrDefault(e => e.Name == "Plan2") ??
                                    workBook.Worksheets.FirstOrDefault(e => e.Name == "Plan3");
                                if (currentWorksheet != null)
                                {
                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                         columnNumber <= currentWorksheet.Dimension.End.Column;
                                         columnNumber++)
                                    {
                                        if ((string)currentWorksheet.Cells[1, columnNumber].Value == null) continue;
                                        
                                        sheetHeaders.Add((string) currentWorksheet.Cells[1, columnNumber].Value,
                                                         columnNumber);
                                    }

                                    if (!sheetHeaders.ContainsKey("Código"))
                                    {
                                        columnNumber = currentWorksheet.Dimension.End.Column + 1;
                                        currentWorksheet.Cells[1, columnNumber].Value = "Código";
                                        sheetHeaders.Add((string)currentWorksheet.Cells[1, columnNumber].Value, columnNumber);
                                    }

                                    for (rowNumber = 2;
                                         rowNumber <= currentWorksheet.Dimension.End.Row;
                                         rowNumber++)
                                    {
                                        /**
                                         * VALIDATE ROW
                                         */
                                        var brokerNameRaw =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome"]].Value;
                                        if (brokerNameRaw == null)
                                        {
                                            continue;
                                        }

                                        /**
                                         * SALE TYPE
                                         */
                                        var saleTypeRaw =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Tipo de Venda"]].Value
                                                                                                            .ToString()
                                                                                                            .ToUpper();
                                        var saleType = saleTypeRaw == "DIRETA" || saleTypeRaw == "SALÃO"
                                                           ? SaleType.DIRECT
                                                           : (saleTypeRaw == "TERCEIROS"
                                                                  ? SaleType.THIRD_PARTY
                                                                  : (saleTypeRaw == "REVENDA"
                                                                    ? SaleType.RESALE
                                                                    : SaleType.INVALID));
                                        if (saleType == SaleType.INVALID)
                                        {
                                            throw new InvalidDataException(String.Format("Invalid Sale Type '{0}'! (At row {1})",
                                                                                         saleTypeRaw, rowNumber));
                                        }

                                        if (saleType == SaleType.RESALE)
                                        {
                                            continue;
                                        }

                                        /**
                                         * SALE STYLE
                                         */
                                        var saleStyleRaw =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Estilo Venda"]].Value
                                                                                                           .ToString()
                                                                                                           .ToUpper();
                                        var saleStyle = saleStyleRaw == "LANÇAMENTO"
                                                            ? SaleStyle.RELEASE
                                                            : (saleStyleRaw == "ESTOQUE"
                                                                   ? SaleStyle.STOCK
                                                                   : SaleStyle.INVALID);
                                        if (saleStyle == SaleStyle.INVALID)
                                        {
                                            throw new InvalidDataException(String.Format("Invalid Sale Style '{0}'!",
                                                                                         saleStyleRaw));
                                        }

                                        /**
                                         * BROKER AND THIRD PARTY COORDINATOR
                                         */
                                        var brokerName = brokerNameRaw.ToString().ToUpper();
                                        Employee thirdPartyCoordinator = null;
                                        Employee broker = null;
                                        if (saleType == SaleType.DIRECT)
                                        {
                                            broker = GetEmployeeWithAlias(brokerName);
                                            if (broker == null)
                                            {
                                                throw new InvalidDataException(String.Format("Could not find broker with name '{0}'", brokerName));
                                            }
                                        } 
                                        else if (saleType == SaleType.THIRD_PARTY)
                                        {
                                            var thirdPartyCoordinatorName = brokerName.Trim();
                                            try
                                            {
                                                thirdPartyCoordinator = GetEmployeeWithAlias(thirdPartyCoordinatorName);
                                            }
                                            catch (Exception e)
                                            {
                                                throw new InvalidDataException(String.Format(
                                                    "Found more than one third party coordinator with name '{0}'",
                                                    thirdPartyCoordinatorName));
                                            }
                                            if (thirdPartyCoordinator == null)
                                            {
                                                throw new InvalidDataException(String.Format("Could not find third party coordinator with name '{0}'", thirdPartyCoordinatorName));
                                            }
                                        }

                                        /**
                                         * MANAGER
                                         */
                                        var managerName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Gerente"]].Value.ToString()
                                                                                                      .ToUpper();
                                        Employee manager;
                                        try
                                        {
                                            manager = GetEmployeeWithAlias(managerName);
                                        }
                                        catch (Exception)
                                        {
                                            throw new InvalidDataException(String.Format(
                                                "Found more than one manager with name '{0}'",
                                                managerName));
                                        }
                                        if (manager == null)
                                        {
                                            throw new InvalidDataException(String.Format(
                                                "Could not find manager with name '{0}'",
                                                managerName)
                                                );
                                        }
                                        /*
                                        if (broker != null && (saleType == SaleType.DIRECT && manager.UserID != broker.Manager.UserID))
                                        {
                                            
                                            throw new InvalidDataException(String.Format(
                                                "Manager Name '{0}' is not the same as '{1}', the '{2}' Broker's registered manager!",
                                                managerName, broker.Manager.ComercialName, broker.ComercialName)
                                                );
                                        }*/

                                        /**
                                         * ENTERPRISE
                                         */
                                        var enterpriseName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Empreendimento"]]
                                                .Value.ToString().ToUpper().Trim();
                                        if (enterpriseName == "AVANTI CLUBE") continue;
                                        Enterprise enterprise;
                                        try
                                        {
                                            enterprise = db.Enterprises.Single(e => e.Name.ToUpper() == enterpriseName);
                                        }
                                        catch (Exception)
                                        {
                                            throw new InvalidDataException(String.Format("Empreendimento '{0}' não foi encontrado.", enterpriseName));
                                        }

                                        /**
                                         * COORDINATORS
                                         */
                                        var coordinatorsNamesRaw = sheetHeaders.ContainsKey("Coord. Access") ? currentWorksheet.Cells[
                                            rowNumber, sheetHeaders["Coord. Access"]].Value.ToString().
                                            Trim().ToUpper() : "XX";
                                        var eCoordinators = enterprise.CoordinatorsAsEmployees.ToList();
                                        if (!(String.IsNullOrEmpty(coordinatorsNamesRaw) || coordinatorsNamesRaw == "XX"))
                                        {
                                            var coordinatorsNames = coordinatorsNamesRaw.Split('/');
                                            eCoordinators =
                                                db.Employees.Where(e => coordinatorsNames.Contains(e.ComercialName)).ToList();
                                        }                                  

                                        /**
                                         * ONLINE COORDINATOR
                                         */
                                        Employee onlineCoordinator = null;
                                        if (!String.IsNullOrEmpty(onlineCoordinatorName) &&
                                            onlineCoordinatorName != "XX")
                                        {
                                            try
                                            {
                                                onlineCoordinator = GetEmployeeWithAlias(onlineCoordinatorName);
                                            }
                                            catch (Exception)
                                            {
                                                throw new InvalidDataException(String.Format(
                                                    "Found more than one online coordinator with name '{0}",
                                                    onlineCoordinatorName));
                                            }
                                        }
                                        else
                                        {
                                            //throw new Exception("No Online Coordinator was specified in the 'Global' worksheet!");
                                        }

                                        /**
                                         * TOWER, UNIT, SALE DATE, DATE, VALUE
                                         */
                                        var tower =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Torre"]].Value.ToString();
                                        var unit =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Unidade"]].Value.ToString();
                                        var saleDate =
                                            DateTime.Parse(
                                                currentWorksheet.Cells[rowNumber, sheetHeaders["Data da Venda"]].Value
                                                                                                                .ToString
                                                    ());
                                        var date = 
                                            DateTime.Parse(sheetHeaders.ContainsKey("Data") ? 
                                                currentWorksheet.Cells[rowNumber, sheetHeaders["Data"]].Value
                                                                                                                .ToString
                                                                                                                () : "01/01/1900");
                                        decimal value;
                                        var valueAsString =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["VGV"]].Value.ToString().Trim();
                                        try
                                        {
                                            value = Convert.ToDecimal(valueAsString);
                                        }
                                        catch (Exception)
                                        {
                                            
                                            throw new Exception(String.Format(
                                                "Failed to parse String to Decimal: '{0}' in row: '{1}'", valueAsString, rowNumber));
                                        }

                                        /**
                                         * CODE ID
                                         */
                                        var idRaw = currentWorksheet.Cells[rowNumber, sheetHeaders["Código"]].Value;

                                        Sale sale = null;
                                        if (idRaw != null)
                                        {
                                            var saleID = Convert.ToInt32(idRaw.ToString());
                                            sale = db.Sales.SingleOrDefault(s => s.SaleID == saleID);
                                        }

                                        /**
                                         * CREATES A NEW SALE
                                         */
                                        if (sale == null)
                                        {
                                            sale = new Sale();
                                            sale.SaleDate = saleDate;
                                            sale.Date = date;
                                            sale.Tower = tower;
                                            sale.Unit = unit;
                                            sale.Style = saleStyle;
                                            sale.Type = saleType;
                                            sale.Value = value;
                                            sale.Modifier = enterprise.Modifier;
                                            sale.Enterprise = enterprise;
                                            sale.EnterpriseID = enterprise.EnterpriseID;

                                            if (sale.Employees == null)
                                            {
                                                sale.Employees = new Collection<Employee>();
                                            }
                                            if (saleType == SaleType.DIRECT && broker != null)
                                            {
                                                sale.BrokerID = broker.UserID;
                                                sale.ManagerID = manager.UserID;
                                                sale.Employees.Add(broker);
                                                sale.Employees.Add(manager);

                                                if (onlineCoordinator != null && broker.BrokerType == BrokerType.ONLINE)
                                                {
                                                    sale.Employees.Add(onlineCoordinator);
                                                    sale.OnlineCoordinatorID = onlineCoordinator.UserID;
                                                }
                                            }
                                            else if (saleType == SaleType.THIRD_PARTY)
                                            {
                                                sale.ThirdPartyCoordinatorID = thirdPartyCoordinator.UserID;
                                                sale.ManagerID = manager.UserID;
                                                sale.Employees.Add(thirdPartyCoordinator);
                                                sale.Employees.Add(manager);

                                                if (onlineCoordinator != null && thirdPartyCoordinator.BrokerType == BrokerType.ONLINE)
                                                {
                                                    sale.Employees.Add(onlineCoordinator);
                                                    sale.OnlineCoordinatorID = onlineCoordinator.UserID;
                                                }
                                            }

                                            sale.NumberOfCoordinators = eCoordinators.Count;
                                            foreach (var eCoordinator in eCoordinators)
                                            {
                                                sale.Employees.Add(eCoordinator);
                                            }

                                            db.Sales.Add(sale);

                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Código"]].Value = sale.SaleID;
                                        }
                                        /**
                                         * EDITS EXISTING SALE
                                         */
                                        else
                                        {
                                            db.Sales.Attach(sale);

                                            sale.SaleDate = saleDate;
                                            sale.Date = date;
                                            sale.Tower = tower;
                                            sale.Unit = unit;
                                            sale.Style = saleStyle;
                                            sale.Type = saleType;
                                            sale.Value = value;
                                            sale.Modifier = enterprise.Modifier;
                                            sale.Enterprise = enterprise;
                                            sale.EnterpriseID = enterprise.EnterpriseID;

                                            sale.Employees.Clear();
                                            if (saleType == SaleType.DIRECT && broker != null)
                                            {
                                                sale.ThirdPartyCoordinatorID = 0;
                                                sale.BrokerID = broker.UserID;
                                                sale.ManagerID = manager.UserID;
                                                sale.Employees.Add(broker);
                                                sale.Employees.Add(manager);

                                                if (onlineCoordinator != null && broker.BrokerType == BrokerType.ONLINE)
                                                {
                                                    sale.Employees.Add(onlineCoordinator);
                                                    sale.OnlineCoordinatorID = onlineCoordinator.UserID;
                                                }
                                                else
                                                {
                                                    sale.OnlineCoordinatorID = 0;
                                                }
                                            }
                                            else if (saleType == SaleType.THIRD_PARTY)
                                            {
                                                sale.BrokerID = 0;
                                                sale.ThirdPartyCoordinatorID = thirdPartyCoordinator.UserID;
                                                sale.ManagerID = manager.UserID;
                                                sale.Employees.Add(thirdPartyCoordinator);
                                                sale.Employees.Add(manager);

                                                if (onlineCoordinator != null && thirdPartyCoordinator.BrokerType == BrokerType.ONLINE)
                                                {
                                                    sale.Employees.Add(onlineCoordinator);
                                                    sale.OnlineCoordinatorID = onlineCoordinator.UserID;
                                                }
                                                else
                                                {
                                                    sale.OnlineCoordinatorID = 0;
                                                }
                                            }
                                            else
                                            {
                                                sale.ThirdPartyCoordinatorID = 0;
                                                sale.BrokerID = 0;
                                                sale.ManagerID = 0;
                                            }

                                            if (eCoordinators != null)
                                            {
                                                sale.NumberOfCoordinators = eCoordinators.Count;
                                                foreach (var eCoordinator in eCoordinators)
                                                {
                                                    sale.Employees.Add(eCoordinator);
                                                }
                                            }
                                            else
                                            {
                                                sale.NumberOfCoordinators = 0;
                                            }
                                            ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager
                                                                        .ChangeObjectState(sale,
                                                                                           EntityState.Modified);
                                        }
                                    }

                                    db.SaveChanges();

                                }



                                //Indications
                                currentWorksheet = workBook.Worksheets.FirstOrDefault(e => e.Name == "Indicações");
                                if (currentWorksheet != null)
                                {
                                    sheetHeaders = new Dictionary<string, int>();
                                    for (columnNumber = 1;
                                         columnNumber <= currentWorksheet.Dimension.End.Column;
                                         columnNumber++)
                                    {
                                        sheetHeaders.Add((string)currentWorksheet.Cells[1, columnNumber].Value,
                                                         columnNumber);
                                    }
                                    for (rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                    {
                                        if (currentWorksheet.Cells[rowNumber, 1].Value == null)
                                        {
                                            continue;
                                        }
                                        var employeeName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Nome"]].Value.ToString().Trim();
                                        var indicatedName =
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Indicado"]].Value.ToString().Trim();
                                        var date = Convert.ToDateTime(
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Data"]].Value.ToString().Trim());
                                        var points = Convert.ToInt32(
                                            currentWorksheet.Cells[rowNumber, sheetHeaders["Pontos"]].Value.ToString().Trim());
                                        var employee = GetEmployeeWithAlias(employeeName);
                                        employee.TempPoints += points;
                                        UpdateModel(employee);
                                        db.Employees.Attach(employee);
                                        ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager
                                                                    .ChangeObjectState(employee,
                                                                                       EntityState.Modified);
                                    }
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        var errorString = e.Message;
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            errorString += "\n\nEntity of type \"" + eve.Entry.Entity.GetType().Name +
                                           "\" in state \"" + eve.Entry.State +
                                           "\" has the following validation errors:";
                            foreach (var ve in eve.ValidationErrors)
                            {
                                errorString += "\n- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage +
                                               "\"";
                            }
                        }
                        Error(errorString + "\n\n" + e);
                        return RedirectToAction("Index");
                    }
                    catch (InvalidDataException e)
                    {
                        Error(e.Message + "\n\n" + e);
                        return RedirectToAction("Index");
                    }
                    catch (InvalidOperationException e)
                    {
                        Error(e.Message + "\n\n" + e);
                        return RedirectToAction("Index");
                    }

                    Success("A planilha foi importada com sucesso!");
                    return RedirectToAction("Index");
                }
            }
            Error("Arquivo inválido ou não existente!");
            return RedirectToAction("Index");
        }

        private Employee GetEmployeeWithAlias(string employeeName, bool activeStatus = true)
        {
            var employee = db.Employees.SingleOrDefault(e => e.ComercialName.ToUpper() == employeeName && e.Active == activeStatus);
            if (employee == null && alias.ContainsKey(employeeName))
            {
                foreach (var aliasName in alias[employeeName])
                {
                    employee =
                        db.Employees.SingleOrDefault(
                            e => e.ComercialName.ToUpper() == aliasName && e.Active == activeStatus);
                    if (employee != null) break;
                }
            }
            if (employee == null)
            {
                if (activeStatus)
                {
                    return GetEmployeeWithAlias(employeeName, false);
                }
                throw new Exception("Could not find Employee with name '" + employeeName + "'");
            }
            return employee;
        }

        private T GetEmployee<T>(string cpf) where T : Employee, new()
        {

            var employee = db.Employees.SingleOrDefault(e => e.CPF == cpf);
            if (employee == null)
            {
                if (membership.GetUser(cpf, false) == null)
                {
                    membership.CreateUserAndAccount(cpf, cpf);
                    roles.AddUsersToRoles(new[] { cpf }, new[] { "Member" });
                }
                employee = new T
                {
                    UserID = membership.GetUserId(cpf)
                };
            }
            if (!(employee is T))
            {
                var target = employee;
                foreach (var eBroker in db.Employees.Where(e => e is Broker).ToArray())
                {
                    var broker = (Broker) eBroker;
                    if (broker.ManagerID != target.UserID) continue;
                    broker.ManagerID = 0;
                    TryUpdateModel(broker);
                }
                db.Employees.Remove(employee);
                employee = new T().CopyFrom(employee);
            }
            return (T)employee;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
