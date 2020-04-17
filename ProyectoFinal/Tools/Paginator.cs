
using System.Linq;
using System.Collections.Generic;

namespace Tools
{
    public interface IPagination<T>
    {
        int LengthForPage { get; }
        int Pages { get; }
        IEnumerable<T> ListaDatos { get; }
    }

    internal class Pagination<T> : IPagination<T>
    {
        public Pagination(int Length, int Pages, IEnumerable<T> ListaDatos)
        {
            this.LengthForPage = Length;
            this.Pages = Pages;
            this.ListaDatos = ListaDatos;
        }
        public int LengthForPage { get; }

        public int Pages { get; }

        public IEnumerable<T> ListaDatos { get; }
    }

    public sealed class Paginator<T>
    {
        public Paginator(IQueryable<T> Content, int Page, int LengthForPage)
        {
            this.Content = Content;
            this.Page = Page;
            this.LengthForPage = LengthForPage;
        }

        private int Page { get; }
        private int LengthForPage { get; }
        private int Pages
        {
            get
            {
                if (this.Content == null)
                    throw new System.Exception("Numenoro de paginas es imposible de calcular");
                if (this.Content.Count() == 0)
                    return 1;
                var pages = this.Content.Count() % this.LengthForPage > 0 ? (this.Content.Count() / this.LengthForPage) + 1 : this.Content.Count() / this.LengthForPage;
                return pages;
            }
        }

        private readonly IQueryable<T> Content;

        private IEnumerable<T> ListaDatos
        {
            get
            {
                return this.Content.Skip((this.Page - 1) * this.LengthForPage).Take(this.LengthForPage).ToList();
            }
        }

        public IPagination<T> Pagination
        {
            get
            {
                return new Pagination<T>(this.LengthForPage, this.Pages, this.ListaDatos);
            }
        }
    }
}








