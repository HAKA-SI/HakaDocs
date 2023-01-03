using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
  public interface ICacheRepository
  {
    Task<List<City>> GetCities();
    Task<List<City>> SetCities();
    // Task<List<AppUser>> GetUsers();
    // Task<List<User>> GetStudents();
    // Task<List<User>> GetParents();
    // Task<List<User>> GetTeachers();
    // Task<List<User>> GetEmployees();
    // Task<List<User>> SetUsers();
    // Task<List<TeacherCourse>> GetTeacherCourses();
    // Task<List<TeacherCourse>> SetTeacherCourses();
    // Task<List<ClassCourse>> GetClassCourses();
    // Task<List<ClassCourse>> SetClassCourses();
    // Task<List<ClassLevel>> GetClassLevels();
    // Task<List<ClassLevel>> SetClassLevels();
    // Task<List<ClassLevelProduct>> GetClassLevelProducts();
    // Task<List<ClassLevelProduct>> SetClassLevelProducts();
    // Task<List<NextYClassLevelProduct>> GetNextYClassLevelProducts();
    // Task<List<NextYClassLevelProduct>> SetNextYClassLevelProducts();
    // Task<List<Class>> GetClasses();
    // Task<List<Class>> SetClasses();
    // Task<List<EducationLevel>> GetEducLevels();
    // Task<List<EducationLevel>> SetEducLevels();
    // Task<List<School>> GetSchools();
    // Task<List<School>> SetSchools();
    // Task<List<Cycle>> GetCycles();
    // Task<List<Cycle>> SetCycles();
    // Task<List<Course>> GetCourses();
    // Task<List<Course>> SetCourses();
    // Task<List<ClassType>> GetClassTypes();
    // Task<List<ClassType>> SetClassTypes();
    // Task<List<ClassLevelClassType>> GetCLClassTypes();
    // Task<List<ClassLevelClassType>> SetCLClassTypes();
    // Task<List<EmailTemplate>> GetEmailTemplates();
    // Task<List<EmailTemplate>> SetEmailTemplates();
    // Task<List<SmsTemplate>> GetSmsTemplates();
    // Task<List<SmsTemplate>> SetSmsTemplates();
    // Task<List<Setting>> GetSettings();
    // Task<List<Setting>> SetSettings();
    // Task<List<Token>> GetTokens();
    // Task<List<Token>> SetTokens();
    // Task<List<ProductDeadLine>> GetProductDeadLines();
    // Task<List<ProductDeadLine>> SetProductDeadLines();
    // Task<List<NextYProductDeadLine>> GetNextYProductDeadLines();
    // Task<List<NextYProductDeadLine>> SetNextYProductDeadLines();
    // Task<List<Role>> GetRoles();
    // Task<List<Role>> SetRoles();
    // Task<List<Order>> GetOrders();
    // Task<List<Order>> SetOrders();
    // Task<List<OrderLine>> GetOrderLines();
    // Task<List<OrderLine>> SetOrderLines();
    // Task<List<OrderLineDeadline>> GetOrderLineDeadLines();
    // Task<List<OrderLineDeadline>> SetOrderLineDeadLines();
    // Task<List<UserLink>> GetUserLinks();
    // Task<List<UserLink>> SetUserLinks();
    // Task<List<FinOp>> GetFinOps();
    // Task<List<FinOp>> SetFinOps();
    // Task<List<FinOpOrderLine>> GetFinOpOrderLines();
    // Task<List<FinOpOrderLine>> SetFinOpOrderLines();
    // Task<List<Cheque>> GetCheques();
    // Task<List<Cheque>> SetCheques();
    // Task<List<Bank>> GetBanks();
    // Task<List<Bank>> SetBanks();
    // Task<List<PaymentType>> GetPaymentTypes();
    // Task<List<PaymentType>> SetPaymentTypes();
    // Task<List<Product>> GetProducts();
    // Task<List<Product>> SetProducts();
    // Task<List<ProductType>> GetProductTypes();
    // Task<List<ProductType>> SetProductTypes();
    // Task<List<UserType>> GetUserTypes();
    // Task<List<UserType>> SetUserTypes();
    // Task<List<Menu>> GetMenus();
    // Task<List<Menu>> SetMenus();
    // Task<List<MenuItem>> GetMenuItems();
    // Task<List<MenuItem>> SetMenuItems();
    // Task<List<Capability>> GetCapabilities();
    // Task<List<Capability>> SetCapabilities();
    // Task<List<Schedule>> GetSchedules();
    // Task<List<Schedule>> SetSchedules();
    // Task<List<ScheduleCourse>> GetScheduleCourses();
    // Task<List<ScheduleCourse>> SetScheduleCourses();
    // Task<List<Agenda>> GetAgendas();
    // Task<List<Agenda>> SetAgendas();
    // Task<List<Session>> GetSessions();
    // Task<List<Session>> SetSessions();
    // Task<List<Event>> GetEvents();
    // Task<List<Event>> SetEvents();
    // Task<List<CourseType>> GetCourseTypes();
    // Task<List<CourseType>> SetCourseTypes();
    // Task<List<Conflict>> GetConflicts();
    // Task<List<Conflict>> SetConflicts();
    // Task<List<CourseConflict>> GetCourseConflicts();
    // Task<List<CourseConflict>> SetCourseConflicts();
    // Task<List<UserRole>> GetUserRoles();
    // Task<List<UserRole>> SetUserRoles();
    // Task<List<Country>> GetCountries();
    // Task<List<Country>> SetCountries();
    // Task<List<District>> GetDistricts();
    // Task<List<District>> SetDistricts();
    // Task<List<MaritalStatus>> GetMaritalStatus();
    // Task<List<MaritalStatus>> SetMaritalStatus();
    // Task<List<Photo>> GetPhotos();
    // Task<List<Photo>> SetPhotos();
    // Task<List<RoleCapability>> GetRoleCapabilities();
    // Task<List<RoleCapability>> SetRoleCapabilities();
    // Task<List<Zone>> GetZones();
    // Task<List<Zone>> SetZones();
    // Task<List<LocationZone>> GetLocationZones();
    // Task<List<LocationZone>> SetLocationZones();
    // Task<List<ProductZone>> GetProductZones();
    // Task<List<ProductZone>> SetProductZones();
    // Task<List<ZoneDeadLine>> GetZoneDeadLines();
    // Task<List<ZoneDeadLine>> SetZoneDeadLines();
    // Task<List<Periodicity>> GetPeriodicities();
    // Task<List<Periodicity>> SetPeriodicities();
    // Task<List<PayableAt>> GetPayableAts();
    // Task<List<PayableAt>> SetPayableAts();
    // Task<List<Subscription>> GetSubscriptions();
    // Task<List<Subscription>> SetSubscriptions();
    // Task<List<OrderLineHistory>> GetOrderLineHistories();
    // Task<List<OrderLineHistory>> SetOrderLineHistories();
    // Task<List<DocsTemplate>> GetDocsTemplates();
    // Task<List<DocsTemplate>> SetDocsTemplates();
    // Task<List<Discount>> GetDiscounts();
    // Task<List<Discount>> SetDiscounts();
    // Task<List<Religion>> GetReligions();
    // Task<List<Religion>> SetReligions();
    // Task<List<BirthCertificate>> GetBirthCertificates();
    // Task<List<BirthCertificate>> SetBirthCertificates();
    // Task<List<MedicalSheet>> GetMedicalSheets();
    // Task<List<MedicalSheet>> SetMedicalSheets();
    // Task<List<TuitionSheet>> GetTuitionSheets();
    // Task<List<TuitionSheet>> SetTuitionSheets();
    // Task<List<ParentalRespSheet>> GetParentalRespSheets();
    // Task<List<ParentalRespSheet>> SetParentalRespSheets();
    // Task<List<Establishment>> GetEstablishments();
    // Task<List<Establishment>> SetEstablishments();
    // Task<List<FileElement>> GetFileElements();
    // Task<List<FileElement>> SetFileElements();
    // Task<List<UserFileElement>> GetUserFileElements();
    // Task<List<UserFileElement>> SetUserFileElements();
    // Task<List<Client>> GetClients();
    // Task<List<Client>> SetClients();
    // Task<List<Job>> GetJobs();
    // Task<List<Job>> SetJobs();
    // Task<List<Evaluation>> GetEvaluations();
    // Task<List<Evaluation>> SetEvaluations();
    // //Task<List<UserEvaluation>> GetUserEvaluations();
    // //Task<List<UserEvaluation>> SetUserEvaluations();
    // Task<List<EvalType>> GetEvalTypes();
    // Task<List<EvalType>> SetEvalTypes();
    // Task<List<ClassLevelDeadLine>> GetClassLevelDeadLines();
    // Task<List<ClassLevelDeadLine>> SetClassLevelDeadLines();
    // Task<List<NextYClassLevelDeadLine>> GetNextYClassLevelDeadLines();
    // Task<List<NextYClassLevelDeadLine>> SetNextYClassLevelDeadLines();
    // Task<List<CourseCoefficient>> GetCourseCoefficients();
    // Task<List<CourseCoefficient>> SetCourseCoefficients();
    // Task<List<ClassLevelCourse>> GetClassLevelCourses();
    // Task<List<ClassLevelCourse>> SetClassLevelCourses();
    // Task<List<Sms>> GetSmses();
    // Task<List<Sms>> SetSmses();
    // Task<List<Activity>> GetActivities();
    // Task<List<Activity>> SetActivities();
    // Task<List<ScheduleActivity>> GetScheduleActivities();
    // Task<List<ScheduleActivity>> SetScheduleActivities();
    // Task<List<Period>> GetPeriods();
    // Task<List<Period>> SetPeriods();
    // Task<List<Group>> GetGroups();
    // Task<List<Group>> SetGroups();
    // Task<List<GroupCourse>> GetGroupCourses();
    // Task<List<GroupCourse>> SetGroupCourses();
    // Task<List<Absence>> GetAbsences();
    // Task<List<Absence>> SetAbsences();
    // Task<List<CallSheet>> GetCallSheets();
    // Task<List<CallSheet>> SetCallSheets();
    // Task<List<LoginPageInfo>> GetLoginPageInfos();
    // Task<List<LoginPageInfo>> SetLoginPageInfos();
    // Task<List<Sanction>> GetSanctions();
    // Task<List<Sanction>> SetSanctions();
    // Task<List<Reason>> GetReasons();
    // Task<List<Reason>> SetReasons();
    // Task<List<ReasonType>> GetReasonTypes();
    // Task<List<ReasonType>> SetReasonTypes();
    // Task<List<Conduct>> GetConducts();
    // Task<List<Conduct>> SetConducts();
    // Task<List<UserConduct>> GetUserConducts();
    // Task<List<UserConduct>> SetUserConducts();
    // Task<List<StudentReport>> GetStudentReports();
    // Task<List<StudentReport>> SetStudentReports();
    // Task<List<GradesValidation>> GetGradesValidations();
    // Task<List<GradesValidation>> SetGradesValidations();
    // Task<List<OpenCourseRequest>> GetOpenCourseRequests();
    // Task<List<OpenCourseRequest>> SetOpenCourseRequests();
    // Task<List<OpenTeacherRequest>> GetOpenTeacherRequests();
    // Task<List<OpenTeacherRequest>> SetOpenTeacherRequests();
    // Task<List<Incident>> GetIncidents();
    // Task<List<Incident>> SetIncidents();
    // Task<List<IncidentReason>> GetIncidentReasons();
    // Task<List<IncidentReason>> SetIncidentReasons();
    // Task<List<IncidentPhoto>> GetIncidentPhotos();
    // Task<List<IncidentPhoto>> SetIncidentPhotos();
    // Task<List<IncidentAction>> GetIncidentActions();
    // Task<List<IncidentAction>> SetIncidentActions();
    // Task<List<API.Entities.Action>> GetActions();
    // Task<List<API.Entities.Action>> SetActions();
    // Task<List<ConductReport>> GetConductReports();
    // Task<List<ConductReport>> SetConductReports();
    // Task<List<Bonus>> GetBonus();
    // Task<List<Bonus>> SetBonus();
    // Task<List<BonusCourse>> GetBonusCourses();
    // Task<List<BonusCourse>> SetBonusCourses();
    // Task<List<RegistrationFee>> GetRegistrationFees();
    // Task<List<RegistrationFee>> SetRegistrationFees();
    // Task<List<RegFeeType>> GetRegFeeTypes();
    // Task<List<RegFeeType>> SetRegFeeTypes();
  }
}