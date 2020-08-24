using GraphQL.Types;

namespace graphql_create.GraphQLModel
{
    public class CakeType : ObjectGraphType<Cake>
    {
        public CakeType()
        {
            Field(_ => _.Id);
            Field(_ => _.Name);
            Field(_ => _.Cost);
        }
    }
}
