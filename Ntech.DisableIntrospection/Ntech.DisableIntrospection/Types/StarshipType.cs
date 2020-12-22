using HotChocolate.Types;
using Ntech.DisableIntrospection.Models;
using Ntech.DisableIntrospection.Resolvers;

namespace Ntech.DisableIntrospection.Types
{
    public class StarshipType : ObjectType<Starship>
    {
        protected override void Configure(IObjectTypeDescriptor<Starship> descriptor)
        {
            descriptor.Field(t => t.Id)
                .Type<NonNullType<IdType>>();

            descriptor.Field<SharedResolvers>(t => t.GetLength(default, default));
        }
    }
}
