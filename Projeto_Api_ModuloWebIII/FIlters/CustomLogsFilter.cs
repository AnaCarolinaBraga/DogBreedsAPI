using Microsoft.AspNetCore.Mvc.Filters;
using DogBreedsAPI.Interfaces;
using DogBreedsAPI.Logs;
using DogBreedsAPI.Models;

namespace DogBreedsAPI.FIlters
{
    public class CustomLogsFilter : IResultFilter, IActionFilter
    {

        private readonly IDogBreedsRepository _repository;
        private readonly Dictionary<int, DogBreeds> _contextDict;

        public CustomLogsFilter(IDogBreedsRepository repository)
        {
            _repository = repository;
            _contextDict = new Dictionary<int, DogBreeds>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.Equals(context.ActionDescriptor.RouteValues["controller"], "DogBreeds", StringComparison.InvariantCultureIgnoreCase))
            {
                int id = 0;
                if (context.ActionArguments.ContainsKey("id") && int.TryParse(context.ActionArguments["id"].ToString(), out id))
                {
                    if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var breed = _repository.GetByKey(id).Result;
                        if (breed != null)
                        {
                            _contextDict.Add(id, breed);
                        }
                    }
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.HttpContext.Request.Path.Value.StartsWith("/api/DogBreeds", StringComparison.InvariantCulture))
            {
                if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase)
                        || context.HttpContext.Request.Method.Equals("post", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (context.HttpContext.Response.StatusCode == 200 && !(context.HttpContext.Request.Method.Equals("post", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        var id = int.Parse(context.HttpContext.Request.Path.ToString().Split("/").Last());
                        if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                            || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var afterUpdate = _repository.GetByKey(id).Result;
                            if (afterUpdate != null)
                            {
                                DogBreeds beforeUpdate;
                                if (_contextDict.TryGetValue(id, out beforeUpdate))
                                {
                                    CustomLogs.SaveLog200(afterUpdate.Id, "DogBreed", afterUpdate.DogType, context.HttpContext.Request.Method, beforeUpdate, afterUpdate);
                                    _contextDict.Remove(id);
                                }
                            }
                        }
                        else if (context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                        {
                            DogBreeds beforeUpdate;
                            if (_contextDict.TryGetValue(id, out beforeUpdate))
                            {
                                CustomLogs.SaveLog200(beforeUpdate.Id, "DogBreed", beforeUpdate.DogType, context.HttpContext.Request.Method);
                                _contextDict.Remove(id);
                            }
                        }
                    }
                    else if (context.HttpContext.Response.StatusCode == 201)
                    {
                        CustomLogs.SaveLog201("DogBreed", context.HttpContext.Request.Method);
                    }
                }      
            }
        }

        #region Não Utilizados
        public void OnActionExecuted(ActionExecutedContext context)
        {   
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
        }
        #endregion
    }

}
