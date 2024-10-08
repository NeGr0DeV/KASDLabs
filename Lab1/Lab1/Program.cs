StreamReader sr = new StreamReader("f.txt");
int dim = Convert.ToInt32(sr.ReadLine());

Matrix tensor = new Matrix(dim, dim);
string?[] lines = new string[tensor.n];
Matrix.MatAdd(lines, tensor, sr);

Console.WriteLine("Tensor matrix:");
Matrix.MatPrint(tensor);

if (!Matrix.IsSymmetric(tensor)) { Console.WriteLine("Matrix is asymmetric"); return; }

Matrix xVector = new Matrix(1, dim);
Matrix.MatAdd(lines, xVector, sr);

Console.WriteLine("Vector x:");
Matrix.MatPrint(xVector);

Matrix xTVector = new Matrix(dim, 1);
for (int i = 0; i < tensor.m; i++)
    xTVector.matrix[i, 0] = xVector.matrix[0, i];

Console.WriteLine("Transponated vector x:");
Matrix.MatPrint(xTVector);

Matrix result = new Matrix(1, dim);
result = xVector * tensor;
result = result * xTVector;
double res = result.matrix[0, 0];
res = Math.Sqrt(res);

Console.WriteLine($"Vector's length = {res}");
sr.Close();

class Matrix
{
    public int n;
    public int m;
    public double[,] matrix;
    public Matrix(int dim1, int dim2)
    {
        n = dim1;
        m = dim2;
        matrix = new double[n, m];
    }
    public static Matrix operator *(Matrix mat1, Matrix mat2)
    {
        Matrix newMat = new Matrix(mat1.n, mat2.m);
        for (int i = 0; i < mat1.n; i++)
        {
            for (int j = 0; j < mat2.m; j++)
            {
                newMat.matrix[i, j] = 0;
                for (int k = 0; k < mat1.m; k++)
                {
                    newMat.matrix[i, j] += mat1.matrix[i, k] * mat2.matrix[k, j];
                }
            }
        }
        return newMat;
    }
    public static void MatPrint(Matrix mat)
    {
        for (int i = 0; i < mat.n; i++)
        {
            for (int j = 0; j < mat.m; j++)
            {
                Console.Write($"{mat.matrix[i, j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public static void MatAdd(string?[] l, Matrix mat, StreamReader sr)
    {
        for (int i = 0; i < mat.n; i++)
        {
            l = sr.ReadLine().Split(' ');
            for (int j = 0; j < mat.m; j++)
            {
                mat.matrix[i, j] = Convert.ToInt32(l[j]);
            }

        }
    }
    public static bool IsSymmetric(Matrix mat)
    {
        if (mat.n == mat.m)
        {
            for (int i = 0; i < mat.n - 1; i++)
            {
                for (int j = i + 1; j < mat.n; j++)
                {
                    if (mat.matrix[i, j] != mat.matrix[j, i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }
}