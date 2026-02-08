using JLokaTestGraphQL.Data;

namespace JLokaTestGraphQL.GraphQL.Types
{
    public class QueryType:ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(x => x.Teacher).Name("teacher")
                .Description("Simple QueryType")
                .Type<TeacherType>()
                .Argument("id", a => a.Type<NonNullType<UuidType>>())
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<Guid>("id");
                    var teacher = await context.Service<AppDbContext>().Teachers.FindAsync("id");
                    return teacher;
                });
        }
    }
}
