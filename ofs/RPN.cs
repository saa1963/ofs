using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ofs
{
    public class RPN
    {
        public Dictionary<string, double> Variables { get; set; }
        public Dictionary<string, FunctionBase> Functions { get; set; }
        public Stack<double> Stack { get; set; }
        public string Expression { get; set; }
        public List<Lexem> Ast { get; private set; }
        public double Result { get; private set; }

        public double this[params Tuple<string, double>[] args]
        {
            get
            {
                RPN r = this.MemberwiseClone() as RPN;
                foreach (var arg in args) r.SetVariable(arg.Item1, arg.Item2);
                r.Execute();
                return r.Result;
            }
        }

        public RPN(string expr)
        {
            this.Variables = new Dictionary<string, double>();
            this.Functions = new Dictionary<string, FunctionBase>();
            this.Stack = new Stack<double>();
            this.Expression = expr;
        }

        private bool IsNumber(string s)
        {
            int d;
            return Int32.TryParse(s, out d);
            //return Regex.IsMatch(s, @"[+-]?\d+(\.\d+)?", RegexOptions.Compiled);
        }
        private bool IsVariable(string s)
        {
            return Regex.IsMatch(s, @"[$_a-zA-Z]+[$_a-zA-Z0-9]*", RegexOptions.Compiled);
        }
        private bool IsOperation(string s)
        {
            return new string[] { "+", "-", "*", "/", "^" }.Contains(s);
        }
        private bool IsDelimier(string s)
        {
            return s == ",";
        }
        private bool IsFunction(string s)
        {
            return HasFunction(s);
        }

        public void CreateAst()
        {
            string[] t = Expression.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Ast = new List<Lexem>();
            foreach (var l in t)
            {
                if (IsNumber(l)) Ast.Add(new Lexem { Value = l, Type = LexemType.Constant });
                else if (IsFunction(l)) Ast.Add(new Lexem { Value = l, Type = LexemType.Function });
                else if (IsVariable(l)) Ast.Add(new Lexem { Value = l, Type = LexemType.Variable });
                else if (IsOperation(l)) Ast.Add(new Lexem { Value = l, Type = LexemType.Operator });
                else if (IsDelimier(l)) Ast.Add(new Lexem { Value = l, Type = LexemType.Delimier });
                else throw new Exception("Unexpected lexem!");
            }
        }

        public void SetVariable(string name, double value)
        {
            if (HasVariable(name.ToLower())) Variables[name.ToLower()] = value;
            else Variables.Add(name.ToLower(), value);
        }

        public void AddFunction(FunctionBase func)
        {
            Functions.Add(func.Name, func);
        }

        public bool HasVariable(string name)
        {
            return Variables.ContainsKey(name.ToLower());
        }

        public bool HasFunction(string name)
        {
            return Functions.ContainsKey(name.ToLower());
        }

        public void Execute()
        {
            foreach (var l in Ast)
            {
                switch (l.Type)
                {
                    case LexemType.Constant: Stack.Push(double.Parse(l.Value)); break;
                    case LexemType.Variable: Stack.Push(Variables[l.Value]); break;
                    case LexemType.Function:
                        {
                            var func = Functions[l.Value];
                            var args = new List<double>();
                            for (int i = 0; i < func.NeededArgs; ++i) args.Add(Stack.Pop());
                            Stack.Push(func.Execute(args.ToArray()));
                        }
                        break;
                    case LexemType.Operator: Stack.Push(BasicFunction(l.Value)); break;
                    default: throw new Exception("Unexpected lexem!");
                }
            }
            Result = Stack.Pop();
        }

        public double BasicFunction(string f)
        {
            switch (f)
            {
                case "+":
                    return Stack.Pop() + Stack.Pop();
                case "-":
                    {
                        var t1 = Stack.Pop();
                        var t2 = Stack.Pop();
                        return t2 - t1;
                    }
                case "*":
                    return Stack.Pop() * Stack.Pop();
                case "/":
                    {
                        var t1 = Stack.Pop();
                        var t2 = Stack.Pop();
                        return t2 / t1;
                    }
                case "^":
                    {
                        var t1 = Stack.Pop();
                        var t2 = Stack.Pop();
                        return Math.Pow(t2, t1);
                    }
                default: return double.NaN;
            }
        }

        public static RPN CreateRPN(string expr)
        {
            RPN rpn = new RPN(expr);
            rpn.CreateAst();
            return rpn;
        }

        public static string StandartToRPN(string expr)
        {
            var ast = GetAst(expr);
            Stack<Lexem> operators = new Stack<Lexem>();
            List<string> result = new List<string>();
            foreach (var l in ast)
            {
                if (new LexemType[]{ LexemType.Variable, LexemType.Constant}.Contains(l.Type))
                    result.Add(l.Value);
                else if (l.Type == LexemType.Operator && l.Value == ")")
                {
                    Lexem l0 = new Lexem();
                    while ((l0 = operators.Pop()).Value != "(")
                        if (l0.Value != ",")
                            result.Add(l0.Value);
                }
                else if (new LexemType[] { LexemType.Operator, LexemType.Function }.Contains(l.Type))
                {
                    if (operators.Count > 0 && GetPriority(operators.Peek()) == GetPriority(l))
                    {
                        var l0 = operators.Pop();
                        if (l0.Value != ",") result.Add(l0.Value);
                    }
                    operators.Push(l);
                }
            }
            while (operators.Count > 0)
            {
                var l0 = operators.Pop();
                if (l0.Value != ",") result.Add(l0.Value);
            }
            string rpnExpr = string.Join(" ", result);
            return rpnExpr;
        }

        public static int GetPriority(Lexem l)
        {
            if (l.Type == LexemType.Operator ||
                l.Type == LexemType.Function)
            {
                if (new string[] { "+", "-" }.Contains(l.Value)) return 1;
                else if (new string[] { "*", "/" }.Contains(l.Value)) return 2;
                else if (new string[] { "^" }.Contains(l.Value)) return 3;
                else if (new string[] { "(" }.Contains(l.Value)) return 5;
                else return 4;
            }
            return -1;
        }

        public static List<Lexem> GetAst(string expr)
        {
            List<Lexem> lexems = new List<Lexem>();
            string acc = "";
            bool readingId = false;
            LexemType nextType = LexemType.Constant;
            for (int i = 0; i < expr.Length; ++i)
            {
                char c = expr[i];
                if (char.IsWhiteSpace(c))
                {
                    /*if (readingId)
                    {
                        lexems.Add(new Lexem { Value = acc, Type = nextType });
                        acc = "";
                    }
                    readingId = false;*/
                    continue;
                }
                else if (char.IsLetter(c))
                {
                    if (!readingId)
                    {
                        nextType = LexemType.Identifier;
                        readingId = true;
                    }
                    if (c == '.') throw new Exception("Unexpected char!");
                    acc += c;
                }
                else if (char.IsDigit(c))
                {
                    if (!readingId)
                    {
                        nextType = LexemType.Constant;
                        readingId = true;
                    }
                    acc += c;
                }
                else if (c == '.' && readingId && nextType == LexemType.Constant)
                    acc += '.';
                else if (char.IsLetterOrDigit(c)) acc += c;
                else if (new char[] { '+', '-' }.Contains(c) && (i > 0 && new char[] { '+', '-' }.Contains(expr[i - 1])) && char.IsDigit(expr[i + 1]) && !readingId)
                {
                    nextType = LexemType.Constant;
                    readingId = true;
                    acc += c;
                }
                else if (new char[]{ '(', ')', '+', '-', '*', '/', '^'}.Contains(c))
                {
                    if (readingId)
                    {
                        readingId = false;
                        if (c == '(' && nextType == LexemType.Identifier) nextType = LexemType.Function;
                        else if (c != '(' && nextType == LexemType.Identifier) nextType = LexemType.Variable;
                        lexems.Add(new Lexem { Value = acc, Type = nextType });
                        acc = "";
                    }
                    lexems.Add(new Lexem { Value = c.ToString(), Type = LexemType.Operator });
                }
                else if (c == ',')
                {
                    if (readingId)
                    {
                        readingId = false;
                        if (c == '(' && nextType == LexemType.Identifier) nextType = LexemType.Function;
                        else if (c != '(' && nextType == LexemType.Identifier) nextType = LexemType.Variable;
                        lexems.Add(new Lexem { Value = acc, Type = nextType });
                        acc = "";
                    }
                    lexems.Add(new Lexem { Value = c.ToString(), Type = LexemType.Delimier });
                }
            }
            if (acc != "" && nextType == LexemType.Identifier)
            {
                nextType = LexemType.Variable;
                lexems.Add(new Lexem { Value = acc, Type = nextType });
            }
            else lexems.Add(new Lexem { Value = acc, Type = nextType });
            return lexems;
        }
    }

    public enum LexemType
    {
        Variable,
        Function,
        Identifier,
        Operator,
        Delimier,
        Constant,
        OpenSub,
        CloseSub
    }

    public class Lexem
    {
        public LexemType Type { get; set; }
        public string Value { get; set; }

        //public override string ToString()
        //{
        //    return "{0} : {1}".Format(Type, Value);
        //}
    }

    public abstract class FunctionBase
    {
        public virtual string Name { get; set; }
        public virtual int NeededArgs { get; set; }

        public virtual double Execute(params double[] args)
        {
            return double.NaN;
        }
    }

    public class NativeFunction : FunctionBase
    {
        public static NativeFunction Sin
        {
            get
            {
                return new NativeFunction("sin", a => Math.Sin(a[0]), 1);
            }
        }
        public static NativeFunction Cos
        {
            get
            {
                return new NativeFunction("cos", a => Math.Cos(a[0]), 1);
            }
        }
        public static NativeFunction Sqrt
        {
            get
            {
                return new NativeFunction("sqrt", a => Math.Sqrt(a[0]), 1);
            }
        }
        public static NativeFunction Exp
        {
            get
            {
                return new NativeFunction("exp", a => Math.Exp(a[0]), 1);
            }
        }
        public static NativeFunction Abs
        {
            get
            {
                return new NativeFunction("abs", a => Math.Abs(a[0]), 1);
            }
        }
        public static NativeFunction Ln
        {
            get
            {
                return new NativeFunction("ln", a => Math.Log10(a[0]), 1);
            }
        }
        public static NativeFunction Lb
        {
            get
            {
                return new NativeFunction("lb", a => Math.Log(a[0], 2.0), 1);
            }
        }
        public static NativeFunction Log
        {
            get
            {
                return new NativeFunction("log", a => Math.Log(a[0], a[1]), 2);
            }
        }
        public static NativeFunction Pow
        {
            get
            {
                return new NativeFunction("pow", a => Math.Pow(a[0], a[1]), 2);
            }
        }
        public static NativeFunction Atan
        {
            get
            {
                return new NativeFunction("atan", a => Math.Atan(a[0]), 1);
            }
        }

        public override string Name { get; set; }
        public Func<double[], double> Function { get; set; }
        public override int NeededArgs { get; set; }

        public NativeFunction(string name, Func<double[], double> function, int neededArgs)
        {
            this.Name = name;
            this.Function = function;
            this.NeededArgs = neededArgs;
        }

        public override double Execute(params double[] args)
        {
            if (Function != null) return Function(args);
            return double.NaN;
        }
    }

    public class RPNFunction : FunctionBase
    {
        public override string Name { get; set; }
        public RPN Function { get; set; }
        public override int NeededArgs { get; set; }

        public void UpdateExpression(string newExpr, int newNeededArgs)
        {
            this.NeededArgs = newNeededArgs;
            this.Function.Expression = newExpr;
        }

        public RPNFunction(string name, RPN function, int neededArgs)
        {
            this.Name = name;
            this.Function = function;
            this.NeededArgs = neededArgs;
        }

        public override double Execute(params double[] args)
        {
            if (this.Function == null) return double.NaN;
            if (args.Length != this.NeededArgs) return double.NaN;
            for (int i = 0; i < args.Length; ++i)
                this.Function.SetVariable(string.Format("${0}", i + 1), args[i]);
            this.Function.Execute();
            return this.Function.Result;
        }

        public override string ToString()
        {
            return this.Function.Expression;
        }
    }
}
