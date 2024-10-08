Complex p1 = new Complex();
Complex p2 = new Complex();
double x; double y;
Console.Write("All comands:\na - input first number a\nb - input second number b\nc - sum a+b\n" +
            "d - sub a-b\ne - mult  a*b\nf - div a/b\n" +
            "g - module(a)\nh - module(b)\ni - arg(a)\nj - arg(b)\nk - real part of a\n" +
            "l - real part of b\nm - imaginary part a\nn - imaginary part b\n" +
            "o - print a\np - print b\nq or Q - exit the program\nr - list of commands\n");

for (; ; )
{
    char input = Convert.ToChar(Console.ReadLine());
    switch (input)
    {
        case 'a':
            Console.Write("Input real part of number: ");
            x = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input imaginary part of number: ");
            y = Convert.ToDouble(Console.ReadLine());

            p1.x = x; p1.y = y;
            break;

        case 'b':
            Console.Write("Input real part of number: ");
            x = Convert.ToDouble(Console.ReadLine());
            Console.Write("Input imaginary part of number: ");
            y = Convert.ToDouble(Console.ReadLine());

            p2.x = x; p2.y = y;
            break;

        case 'c':
            p1 = p1 + p2;
            Complex.Print(p1);
            break;

        case 'd':
            p1 = p1 - p2;
            Complex.Print(p1);
            break;

        case 'e':
            p1 = p1 * p2;
            Complex.Print(p1);
            break;

        case 'f':
            p1 = p1 / p2;
            Complex.Print(p1);
            break;

        case 'g':
            Console.WriteLine(Complex.Mod(p1));
            break;

        case 'h':
            Console.WriteLine(Complex.Mod(p2));
            break;

        case 'i':
            Console.Write(Complex.Arg(p1));
            break;

        case 'j':
            Console.Write(Complex.Arg(p2));
            break;

        case 'k':
            Console.Write(Complex.GetReal(p1));
            break;

        case 'l':
            Console.Write(Complex.GetReal(p2));
            break;

        case 'm':
            Console.Write(Complex.GetImaginary(p1));
            break;

        case 'n':
            Console.Write(Complex.GetImaginary(p2));
            break;

        case 'o':
            Complex.Print(p1);
            break;

        case 'p':
            Complex.Print(p2);
            break;

        case 'q':
            Console.Write("Exit\n");
            return;

        case 'Q':
            Console.Write("Exit\n");
            return;

        case 'r':
            Console.Write("All comands:\na - input first number a\nb - input second number b\nc - sum a+b\n" +
                "d - sub a-b\ne - mult  a*b\nf - div a/b\n" +
                "g - module(a)\nh - module(b)\ni - arg(a)\nj - arg(b)\nk - real part of a\n" +
                "l - real part of b\nm - imaginary part a\nn - imaginary part b\n" +
                "o - print a\np - print b\nq or Q - exit the program\nr - list of commands\n");
            break;

        default:
            Console.Write("Unknown command\n");
            break;
    }
}

public struct Complex
{
    public double x;
    public double y;

    public Complex() { x = 0; y = 0; }
    public Complex(double x1, double y1)
    {
        x = x1; y = y1;
    }
    public static Complex operator +(Complex a, Complex b)
    {
        Complex c = new Complex(); c.x = a.x + b.x; c.y = a.y + b.y;
        return c;
    }
    public static Complex operator -(Complex a, Complex b)
    {
        Complex c = new Complex(); c.x = a.x - b.x; c.y = a.y - b.y; return c;
    }
    public static Complex operator *(Complex a, Complex b)
    {
        Complex c = new Complex(); c.x = a.x * b.x - (a.y * b.y);
        c.y = a.x * b.y + a.y * b.x; return c;
    }
    public static Complex operator /(Complex a, Complex b)
    {
        Complex c = new Complex(); Complex t = new Complex(); Complex t1 = new Complex();
        Complex b1 = new Complex(b.x, -b.y); t = a * b1; t1 = b * b1;
        double m = t1.x + t1.y; c.x = t.x / m; c.y = t.y / m; return c;
    }
    public static void Print(Complex a)
    {
        Console.WriteLine($"({a.x}, {a.y}i)"); Console.Write("\n");
    }
    public static double GetReal(Complex a)
    {
        double x = a.x; return x;
    }
    public static double GetImaginary(Complex a)
    {
        double x = a.y; return x;
    }
    public static double Mod(Complex a)
    {
        return Math.Sqrt((a.x * a.x) + (a.y * a.y));
    }
    public static double Arg(Complex a)
    {
        if (a.x > 0) return Math.Atan(a.y / a.x);
        if (a.x < 0 && a.y > 0) return Math.Atan(a.y / a.x) - Math.PI; if (a.x < 0 && a.y < 0) return Math.Atan(a.y / a.x) - Math.PI;
        if (a.x < 0 && a.y == 0) return Math.PI; if (a.x == 0 && a.y > 0) return Math.PI / 2;
        if (a.x > 0 && a.y == 0) return 0; if (a.x == 0 && a.y < 0) return -Math.PI / 2;
        return 0;
    }
}