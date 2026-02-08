using JLokaTestGraphQL.Data;
using JLokaTestGraphQL.Models;

namespace JLokaTestGraphQL.GraphQL.Types;

public class TeacherType: ObjectType<Teacher>
{
    protected override void Configure(IObjectTypeDescriptor<Teacher> descriptor)
    {
        Console.WriteLine("Configure the TeacherType");

        descriptor.Field(x => x.Department).Name("department").Description("Simple Description").Resolve(async context =>
        {
            var department = await context.Service<AppDbContext>().Departments.FindAsync(context.Parent<Teacher>().DepartmentId);
            Console.WriteLine("Loading Department");
            return department;
        });
    }
}