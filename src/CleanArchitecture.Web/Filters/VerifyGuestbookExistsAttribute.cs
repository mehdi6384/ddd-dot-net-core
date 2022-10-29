using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Web.Filters
{
    public class VerifyGuestbookExistsAttribute : TypeFilterAttribute
    {
        public VerifyGuestbookExistsAttribute() : base(typeof(VerifyGuestbookExistsFilter))
        {
        }

        private class VerifyGuestbookExistsFilter : IAsyncActionFilter
        {
            private readonly IRepository _repository;

            public VerifyGuestbookExistsFilter(IRepository repository)
            {
                _repository = repository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, 
                ActionExecutionDelegate next)
            {
                if(context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as int?;
                    if(id.HasValue)
                    {
                        var guestbook = _repository.GetById<Guestbook>(id.Value);
                        if(guestbook is null)
                        {
                            context.Result = new NotFoundObjectResult(id.Value);
                            return;
                        }

                    }
                }
                await next();
            }
        }
    }
}
