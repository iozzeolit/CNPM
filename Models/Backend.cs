static class Backend{
    public static void Start()
    {
        QDatabase.Init("PC", "DbCnpm");
        Query q = new(Tbl.HopDong);
        QDatabase.Exec(conn => Console.WriteLine(q.Count(conn)));
    }
}