namespace InvoiceApp.Common.Enums;

public enum JoinType
{
    Inner,
    Left,
    Right,
    FullOuter,
}

public static class JoinTypeExtensions
{
    public static string ToQueryString(this JoinType joinType)
        => joinType switch
        {
            JoinType.Inner => "INNER JOIN",
            JoinType.Left => "LEFT JOIN",
            JoinType.Right => "RIGHT JOIN",
            JoinType.FullOuter => "FULL OUTER JOIN",
            _ => throw new InvalidOperationException("Passed join type is not managed.")
        };
}