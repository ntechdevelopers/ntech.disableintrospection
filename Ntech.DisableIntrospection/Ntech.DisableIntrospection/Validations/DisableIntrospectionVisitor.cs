using HotChocolate.Language;
using HotChocolate.Validation;

namespace Ntech.DisableIntrospection.Validations
{


    public class DisableIntrospectionVisitor : QuerySyntaxWalker<DisableIntrospectionContext>
    {
        protected override void VisitName(NameNode node, DisableIntrospectionContext context)
        {
            var name = node.Value;

            if (name.Contains("__schema") || name.Contains("__type"))
            {
                context.ReportError(new ValidationError($"The field {name} can only be accessed", node));
            }

            base.VisitName(node, context);
        }
    }
}
