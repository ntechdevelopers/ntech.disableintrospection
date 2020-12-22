using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Ntech.DisableIntrospection.Models;
using Ntech.DisableIntrospection.Resolvers;

namespace Ntech.DisableIntrospection.Types
{
    public class DroidType : ObjectType<Droid>
    {
        protected override void Configure(IObjectTypeDescriptor<Droid> descriptor)
        {
            descriptor.Interface<CharacterType>();

            descriptor.Field(t => t.Id)
                .Type<NonNullType<IdType>>();

            descriptor.Field(t => t.AppearsIn)
                .Type<ListType<EpisodeType>>();

            descriptor.Field<SharedResolvers>(r => r.GetCharacter(default, default))
                .UsePaging<CharacterType>()
                .Name("friends");

            descriptor.Field<SharedResolvers>(t => t.GetHeight(default, default))
                .Type<FloatType>()
                .Argument("unit", a => a.Type<EnumType<Unit>>())
                .Name("height");
        }
    }
}
