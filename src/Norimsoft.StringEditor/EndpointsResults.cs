namespace Norimsoft.StringEditor;

internal record ErrorResult(string Message);
internal record ErrorCodeResult(string Code);
internal record DeletedResult(int Count);

public static class ErrorResults
{
    public static IResult NotFound() =>
        Results.UnprocessableEntity(new ErrorCodeResult(nameof(NotFound)));
    
    public static IResult NoChange() =>
        Results.UnprocessableEntity(new ErrorCodeResult(nameof(NoChange)));

    public static IResult BadRequest(string message) =>
        Results.BadRequest(new ErrorResult(message));
}
