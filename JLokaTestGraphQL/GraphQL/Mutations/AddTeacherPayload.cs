using JLokaTestGraphQL.Models;

namespace JLokaTestGraphQL.GraphQL.Mutations;

public class AddTeacherPayload
{
    public Teacher Teacher { get; }
    public AddTeacherPayload(Teacher teacher)
    {
        Teacher = teacher;
    }
}
