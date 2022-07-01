namespace API.Data
{
    public class CacheRepository : ICacheRepository
  {
    private readonly DataContext _context;
    private readonly IMemoryCache _cache;
    private readonly IDatabase _redis;
    int teacherTypeId, parentTypeId, studentTypeId, adminTypeId, ttl;
    public readonly IConfiguration _config;
    public readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();
    private readonly IMapper _mapper;
    private JsonSerializerOptions jsonOptions;

    public CacheRepository(DataContext context, IConfiguration config, IMemoryCache memoryCache, IConnectionMultiplexer redis,
      IHttpContextAccessor httpContext, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager,
      IMapper mapper)
    {
      _mapper = mapper;
      _roleManager = roleManager;
      _userManager = userManager;
      _httpContext = httpContext;
      _config = config;
      _context = context;
      _cache = memoryCache;
      _redis = redis.GetDatabase();
      teacherTypeId = _config.GetValue<int>("AppSettings:teacherTypeId");
      parentTypeId = _config.GetValue<int>("AppSettings:parentTypeId");
      adminTypeId = _config.GetValue<int>("AppSettings:adminTypeId");
      studentTypeId = _config.GetValue<int>("AppSettings:studentTypeId");
      ttl = 90; // data stays on cache for 90 days

      jsonOptions = new JsonSerializerOptions
      {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReferenceHandler = ReferenceHandler.Preserve,
        WriteIndented = true
      };

    }

    // public async Task<List<AppUser>> GetUsers()
    // {
    //   List<AppUser> users = new List<AppUser>();

    //   string key = subDomain + CacheKeys.Users;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       users = JsonSerializer.Deserialize<List<AppUser>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       users = await SetUsers();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return users;
    // }

    // public async Task<List<AppUser>> GetStudents()
    // {
    //   List<AppUser> students = (await GetUsers()).Where(u => u.UserTypeId == studentTypeId).ToList();
    //   return students;
    // }

    // public async Task<List<AppUser>> GetParents()
    // {
    //   List<User> parents = (await GetUsers()).Where(u => u.UserTypeId == parentTypeId).ToList();
    //   return parents;
    // }

    // public async Task<List<AppUser>> GetTeachers()
    // {
    //   List<AppUser> teachers = (await GetUsers()).Where(u => u.UserTypeId == teacherTypeId).ToList();
    //   return teachers;
    // }

    // public async Task<List<AppUser>> GetEmployees()
    // {
    //   List<AppUser> employees = (await GetUsers()).Where(u => u.UserTypeId == adminTypeId).ToList();
    //   return employees;
    // }

    // public async Task<List<AppUser>> SetUsers()
    // {
    //   List<AppUser> users = await _userManager.Users.Include(p => p.Photos)
    //                                              .Include(c => c.Class)
    //                                              .Include(i => i.EducLevel)
    //                                              .Include(c => c.ClassLevel)
    //                                              .Include(c => c.District)
    //                                              .Include(c => c.City)
    //                                              .Include(i => i.UserType)
    //                                              .OrderBy(o => o.LastName).ThenBy(o => o.FirstName)
    //                                              .ToListAsync();

    //   string key = subDomain + CacheKeys.Users;
    //   if (users.Count() > 0)
    //   {
    //     List<UserToCacheDto> usersDto = _mapper.Map<List<UserToCacheDto>>(users);
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(usersDto, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return users;
    // }

    // public async Task<List<UserRole>> GetUserRoles()
    // {
    //   List<UserRole> userRoles = new List<UserRole>();

    //   string key = subDomain + CacheKeys.UserRoles;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       userRoles = JsonSerializer.Deserialize<List<UserRole>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       userRoles = await SetUserRoles();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return userRoles;
    // }

    // public async Task<List<UserRole>> SetUserRoles()
    // {
    //   List<UserRole> userRoles = await _context.UserRoles.Include(p => p.User).ThenInclude(i => i.Photos)
    //                                                      .Include(i => i.Role)
    //                                                      .OrderBy(o => o.Role.Name)
    //                                                      .ToListAsync();

    //   string key = subDomain + CacheKeys.UserRoles;
    //   if (userRoles.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(userRoles, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return userRoles;
    // }

    // public async Task<List<TeacherCourse>> GetTeacherCourses()
    // {
    //   List<TeacherCourse> teacherCourses = new List<TeacherCourse>();

    //   string key = subDomain + CacheKeys.TeacherCourses;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       teacherCourses = JsonSerializer.Deserialize<List<TeacherCourse>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       teacherCourses = await SetTeacherCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return teacherCourses;
    // }

    // public async Task<List<TeacherCourse>> SetTeacherCourses()
    // {
    //   List<TeacherCourse> teachercourses = await _context.TeacherCourses.Include(p => p.Course).ThenInclude(i => i.CourseType)
    //                                                                     .ToListAsync();

    //   string key = subDomain + CacheKeys.TeacherCourses;
    //   if (teachercourses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(teachercourses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return teachercourses;
    // }

    // public async Task<List<ClassCourse>> GetClassCourses()
    // {
    //   List<ClassCourse> classCourses = new List<ClassCourse>();

    //   string key = subDomain + CacheKeys.ClassCourses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       classCourses = JsonSerializer.Deserialize<List<ClassCourse>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       classCourses = await SetClassCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return classCourses;
    // }

    // public async Task<List<ClassCourse>> SetClassCourses()
    // {
    //   List<ClassCourse> classcourses = await _context.ClassCourses
    //     .Include(i => i.Teacher).ThenInclude(i => i.Photos)
    //     .Include(i => i.Class).ThenInclude(i => i.ClassLevel).ThenInclude(i => i.EducationLevel)
    //     .Include(i => i.Course)
    //     .Include(i => i.Class)
    //     .ToListAsync();

    //   string key = subDomain + CacheKeys.ClassCourses;
    //   if(classcourses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(classcourses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return classcourses;
    // }

    // public async Task<List<Evaluation>> GetEvaluations()
    // {
    //   List<Evaluation> evals = new List<Evaluation>();

    //   string key = subDomain + CacheKeys.Evaluations;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       evals = JsonSerializer.Deserialize<List<Evaluation>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       evals = await SetEvaluations();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return evals;
    // }

    // public async Task<List<Evaluation>> SetEvaluations()
    // {
    //   List<Evaluation> evals = await _context.Evaluations.Include(i => i.EvalType)
    //                                                      .Include(i => i.Class).ThenInclude(i => i.ClassLevel)
    //                                                      .Include(i => i.Course)
    //                                                      .OrderBy(o => o.EvalDate)
    //                                                      .ToListAsync();

    //   string key = subDomain + CacheKeys.Evaluations;
    //   if(evals.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(evals, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return evals;
    // }

    // public async Task<List<UserEvaluation>> GetUserEvaluations()
    // {
    //   List<UserEvaluation> userevals = new List<UserEvaluation>();

    //   string key = subDomain + CacheKeys.UserEvaluations;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       userevals = JsonSerializer.Deserialize<List<UserEvaluation>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       userevals = await SetUserEvaluations();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return userevals;
    // }

    // public async Task<List<UserEvaluation>> SetUserEvaluations()
    // {
    //   List<UserEvaluation> userevals = await _context.UserEvaluations.Include(i => i.Evaluation).ThenInclude(i => i.EvalType)
    //                                                                  .Include(i => i.User)
    //                                                                  .OrderBy(o => o.Evaluation.EvalDate)
    //                                                                  .ToListAsync();

    //   string key = subDomain + CacheKeys.UserEvaluations;
    //   if(userevals.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(userevals, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return userevals;
    // }

    // public async Task<List<EvalType>> GetEvalTypes()
    // {
    //   List<EvalType> evaltypes = new List<EvalType>();

    //   string key = subDomain + CacheKeys.EvalTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       evaltypes = JsonSerializer.Deserialize<List<EvalType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       evaltypes = await SetEvalTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return evaltypes;
    // }

    // public async Task<List<EvalType>> SetEvalTypes()
    // {
    //   List<EvalType> evaltypes = await _context.EvalTypes.ToListAsync();

    //   string key = subDomain + CacheKeys.EvalTypes;
    //   if(evaltypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(evaltypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return evaltypes;
    // }

    // public async Task<List<Sms>> GetSmses()
    // {
    //   List<Sms> smses = new List<Sms>();

    //   string key = subDomain + CacheKeys.Smses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       smses = JsonSerializer.Deserialize<List<Sms>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       smses = await SetSmses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return smses;
    // }

    // public async Task<List<Sms>> SetSmses()
    // {
    //   List<Sms> smses = await _context.Sms.ToListAsync();

    //   string key = subDomain + CacheKeys.Smses;
    //   if(smses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(smses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return smses;
    // }

    // public async Task<List<ClassLevel>> GetClassLevels()
    // {
    //   List<ClassLevel> classLevels = new List<ClassLevel>();

    //   string key = subDomain + CacheKeys.ClassLevels;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       classLevels = JsonSerializer.Deserialize<List<ClassLevel>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       classLevels = await SetClassLevels();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return classLevels;
    // }

    // public async Task<List<ClassLevel>> SetClassLevels()
    // {
    //   List<ClassLevel> classLevels = await _context.ClassLevels.OrderBy(o => o.DsplSeq).ToListAsync();

    //   string key = subDomain + CacheKeys.ClassLevels;
    //   if (classLevels.Count() > 0)
    //   {
    //     // var usersDto = _mapper.Map<List<UserToCacheDto>>(users);
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(classLevels, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return classLevels;
    // }

    // public async Task<List<ClassLevelProduct>> GetClassLevelProducts()
    // {
    //   List<ClassLevelProduct> levelproducts = new List<ClassLevelProduct>();

    //   string key = subDomain + CacheKeys.ClassLevelProducts;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       levelproducts = JsonSerializer.Deserialize<List<ClassLevelProduct>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       levelproducts = await SetClassLevelProducts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return levelproducts;
    // }

    // public async Task<List<ClassLevelProduct>> SetClassLevelProducts()
    // {
    //   List<ClassLevelProduct> levelproducts = await _context.ClassLevelProducts.Include(i => i.ClassLevel)
    //                                                                            .Include(i => i.Product)
    //                                                                            .OrderBy(o => o.ClassLevel.DsplSeq)
    //                                                                            .ToListAsync();

    //   string key = subDomain + CacheKeys.ClassLevelProducts;
    //   if (levelproducts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(levelproducts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return levelproducts;
    // }

    // public async Task<List<NextYClassLevelProduct>> GetNextYClassLevelProducts()
    // {
    //   List<NextYClassLevelProduct> levelproducts = new List<NextYClassLevelProduct>();

    //   string key = subDomain + CacheKeys.NextYClassLevelProducts;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       levelproducts = JsonSerializer.Deserialize<List<NextYClassLevelProduct>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       levelproducts = await SetNextYClassLevelProducts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return levelproducts;
    // }

    // public async Task<List<NextYClassLevelProduct>> SetNextYClassLevelProducts()
    // {
    //   List<NextYClassLevelProduct> levelproducts = await _context.NextYClassLevelProducts.Include(i => i.ClassLevel)
    //                                                                            .Include(i => i.Product)
    //                                                                            .OrderBy(o => o.ClassLevel.DsplSeq)
    //                                                                            .ToListAsync();

    //   string key = subDomain + CacheKeys.NextYClassLevelProducts;
    //   if (levelproducts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(levelproducts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return levelproducts;
    // }

    // public async Task<List<Class>> GetClasses()
    // {
    //   List<Class> classes = new List<Class>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Classes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       classes = JsonSerializer.Deserialize<List<Class>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       classes = await SetClasses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return classes;
    // }

    // public async Task<List<Class>> SetClasses()
    // {
    //   List<Class> classes = await _context.Classes.Include(i => i.ClassType)
    //                                               .Include(i => i.ClassLevel).ThenInclude(i => i.EducationLevel)
    //                                               .OrderBy(o => o.ClassLevel.DsplSeq).ThenBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Classes;
    //   if (classes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(classes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return classes;
    // }

    // public async Task<List<EducationLevel>> GetEducLevels()
    // {
    //   List<EducationLevel> educLevels = new List<EducationLevel>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.EducLevels;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       educLevels = JsonSerializer.Deserialize<List<EducationLevel>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       educLevels = await SetEducLevels();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return educLevels;
    // }

    // public async Task<List<EducationLevel>> SetEducLevels()
    // {
    //   List<EducationLevel> educLevels = await _context.EducationLevels.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.EducLevels;
    //   if (educLevels.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(educLevels, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return educLevels;
    // }

    // public async Task<List<School>> GetSchools()
    // {
    //   List<School> schools = new List<School>();

    //   string key = subDomain + CacheKeys.Schools;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       schools = JsonSerializer.Deserialize<List<School>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       schools = await SetSchools();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return schools;
    // }

    // public async Task<List<School>> SetSchools()
    // {
    //   List<School> schools = await _context.Schools.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Schools;
    //   if (schools.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(schools, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return schools;
    // }

    // public async Task<List<Cycle>> GetCycles()
    // {
    //   List<Cycle> cycles = new List<Cycle>();

    //   string key = subDomain + CacheKeys.Cycles;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       cycles = JsonSerializer.Deserialize<List<Cycle>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       cycles = await SetCycles();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return cycles;
    // }

    // public async Task<List<Cycle>> SetCycles()
    // {
    //   List<Cycle> cycles = await _context.Cycles.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Cycles;
    //   if (cycles.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(cycles, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return cycles;
    // }

    // public async Task<List<Course>> GetCourses()
    // {
    //   List<Course> courses = new List<Course>();

    //   string key = subDomain + CacheKeys.Courses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       courses = JsonSerializer.Deserialize<List<Course>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       courses = await SetCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return courses;
    // }

    // public async Task<List<Course>> SetCourses()
    // {
    //   List<Course> courses = await _context.Courses.Include(i => i.CourseType)
    //                                                .Include(i => i.CourseP)
    //                                                .OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Courses;
    //   if (courses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(courses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return courses;
    // }

    // public async Task<List<Job>> GetJobs()
    // {
    //   List<Job> jobs = new List<Job>();

    //   string key = subDomain + CacheKeys.Jobs;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       jobs = JsonSerializer.Deserialize<List<Job>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       jobs = await SetJobs();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return jobs;
    // }

    // public async Task<List<Job>> SetJobs()
    // {
    //   List<Job> jobs = await _context.Jobs.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Jobs;
    //   if (jobs.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(jobs, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return jobs;
    // }

    // public async Task<List<ClassType>> GetClassTypes()
    // {
    //   List<ClassType> classtypes = new List<ClassType>();
    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.ClassTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       classtypes = JsonSerializer.Deserialize<List<ClassType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       classtypes = await SetClassTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return classtypes;
    // }

    // public async Task<List<ClassType>> SetClassTypes()
    // {
    //   List<ClassType> classtypes = await _context.ClassTypes.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.ClassTypes;
    //   if (classtypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(classtypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return classtypes;
    // }

    // public async Task<List<ClassLevelClassType>> GetCLClassTypes()
    // {
    //   List<ClassLevelClassType> clclasstypes = new List<ClassLevelClassType>();
    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.CLClassTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       clclasstypes = JsonSerializer.Deserialize<List<ClassLevelClassType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       clclasstypes = await SetCLClassTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return clclasstypes;
    // }

    // public async Task<List<ClassLevelClassType>> SetCLClassTypes()
    // {
    //   List<ClassLevelClassType> clclasstypes = await _context.ClassLevelClassTypes.Include(i => i.ClassType)
    //                                                                               .Include(i => i.ClassLevel)
    //                                                                               .OrderBy(o => o.ClassType.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.CLClassTypes;
    //   if (clclasstypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(clclasstypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return clclasstypes;
    // }

    // public async Task<List<EmailTemplate>> GetEmailTemplates()
    // {
    //   List<EmailTemplate> templates = new List<EmailTemplate>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.EmailTemplates;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       templates = JsonSerializer.Deserialize<List<EmailTemplate>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       templates = await SetEmailTemplates();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return templates;
    // }

    // public async Task<List<EmailTemplate>> SetEmailTemplates()
    // {
    //   List<EmailTemplate> templates = await _context.EmailTemplates.Include(i => i.EmailCategory)
    //                                                                .OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.EmailTemplates;
    //   if (templates.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(templates, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return templates;
    // }

    // public async Task<List<DocsTemplate>> GetDocsTemplates()
    // {
    //   List<DocsTemplate> templates = new List<DocsTemplate>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.DocsTemplates;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       templates = JsonSerializer.Deserialize<List<DocsTemplate>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       templates = await SetDocsTemplates();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return templates;
    // }

    // public async Task<List<DocsTemplate>> SetDocsTemplates()
    // {
    //   List<DocsTemplate> templates = await _context.DocsTemplates.ToListAsync();

    //   string key = subDomain + CacheKeys.DocsTemplates;
    //   if (templates.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(templates, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return templates;
    // }

    // public async Task<List<SmsTemplate>> GetSmsTemplates()
    // {
    //   List<SmsTemplate> templates = new List<SmsTemplate>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.SmsTemplates;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       templates = JsonSerializer.Deserialize<List<SmsTemplate>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       templates = await SetSmsTemplates();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return templates;
    // }

    // public async Task<List<SmsTemplate>> SetSmsTemplates()
    // {
    //   List<SmsTemplate> templates = await _context.SmsTemplates.Include(i => i.SmsCategory)
    //                                                            .OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.SmsTemplates;
    //   if (templates.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(templates, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return templates;
    // }

    // public async Task<List<Setting>> GetSettings()
    // {
    //   List<Setting> settings = new List<Setting>();

    //   string key = subDomain + CacheKeys.Settings;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       settings = JsonSerializer.Deserialize<List<Setting>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       settings = await SetSettings();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return settings;
    // }

    // public async Task<List<Setting>> SetSettings()
    // {
    //   List<Setting> settings = await _context.Settings.OrderBy(o => o.DisplayName).ToListAsync();

    //   string key = subDomain + CacheKeys.Settings;
    //   if (settings.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(settings, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return settings;
    // }

    // public async Task<List<Token>> GetTokens()
    // {
    //   List<Token> tokens = new List<Token>();

    //   string key = subDomain + CacheKeys.Tokens;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       tokens = JsonSerializer.Deserialize<List<Token>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       tokens = await SetTokens();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return tokens;
    // }

    // public async Task<List<Token>> SetTokens()
    // {
    //   List<Token> tokens = await _context.Tokens.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Tokens;
    //   if (tokens.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(tokens, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return tokens;
    // }

    // public async Task<List<ProductDeadLine>> GetProductDeadLines()
    // {
    //   List<ProductDeadLine> productdeadlines = new List<ProductDeadLine>();

    //   string key = subDomain + CacheKeys.ProductDeadLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       productdeadlines = JsonSerializer.Deserialize<List<ProductDeadLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       productdeadlines = await SetProductDeadLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return productdeadlines;
    // }

    // public async Task<List<ProductDeadLine>> SetProductDeadLines()
    // {
    //   List<ProductDeadLine> productdeadlines = await _context.ProductDeadLines.OrderBy(o => o.DueDate).ToListAsync();

    //   string key = subDomain + CacheKeys.ProductDeadLines;
    //   if (productdeadlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(productdeadlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return productdeadlines;
    // }

    // public async Task<List<NextYProductDeadLine>> GetNextYProductDeadLines()
    // {
    //   List<NextYProductDeadLine> productdeadlines = new List<NextYProductDeadLine>();

    //   string key = subDomain + CacheKeys.NextYProductDeadLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       productdeadlines = JsonSerializer.Deserialize<List<NextYProductDeadLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       productdeadlines = await SetNextYProductDeadLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return productdeadlines;
    // }

    // public async Task<List<NextYProductDeadLine>> SetNextYProductDeadLines()
    // {
    //   List<NextYProductDeadLine> productdeadlines = await _context.NextYProductDeadLines.OrderBy(o => o.DueDate).ToListAsync();

    //   string key = subDomain + CacheKeys.NextYProductDeadLines;
    //   if (productdeadlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(productdeadlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return productdeadlines;
    // }

    // public async Task<List<ClassLevelDeadLine>> GetClassLevelDeadLines()
    // {
    //   List<ClassLevelDeadLine> leveldeadlines = new List<ClassLevelDeadLine>();

    //   string key = subDomain + CacheKeys.ClassLevelDeadLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       leveldeadlines = JsonSerializer.Deserialize<List<ClassLevelDeadLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       leveldeadlines = await SetClassLevelDeadLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return leveldeadlines;
    // }

    // public async Task<List<ClassLevelDeadLine>> SetClassLevelDeadLines()
    // {
    //   List<ClassLevelDeadLine> leveldeadlines = await _context.ClassLevelDeadLines.Include(i => i.ProductDeadLine).ToListAsync();

    //   string key = subDomain + CacheKeys.ClassLevelDeadLines;
    //   if (leveldeadlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(leveldeadlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return leveldeadlines;
    // }

    // public async Task<List<NextYClassLevelDeadLine>> GetNextYClassLevelDeadLines()
    // {
    //   List<NextYClassLevelDeadLine> leveldeadlines = new List<NextYClassLevelDeadLine>();

    //   string key = subDomain + CacheKeys.NextYClassLevelDeadLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       leveldeadlines = JsonSerializer.Deserialize<List<NextYClassLevelDeadLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       leveldeadlines = await SetNextYClassLevelDeadLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return leveldeadlines;
    // }

    // public async Task<List<NextYClassLevelDeadLine>> SetNextYClassLevelDeadLines()
    // {
    //   List<NextYClassLevelDeadLine> leveldeadlines = await _context.NextYClassLevelDeadLines.Include(i => i.ProductDeadLine)
    //                                                                               .ToListAsync();

    //   string key = subDomain + CacheKeys.NextYClassLevelDeadLines;
    //   if (leveldeadlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(leveldeadlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return leveldeadlines;
    // }

    // public async Task<List<API.Entities.Role>> GetRoles()
    // {
    //   List<API.Entities.Role> roles = new List<API.Entities.Role>();

    //   string key = subDomain + CacheKeys.Roles;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       roles = JsonSerializer.Deserialize<List<API.Entities.Role>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       roles = await SetRoles();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return roles;
    // }

    // public async Task<List<API.Entities.Role>> SetRoles()
    // {
    //   List<API.Entities.Role> roles = await _context.Roles.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Roles;
    //   if (roles.Count() > 0)
    //   {
    //     var rolesDto = _mapper.Map<List<RoleToCacheDto>>(roles);
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(rolesDto, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return roles;
    // }

    // public async Task<List<API.Entities.Order>> GetOrders()
    // {
    //   List<API.Entities.Order> orders = new List<API.Entities.Order>();

    //   string key = subDomain + CacheKeys.Orders;
    //   // Look for cache key.
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       orders = JsonSerializer.Deserialize<List<API.Entities.Order>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       orders = await SetOrders();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return orders;
    // }

    // public async Task<List<API.Entities.Order>> SetOrders()
    // {
    //   List<API.Entities.Order> orders = await _context.Orders.Include(i => i.Child)
    //                                             .Include(i => i.Mother).ThenInclude(i => i.UserType)
    //                                             .Include(i => i.Father).ThenInclude(i => i.UserType)
    //                                             .ToListAsync();

    //   string key = subDomain + CacheKeys.Orders;
    //   if (orders.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(orders, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return orders;
    // }

    // public async Task<List<OrderLine>> GetOrderLines()
    // {
    //   List<OrderLine> lines = new List<OrderLine>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.OrderLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       lines = JsonSerializer.Deserialize<List<OrderLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       lines = await SetOrderLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return lines;
    // }

    // public async Task<List<OrderLine>> SetOrderLines()
    // {
    //   List<OrderLine> lines = await _context.OrderLines.Include(i => i.Order)
    //                                                    .Include(i => i.Product)
    //                                                    .Include(i => i.Child).ThenInclude(i => i.Photos)
    //                                                    .Include(i => i.ClassLevel)
    //                                                    .Include(i => i.Product)
    //                                                    .ToListAsync();

    //   string key = subDomain + CacheKeys.OrderLines;
    //   if (lines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(lines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return lines;
    // }

    // public async Task<List<OrderLineHistory>> GetOrderLineHistories()
    // {
    //   List<OrderLineHistory> lineHistories = new List<OrderLineHistory>();

    //   string key = subDomain + CacheKeys.OrderLineHistories;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       lineHistories = JsonSerializer.Deserialize<List<OrderLineHistory>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       lineHistories = await SetOrderLineHistories();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return lineHistories;
    // }

    // public async Task<List<OrderLineHistory>> SetOrderLineHistories()
    // {
    //   List<OrderLineHistory> lineHistories = await _context.OrderLineHistories.Include(i => i.OrderLine)
    //                                                                           .Include(i => i.FinOp)
    //                                                                           .Include(i => i.User)
    //                                                                           .ToListAsync();

    //   string key = subDomain + CacheKeys.OrderLineHistories;
    //   if (lineHistories.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(lineHistories, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return lineHistories;
    // }

    // public async Task<List<OrderLineDeadline>> GetOrderLineDeadLines()
    // {
    //   List<OrderLineDeadline> linedeadlines = new List<OrderLineDeadline>();

    //   string key = subDomain + CacheKeys.OrderLineDeadLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       linedeadlines = JsonSerializer.Deserialize<List<OrderLineDeadline>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       linedeadlines = await SetOrderLineDeadLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return linedeadlines;
    // }

    // public async Task<List<OrderLineDeadline>> SetOrderLineDeadLines()
    // {
    //   List<OrderLineDeadline> linedeadlines = await _context.OrderLineDeadlines
    //     .Include(i => i.OrderLine).ThenInclude(i => i.Child).ThenInclude(i => i.Photos)
    //     .ToListAsync();

    //   string key = subDomain + CacheKeys.OrderLineDeadLines;
    //   if (linedeadlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(linedeadlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return linedeadlines;
    // }

    // public async Task<List<UserLink>> GetUserLinks()
    // {
    //   List<UserLink> userlinks = new List<UserLink>();

    //   string key = subDomain + CacheKeys.UserLinks;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       userlinks = JsonSerializer.Deserialize<List<UserLink>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       userlinks = await SetUserLinks();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return userlinks;
    // }

    // public async Task<List<UserLink>> SetUserLinks()
    // {
    //   List<UserLink> userlinks = await _context.UserLinks.ToListAsync();

    //   string key = subDomain + CacheKeys.UserLinks;
    //   if (userlinks.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(userlinks, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return userlinks;
    // }

    // public async Task<List<FinOp>> GetFinOps()
    // {
    //   List<FinOp> finops = new List<FinOp>();

    //   string key = subDomain + CacheKeys.FinOps;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       finops = JsonSerializer.Deserialize<List<FinOp>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       finops = await SetFinOps();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return finops;
    // }

    // public async Task<List<FinOp>> SetFinOps()
    // {
    //   List<FinOp> finops = await _context.FinOps.Include(i => i.Cheque).ThenInclude(i => i.Bank)
    //                                             .Include(i => i.PaymentType)
    //                                             .Include(i => i.Invoice)
    //                                             .Include(i => i.FromBank)
    //                                             .Include(i => i.FromBankAccount)
    //                                             .Include(i => i.FromCashDesk)
    //                                             .Include(i => i.ToBankAccount)
    //                                             .Include(i => i.ToCashDesk)
    //                                             .ToListAsync();

    //   string key = subDomain + CacheKeys.FinOps;
    //   if (finops.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(finops, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return finops;
    // }

    // public async Task<List<FinOpOrderLine>> GetFinOpOrderLines()
    // {
    //   List<FinOpOrderLine> finoporderlines = new List<FinOpOrderLine>();

    //   string key = subDomain + CacheKeys.FinOpOrderLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       finoporderlines = JsonSerializer.Deserialize<List<FinOpOrderLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       finoporderlines = await SetFinOpOrderLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return finoporderlines;
    // }

    // public async Task<List<FinOpOrderLine>> SetFinOpOrderLines()
    // {
    //   List<FinOpOrderLine> finoporderlines = await _context.FinOpOrderLines
    //                                         .Include(d => d.Invoice)
    //                                         .Include(o => o.OrderLine).ThenInclude(p => p.Product)
    //                                         .Include(o => o.OrderLine).ThenInclude(p => p.Order)
    //                                         .Include(o => o.OrderLine).ThenInclude(c => c.Child)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.Order)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.Cheque).ThenInclude(i => i.Bank)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.PaymentType)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.FromBankAccount)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.ToBankAccount)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.FromCashDesk)
    //                                         .Include(i => i.FinOp).ThenInclude(i => i.ToCashDesk)
    //                                         .ToListAsync();

    //   string key = subDomain + CacheKeys.FinOpOrderLines;
    //   if (finoporderlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(finoporderlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return finoporderlines;
    // }

    // public async Task<List<Cheque>> GetCheques()
    // {
    //   List<Cheque> cheques = new List<Cheque>();

    //   string key = subDomain + CacheKeys.Cheques;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       cheques = JsonSerializer.Deserialize<List<Cheque>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       cheques = await SetCheques();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return cheques;
    // }

    // public async Task<List<Cheque>> SetCheques()
    // {
    //   List<Cheque> cheques = await _context.Cheques.ToListAsync();

    //   string key = subDomain + CacheKeys.Cheques;
    //   if (cheques.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(cheques, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return cheques;
    // }

    // public async Task<List<Bank>> GetBanks()
    // {
    //   List<Bank> banks = new List<Bank>();

    //   string key = subDomain + CacheKeys.Banks;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       banks = JsonSerializer.Deserialize<List<Bank>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       banks = await SetBanks();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return banks;
    // }

    // public async Task<List<Bank>> SetBanks()
    // {
    //   List<Bank> banks = await _context.Banks.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Banks;
    //   if (banks.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(banks, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return banks;
    // }

    // public async Task<List<PaymentType>> GetPaymentTypes()
    // {
    //   List<PaymentType> paymentTypes = new List<PaymentType>();

    //   string key = subDomain + CacheKeys.PaymentTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       paymentTypes = JsonSerializer.Deserialize<List<PaymentType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       paymentTypes = await SetPaymentTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return paymentTypes;
    // }

    // public async Task<List<PaymentType>> SetPaymentTypes()
    // {
    //   List<PaymentType> paymentTypes = await _context.PaymentTypes.OrderBy(o => o.DsplSeq).ToListAsync();

    //   string key = subDomain + CacheKeys.PaymentTypes;
    //   if (paymentTypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(paymentTypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return paymentTypes;
    // }

    // public async Task<List<Product>> GetProducts()
    // {
    //   List<Product> products = new List<Product>();

    //   string key = subDomain + CacheKeys.Products;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       products = JsonSerializer.Deserialize<List<Product>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       products = await SetProducts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return products;
    // }

    // public async Task<List<Product>> SetProducts()
    // {
    //   List<Product> products = await _context.Products.Include(a => a.Periodicity)
    //                                                   .Include(a => a.PayableAt)
    //                                                   .Include(i => i.ProductType)
    //                                                   .OrderBy(o => o.DsplSeq)
    //                                                   .ToListAsync();

    //   string key = subDomain + CacheKeys.Products;
    //   if (products.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(products, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return products;
    // }

    // public async Task<List<ProductType>> GetProductTypes()
    // {
    //   List<ProductType> productTypes = new List<ProductType>();

    //   string key = subDomain + CacheKeys.ProductTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       productTypes = JsonSerializer.Deserialize<List<ProductType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       productTypes = await SetProductTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return productTypes;
    // }

    // public async Task<List<ProductType>> SetProductTypes()
    // {
    //   List<ProductType> productTypes = await _context.ProductTypes.OrderBy(o => o.Name)
    //                                                               .ToListAsync();

    //   string key = subDomain + CacheKeys.ProductTypes;
    //   if (productTypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(productTypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return productTypes;
    // }

    // public async Task<List<UserType>> GetUserTypes()
    // {
    //   List<UserType> usertypes = new List<UserType>();

    //   string key = subDomain + CacheKeys.UserTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       usertypes = JsonSerializer.Deserialize<List<UserType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       usertypes = await SetUserTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return usertypes;
    // }

    // public async Task<List<UserType>> SetUserTypes()
    // {
    //   List<UserType> usertypes = await _context.UserTypes.ToListAsync();

    //   string key = subDomain + CacheKeys.UserTypes;
    //   if (usertypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(usertypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return usertypes;
    // }

    // public async Task<List<Menu>> GetMenus()
    // {
    //   List<Menu> menus = new List<Menu>();

    //   string key = subDomain + CacheKeys.Menus;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       menus = JsonSerializer.Deserialize<List<Menu>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       menus = await SetMenus();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return menus;
    // }

    // public async Task<List<Menu>> SetMenus()
    // {
    //   List<Menu> menus = await _context.Menus.Include(i => i.UserType).OrderBy(o => o.Name)
    //                                          .ToListAsync();

    //   string key = subDomain + CacheKeys.Menus;
    //   if (menus.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(menus, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return menus;
    // }

    // public async Task<List<MenuItem>> GetMenuItems()
    // {
    //   List<MenuItem> menuItems = new List<MenuItem>();

    //   string key = subDomain + CacheKeys.MenuItems;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       menuItems = JsonSerializer.Deserialize<List<MenuItem>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       menuItems = await SetMenuItems();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return menuItems;
    // }

    // public async Task<List<MenuItem>> SetMenuItems()
    // {
    //   List<MenuItem> menuItems = await _context.MenuItems
    //                                     .Include(i => i.ParentMenu)
    //                                     .OrderBy(o => o.ParentMenuId).ThenBy(o => o.DsplSeq).ThenBy(o => o.DisplayName)
    //                                     .ToListAsync();

    //   string key = subDomain + CacheKeys.MenuItems;
    //   if (menuItems.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(menuItems, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return menuItems;
    // }

    // public async Task<List<Capability>> GetCapabilities()
    // {
    //   List<Capability> capabilities = new List<Capability>();

    //   string key = subDomain + CacheKeys.Capabilities;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       capabilities = JsonSerializer.Deserialize<List<Capability>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       capabilities = await SetCapabilities();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return capabilities;
    // }

    // public async Task<List<Capability>> SetCapabilities()
    // {
    //   List<Capability> capabilities = await _context.Capabilities.Include(i => i.MenuItem).ToListAsync();

    //   string key = subDomain + CacheKeys.Capabilities;
    //   if (capabilities.Count() > 0)
    //   {
    //     _redis.KeyDelete(key);
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(capabilities, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return capabilities;
    // }

    // public async Task<List<RoleCapability>> GetRoleCapabilities()
    // {
    //   List<RoleCapability> roleCapabilities = new List<RoleCapability>();

    //   string key = subDomain + CacheKeys.RoleCapabilities;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       roleCapabilities = JsonSerializer.Deserialize<List<RoleCapability>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       roleCapabilities = await SetRoleCapabilities();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return roleCapabilities;
    // }

    // public async Task<List<RoleCapability>> SetRoleCapabilities()
    // {
    //   List<RoleCapability> roleCapabilities = await _context.RoleCapabilities.Include(i => i.Role)
    //                                                                          .Include(i => i.Capability)
    //                                                                          .ToListAsync();

    //   string key = subDomain + CacheKeys.RoleCapabilities;
    //   if (roleCapabilities.Count() > 0)
    //   {
    //     _redis.KeyDelete(key);
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(roleCapabilities, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return roleCapabilities;
    // }

    // public async Task<List<Schedule>> GetSchedules()
    // {
    //   List<Schedule> schedules = new List<Schedule>();

    //   string key = subDomain + CacheKeys.Schedules;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       schedules = JsonSerializer.Deserialize<List<Schedule>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       schedules = await SetSchedules();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return schedules;
    // }

    // public async Task<List<Schedule>> SetSchedules()
    // {
    //   List<Schedule> schedules = await _context.Schedules.Include(i => i.Class).ThenInclude(i => i.ClassLevel)
    //                                                      .ToListAsync();

    //   string key = subDomain + CacheKeys.Schedules;
    //   if (schedules.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(schedules, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return schedules;
    // }

    // public async Task<List<ScheduleCourse>> GetScheduleCourses()
    // {
    //   List<ScheduleCourse> schedules = new List<ScheduleCourse>();

    //   string key = subDomain + CacheKeys.ScheduleCourses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       schedules = JsonSerializer.Deserialize<List<ScheduleCourse>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       schedules = await SetScheduleCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return schedules;
    // }

    // public async Task<List<ScheduleCourse>> SetScheduleCourses()
    // {
    //   List<ScheduleCourse> scheduleCourses = await _context.ScheduleCourses
    //                                     .Include(i => i.Course)
    //                                     .Include(i => i.Schedule).ThenInclude(i => i.Class)
    //                                     .Include(i => i.Teacher)
    //                                     .ToListAsync();

    //   string key = subDomain + CacheKeys.ScheduleCourses;
    //   if (scheduleCourses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(scheduleCourses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return scheduleCourses;
    // }

    // public async Task<List<ScheduleActivity>> GetScheduleActivities()
    // {
    //   List<ScheduleActivity> activities = new List<ScheduleActivity>();

    //   string key = subDomain + CacheKeys.ScheduleActivities;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       activities = JsonSerializer.Deserialize<List<ScheduleActivity>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       activities = await SetScheduleActivities();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return activities;
    // }

    // public async Task<List<ScheduleActivity>> SetScheduleActivities()
    // {
    //   List<ScheduleActivity> scheduleActivities = await _context.ScheduleActivities
    //                                     .Include(i => i.Activity)
    //                                     .Include(i => i.Schedule).ThenInclude(i => i.Class)
    //                                     .ToListAsync();

    //   string key = subDomain + CacheKeys.ScheduleActivities;
    //   if (scheduleActivities.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(scheduleActivities, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return scheduleActivities;
    // }

    // public async Task<List<Agenda>> GetAgendas()
    // {
    //   List<Agenda> agendas = new List<Agenda>();

    //   string key = subDomain + CacheKeys.Agendas;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       agendas = JsonSerializer.Deserialize<List<Agenda>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       agendas = await SetAgendas();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return agendas;
    // }

    // public async Task<List<Agenda>> SetAgendas()
    // {
    //   List<Agenda> agendas = await _context.Agendas
    //                                     .Include(i => i.Session).ThenInclude(i => i.Class)
    //                                     .Include(i => i.Session).ThenInclude(i => i.Course)
    //                                     .Include(i => i.Session)
    //                                     .Include(i => i.DoneSetBy)
    //                                     .ToListAsync();

    //   string key = subDomain + CacheKeys.Agendas;
    //   if (agendas.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(agendas, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return agendas;
    // }

    // public async Task<List<Session>> GetSessions()
    // {
    //   List<Session> sessions = new List<Session>();

    //   string key = subDomain + CacheKeys.Sessions;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       sessions = JsonSerializer.Deserialize<List<Session>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       sessions = await SetSessions();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return sessions;
    // }

    // public async Task<List<Session>> SetSessions()
    // {
    //   List<Session> sessions = await _context.Sessions.Include(i => i.ScheduleCourse)
    //                                                   .Include(i => i.Teacher)
    //                                                   .Include(i => i.Course)
    //                                                   .Include(i => i.Class).ThenInclude(i => i.ClassLevel)
    //                                                   .ToListAsync();

    //   string key = subDomain + CacheKeys.Sessions;
    //   if (sessions.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(sessions, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return sessions;
    // }

    // public async Task<List<Event>> GetEvents()
    // {
    //   List<Event> events = new List<Event>();

    //   string key = subDomain + CacheKeys.Events;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       events = JsonSerializer.Deserialize<List<Event>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       events = await SetEvents();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return events;
    // }

    // public async Task<List<Event>> SetEvents()
    // {
    //   List<Event> events = await _context.Events.Include(i => i.User)
    //                                             .Include(i => i.EventType)
    //                                             .Include(i => i.Evaluation)
    //                                             .Include(i => i.Class)
    //                                             .Include(i => i.Session)
    //                                             .ToListAsync();

    //   string key = subDomain + CacheKeys.Events;
    //   if (events.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(events, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return events;
    // }

    // public async Task<List<CourseType>> GetCourseTypes()
    // {
    //   List<CourseType> courseTypes = new List<CourseType>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.CourseTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       courseTypes = JsonSerializer.Deserialize<List<CourseType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       courseTypes = await SetCourseTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return courseTypes;
    // }

    // public async Task<List<CourseType>> SetCourseTypes()
    // {
    //   List<CourseType> courseTypes = await _context.CourseTypes.ToListAsync();

    //   string key = subDomain + CacheKeys.CourseTypes;
    //   if (courseTypes.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(courseTypes, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return courseTypes;
    // }

    // public async Task<List<Conflict>> GetConflicts()
    // {
    //   List<Conflict> conflicts = new List<Conflict>();

    //   string key = subDomain + CacheKeys.Conflicts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       conflicts = JsonSerializer.Deserialize<List<Conflict>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       conflicts = await SetConflicts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return conflicts;
    // }

    // public async Task<List<Conflict>> SetConflicts()
    // {
    //   List<Conflict> conflicts = await _context.Conflicts.ToListAsync();

    //   string key = subDomain + CacheKeys.Conflicts;
    //   if (conflicts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(conflicts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return conflicts;
    // }

    // public async Task<List<CourseConflict>> GetCourseConflicts()
    // {
    //   List<CourseConflict> courseConflicts = new List<CourseConflict>();

    //   string key = subDomain + CacheKeys.CourseConflicts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       courseConflicts = JsonSerializer.Deserialize<List<CourseConflict>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       courseConflicts = await SetCourseConflicts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return courseConflicts;
    // }

    // public async Task<List<CourseConflict>> SetCourseConflicts()
    // {
    //   List<CourseConflict> courseConflicts = await _context.CourseConflicts.ToListAsync();

    //   string key = subDomain + CacheKeys.CourseConflicts;
    //   if (courseConflicts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(courseConflicts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return courseConflicts;
    // }

    // public async Task<List<MaritalStatus>> GetMaritalStatus()
    // {
    //   List<MaritalStatus> status = new List<MaritalStatus>();

    //   string key = subDomain + CacheKeys.MaritalStatus;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       status = JsonSerializer.Deserialize<List<MaritalStatus>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       status = await SetMaritalStatus();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return status;
    // }

    // public async Task<List<MaritalStatus>> SetMaritalStatus()
    // {
    //   List<MaritalStatus> status = await _context.MaritalStatuses.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.MaritalStatus;
    //   if (status.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(status, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return status;
    // }

    // public async Task<List<Country>> GetCountries()
    // {
    //   List<Country> countries = new List<Country>();

    //   string key = subDomain + CacheKeys.Countries;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       countries = JsonSerializer.Deserialize<List<Country>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       countries = await SetCountries();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return countries;
    // }

    // public async Task<List<Country>> SetCountries()
    // {
    //   List<Country> countries = await _context.Countries.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Countries;
    //   if (countries.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(countries, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return countries;
    // }

    // public async Task<List<City>> GetCities()
    // {
    //   List<City> cities = new List<City>();

    //   string key = subDomain + CacheKeys.Cities;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       cities = JsonSerializer.Deserialize<List<City>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       cities = await SetCities();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return cities;
    // }

    // public async Task<List<City>> SetCities()
    // {
    //   List<City> cities = await _context.Cities.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Cities;
    //   if (cities.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(cities, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return cities;
    // }

    // public async Task<List<District>> GetDistricts()
    // {
    //   List<District> districts = new List<District>();

    //   string key = subDomain + CacheKeys.Districts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       districts = JsonSerializer.Deserialize<List<District>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       districts = await SetDistricts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return districts;
    // }

    // public async Task<List<District>> SetDistricts()
    // {
    //   List<District> districts = await _context.Districts.Include(i => i.City).OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Districts;
    //   if (districts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(districts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return districts;
    // }

    // public async Task<List<Photo>> GetPhotos()
    // {
    //   List<Photo> photos = new List<Photo>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Photos;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       photos = JsonSerializer.Deserialize<List<Photo>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       photos = await SetPhotos();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return photos;
    // }

    // public async Task<List<Photo>> SetPhotos()
    // {
    //   List<Photo> photos = await _context.Photos.ToListAsync();

    //   string key = subDomain + CacheKeys.Photos;
    //   if (photos.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(photos, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return photos;
    // }

    // public async Task<List<Zone>> GetZones()
    // {
    //   List<Zone> zones = new List<Zone>();

    //   string key = subDomain + CacheKeys.Zones;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       zones = JsonSerializer.Deserialize<List<Zone>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       zones = await SetZones();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return zones;
    // }

    // public async Task<List<Zone>> SetZones()
    // {
    //   List<Zone> zones = await _context.Zones.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Zones;
    //   if (zones.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(zones, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return zones;
    // }

    // public async Task<List<ZoneDeadLine>> GetZoneDeadLines()
    // {
    //   List<ZoneDeadLine> zoneDeadlines = new List<ZoneDeadLine>();

    //   string key = subDomain + CacheKeys.ZoneDeadLines;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       zoneDeadlines = JsonSerializer.Deserialize<List<ZoneDeadLine>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       zoneDeadlines = await SetZoneDeadLines();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return zoneDeadlines;
    // }

    // public async Task<List<ZoneDeadLine>> SetZoneDeadLines()
    // {
    //   List<ZoneDeadLine> zoneDeadlines = await _context.ZoneDeadLines.Include(i => i.ProductDeadLine).ToListAsync();

    //   string key = subDomain + CacheKeys.ZoneDeadLines;
    //   if (zoneDeadlines.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(zoneDeadlines, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return zoneDeadlines;
    // }

    // public async Task<List<LocationZone>> GetLocationZones()
    // {
    //   List<LocationZone> locationzones = new List<LocationZone>();

    //   string key = subDomain + CacheKeys.LocationZones;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       locationzones = JsonSerializer.Deserialize<List<LocationZone>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       locationzones = await SetLocationZones();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return locationzones;
    // }

    // public async Task<List<LocationZone>> SetLocationZones()
    // {
    //   List<LocationZone> locationzones = await _context.LocationZones.Include(i => i.District)
    //                                                                  .Include(i => i.City)
    //                                                                  .Include(i => i.Country)
    //                                                                  .ToListAsync();

    //   string key = subDomain + CacheKeys.LocationZones;
    //   if (locationzones.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(locationzones, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return locationzones;
    // }

    // public async Task<List<ProductZone>> GetProductZones()
    // {
    //   List<ProductZone> productZones = new List<ProductZone>();

    //   string key = subDomain + CacheKeys.ProductZones;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       productZones = JsonSerializer.Deserialize<List<ProductZone>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       productZones = await SetProductZones();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return productZones;
    // }

    // public async Task<List<ProductZone>> SetProductZones()
    // {
    //   List<ProductZone> productZones = await _context.ProductZones.Include(i => i.Product)
    //                                                               .Include(i => i.Zone)
    //                                                               .OrderBy(o => o.Zone.Name)
    //                                                               .ToListAsync();

    //   string key = subDomain + CacheKeys.ProductZones;
    //   if (productZones.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(productZones, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return productZones;
    // }

    // public async Task<List<Periodicity>> GetPeriodicities()
    // {
    //   List<Periodicity> periodicities = new List<Periodicity>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Periodicities;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       periodicities = JsonSerializer.Deserialize<List<Periodicity>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       periodicities = await SetPeriodicities();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return periodicities;
    // }

    // public async Task<List<Periodicity>> SetPeriodicities()
    // {
    //   List<Periodicity> periodicities = await _context.Periodicities.OrderBy(p => p.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Periodicities;
    //   if (periodicities.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(periodicities, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return periodicities;
    // }

    // public async Task<List<PayableAt>> GetPayableAts()
    // {
    //   List<PayableAt> payableAts = new List<PayableAt>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.PayableAts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       payableAts = JsonSerializer.Deserialize<List<PayableAt>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       payableAts = await SetPayableAts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return payableAts;
    // }

    // public async Task<List<PayableAt>> SetPayableAts()
    // {
    //   List<PayableAt> payableAts = await _context.PayableAts.OrderBy(p => p.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.PayableAts;
    //   if (payableAts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(payableAts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return payableAts;
    // }

    // public async Task<List<Subscription>> GetSubscriptions()
    // {
    //   List<Subscription> subscriptions = new List<Subscription>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Subscriptions;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       subscriptions = JsonSerializer.Deserialize<List<Subscription>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       subscriptions = await SetSubscriptions();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return subscriptions;
    // }

    // public async Task<List<Subscription>> SetSubscriptions()
    // {
    //   List<Subscription> subscriptions = await _context.Subscriptions.Include(i => i.Product).ThenInclude(i => i.PayableAt)
    //                                                                  .Include(i => i.Product).ThenInclude(i => i.Periodicity)
    //                                                                  .Include(i => i.User).ThenInclude(i => i.Photos)
    //                                                                  .ToListAsync();

    //   string key = subDomain + CacheKeys.Subscriptions;
    //   if (subscriptions.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(subscriptions, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return subscriptions;
    // }

    // public async Task<List<Discount>> GetDiscounts()
    // {
    //   List<Discount> discounts = new List<Discount>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Discounts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       discounts = JsonSerializer.Deserialize<List<Discount>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       discounts = await SetDiscounts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return discounts;
    // }

    // public async Task<List<Discount>> SetDiscounts()
    // {
    //   List<Discount> discounts = await _context.Discounts.Include(i => i.Product).Include(i => i.DiscountType).OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Discounts;
    //   if (discounts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(discounts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return discounts;
    // }

    // public async Task<List<Religion>> GetReligions()
    // {
    //   List<Religion> religions = new List<Religion>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Religions;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       religions = JsonSerializer.Deserialize<List<Religion>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       religions = await SetReligions();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return religions;
    // }

    // public async Task<List<Religion>> SetReligions()
    // {
    //   List<Religion> religions = await _context.Religions.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Religions;
    //   if (religions.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(religions, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return religions;
    // }

    // public async Task<List<BirthCertificate>> GetBirthCertificates()
    // {
    //   List<BirthCertificate> birthcertifs = new List<BirthCertificate>();

    //   string key = subDomain + CacheKeys.BirthCertificates;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       birthcertifs = JsonSerializer.Deserialize<List<BirthCertificate>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       birthcertifs = await SetBirthCertificates();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return birthcertifs;
    // }

    // public async Task<List<BirthCertificate>> SetBirthCertificates()
    // {
    //   List<BirthCertificate> birthcertifs = await _context.BirthCertificates.Include(i => i.User)
    //                                                                         .OrderBy(o => o.User.LastName)
    //                                                                         .ThenBy(o => o.User.FirstName).ToListAsync();

    //   string key = subDomain + CacheKeys.BirthCertificates;
    //   if (birthcertifs.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(birthcertifs, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return birthcertifs;
    // }

    // public async Task<List<MedicalSheet>> GetMedicalSheets()
    // {
    //   List<MedicalSheet> medicalSheets = new List<MedicalSheet>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.MedicalSheets;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       medicalSheets = JsonSerializer.Deserialize<List<MedicalSheet>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       medicalSheets = await SetMedicalSheets();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return medicalSheets;
    // }

    // public async Task<List<MedicalSheet>> SetMedicalSheets()
    // {
    //   List<MedicalSheet> medicalSheets = await _context.MedicalSheets.Include(i => i.User)
    //                                                                  .OrderBy(o => o.User.LastName)
    //                                                                  .ThenBy(o => o.User.FirstName).ToListAsync();

    //   string key = subDomain + CacheKeys.MedicalSheets;
    //   if (medicalSheets.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(medicalSheets, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return medicalSheets;
    // }

    // public async Task<List<TuitionSheet>> GetTuitionSheets()
    // {
    //   List<TuitionSheet> tuitionSheets = new List<TuitionSheet>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.TuitionSheets;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       tuitionSheets = JsonSerializer.Deserialize<List<TuitionSheet>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       tuitionSheets = await SetTuitionSheets();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return tuitionSheets;
    // }

    // public async Task<List<TuitionSheet>> SetTuitionSheets()
    // {
    //   List<TuitionSheet> tuitionSheets = await _context.TuitionSheets.Include(i => i.User)
    //                                                                  .OrderBy(o => o.User.LastName)
    //                                                                  .ThenBy(o => o.User.FirstName).ToListAsync();

    //   string key = subDomain + CacheKeys.TuitionSheets;
    //   if (tuitionSheets.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(tuitionSheets, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return tuitionSheets;
    // }

    // public async Task<List<ParentalRespSheet>> GetParentalRespSheets()
    // {
    //   List<ParentalRespSheet> sheets = new List<ParentalRespSheet>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.ParentalRespSheets;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       sheets = JsonSerializer.Deserialize<List<ParentalRespSheet>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       sheets = await SetParentalRespSheets();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return sheets;
    // }

    // public async Task<List<ParentalRespSheet>> SetParentalRespSheets()
    // {
    //   List<ParentalRespSheet> sheets = await _context.ParentalRespSheets.Include(i => i.User)
    //                                                                  .OrderBy(o => o.User.LastName)
    //                                                                  .ThenBy(o => o.User.FirstName).ToListAsync();

    //   string key = subDomain + CacheKeys.ParentalRespSheets;
    //   if (sheets.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(sheets, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return sheets;
    // }

    // public async Task<List<Establishment>> GetEstablishments()
    // {
    //   List<Establishment> schools = new List<Establishment>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Establishments;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       schools = JsonSerializer.Deserialize<List<Establishment>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       schools = await SetEstablishments();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return schools;
    // }

    // public async Task<List<Establishment>> SetEstablishments()
    // {
    //   List<Establishment> schools = await _context.Establishments.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Establishments;
    //   if (schools.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(schools, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return schools;
    // }

    // public async Task<List<FileElement>> GetFileElements()
    // {
    //   List<FileElement> elements = new List<FileElement>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.FileElements;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       elements = JsonSerializer.Deserialize<List<FileElement>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       elements = await SetFileElements();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return elements;
    // }

    // public async Task<List<FileElement>> SetFileElements()
    // {
    //   List<FileElement> elements = await _context.FileElements.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.FileElements;
    //   if (elements.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(elements, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return elements;
    // }

    // public async Task<List<UserFileElement>> GetUserFileElements()
    // {
    //   List<UserFileElement> elements = new List<UserFileElement>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.UserFileElements;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       elements = JsonSerializer.Deserialize<List<UserFileElement>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       elements = await SetUserFileElements();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return elements;
    // }

    // public async Task<List<UserFileElement>> SetUserFileElements()
    // {
    //   List<UserFileElement> elements = await _context.UserFileElements.Include(i => i.FileElement).ToListAsync();

    //   string key = subDomain + CacheKeys.UserFileElements;
    //   if (elements.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(elements, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return elements;
    // }

    // public async Task<List<Client>> GetClients()
    // {
    //   List<Client> clients = new List<Client>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Clients;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       clients = JsonSerializer.Deserialize<List<Client>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       clients = await SetClients();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return clients;
    // }

    // public async Task<List<Client>> SetClients()
    // {
    //   List<Client> clients = await _context.Clients.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Clients;
    //   if (clients.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(clients, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return clients;
    // }

    // public async Task<List<CourseCoefficient>> GetCourseCoefficients()
    // {
    //   List<CourseCoefficient> courseCoeffs = new List<CourseCoefficient>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.CourseCoefficients;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       courseCoeffs = JsonSerializer.Deserialize<List<CourseCoefficient>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       courseCoeffs = await SetCourseCoefficients();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return courseCoeffs;
    // }

    // public async Task<List<CourseCoefficient>> SetCourseCoefficients()
    // {
    //   List<CourseCoefficient> courseCoeffs = await _context.CourseCoefficients.Include(i => i.Course)
    //                                                                           .Include(i => i.ClassType)
    //                                                                           .Include(i => i.ClassLevel)
    //                                                                           .ToListAsync();

    //   string key = subDomain + CacheKeys.CourseCoefficients;
    //   if (courseCoeffs.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(courseCoeffs, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return courseCoeffs;
    // }

    // public async Task<List<ClassLevelCourse>> GetClassLevelCourses()
    // {
    //   List<ClassLevelCourse> levelCourses = new List<ClassLevelCourse>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.ClassLevelCourses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       levelCourses = JsonSerializer.Deserialize<List<ClassLevelCourse>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       levelCourses = await SetClassLevelCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return levelCourses;
    // }

    // public async Task<List<ClassLevelCourse>> SetClassLevelCourses()
    // {
    //   List<ClassLevelCourse> levelCourses = await _context.ClassLevelCourses.ToListAsync();

    //   string key = subDomain + CacheKeys.ClassLevelCourses;
    //   if (levelCourses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(levelCourses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return levelCourses;
    // }

    // public async Task<List<Activity>> GetActivities()
    // {
    //   List<Activity> activities = new List<Activity>();

    //   string key = subDomain + CacheKeys.Activities;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       activities = JsonSerializer.Deserialize<List<Activity>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       activities = await SetActivities();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return activities;
    // }

    // public async Task<List<Activity>> SetActivities()
    // {
    //   List<Activity> activities = await _context.Activities.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Activities;
    //   if(activities.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(activities, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return activities;
    // }

    // public async Task<List<Period>> GetPeriods()
    // {
    //   List<Period> periods = new List<Period>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Periods;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       periods = JsonSerializer.Deserialize<List<Period>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       periods = await SetPeriods();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return periods;
    // }

    // public async Task<List<Period>> SetPeriods()
    // {
    //   List<Period> periods = await _context.Periods.Include(i => i.EducationLevel).OrderBy(p => p.StartDate).ToListAsync();

    //   string key = subDomain + CacheKeys.Periods;
    //   if (periods.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(periods, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return periods;
    // }

    // public async Task<List<API.Entities.Group>> GetGroups()
    // {
    //   List<API.Entities.Group> groups = new List<API.Entities.Group>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Groups;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       groups = JsonSerializer.Deserialize<List<API.Entities.Group>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       groups = await SetGroups();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return groups;
    // }

    // public async Task<List<API.Entities.Group>> SetGroups()
    // {
    //   List<API.Entities.Group> groups = await _context.Groups.OrderBy(p => p.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Groups;
    //   if (groups.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(groups, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return groups;
    // }

    // public async Task<List<GroupCourse>> GetGroupCourses()
    // {
    //   List<GroupCourse> groupcourses = new List<GroupCourse>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.GroupCourses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       groupcourses = JsonSerializer.Deserialize<List<GroupCourse>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       groupcourses = await SetGroupCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return groupcourses;
    // }

    // public async Task<List<GroupCourse>> SetGroupCourses()
    // {
    //   List<GroupCourse> groupcourses = await _context.GroupCourses.Include(i => i.Course)
    //                                                               .Include(i => i.Group)
    //                                                               .ToListAsync();

    //   string key = subDomain + CacheKeys.GroupCourses;
    //   if (groupcourses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(groupcourses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return groupcourses;
    // }

    // public async Task<List<Absence>> GetAbsences()
    // {
    //   List<Absence> absences = new List<Absence>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Absences;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       absences = JsonSerializer.Deserialize<List<Absence>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       absences = await SetAbsences();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return absences;
    // }

    // public async Task<List<Absence>> SetAbsences()
    // {
    //   List<Absence> absences = await _context.Absences
    //     .Include(i => i.User).ThenInclude(i => i.Class)
    //     .Include(i => i.User).ThenInclude(i => i.Photos)
    //     .Include(i => i.User).ThenInclude(i => i.UserType)
    //     .Include(i => i.DoneBy)
    //     .Include(i => i.AbsenceType)
    //     .Include(i => i.Session).ThenInclude(i => i.Course)
    //     .Include(i => i.Period)
    //     .ToListAsync();

    //   string key = subDomain + CacheKeys.Absences;
    //   if (absences.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(absences, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return absences;
    // }

    // public async Task<List<CallSheet>> GetCallSheets()
    // {
    //   List<CallSheet> callSheets = new List<CallSheet>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.CallSheets;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       callSheets = JsonSerializer.Deserialize<List<CallSheet>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       callSheets = await SetCallSheets();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return callSheets;
    // }

    // public async Task<List<CallSheet>> SetCallSheets()
    // {
    //   List<CallSheet> callSheets = await _context.CallSheets.Include(i => i.Session)
    //                                                         .Include(i => i.DoneBy).ToListAsync();

    //   string key = subDomain + CacheKeys.CallSheets;
    //   if (callSheets.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(callSheets, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return callSheets;
    // }

    // public async Task<List<LoginPageInfo>> GetLoginPageInfos()
    // {
    //   List<LoginPageInfo> infos = new List<LoginPageInfo>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.LoginPageInfos;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       infos = JsonSerializer.Deserialize<List<LoginPageInfo>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       infos = await SetLoginPageInfos();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return infos;
    // }

    // public async Task<List<LoginPageInfo>> SetLoginPageInfos()
    // {
    //   List<LoginPageInfo> infos = await _context.LoginPageInfos.OrderBy(o => o.InfoDate).ToListAsync();

    //   string key = subDomain + CacheKeys.LoginPageInfos;
    //   if (infos.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(infos, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return infos;
    // }

    // public async Task<List<Sanction>> GetSanctions()
    // {
    //   List<Sanction> sanctions = new List<Sanction>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Sanctions;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       sanctions = JsonSerializer.Deserialize<List<Sanction>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       sanctions = await SetSanctions();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return sanctions;
    // }

    // public async Task<List<Sanction>> SetSanctions()
    // {
    //   List<Sanction> sanctions = await _context.Sanctions.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Sanctions;
    //   if (sanctions.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(sanctions, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return sanctions;
    // }

    // public async Task<List<Reason>> GetReasons()
    // {
    //   List<Reason> reasons = new List<Reason>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Reasons;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       reasons = JsonSerializer.Deserialize<List<Reason>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       reasons = await SetReasons();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return reasons;
    // }

    // public async Task<List<Reason>> SetReasons()
    // {
    //   List<Reason> reasons = await _context.Reasons.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Reasons;
    //   if (reasons.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(reasons, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return reasons;
    // }

    // public async Task<List<ReasonType>> GetReasonTypes()
    // {
    //   List<ReasonType> types = new List<ReasonType>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.ReasonTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       types = JsonSerializer.Deserialize<List<ReasonType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       types = await SetReasonTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return types;
    // }

    // public async Task<List<ReasonType>> SetReasonTypes()
    // {
    //   List<ReasonType> reasons = await _context.ReasonTypes.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.ReasonTypes;
    //   if (reasons.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(reasons, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return reasons;
    // }

    // public async Task<List<Conduct>> GetConducts()
    // {
    //   List<Conduct> conducts = new List<Conduct>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Conducts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       conducts = JsonSerializer.Deserialize<List<Conduct>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       conducts = await SetConducts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return conducts;
    // }

    // public async Task<List<Conduct>> SetConducts()
    // {
    //   List<Conduct> conducts = await _context.Conducts.OrderBy(o => o.ConductDate).ToListAsync();

    //   string key = subDomain + CacheKeys.Conducts;
    //   if (conducts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(conducts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return conducts;
    // }

    // public async Task<List<UserConduct>> GetUserConducts()
    // {
    //   List<UserConduct> userconducts = new List<UserConduct>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.UserConducts;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       userconducts = JsonSerializer.Deserialize<List<UserConduct>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       userconducts = await SetUserConducts();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return userconducts;
    // }

    // public async Task<List<UserConduct>> SetUserConducts()
    // {
    //   List<UserConduct> userconducts = await _context.UserConducts.Include(i => i.User).ThenInclude(i => i.Photos)
    //                                                               .Include(i => i.Conduct).ToListAsync();

    //   string key = subDomain + CacheKeys.UserConducts;
    //   if (userconducts.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(userconducts, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return userconducts;
    // }

    // public async Task<List<StudentReport>> GetStudentReports()
    // {
    //   List<StudentReport> studentReports = new List<StudentReport>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.StudentReports;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       studentReports = JsonSerializer.Deserialize<List<StudentReport>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       studentReports = await SetStudentReports();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return studentReports;
    // }

    // public async Task<List<StudentReport>> SetStudentReports()
    // {
    //   List<StudentReport> studentReports = await _context.StudentReports.Include(i => i.Student)
    //                                                                     .Include(i => i.Course)
    //                                                                     .Include(i => i.Period).ToListAsync();

    //   string key = subDomain + CacheKeys.StudentReports;
    //   if (studentReports.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(studentReports, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return studentReports;
    // }

    // public async Task<List<GradesValidation>> GetGradesValidations()
    // {
    //   List<GradesValidation> gradesValidations = new List<GradesValidation>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.GradesValidations;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       gradesValidations = JsonSerializer.Deserialize<List<GradesValidation>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       gradesValidations = await SetGradesValidations();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return gradesValidations;
    // }

    // public async Task<List<GradesValidation>> SetGradesValidations()
    // {
    //   List<GradesValidation> gradesValidations = await _context.GradesValidations.ToListAsync();

    //   string key = subDomain + CacheKeys.GradesValidations;
    //   if (gradesValidations.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(gradesValidations, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return gradesValidations;
    // }

    // public async Task<List<OpenCourseRequest>> GetOpenCourseRequests()
    // {
    //   List<OpenCourseRequest> requests = new List<OpenCourseRequest>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.OpenCourseRequests;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       requests = JsonSerializer.Deserialize<List<OpenCourseRequest>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       requests = await SetOpenCourseRequests();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return requests;
    // }

    // public async Task<List<OpenCourseRequest>> SetOpenCourseRequests()
    // {
    //   List<OpenCourseRequest> requests = await _context.OpenCourseRequests.ToListAsync();

    //   string key = subDomain + CacheKeys.OpenCourseRequests;
    //   if (requests.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(requests, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return requests;
    // }

    // public async Task<List<OpenTeacherRequest>> GetOpenTeacherRequests()
    // {
    //   List<OpenTeacherRequest> requests = new List<OpenTeacherRequest>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.OpenTeacherRequests;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       requests = JsonSerializer.Deserialize<List<OpenTeacherRequest>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       requests = await SetOpenTeacherRequests();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return requests;
    // }

    // public async Task<List<OpenTeacherRequest>> SetOpenTeacherRequests()
    // {
    //   List<OpenTeacherRequest> requests = await _context.OpenTeacherRequests.ToListAsync();

    //   string key = subDomain + CacheKeys.OpenTeacherRequests;
    //   if (requests.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(requests, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return requests;
    // }

    // public async Task<List<Incident>> GetIncidents()
    // {
    //   List<Incident> incidents = new List<Incident>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Incidents;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       incidents = JsonSerializer.Deserialize<List<Incident>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       incidents = await SetIncidents();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return incidents;
    // }

    // public async Task<List<Incident>> SetIncidents()
    // {
    //   List<Incident> incidents = await _context.Incidents.OrderBy(o => o.IncidentDate).ToListAsync();

    //   string key = subDomain + CacheKeys.Incidents;
    //   if (incidents.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(incidents, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return incidents;
    // }

    // public async Task<List<IncidentReason>> GetIncidentReasons()
    // {
    //   List<IncidentReason> reasons = new List<IncidentReason>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.IncidentReasons;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       reasons = JsonSerializer.Deserialize<List<IncidentReason>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       reasons = await SetIncidentReasons();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return reasons;
    // }

    // public async Task<List<IncidentReason>> SetIncidentReasons()
    // {
    //   List<IncidentReason> reasons = await _context.IncidentReasons.ToListAsync();

    //   string key = subDomain + CacheKeys.IncidentReasons;
    //   if (reasons.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(reasons, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return reasons;
    // }

    // public async Task<List<IncidentPhoto>> GetIncidentPhotos()
    // {
    //   List<IncidentPhoto> photos = new List<IncidentPhoto>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.IncidentPhotos;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       photos = JsonSerializer.Deserialize<List<IncidentPhoto>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       photos = await SetIncidentPhotos();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return photos;
    // }

    // public async Task<List<IncidentPhoto>> SetIncidentPhotos()
    // {
    //   List<IncidentPhoto> photos = await _context.IncidentPhotos.ToListAsync();

    //   string key = subDomain + CacheKeys.IncidentPhotos;
    //   if (photos.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(photos, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return photos;
    // }

    // public async Task<List<IncidentAction>> GetIncidentActions()
    // {
    //   List<IncidentAction> actions = new List<IncidentAction>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.IncidentActions;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       actions = JsonSerializer.Deserialize<List<IncidentAction>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       actions = await SetIncidentActions();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return actions;
    // }

    // public async Task<List<IncidentAction>> SetIncidentActions()
    // {
    //   List<IncidentAction> actions = await _context.IncidentActions.ToListAsync();

    //   string key = subDomain + CacheKeys.IncidentActions;
    //   if (actions.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(actions, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return actions;
    // }

    // public async Task<List<API.Entities.Action>> GetActions()
    // {
    //   List<API.Entities.Action> actions = new List<API.Entities.Action>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Actions;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       actions = JsonSerializer.Deserialize<List<API.Entities.Action>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       actions = await SetActions();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return actions;
    // }

    // public async Task<List<API.Entities.Action>> SetActions()
    // {
    //   List<API.Entities.Action> actions = await _context.Actions.ToListAsync();

    //   string key = subDomain + CacheKeys.Actions;
    //   if (actions.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(actions, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return actions;
    // }

    // public async Task<List<ConductReport>> GetConductReports()
    // {
    //   List<ConductReport> reports = new List<ConductReport>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.ConductReports;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       reports = JsonSerializer.Deserialize<List<ConductReport>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       reports = await SetConductReports();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return reports;
    // }

    // public async Task<List<ConductReport>> SetConductReports()
    // {
    //   List<ConductReport> reports = await _context.ConductReports.Include(i => i.Student).ThenInclude(i => i.Photos)
    //                                                              .Include(i => i.Period).ToListAsync();

    //   string key = subDomain + CacheKeys.ConductReports;
    //   if (reports.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(reports, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return reports;
    // }

    // public async Task<List<Bonus>> GetBonus()
    // {
    //   List<Bonus> bonus = new List<Bonus>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.Bonus;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       bonus = JsonSerializer.Deserialize<List<Bonus>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       bonus = await SetBonus();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return bonus;
    // }

    // public async Task<List<Bonus>> SetBonus()
    // {
    //   List<Bonus> bonus = await _context.Bonus.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.Bonus;
    //   if (bonus.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(bonus, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return bonus;
    // }

    // public async Task<List<BonusCourse>> GetBonusCourses()
    // {
    //   List<BonusCourse> bonuscourses = new List<BonusCourse>();

    //   // Look for cache key.
    //   string key = subDomain + CacheKeys.BonusCourses;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       bonuscourses = JsonSerializer.Deserialize<List<BonusCourse>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       bonuscourses = await SetBonusCourses();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return bonuscourses;
    // }

    // public async Task<List<BonusCourse>> SetBonusCourses()
    // {
    //   List<BonusCourse> bonuscourses = await _context.BonusCourses.ToListAsync();

    //   string key = subDomain + CacheKeys.BonusCourses;
    //   if (bonuscourses.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(bonuscourses, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return bonuscourses;
    // }

    // public async Task<List<RegistrationFee>> GetRegistrationFees()
    // {
    //   List<RegistrationFee> fees = new List<RegistrationFee>();

    //   string key = subDomain + CacheKeys.RegistrationFees;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       fees = JsonSerializer.Deserialize<List<RegistrationFee>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       fees = await SetRegistrationFees();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return fees;
    // }

    // public async Task<List<RegistrationFee>> SetRegistrationFees()
    // {
    //   List<RegistrationFee> fees = await _context.RegistrationFees.Include(i => i.RegFeeType).OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.RegistrationFees;
    //   if (fees.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(fees, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return fees;
    // }

    // public async Task<List<RegFeeType>> GetRegFeeTypes()
    // {
    //   List<RegFeeType> types = new List<RegFeeType>();

    //   string key = subDomain + CacheKeys.RegFeeTypes;
    //   SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
    //   await mylock.WaitAsync();
    //   try
    //   {
    //     var data = await _redis.StringGetAsync(key);
    //     if (!string.IsNullOrEmpty(data))
    //     {
    //       types = JsonSerializer.Deserialize<List<RegFeeType>>(data, jsonOptions);
    //     }
    //     else
    //     {
    //       // Key not in cache, so get data in DB.
    //       types = await SetRegFeeTypes();
    //     }
    //   }
    //   catch
    //   {
    //     _redis.KeyDelete(key);
    //   }
    //   finally
    //   {
    //     mylock.Release();
    //   }

    //   return types;
    // }

    // public async Task<List<RegFeeType>> SetRegFeeTypes()
    // {
    //   List<RegFeeType> types = await _context.RegFeeTypes.OrderBy(o => o.Name).ToListAsync();

    //   string key = subDomain + CacheKeys.RegFeeTypes;
    //   if (types.Count() > 0)
    //   {
    //     await _redis.StringSetAsync(key, JsonSerializer.Serialize<object>(types, jsonOptions), TimeSpan.FromDays(ttl));
    //   }
    //   else
    //   {
    //     _redis.KeyDelete(key);
    //   }

    //   return types;
    // }

  }
}
