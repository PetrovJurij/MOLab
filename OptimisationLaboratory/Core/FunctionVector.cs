using System;

namespace Core
{

    public delegate double functionsMask(Matrix x);
    public class FunctionVector
    {
        Func<Matrix,double>[] functions;

        public FunctionVector(FunctionVector funcs)
        {
            functions = new Func<Matrix, double>[funcs.Count];

            for (int i = 0; i < funcs.Count; i++)
            {
                functions[i] = (Func<Matrix, double>)Delegate.CreateDelegate
                    (typeof(Func<Matrix, double>), funcs[i].Target, funcs[i].Method);
            }

            Count = funcs.Count;
        }

        public FunctionVector(functionsMask[] funcs)
        {
            functions = new Func<Matrix, double>[funcs.Length];

            for(int i=0;i<funcs.Length;i++)
            {
                functions[i] = (Func<Matrix,double>)Delegate.CreateDelegate
                    (typeof(Func<Matrix, double>), funcs[i].Target, funcs[i].Method);
            }

            Count = funcs.Length;
        }

        public FunctionVector(Func<Matrix,double>[] funcs)
        {
            functions = new Func<Matrix, double>[funcs.Length];

            for (int i = 0; i < funcs.Length; i++)
            {
                functions[i] = (Func<Matrix, double>)Delegate.CreateDelegate
                    (typeof(Func<Matrix, double>), funcs[i].Target, funcs[i].Method);
            }

            Count = funcs.Length;
        }

        public Vector ExecuteFunctions(Matrix x)
        {
            Vector res = new PenaltyVector(functions.Length);

            OutOfDefinitionRangeException ex = new OutOfDefinitionRangeException();

            for(int i=0;i<functions.Length;i++)
            {
                try
                {
                    res[i] = functions[i](x);
                }
                catch (Exception e)
                {
                    res[i] = Double.NaN;
                    ex.AddFunction(i);
                }
            }

            return res;
        }

        public Func<Matrix, double> this[int index]
        {
            get
            {
                return functions[index];
            }
        }
        

        public int Count {private set; get; }
    }
}
