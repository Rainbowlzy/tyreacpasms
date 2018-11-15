using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using T.Interfaces;
using T.Models;
using TEntities.EF;
using TENtities;
using TENtities.EF;

namespace T.Evaluators
{
    public abstract class Evaluator : IEvaluator
    {
        private static Dictionary<string, Dictionary<string, object>> _sessions =
            new Dictionary<string, Dictionary<string, object>>(10);

        private UserInformation _user;

        private CommonRequest Request { get; set; }

        protected Evaluator()
        {
            _user = CurrentUserInformation;

            using (var ctx = new DefaultContext())
            {
                ctx.BuildMenu();
                ctx.SaveChanges();
                var TransactionID = Guid.NewGuid().ToString();
                if (ctx.Warehouse.Any()) return;
                ctx.Cargo.AddRange("海尔冰箱，格力空调，海尔洗衣机，三星洗衣机".Split(new[] { '，' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new Cargo
                    {
                        CNameOfGoods = s,
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    }));
                ctx.Supplier.AddRange("海尔，格力，三星".Split(new[] { '，' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new Supplier
                    {
                        SSupplierName = s,
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    }));
                ctx.Warehouse.AddRange(new[]
                {
                    new Warehouse
                    {
                        WLocality = "五角场仓库",
                        WResponsibleForManualNumber = 10003,
                        WCapacity = 500,
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                    new Warehouse
                    {
                        WLocality = "新江湾城仓库",
                        WResponsibleForManualNumber = 10004,
                        WCapacity = 500,
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    }
                });
                ctx.Customertype.AddRange(new[]
                {
                    new Customertype
                    {
                        CCustomerNumber = 30001,
                        CName = "张三",
                        CCommonModeOfContact = "13222210188",
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                    new Customertype
                    {
                        CCustomerNumber = 30002,
                        CName = "李四",
                        CCommonModeOfContact = "13222210183",
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                });
                ctx.GoodsShelves.AddRange(new[]
                {
                    new GoodsShelves
                    {
                        GSResponsibleForManualNumber = 10003,
                        GSLocality = "五角场货架",
                        GSCapacity = 500,
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                    new GoodsShelves
                    {
                        GSResponsibleForManualNumber = 10004,
                        GSLocality = "新江湾城货架",
                        GSCapacity = 500,
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    }
                });
                ctx.Staffname.AddRange(new[]
                {
                    new Staffname
                    {
                        SJobNumber = 10001,
                        SName = "销售员1",
                        SCommonModeOfContact = "13222210155",
                        SEducation = "本科",
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                    new Staffname
                    {
                        SJobNumber = 10002,
                        SName = "销售员2",
                        SCommonModeOfContact = "13222288101",
                        SEducation = "本科",
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                    new Staffname
                    {
                        SJobNumber = 10003,
                        SName = "仓库管理员1",
                        SCommonModeOfContact = "13222210991",
                        SEducation = "本科",
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                    new Staffname
                    {
                        SJobNumber = 10004,
                        SName = "仓库管理员2",
                        SCommonModeOfContact = "13222440101",
                        SEducation = "本科",
                        CreateBy = "JOB",
                        TransactionID = TransactionID,
                        IsDeleted = 0,
                        DataLevel = "01",
                        CreateOn = DateTime.Now,
                    },
                });


                ctx.SaveChanges();
            }
        }

        public virtual string Comments => "重写此方法给出中文描述";

        protected void BuildSession(string token, UserInformation user)
        {
            if (_sessions.Count == 0 && File.Exists("_session.txt"))
                _sessions =
                    JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(
                        File.ReadAllText("_session.txt"));
            _sessions.Add(token,
                new Dictionary<string, object> {{nameof(UserInformation), user}, {nameof(token), token}});
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public UserInformation CurrentUserInformation
        {
            get
            {
                var key = "UserInformation";
                return Sessions == null || !Sessions.ContainsKey(key)
                    ? new UserInformation
                    {
                        UIRealName = "公众",
                        UILoginName = "public",
                        DataLevel = "019999",
                    }
                    : Sessions[key] as UserInformation;
            }
        }

        protected void Exit()
        {
            _sessions.Remove(Request.auth);
        }

        class CachedEntity<T> where T : class
        {
            public T data { get; set; }
            public DateTime createdtime { get; set; }

            public CachedEntity(T t)
            {
                data = t;
                createdtime = DateTime.Now;
            }
        }

        /// <summary>
        /// 暂存类标签属性
        /// </summary>
        private static Dictionary<Type, Dictionary<Type, object>> options = typeof(Evaluator).Assembly.GetTypes()
            .Where(t => t.BaseType == typeof(Evaluator))
            .GroupBy(t => t)
            .ToDictionary(t => t.Key, t =>
                t.FirstOrDefault()?.GetCustomAttributes(true).GroupBy(k => k.GetType())
                    .ToDictionary(k => k.Key, k => k.FirstOrDefault()));

        /// <summary>
        /// 获取当前类标签属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetTypeOption(Type type)
        {
            if (options.ContainsKey(GetType()) && options[GetType()].ContainsKey(type))
            {
                return options[GetType()][type];
            }

            return null;
        }

        private T Cache<T>(string key, Func<T> f) where T : class
        {
            CacheOptionsAttribute attr = GetTypeOption(typeof(CacheOptionsAttribute)) as CacheOptionsAttribute;
            int CachePeriod = 5;
            if (attr == null) return f();
            if (attr.Timeout > 0)
            {
                CachePeriod = attr.Timeout;
            }

            if (Sessions == null) return f();

            if (Sessions.Any() && Sessions.ContainsKey(key))
            {
                var entity = Sessions[key] as CachedEntity<T>;
                if (entity == null || entity.createdtime > DateTime.Now.AddMinutes(-CachePeriod))
                {
                    var f1 = f();
                    Sessions[key] = new CachedEntity<T>(f1);
                    return f1;
                }

                return entity.data;
            }

            T t = f();
            if (Sessions.ContainsKey(key)) Sessions[key] = new CachedEntity<T>(t);
            else Sessions.Add(key, new CachedEntity<T>(t));
            return t;
        }

        protected Dictionary<string, object> Sessions
        {
            get
            {
                var session_cache_file = "_session.txt";
                if (_sessions == null && File.Exists(session_cache_file))
                {
                    _sessions =
                        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(
                            File.ReadAllText(session_cache_file));
                }

                var auth = Request?.auth;
                if (_sessions != null && auth != null)
                {
                    return _sessions.ContainsKey(auth)
                        ? _sessions[auth]
                        : _sessions[auth] = new Dictionary<string, object>();
                }

                return null;
            }
        }

        public static Dictionary<string, IEvaluator> Registrations1 => Registrations;

        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <returns></returns>  
        private static HttpWebResponse CreateGetHttpResponse(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            return request.GetResponse() as HttpWebResponse;
        }

        private static async Task<WebResponse> CreateGetHttpResponseAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            return await request.GetResponseAsync();
        }

        /// <summary>
        /// 翻译接口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Translate(string key)
        {
            return HttpGet($"http://localhost/Translator/api/Main?key={key}");
        }

        public static async Task<T> Call<T>(string method, object data, string auth = "") where T : class, new()
        {
            var result =
                await HttpGetAsync(
                    $"http://localhost/T/DefaultHandler.ashx?method={method}&data={data}&auth_user={auth}");
            return result.Deserialize<T>();
        }

        /// <summary>
        /// 发送HTTP GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string HttpGet(string url)
        {
            Stream responseStream = CreateGetHttpResponse(url).GetResponseStream();
            string buf = string.Empty;
            if (responseStream != null)
            {
                using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                {
                    buf = reader.ReadToEnd();
                }

                responseStream.Close();
            }

            return buf;
        }

        private static async Task<string> HttpGetAsync(string url)
        {
            var resp = await CreateGetHttpResponseAsync(url);
            Stream responseStream = resp.GetResponseStream();
            string buf = string.Empty;
            if (responseStream != null)
            {
                using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                {
                    buf = reader.ReadToEnd();
                }

                responseStream.Close();
            }

            return buf;
        }

        private static readonly Dictionary<string, Type> Types = new[] {typeof(Evaluator), typeof(DefaultContext)}
            .SelectMany(t => t.Assembly.GetTypes())
            .GroupBy(t => t.Name.ToLower())
            .ToDictionary(t => t.Key, t => t.FirstOrDefault());

        /// <summary>
        /// 处理机映射
        /// </summary>
        private static readonly Dictionary<string, IEvaluator> Registrations = typeof(Evaluator).Assembly.GetTypes()
            .Where(t => t.BaseType == typeof(Evaluator))
            .GroupBy(t => t.Name.ToLower().Replace(nameof(Evaluator).ToLower(), string.Empty))
            .ToDictionary(t => t.Key, New);

        private static IEvaluator New(IGrouping<string, Type> t)
        {
            var type = t.FirstOrDefault();
            if (type == null) return (IEvaluator) null;
            return Activator.CreateInstance(type) as IEvaluator;
        }

        protected object Redirect(string method)
        {
            Request.method = method;
            var evaluator = Build(Request);
            return evaluator.Eval(Request);
        }

        private static object TryCreateInstance(string typeName)
        {
            typeName = typeName.TrimStart("get".ToCharArray()).TrimStart("save".ToCharArray())
                .TrimStart("delete".ToCharArray()).TrimEnd("list".ToCharArray()).TrimEnd("count".ToCharArray());
            if (!Types.ContainsKey(typeName)) return null;
            return Activator.CreateInstance(Types[typeName]);
        }

        public static UserInformation GetUser(string auth_user)
        {
            return (Build(new CommonRequest
            {
                method = nameof(GetUserInformationListEvaluator),
                auth = auth_user,
            }) as GetUserInformationListEvaluator)?.CurrentUserInformation;
        }

        /// <summary>
        /// 创建可以处理数据的处理机
        /// </summary>
        /// <param name="request">待处理数据</param>
        /// <returns>处理机</returns>
        public static IEvaluator Build(CommonRequest request)
        {
            var method = request.method.ToLower().Trim();
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (!method.EndsWith("evaluator"))
            {
                request.method += "evaluator";
            }

            if (method == "list_all_of_evaluators")
            {
                return
                    new ShowEvaluator(
                        Registrations1.Keys
                            .Select(p => p.Replace("evaluator", string.Empty))
                            .Select(p => new
                            {
                                name = p,
                                description = string.Join(" ", JsonConvert.DeserializeObject<string[]>(Translate(p))),
                                uri = $"http://223.112.112.26:58080:8091/T/DefaultHandler.ashx?method={p}&data={{}}",
                                relateduri = $"/T/DefaultHandler.ashx?method={p}&data={{}}",
                                post_body = $"",
                                contact = "路正遥",
                                mail = "zhengyao.lu@qq.com",
                                method = p,
                                data = TryCreateInstance(p)
                            })
                            .OrderBy(p => p.name));
            }

            //if (!Registrations.ContainsKey(request.method))
            //{
            //    return new RemoteEvaluator();
            //}

            if (!Registrations1.ContainsKey(method))
            {
                throw new Exception($"Method not found with {method}");
            }

            var evaluator = Registrations1[method];
            var o = evaluator as Evaluator;
            if (o != null) o.Request = request;
            return evaluator;
        }

        public object Eval(CommonRequest request)
        {
            try
            {
                var method = request.method.ToLower();
                if ("save,delete".Split(',').Any(s => method.StartsWith(s)))
                {
                    var keys = Sessions?.Keys.Where(p => p.Contains(method.Substring(3) + ",")).ToList();
                    if (keys != null)
                        foreach (var key in keys)
                        {
                            Sessions.Remove(key);
                        }

                    return OnAfterEvaluate(Evaluate(OnBeforeEvaluate(request)));
                }

                return Cache($"{request.method},{request.data},{request.auth}",
                    () => OnAfterEvaluate(Evaluate(OnBeforeEvaluate(request))));
            }
            catch (Exception e)
            {
                return new
                {
                    success = false,
                    message = e.Message,
                    exception = e
                };
            }
        }

        protected abstract object Evaluate(CommonRequest request);

        /// <summary>
        /// 控制方法执行前
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual CommonRequest OnBeforeEvaluate(CommonRequest request)
        {
            return request;
        }

        /// <summary>
        /// 控制方法执行后
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual object OnAfterEvaluate(object obj)
        {
            return obj;
        }

        public void Dispose()
        {
            Request = null;
            _user = null;
        }
    }
}