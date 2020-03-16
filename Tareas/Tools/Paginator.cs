
using System.Collections.Generic;
using System.Linq;

namespace Tareas.Tools
{
    public interface IPagination<T>
    {
        int Page { get; }
        int LengthForPage { get; }
        int Pages{get;}
        string ActionName { get; }
        string ControllerName { get; }
        IEnumerable<T> ListaDatos { get; }
    }

    public class Paginator<T, TResult> : IPagination<TResult>
    {
        public Paginator(IQueryable<T> Content, int Page, int LengthForPage, string ActionName, string ControllerName)
        {
            this.Content = Content;
            this.Page = Page;
            this.LengthForPage = LengthForPage;
            this.ControllerName = ControllerName;
            this.ActionName = ActionName;
        }

        public int Page{get;}
        public int LengthForPage{get;}
        public int Pages
        {
            get
            {
                if(this.Content == null)
                    throw new System.Exception("Numenoro de paginas es imposible de calcular");
                if(this.Content.Count() == 0)
                    return 1;
                var pages = this.Content.Count()%this.LengthForPage > 0 ? (this.Content.Count()/this.LengthForPage) + 1 : this.Content.Count()/this.LengthForPage ;
                return pages;
            }
        }
        public string ActionName { get; }
        public string ControllerName { get; }
        readonly IQueryable<T> Content;

        private TResult InvoqueConvert(T t)
        {
            var convert = t.GetType()
                .GetMethods()
                .FirstOrDefault(x=>
                    x.Name == "op_Explicit" 
                    && x.ReturnType == typeof(TResult)
                    //&& x.GetGenericArguments().Any(y=>y.GetType() == typeof(t)))
                );
            var tresult = (TResult)convert.Invoke(t, new object[]{t});
            return tresult;
        }
        public IEnumerable<TResult> ListaDatos
        {
            get
            {
                var list = this.Content.Skip((this.Page-1) * this.LengthForPage).Take(this.LengthForPage).ToList();
                return list.Select(x=>this.InvoqueConvert(x));
            }
        }

        public IPagination<TResult> Pagination
        { 
            get
            {
                return this;
            }
        }
    }
}


