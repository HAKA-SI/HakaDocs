
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace API.Helpers
{
  public static class CacheKeys
  {
    public static string Users { get { return "_Users"; } }
    public static string ClassLevels { get { return "_ClassLevels"; } }
    public static string ClassLevelProducts { get { return "_ClassLevelProducts"; } }
    public static string EmailTemplates { get { return "_EmailTemplates"; } }
    public static string SmsTemplates { get { return "_SmsTemplates"; } }
    public static string Settings { get { return "_Settings"; } }
    public static string Tokens { get { return "_Tokens"; } }
    public static string ProductDeadLines { get { return "_ProductDeadLines"; } }
    public static string Roles { get { return "_Roles"; } }
    public static string Capabilities { get { return "_Capabilities"; } }
    public static string RoleCapabilities { get { return "_RoleCapabilities"; } }
    public static string Menus { get { return "_Menus"; } }
    public static string MenuItems { get { return "_MenuItems"; } }
    public static string Orders { get { return "_Orders"; } }
    public static string OrderLines { get { return "_OrderLines"; } }
    public static string OrderLineDeadLines { get { return "_OrderLineDeadLines"; } }
    public static string UserLinks { get { return "_UserLinks"; } }
    public static string FinOps { get { return "_FinOps"; } }
    public static string FinOpOrderLines { get { return "_FinOpOrderLines"; } }
    public static string Cheques { get { return "_Cheques"; } }
    public static string Banks { get { return "_Banks"; } }
     public static string PaymentTypes  { get { return "_PaymentTypes"; } }
    public static string Products { get { return "_Products"; } }
    public static string ProductTypes { get { return "_ProductTypes"; } }
    public static string UserTypes { get { return "_UserTypes"; } }
    public static string Events { get { return "_Events"; } }
    public static string UserRoles { get { return "_UserRoles"; } }
    public static string Countries { get { return "_Countries"; } }
    public static string Cities { get { return "_Cities"; } }
    public static string Districts { get { return "_Districts"; } }
    public static string MaritalStatus { get { return "_MaritalStatus"; } }
    public static string Photos { get { return "_Photos"; } }
    public static string Zones { get { return "_Zones"; } }
    public static string LocationZones { get { return "_LacationZones"; } }
    public static string Periodicities { get { return "_Periodicities"; } }
    public static string PayableAts { get { return "_PayableAts"; } }
    public static string Subscriptions { get { return "_Subscriptions"; } }
    public static string OrderLineHistories { get { return "_OrderLineHistories"; } }
    public static string DocsTemplates { get { return "_DocsTemplates"; } }
    public static string Discounts { get { return "_Discounts"; } }
    public static string Establishments { get  { return "_Establishments"; } }
    public static string Jobs { get  { return "_Jobs"; } }
    public static string Smses { get  { return "_Smses"; } }
    public static string Periods { get  { return "_Periods"; } }
    public static string LoginPageInfos { get  { return "_LoginPageInfos"; } }
    public static string OpenTeacherRequests { get  { return "_OpenTeacherRequests"; } }
    public static string RegistrationFees { get  { return "_RegistrationFees"; } }
    public static string RegFeeTypes { get  { return "_RegFeeTypes"; } }
  }
}