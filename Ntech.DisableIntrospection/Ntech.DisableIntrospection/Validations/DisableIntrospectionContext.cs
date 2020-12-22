using HotChocolate;
using System;

namespace Ntech.DisableIntrospection.Validations
{
    public class DisableIntrospectionContext
    {
        public DisableIntrospectionContext(Action<IError> reportError)
        {
            ReportError = reportError;
        }

        public Action<IError> ReportError { get; }
    }
}
