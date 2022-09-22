using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.UriParser;
using OData.WebApi.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace OData.WebApi.Extensions;

public class StudentSearchBinder : ISearchBinder
{
    // $search=A+
    public Expression BindSearch(SearchClause searchClause, QueryBinderContext context)
    {
        SearchTermNode node = searchClause.Expression as SearchTermNode;
        if (node != null)
        {
            Expression<Func<Student, bool>> exp = p =>
                (p.Grade >= 95 ? "A+" :
                    p.Grade >= 90 ? "A" :
                        p.Grade >= 85 ? "B+" :
                            p.Grade >= 80 ? "B" :
                                p.Grade >= 75 ? "C+" :
                                    p.Grade >= 70 ? "C" :
                                        p.Grade >= 65 ? "D+" :
                                            p.Grade >= 60 ? "D" : "F") == node.Text;

            return exp;
        }

        throw new InvalidOperationException("Unknown search on product or sale!");
    }
}
