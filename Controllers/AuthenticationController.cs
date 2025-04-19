using Authentication.Interfaces;
using BUMC_FacultyDLL.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPH_CoveragePlanDLL.Model;
using SPH_CoveragePlanWeb.Extensions;
using System.Security.Claims;

namespace SPH_CoveragePlanWeb.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(
            SPH_CoveragePlanDLL.Model.SPH_CoveragePlanContext _context,
            AFR_DLL.Model.BumcProjectContext _contextAFR,
            SPH_CoveragePlanDLL.Interfaces.IClaimsManager _claimsMgr,
            IConfiguration _config,
            SPH_CoveragePlanDLL.Interfaces.IAuthorizationManager _authMgr,
            SPH_CoveragePlanDLL.Interfaces.IAppSettings _appsettings,
            EmailHelper.Interfaces.IEmailHelper _emailHelper,
            BumcFacultyContext _contextFaculty,
            BUMC_FacultyDLL.Interfaces.IFacultyBLL _facultyBLL,
            ProjectV2.Interfaces.IPersonBLL _projPersonBLL,
            ProjectV2.Interfaces.IPersonRoleBLL _projPersonRoleBLL,
            Authentication.Interfaces.IAuthentication _authentication,
            ProjectV2.Interfaces.IProject _project
            )
            : base(_context, _contextAFR, _authMgr, _appsettings, _emailHelper)
        {
            claimsMgr = _claimsMgr;
            config = _config;
            contextFaculty = _contextFaculty;
            facultyBLL = _facultyBLL;
            projPersonBLL = _projPersonBLL;
            authentication = _authentication;
            project = _project;
        }

        private readonly BumcFacultyContext contextFaculty;
        private readonly BUMC_FacultyDLL.Interfaces.IFacultyBLL facultyBLL;
        private readonly ProjectV2.Interfaces.IPersonBLL projPersonBLL;
        private readonly ProjectV2.Interfaces.IPersonRoleBLL persRoleBLL;
        private readonly SPH_CoveragePlanDLL.Interfaces.IClaimsManager claimsMgr;
        private readonly IConfiguration config;
        private readonly Authentication.Interfaces.IAuthentication authentication;
        private readonly ProjectV2.Interfaces.IProject project;
        private const int projectId = SPH_CoveragePlanDLL.Helper.Constants.SPH_CoveragePlan_ProjectId;

        private Authentication.Interfaces.ILoginResponse GetDefaultLoginResponse()
        {
            //Authentication.LoginResponseShibboleth loginResponse = new Authentication.LoginResponseShibboleth() { Alias = "hjcab", BUID = "U05190924", Email = "hjcab@bu.edu", FirstName = "Howard", LastName = "Cabral", FullName = "Howard Cabral", PersonTargetID = "11654", Message = "Success" };
            // Adjunct Faculty
            //Authentication.Model.LoginResponseShibboleth loginResponse = new Authentication.Model.LoginResponseShibboleth() { Alias = "dupuis", BUID = "U72222050", Email = "dupuis@bu.edu", FirstName = "Josee", LastName = "Dupuis", FullName = "Josee Dupuis", PersonTargetID = "6495", Message = "Success" };
            //Authentication.LoginResponseShibboleth loginResponse = new Authentication.LoginResponseShibboleth() { Alias = "pflynn", BUID = "U89087945", Email = "pflynn@bu.edu", FirstName = "Peter", LastName = "Flynn", FullName = "Peter Flynn", PersonTargetID = "1", Message = "Success" };
            Authentication.Model.LoginResponseShibboleth loginResponse = new Authentication.Model.LoginResponseShibboleth() { Alias = "vbe", BUID = "U81067567", Email = "vbe@bu.edu", FirstName = "Vanessa", LastName = "Edouard", FullName = "Vanessa Edouard", PersonTargetID = "58092", Message = "Success" };
            //Authentication.Model.LoginResponseShibboleth loginResponse = new Authentication.Model.LoginResponseShibboleth() { Alias = "jonlevy", BUID = "U40758277", Email = "jonlevy@bu.edu", FirstName = "Jonathan", LastName = "Levy", FullName = "Jonathan Levy", PersonTargetID = "17636", Message = "Success" };

            return loginResponse;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewBag.IsTestEnvironment = bool.Parse(config.GetValue<string>("IsTestEnvironment").ToString());
            return View();
        }

        [AllowAnonymous]
        public IActionResult LoginPostShibboleth(string returnUrl = "")
        {
            try
            {
                Authentication.Interfaces.ILoginResponse loginResponse = GetLoginResponse();
                ProjectV2.Model.Person person = projPersonBLL.GetByLoginResponse(loginResponse);
                return Authorize(this, loginResponse, GetClaims(person), returnUrl).Result;
            }
            catch (Exception ex)
            {
                //return Content(APPSRVSCore3.Helper.ExceptionHelper.GetMessage(ex));
                return base.HandleError("Authentication Error", new Exceptions.ExceptionSafeToDisplay("There was an error retrieving your login account."));
            }
        }

        private async Task<IActionResult> Authorize(ControllerBase controller, ILoginResponse loginResponse, List<ProjectV2.Interfaces.IClaim> claimsList, string returnUrl)
        {
            ProjectV2.Model.Person person = projPersonBLL.GetByLoginResponse(loginResponse);

            //var person = projPerson.Get().Where(x => x.Identifier == identifier).FirstOrDefault();
            List<ProjectV2.Interfaces.IRole> roleList = projPersonBLL.GetRolesByPersonAndProjectId(person, project);

            ClaimsPrincipal userPrincipal = claimsMgr.CreateApplicationPrincipal(person, roleList, claimsList, person);

            await controller.ControllerContext.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            //return RedirectToAction("SendRenewalEmails_Initial", "Admin");
            if (!string.IsNullOrEmpty(returnUrl) && !returnUrl.Equals("/"))
            {
                return RedirectUser(controller, userPrincipal, returnUrl);
            }
            else
            {
                return controller.RedirectToAction(project.DefaultAction, project.DefaultController);
            }
        }
        private Microsoft.AspNetCore.Mvc.ActionResult RedirectUser(Microsoft.AspNetCore.Mvc.ControllerBase controller, ClaimsPrincipal userPrincipal, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToLocal(controller, returnUrl);
            }
            return controller.RedirectToAction(project.DefaultAction, project.DefaultController);
        }

        private Microsoft.AspNetCore.Mvc.ActionResult RedirectToLocal(Microsoft.AspNetCore.Mvc.ControllerBase controller, string returnUrl)
        {
            if (controller.Url.IsLocalUrl(returnUrl))
            {
                return controller.Redirect(returnUrl);
            }
            else
            {
                return controller.RedirectToAction(project.DefaultAction, project.DefaultController);
            }
        }

        public IActionResult SwitchRoleFaculty()
        {
            if (Request.IsLocal())
            {

                HttpContext.SignOutAsync();

                Authentication.Interfaces.ILoginResponse loginResponse = new Authentication.Model.LoginResponseShibboleth()
                { Alias = "hjcab", BUID = "U05190924", Email = "hjcab@bu.edu", FirstName = "Howard", LastName = "Cabral", FullName = "Howard Cabral", PersonTargetID = "11654", Message = "Success" };

                string returnUrl = "~/Faculty/Detail/" + loginResponse.PersonTargetID;

                ProjectV2.Model.Person person = projPersonBLL.GetByLoginResponse(loginResponse);
                return Authorize(this, loginResponse, GetClaims(person), returnUrl).Result;
            }
            else
            {
                return RedirectToAction("NotAuthorized", "Home");
            }
        }
        public IActionResult SwitchRoleSupervisor()
        {
            if (Request.IsLocal())
            {
                string returnUrl = "~/Faculty/Index";
                HttpContext.SignOutAsync();

                Authentication.Interfaces.ILoginResponse loginResponse = new Authentication.Model.LoginResponseShibboleth()
                { Alias = "jonlevy", BUID = "U40758277", Email = "jonlevy@bu.edu", FirstName = "Jonathan", LastName = "Levy", FullName = "Jonathan Levy", PersonTargetID = "17636", Message = "Success" };

                // todo pff remove this demo note
                // Get the person so you can get the claims below
                ProjectV2.Model.Person person = projPersonBLL.GetByLoginResponse(loginResponse);
                return Authorize(this, loginResponse, GetClaims(person), returnUrl).Result;
            }
            else
            {
                return RedirectToAction("NotAuthorized", "Home");
            }
        }
        public IActionResult SwitchRoleDeptAdmin()
        {
            // todo pff: complete this method
            return RedirectToAction("NotAuthorized", "Home");
        }
        public IActionResult SwitchRoleGrantAdmin()
        {
            // todo pff: complete this method
            return RedirectToAction("NotAuthorized", "Home");
        }
        public IActionResult SwitchRoleFRO()
        {
            if (Request.IsLocal())
            {
                string returnUrl = "~/Faculty/Index";
                HttpContext.SignOutAsync();

                Authentication.Interfaces.ILoginResponse loginResponse = new Authentication.Model.LoginResponseShibboleth()
                { Alias = "vbe", BUID = "U81067567", Email = "vbe@bu.edu", FirstName = "Vanessa", LastName = "Edouard", FullName = "Vanessa Edouard", PersonTargetID = "58092", Message = "Success" };

                // todo pff remove this demo note
                // Get the person so you can get the claims below
                ProjectV2.Model.Person person = projPersonBLL.GetByLoginResponse(loginResponse);
                return Authorize(this, loginResponse, GetClaims(person), returnUrl).Result;
            }
            else
            {
                return RedirectToAction("NotAuthorized", "Home");
            }
        }


        private List<ProjectV2.Interfaces.IClaim> GetClaims(ProjectV2.Model.Person person)
        {
            List<ProjectV2.Interfaces.IClaim> claimList = new List<ProjectV2.Interfaces.IClaim>();
            AddClaimIfFacultyMember(person, claimList);
            AddClaimIfFacultySupervisor(person, claimList);
            //AddClaimIfDeptAdmin(person.PersonId, claimNameValuePairs);
            //AddClaimIfGrantAdmin(person.PersonId, claimNameValuePairs);
            //AddClaimIfFRO(person.PersonId, person.Buid, claimNameValuePairs);
            AddClaimIfLocalHost(claimList);
            return claimList;
        }

        private void AddClaimIfLocalHost(List<ProjectV2.Interfaces.IClaim> claimList)
        {
            claimList.Add(new ProjectV2.Model.Claim()
            {
                ClaimValue = Request.IsLocal().ToString().ToLower(),
                ClaimName = "IsLocal"
            });
        }

        //private bool AddClaimIfStudent(int personId, string buid, List<Model.ClaimNameValuePair> claimNameValuePairs)
        //{
        //    Models.ShadowPerson? shadowPerson =
        //            context.ShadowPeople.Where(m => m.PersonId == personId && m.IsStudent == true).FirstOrDefault();

        //    if (shadowPerson != null)
        //    {
        //        claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "IsStudent", ClaimValue = "true" });
        //        return true;
        //    }

        //    Models.TblStudent? student = context.TblStudents.Where(a => a.BuId == buid).FirstOrDefault();
        //    if (student != null)
        //    {
        //        claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "IsStudent", ClaimValue = "true" });
        //        return true;
        //    }
        //    return false;
        //}
        //private void AddClaimIfStudentAffairs(int personId, List<Models.ClaimNameValuePair> claimNameValuePairs)
        //{
        //    Models.ShadowPerson? shadowPerson =
        //            context.ShadowPeople.Include(i => i.Department).Where(m => m.PersonId == personId && m.IsStudentAffairs == true).FirstOrDefault();

        //    if (shadowPerson != null)
        //    {
        //        claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "IsStudentAffairs", ClaimValue = "true" });
        //    }
        //}
        //private void AddClaimIfCoordinator(int personId, List<Models.ClaimNameValuePair> claimNameValuePairs)
        //{
        //    Models.ShadowPerson? shadowPerson =
        //            context.ShadowPeople.Include(i => i.Department).Where(m => m.PersonId == personId && m.IsCoordinator == true).FirstOrDefault();

        //    if (shadowPerson != null)
        //    {
        //        claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "IsCoordinator", ClaimValue = "true" });
        //        if (shadowPerson.Department != null)
        //        {
        //            claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "Department", ClaimValue = shadowPerson.Department.DepartmentName });
        //            claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "DepartmentId", ClaimValue = shadowPerson.DepartmentId.ToString() });
        //        }
        //    }
        //}
        //private void AddClaimIfPhysician(int personId, List<Models.ClaimNameValuePair> claimNameValuePairs)
        //{
        //    Models.ShadowPerson? shadowPerson =
        //            context.ShadowPeople.Include(i => i.Department).Where(m => m.PersonId == personId && m.IsPhysician == true).FirstOrDefault();

        //    if (shadowPerson != null)
        //    {
        //        claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "IsPhysician", ClaimValue = "true" });
        //        if (shadowPerson.Department != null)
        //        {
        //            claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "Department", ClaimValue = shadowPerson.Department.DepartmentName });
        //            claimNameValuePairs.Add(new ClaimNameValuePair() { ClaimName = "DepartmentId", ClaimValue = shadowPerson.DepartmentId.ToString() });
        //        }
        //    }
        //}

        private ActionResult RedirectUser(ClaimsPrincipal userPrincipal, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToLocal(returnUrl);
            }
            return RedirectToAction(project.DefaultAction, project.DefaultController);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(project.DefaultAction, project.DefaultController);
            }
        }

        private Authentication.Interfaces.ILoginResponse GetLoginResponse()
        {
            if (Request.IsLocal())
            {
                return GetDefaultLoginResponse();
            }
            else
            {

                return authentication.GetLoginResponse(this);
            }
        }

        private bool IsFacultyMember(ProjectV2.Model.Person person)
        {
            if (context.PersonYear.Any(w => w.PersonId == person.PersonId))
            {
                return true;
            }
            return person.Buid != null && facultyBLL.IsActiveSPHFaculty(person.Buid);
        }

        private string GetListOfSupervisedFacultyPersonIds(ProjectV2.Model.Person person)
        {
            string buid = person.Buid;
            int current_review_year = appsettings.CurrentReviewYear;

            AFR_DLL.Model.Person project_person = contextAFR.People.Where(w => w.Buid == buid).First();
            if (project_person != null)
            {
                List<string> faculty_buids = contextAFR.PersonEvaluees.Where(w => w.EvaluatorId == project_person.PersonId && w.FiscalYear == current_review_year).Select(w => w.Person.Buid).ToList();
                List<int> faculty_personIds = context.Person.Where(w => faculty_buids.Contains(w.BUID)).Select(s => s.PersonId).ToList();
                if (faculty_personIds.Count > 0)
                {
                    return string.Join(',', faculty_personIds.ToArray());
                }
            }
            return string.Empty;
        }

        private void AddClaimIfFacultyMember(ProjectV2.Model.Person person, List<ProjectV2.Interfaces.IClaim> claimList)
        {
            bool isFacultyMember = IsFacultyMember(person);
            if (isFacultyMember)
            {
                AddPersonIfNeeded(person);
                AddPersonYearNeeded(person);
                claimList.Add(new ProjectV2.Model.Claim()
                {
                    ClaimValue = "true",
                    ClaimName = (SPH_CoveragePlanDLL.Enumeration.ClaimTypes.FacultyMember).ToString()
                });
            }
        }

        private void AddClaimIfFacultySupervisor(ProjectV2.Model.Person person, List<ProjectV2.Interfaces.IClaim> claimList)
        {
            string supervisedFacultyPersonIds = GetListOfSupervisedFacultyPersonIds(person);
            if (!string.IsNullOrEmpty(supervisedFacultyPersonIds))
            {
                claimList.Add(new ProjectV2.Model.Claim()
                {
                    ClaimValue = "true",
                    ClaimName = "IsSupervisor"
                });
                claimList.Add(new ProjectV2.Model.Claim()
                {
                    ClaimValue = supervisedFacultyPersonIds,
                    ClaimName = "SupervisedFacultyPersonIds"
                });
            }
        }

        private void AddPersonYearNeeded(ProjectV2.Model.Person person)
        {
            BUMC_FacultyDLL.Model.Faculty? faculty = contextFaculty.Faculties.Where(w => w.Uid == person.Buid).FirstOrDefault();

            if (faculty != null && faculty.PercentTime.HasValue)
            {
                BUMC_FacultyDLL.Model.FacultyRank? facultyRank =
                    contextFaculty.FacultyRanks.Where(w => w.Uid == person.Buid && w.AppointmentType == 1 && w.ActiveStatus == 1).OrderBy(ob => ob.AppointmentLevel).FirstOrDefault();

                if (facultyRank != null && facultyRank.MasterCostCenter != null)
                {
                    int currentYear = GetCurrentYear();
                    if (!context.PersonYear.Any(a => a.PersonId == person.PersonId && a.YearId == currentYear))
                    {
                        SPH_CoveragePlanDLL.Model.PersonYear personYear = new PersonYear()
                        {
                            PersonId = person.PersonId,
                            YearId = currentYear,
                            TotalPercentEffort = faculty.PercentTime.Value,
                            AllocationStatusId = (byte)SPH_CoveragePlanDLL.Enumeration.AllocationStatus.Draft,
                            PrimaryMCC = facultyRank.MasterCostCenter,
                            PersonCategoryId = GetFacultyCategory(facultyRank),
                            CreatedBy = person.PersonId,
                            CreatedDate = DateTime.Now,
                            UpdatedBy = person.PersonId,
                            UpdatedDate = DateTime.Now
                        };

                        context.PersonYear.Add(personYear);
                        context.SaveChanges();
                        // todo pff add audit trail.
                    }
                }
            }
        }

        private void AddPersonIfNeeded(ProjectV2.Model.Person person)
        {
            SPH_CoveragePlanDLL.Model.Person? p = context.Person.Where(w => w.PersonId == person.PersonId).FirstOrDefault();
            if (p == null
                && person.Identifier != null
                && person.LastName != null
                && person.FirstName != null
                && person.EmailPreferred != null)
            {
                p = new SPH_CoveragePlanDLL.Model.Person();
                p.PersonId = person.PersonId;
                p.DisplayName = person.DisplayName;
                p.Identifier = person.Identifier;
                p.LastName = person.LastName;
                p.FirstName = person.FirstName;
                p.BUID = person.Buid;
                p.BUAlias = person.Alias;
                p.EmailPreferred = person.EmailPreferred;
                context.Person.Add(p);
                context.SaveChanges();
                // todo pff add audit trail.
            }
        }

        private byte GetFacultyCategory(FacultyRank facultyRank)
        {
            if (facultyRank.JobMedText == null)
            {
                return (byte)SPH_CoveragePlanDLL.Enumeration.FacultyCategory.Unknown;
            }
            else if (facultyRank.JobMedText.ToLower().Contains("emeritus"))
            {
                return (byte)SPH_CoveragePlanDLL.Enumeration.FacultyCategory.Emeritus;
            }
            else if (facultyRank.JobMedText.ToLower().Contains("adjunct"))
            {
                return (byte)SPH_CoveragePlanDLL.Enumeration.FacultyCategory.Adjunct;
            }
            else if (facultyRank.AppointmentLevel == 1)
            {
                return (byte)SPH_CoveragePlanDLL.Enumeration.FacultyCategory.Primary;
            }
            else if (facultyRank.AppointmentLevel == 2)
            {
                return (byte)SPH_CoveragePlanDLL.Enumeration.FacultyCategory.Secondary;
            }
            else
            {
                return (byte)SPH_CoveragePlanDLL.Enumeration.FacultyCategory.Unknown;
            }
        }

        private int GetCurrentYear()
        {
            int currentYear = DateTime.Today.Year;
            SPH_CoveragePlanDLL.Model.REF_Year? refYear = context.REF_Year.Where(w => w.IsActive == true).OrderByDescending(ob => ob.YearId).FirstOrDefault();
            if (refYear != null)
            {
                currentYear = refYear.YearId;
            }
            return currentYear;
        }

        //private readonly BumcFacultyContext contextFaculty;
        //private readonly BUMC_FacultyDLL.Interfaces.IFacultyBLL facultyBLL;
        //private readonly ProjectV2.Interfaces.IPersonBLL projPersonBLL;
        //private readonly ProjectV2.Interfaces.IPersonRoleBLL persRoleBLL;
        //private readonly SPH_CoveragePlanDLL.Interfaces.IClaimsManager claimsMgr;
        //private readonly IConfiguration config;
        //private readonly Authentication.Interfaces.IAuthentication authentication;
        //private readonly ProjectV2.Interfaces.IProject project;
        //private const int projectId = SPH_CoveragePlanDLL.Helper.Constants.SPH_CoveragePlan_ProjectId;
        ////private readonly ProjectV2.Interfaces.IAuthorization authorization;

    }
}
