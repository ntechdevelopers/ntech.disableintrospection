using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Validation;
using System.Collections.Generic;
using System.Linq;

namespace Ntech.DisableIntrospection.Validations
{
    public class DisableIntrospectionValidationRule : IQueryValidationRule
    {
        public QueryValidationResult Validate(ISchema schema, DocumentNode queryDocument)
        {
            var errors = new List<IError>();

            var visitor = new DisableIntrospectionVisitor();
            var context = new DisableIntrospectionContext(errors.Add);

            visitor.Visit(queryDocument, context);

            return errors.Any() ? new QueryValidationResult(errors) : QueryValidationResult.OK;
        }
    }
}
