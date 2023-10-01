namespace AlgsTimeComplexity.Models;

public static class SecondTaskTestingMethods
{
    public static double SimplePow(int[] list, int size)
    {
        long counter = 0;
        var element = list[size - 1];
        
        for (int i = 0; i < size; i++)
        {
            counter++;
            element *= element;
        }


        return counter;
    }

    public static double RecursivePow(int[] list, int size)
    {
        var element = list[size - 1];
        long counter = 0;
        
        Recursive(element, size, ref counter);


        return counter;
    }

    private static int Recursive(int number, int power, ref long counter)
    {
        int m;
        
        if (power == 0)
            return 1;
        if (number % 2 == 0)
        { 
            m = Recursive(number, power / 2, ref counter);
            counter++;
            return m * m;
        }

        return number * Recursive(number, power - 1, ref counter);
    }

    public static double QuickPow(int[] list, int size)
    {
        long counter = 0;
        Quick(list[size - 1], size, ref counter);
        return counter;
    }

    private static int Quick(int number, int power, ref long counter)
    {
        int f = power % 2 == 1 ? number : 1;

        while (power != 0)
        {
            counter++;
            
            power /= 2;
            number *= number;

            if (power % 2 == 1)
                f *= number;
        }

        return f;
    }

    public static double QuickPow1(int[] list, int size)
    {
        long counter = 0;
        Quick1(list[size - 1], size, ref counter);
        return counter;
    }

    private static int Quick1(int number, int power, ref long counter)
    {
        int c = number;
        int f = 1;
        int k = power;

        while (k != 0)
        {
            counter++;
            if (k % 2 == 0)
            {
                c *= c;
                k /= 2;
            }
            else
            {
                f *= c;
                k -= 1;
            }
        }

        return f;
    }
}