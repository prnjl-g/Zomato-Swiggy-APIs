using GraphQL;
using GraphQL.Types;

namespace graphql_create.GraphQLModel
{
    public class RootSchema : Schema, ISchema
    {
        public RootSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQueryType>();
        }
    }
}
