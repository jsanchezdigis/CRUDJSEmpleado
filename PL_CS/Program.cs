Console.WriteLine("Numero Entero a Binario");
Console.WriteLine("Ingrese el numero:");
int num = int.Parse(Console.ReadLine());
string numBinario = Convert.ToString(num,2);//Preguntar sobre esto

Console.WriteLine(numBinario);

Console.WriteLine("Numeros Primos 1-100");
for (int i = 1; i <= 100; i++)
{
    BL.Metodos metodos = new BL.Metodos();
    if (metodos.Primo(i))
    {
        Console.Write(i+ " ");
    }
}

Console.ReadKey();