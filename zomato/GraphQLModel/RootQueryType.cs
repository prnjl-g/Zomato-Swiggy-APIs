using GraphQL.Types;
using graphql_create.Extension;

namespace graphql_create.GraphQLModel
{
    public class RootQueryType : ObjectGraphType
    {
        public RootQueryType(ICakeRepository cakeRepository)
        {
            Field<ListGraphType<CakeType>>("allCakes", resolve: context =>
             {
                 return cakeRepository.GetCake();
             }).AddPermissions(Role.ProjectManager);

            Field<ListGraphType<CakeType>>("cakesList", resolve: context =>
            {
                return cakeRepository.GetCake();
            }).AddPermissions(Role.ProjectManager);
        }
    }
}
