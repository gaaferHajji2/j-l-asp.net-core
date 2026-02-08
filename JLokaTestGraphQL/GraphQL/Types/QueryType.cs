using JLokaTestGraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestGraphQL.GraphQL.Types
{
    public class QueryType:ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {

            descriptor.Field(x => x.Teachers)
            .Description("This is the list of teachers in the school.")
            .Type<ListType<TeacherType>>()
            .Resolve(async context =>
            {
                var db = context.Service<AppDbContext>();
                var teachers = await db.Teachers.ToListAsync();
                return teachers;
            });


            descriptor.Field(x => x.Teacher).Name("teacher")
                .Description("Simple QueryType")
                .Type<TeacherType>()
                .Argument("id", a => a.Type<NonNullType<UuidType>>())
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<Guid>("id");
                    var teacher = await context.Service<AppDbContext>().Teachers.FindAsync(id);
                    return teacher;
                });
        }
    }
}
