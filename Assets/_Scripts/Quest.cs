public class Quest
{
    private bool[] results;
    private string ink;

    public Quest(bool[] r, string ink)
    {
        for (int i = 0; i < results.Length - 1; i++)
        {
            this.results[i] = r[i];
        }

        this.ink = ink;
    }

    public bool GetResult(int choice)
    {
        return results[choice];
    }

    public string GetInk() 
    {
        return ink;
    }
}
