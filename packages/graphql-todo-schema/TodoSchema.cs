namespace GraphQL.Todo.Schema;

public class TodoSchema : GraphQL.Types.Schema
{
    public TodoSchema(IServiceProvider provider) : base(provider)
    {
        Query = new TodoQuery();
        // Mutation = new TodoMutation();
        // Subscription = new TodoSubscriptions();
    }
}
