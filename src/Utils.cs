public static class Utils
{
    public static int getRandomNumber(int start, int end)
    {
        Random generator = new Random();
        return generator.Next(start, end);
    }
}

